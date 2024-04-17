using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgiletyFramework.WebCore.DownloadFileExtend
{
    public class DownloadImagesMiddleware
    {
        private readonly RequestDelegate next;
        private string? directoryPath = null;

        public DownloadImagesMiddleware(RequestDelegate next, string? directoryPath)
        {
            this.next = next;
            this.directoryPath = directoryPath;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            bool bResult = context.Request.Path.Value!.EndsWith(".jpg")
                || context.Request.Path.Value!.EndsWith(".png")
                || context.Request.Path.Value!.EndsWith(".gif");

            if (context.Request.Path.Value!.EndsWith(".jpg"))
            {
                string fileUrl = $"{directoryPath}{context.Request.Path.Value}";

                if (!File.Exists(fileUrl))
                {
                    await next(context);
                }
                else
                {
                    context.Request.EnableBuffering();
                    context.Request.Body.Position = 0;
                    var responseStream = context.Response.Body;

                    using (FileStream newStream = new FileStream(fileUrl, FileMode.Open))
                    {
                        context.Response.Body = newStream;

                        newStream.Position = 0;
                        var responseReader = new StreamReader(newStream);
                        var responseContent = await responseReader.ReadToEndAsync();
                        newStream.Position = 0;

                        await newStream.CopyToAsync(responseStream);
                        context.Response.Body = responseStream;
                    }
                    await next(context);

                }

            }
            else
            {
                await next.Invoke(context);
            }


        }


    }
}
