using AgiletyFramework.WebCore.WebApplicationBuilderExtend;
using EasyDESEncrypt;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgiletyFramework.WebCore.DESEncryptExtend
{
    public static class DESEncryptExtensions
    {

        public static void AddDESEncryptExt(this WebApplicationBuilder builder)
        {
            string? EncryptKey = builder.Configuration.GetSection("EncryptKey").Value;
            builder.Services.AddDESEncrypt(EncryptKey);
        }


    }
}
