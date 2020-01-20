using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Biz.Common
{
    public static class Helper
    {
        public static IEnumerable<OutputRowModel> ToResult(this IEnumerable<(string[] v, int i)> values, IMapper mapper)
        {
            foreach (var v in values.Where(v => v.i >= 0)
                        .Select(v => mapper.Map<RowModel>(v))
                        .Select(mapper.Map<OutputRowModel>))
                yield return v;
        }
        public static async IAsyncEnumerable<OutputRowModel> ToResultAsync(this IAsyncEnumerable<(string[] v, int i)> values, IMapper mapper)
        {
            await foreach (var v in values.Where(v => v.i >= 0)
                        .Select(v => mapper.Map<RowModel>(v))
                        .Select(mapper.Map<OutputRowModel>))
                yield return v;
        }
    }
}
