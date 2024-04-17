using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AgiletyFramework.WebCore.WebApplicationBuilderExtend
{
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder? Builder { get; set; }

        public static void SetBuilder(this WebApplicationBuilder orignalBuilder)
        {
            Builder = orignalBuilder;
        }
    }
}
