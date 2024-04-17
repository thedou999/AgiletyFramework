namespace AgiletyFramework.Commons
{
    public class ApiResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }

        public ApiResult()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Success"></param>
        /// <param name="Message"></param>
        public ApiResult(bool Success,string Message)
        {

        }

       
    }

    public class ApiDataResult<T> : ApiResult
    {
        public T? Data { get; set; }
        public object? OValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Success"></param>
        /// <param name="Message"></param>
        /// <param name="Data"></param>
        /// <param name="OValue"></param>
        public ApiDataResult(bool Success,string? Message,T? Data,object? OValue) : base(Success,Message)
        {
            this.Data = Data;
            this.OValue = OValue;
        }

        public ApiDataResult():base()
        {
        }

        public static ApiDataResult<T> NotFoundedResult()
        {
            return new ApiDataResult<T>()
            {
                Success = false,
                Message = "数据未找到",
                OValue = null
            };
        }
    }
}
