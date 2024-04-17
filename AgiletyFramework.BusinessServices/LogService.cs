using AgiletyFramework.IBusinessServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgiletyFramework.BusinessServices
{
    public class LogService : BaseService, ILogService
    {
        public LogService(DbContext dbContext) : base(dbContext)
        {
        }


    }
}
