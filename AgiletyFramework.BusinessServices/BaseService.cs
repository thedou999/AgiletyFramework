using AgiletyFramework.IBusinessServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AgiletyFramework.IBusinessServices;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace AgiletyFramework.BusinessServices
{
    public abstract class BaseService : IBaseService
    {
        public DbContext dbContext { get; set; }
        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="context"></param>
        public BaseService(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        #region Query
        /// <summary>
        /// 主键查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Find<T>(int id) where T : class
        {
            return this.dbContext.Set<T>().Find(id);
        }

        /// <summary>
        /// 尽量不要给外界暴露以防止发生线程安全问题
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// [Obsolete("尽量避免使用，using带表达式目录树的代替")]
        public IQueryable<T> Set<T>() where T : class
        {
            return dbContext.Set<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="funcWhere"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IQueryable<T> Query<T>(Expression<Func<T, bool>> funcWhere) where T : class
        {
            return this.dbContext.Set<T>().Where<T>(funcWhere);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="S"></typeparam>
        /// <param name="funcWhere"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="funcOrderby"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public PagingData<T> QueryPage<T, S>(Expression<Func<T, bool>> funcWhere, int pageSize, int pageIndex, Expression<Func<T, S>> funcOrderby, bool isAsc = true) where T : class
        {
            var list = Set<T>();
            if(funcWhere != null)
            {
                list = list.Where<T>(funcWhere);
            }
            if(isAsc)
            {
                list = list.OrderBy(funcOrderby);
            }
            else
            {
                list = list.OrderByDescending(funcOrderby);
            }
            PagingData<T> result = new PagingData<T>()
            { 
                DataList = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList(),
                PageIndex = pageIndex,
                PageSize = pageSize,
                RecordCount = list.Count()
            };
            return result;
        }
        #endregion


        #region Insert

        /// <summary>
        /// 即使保存，不需要在commit
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public T Insert<T>(T t) where T : class
        {
            t = this.dbContext.Set<T>().Add(t).Entity;
            int result = this.Commit();
            return t;
        }

        /// <summary>
        ///     
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tList"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<T> Insert<T>(IEnumerable<T> tList) where T : class
        {
            this.dbContext.Set<T>().AddRange(tList);
            this.Commit();
            return tList;
        }
        #endregion


        #region Update

        /// <summary>
        /// 没有实现查询直接更新，需要attach和state
        /// 
        /// 如果是已经在context，只能再封装一个（再具体的service）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Update<T>(T t) where T : class
        {
            if (t == null) throw new Exception("t is null");

            this.dbContext.Set<T>().Attach(t);//将数据附加到上下文，支持实体修改，重置为UnChanged
            this.dbContext.Entry<T>(t).State = EntityState.Modified;
            this.Commit();//保存 然后充值为UnChanged
        }

        public void Update<T>(IEnumerable<T> tList) where T : class
        {
            foreach (var t in tList)
            {
                this.dbContext.Set<T>().Attach(t);
                this.dbContext.Entry<T>(t).State = EntityState.Modified;
            }
            this.Commit();
        }
        #endregion



        #region Delete
        /// <summary>
        /// 先附加 再删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Delete<T>(T t) where T : class
        {
            if (t == null) throw new Exception("t is null");
            this.dbContext.Set<T>().Attach(t);
            this.dbContext.Set<T>().Remove(t);
            this.Commit();
        }

        /// <summary>
        /// 还可以增加非即时commit版本
        /// 做成protected
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <exception cref="Exception"></exception>
        public void Delete<T>(int id) where T : class
        {
            T t = this.Find<T>(id);
            if (t == null) throw new Exception("t is null");
            this.dbContext.Set<T>().Remove(t);
            this.Commit();
        }

        public void Delete<T>(IEnumerable<T> tList) where T : class
        {
            foreach(T t in tList)
            {
                this.dbContext.Set<T>().Attach(t);
            }
            this.dbContext.Set<T>().RemoveRange(tList);
            this.Commit();
        }
        #endregion



        #region Other
        public int Commit()
        {
            int result = -1;
            try
            {
                result = dbContext.SaveChanges();
                return result;
            }catch(Exception ex)
            {
                return result;
            }
        }
        public IQueryable<T> ExcuteQuery<T>(string sql, SqlParameter[] parameters) where T : class
        {
            return this.dbContext.Set<T>().FromSqlRaw(sql, parameters);
        }



        public void Excute<T>(string sql, SqlParameter[] parameters) where T : class
        {
            IDbContextTransaction trans = null;
            try
            {
                trans = dbContext.Database.BeginTransaction();
                this.dbContext.Database.ExecuteSqlRaw(sql, parameters);
                trans.Commit();
            }
            catch (Exception)
            {
                if(trans != null)
                {
                    trans.Rollback();
                }
                throw;
            }
        }

        public DbContext GetDbContext()
        {
            return dbContext;
        }

        #endregion
    }
}
