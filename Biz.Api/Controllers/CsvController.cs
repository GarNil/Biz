using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Biz.Api.Controllers
{
    [Route("api/[controller]/filter")]
    [ApiController]
    public class CsvController : ControllerBase
    {
        [HttpGet]
        public async Task GetCsvAsync([FromQuery, Required] string csvUri)
            => await Task.CompletedTask;

        [HttpPost]
        public async Task PostCsvAsync([FromBody] string csvUri)
            => await Task.CompletedTask;
    }
}
