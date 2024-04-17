using AgiletyFramework.BusinessServices;
using AgiletyFramework.WebCore;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AgiletyFramework.Commons;
using ModelDto;
using AgiletyFramework.Commons;
using AgiletyFramework.IBusinessServices;
using AgiletyFramework.DBModels.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Autofac;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace AgiletyFramework.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi =false,GroupName =nameof(ApiVersions.V1))]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LogController : ControllerBase
    {
        private readonly ILogger<LogController> logger;
        private readonly ILogService logService;
        private readonly IMapper mapper;

        public LogController(IMapper mapper, IComponentContext componentContext, ILogger<LogController> logger)
        {
            this.mapper = mapper;
            this.logService = componentContext.Resolve<ILogService>();
            this.logger = logger;

            Console.WriteLine(logger.ToString());
            Console.WriteLine(logService.ToString());
            Console.WriteLine(mapper.ToString());
        }

        [HttpGet]
        public async Task<ActionResult<ApiDataResult<PagingData<LogDto>>>> GetLogPage(int pageIndex,int pageSize,string? searchString)
        {
            PagingData<SystemLog> paginDataSystemLog = null;
            if (string.IsNullOrEmpty(searchString))
            {
                paginDataSystemLog = logService.QueryPage<SystemLog, DateTime>(o => true, pageSize, pageIndex, o => o.CreateTime, false);
            }
            else
            {
                paginDataSystemLog = logService.QueryPage<SystemLog, DateTime>(o => o.Message.Contains(searchString), pageSize, pageIndex, o => o.CreateTime, false);
            }
            var logDto = mapper.Map<PagingData<SystemLog>, PagingData<LogDto>>(paginDataSystemLog);
            if(logDto == null)
            {
                return new JsonResult(ApiDataResult<PagingData<LogDto>>.NotFoundedResult());
            }

            return new JsonResult(new ApiDataResult<PagingData<LogDto>>()
            {
                Success = true,
                Data = logDto,
                Message = "发送数据",
                OValue = null
            });
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult>> TestSystemLog()
        {
            try
            {
                throw new Exception($"{DateTime.Now}_抛出一个异常");
            }catch(Exception e)
            {
                logger.LogWarning(e.Message);
                var log = new SystemLog() { Level="Warning", Message=e.Message, Date=DateTime.Now, Exception=e.ToString(), Logger=e.Message, Thread=Thread.CurrentThread.ManagedThreadId.ToString() };
                logService.Insert<SystemLog>(log);
                return new JsonResult(new ApiResult(true,"成功"));
            }
            return new JsonResult(new ApiResult(false, "失败"));

        }



    }
}
