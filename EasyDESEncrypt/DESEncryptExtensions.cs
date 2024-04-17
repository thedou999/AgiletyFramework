using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDESEncrypt
{
    public static class DESEncryptExtensions
    {
        public static void AddDESEncrypt(this IServiceCollection services, string EncryptKey)
        {
            DESEncryptContext.InitializeDESEncryptContext(EncryptKey);
        }
    }
}
