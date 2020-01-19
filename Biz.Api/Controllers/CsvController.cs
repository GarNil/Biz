using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Biz.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Biz.Api.Controllers
{
    [Route("api/[controller]/filter")]
    [ApiController]
    public class CsvController : ControllerBase
    {
        private IMapper Mapper
        {
            get => new MapperConfiguration(cfg => _ = MapperHelper.Configure(cfg)).CreateMapper();
        }

        [HttpGet]
        public async Task<string> GetCsvAsync([FromQuery, Required] string csvUri)
            => await Task.FromResult(string.Empty);

        [HttpPost]
        public async Task<string> PostCsvAsync([FromBody] string csv)
        {

            var result = Enumerable.Empty<string>();
            //using (var stream = new StringReader(csv))
            //{
            //    result = OutputType.Json.GetFormatter().Serialize(CsvHelperProxy
            //        .ReadRows(stream.BaseStream)
            //        .Skip(1)
            //        .Select((v, i) => Mapper.Map<RowModel>((i, v)))
            //        .Select(Mapper.Map<OutputRowModel>)
            //    );
            //}
            return await Task.FromResult(string.Join("", result));
        }
    }
}
