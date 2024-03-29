﻿using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Biz.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Biz.Api.Controllers
{
    [Route("api/[controller]/filter")]
    [ApiController]
    public class CsvController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CsvController> _logger;


        public CsvController(IMapper mapper, ILogger<CsvController> logger) 
        {
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ObjectResult> GetCsvAsync([FromQuery, Required] string csvUri)
        {
            _logger.LogDebug($"GetCsvAsync({csvUri}");
            var response = await ServiceHttpClient.GetAsync(csvUri);
            var result = Enumerable.Empty<OutputRowModel>();
            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                result = CsvHelperProxy.ReadRowsFromStream(stream).ToResult(_mapper).ToList();
            }
            return Ok(result);
        }

        [HttpPost]
        [Consumes("text/plain")]
        public async Task<ObjectResult> PostCsvAsync([FromBody] string csv)
        {
            _logger.LogDebug($"PostCsvAsync({csv}");
            var result = Enumerable.Empty<OutputRowModel>();
            using (var stream = csv.ToStream())
            {
                result = CsvHelperProxy.ReadRowsFromStream(stream).ToResult(_mapper).ToList();
            }
            return Ok(result);
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
