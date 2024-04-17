using AgiletyFramework.WebCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AgiletyFramework.Commons;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace AgiletyFramework.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi =false, GroupName = nameof(ApiVersions.V1))]
    public class FileController : ControllerBase
    {
        private readonly ILogger<FileController> logger;

        public FileController(ILogger<FileController> logger)
        {
            this.logger = logger;
        }

        [HttpPost]
        public async Task<JsonResult> UploadFiles([FromForm] IFormFile file)
        {
            string suffix = string.Empty;

            #region 获取文件后缀
            string filename = file.FileName.Trim();
            int index = filename.LastIndexOf(".");
            if(index>0 && index<filename.Length - 1)
            {
                suffix = filename.Substring(index + 1);
            }
            #endregion

            #region 重新命名保存文件的名字
            string saveDirectory = $"FileUpload\\{DateTime.Now.ToString("yyyy-MM-dd")}";
            string allSavePath = $"{Directory.GetCurrentDirectory()}\\{saveDirectory}";
            if(Directory.Exists(allSavePath) == false)
            {
                Directory.CreateDirectory(allSavePath);
            }
            //保存的新文件名
            string newFileName = $"{DateTime.Now.ToString("yyyyMMddHHmmss")}_" +
                $"{Guid.NewGuid().ToString()}.{suffix}";

            //保存的文件名
            string allSaveFilePath = $"{allSavePath}\\{newFileName}";

            #endregion

            try
            {
                using (var stream = System.IO.File.Create(allSaveFilePath))
                {
                    await file.CopyToAsync(stream);
                }
                return new JsonResult(new ApiDataResult<string>()
                {
                    Success = true,
                    Message = "文件上传成功",
                    Data = $"{saveDirectory}\\{newFileName}"
                });
            }
            catch (Exception)
            {
                return new JsonResult(new ApiDataResult<string>()
                {
                    Success = false,
                    Message = "文件上传失败"
                });
            }

        }

        [HttpGet]
        public async Task<FileContentResult> GetImage([FromQuery]string imagePath)
        {
            string allImagePath = $"{Directory.GetCurrentDirectory()}\\{imagePath}";

            using (var sw = new FileStream(allImagePath,FileMode.Open))
            {
                var bytes = new byte[sw.Length];
                sw.Read(bytes, 0, bytes.Length);
                sw.Close();
                var result = new FileContentResult(bytes,"image/jpeg");
                return result;
            }
        }


    }
}
