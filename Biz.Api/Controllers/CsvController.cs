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
        [Produces("application/json")]
        public async Task<IEnumerable<OutputRowModel>> GetCsvAsync([FromQuery, Required] string csvUri)
        {
            var response = await ServiceHttpClient.GetAsync(csvUri);
            var result = Enumerable.Empty<OutputRowModel>();
            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                result = CsvHelperProxy.ReadRowsFromStream(stream).ToResult(Mapper).ToList();
            }
            return await Task.FromResult(result);
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("text/plain")]
        public async Task<IEnumerable<OutputRowModel>> PostCsvAsync([FromBody] string csv)
        {
            var result = Enumerable.Empty<OutputRowModel>();
            using (var stream = csv.ToStream())
            {
                result = CsvHelperProxy.ReadRowsFromStream(stream).ToResult(Mapper).ToList();
            }
            return await Task.FromResult(result);
        }

        //[HttpGet]
        //[Produces("application/json")]
        //public async IAsyncEnumerable<OutputRowModel> GetCsvAsync([FromQuery, Required] string csvUri)
        //{
        //    var response = await ServiceHttpClient.GetAsync(csvUri);
        //    using (var stream = await response.Content.ReadAsStreamAsync())
        //    {
        //        await foreach (var r in CsvHelperProxy.ReadRowsFromStreamAsync(stream).ToResultAsync(Mapper))
        //            yield return r;
        //    }
        //}

        //[HttpPost]
        //[Produces("application/json")]
        //[Consumes("text/plain")]
        //public async IAsyncEnumerable<OutputRowModel> PostCsvAsync([FromBody] string csv)
        //{
        //    using (var stream = csv.ToStream())
        //    {
        //        await foreach (var r in CsvHelperProxy.ReadRowsFromStreamAsync(stream).ToResultAsync(Mapper))
        //            yield return r;
        //    }
        //}

    }
}
