using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgiletyFramework.DBModels.DbContexts
{
    public class DesignTimeAgiletyDbContextFactory : IDesignTimeDbContextFactory<AgiletyDbContext>
    {
        public AgiletyDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AgiletyDbContext>();
            builder.UseSqlServer("Data Source=PC-20240312PQQX\\SQLEXPRESS;Initial Catalog=Agilety;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
            return new AgiletyDbContext(builder.Options);
        }
    }
}
