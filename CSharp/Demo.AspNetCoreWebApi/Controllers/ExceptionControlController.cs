using Microsoft.AspNetCore.Mvc;
using System;

namespace Demo.AspNetCoreWebApi.Controllers
{
    /// <summary>
    /// 全局异常拦截测试控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ExceptionControlController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            throw new ArgumentException("抛出一个参数异常测试");
        }
    }
}
