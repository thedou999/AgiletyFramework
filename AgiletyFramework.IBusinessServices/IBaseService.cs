using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AgiletyFramework.IBusinessServices
{
    public interface IBaseService
    {
        #region Query
        public T Find<T>(int id) where T : class;
        public IQueryable<T> Set<T>() where T : class;
        public IQueryable<T> Query<T>(Expression<Func<T, bool>> funcWhere) where T : class;
        public PagingData<T> QueryPage<T, S>(
            Expression<Func<T, bool>> funcWhere,
            int pageSize, int pageIndex, Expression<Func<T, S>> funcOrderby,
            bool isAsc = true) where T : class;
        #endregion


        #region Insert
        public T Insert<T>(T t) where T : class;
        public IEnumerable<T> Insert<T>(IEnumerable<T> tList) where T : class;
        #endregion


        #region Update
        public void Update<T>(T t) where T : class;
        public void Update<T>(IEnumerable<T> tList) where T : class;
        #endregion


        #region Delete
        public void Delete<T>(T t) where T : class;
        public void Delete<T>(int id) where T : class;
        public void Delete<T>(IEnumerable<T> tList) where T : class;
        #endregion


        #region Other
        public int Commit();
        public IQueryable<T> ExcuteQuery<T>(string sql, SqlParameter[] parameters) where T : class;
        public void Excute<T>(string sql, SqlParameter[] parameters) where T : class;
        public DbContext GetDbContext();
        #endregion


    }
}
