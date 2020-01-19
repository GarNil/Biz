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
        public async Task<string> GetCsvAsync([FromQuery, Required] string csvUri)
            => await Task.FromResult(string.Empty);

        [HttpPost]
        public async Task<string> PostCsvAsync([FromBody] string csvUri)
            => await Task.FromResult(string.Empty);
    }
}
