using System;
using System.Collections.Generic;
using System.Text;

namespace Biz.Common
{

    public class RowModelMapper : ICsvRowMapper<RowModel>
    {
        private static int? ConvertToNullableInt(string value)
            => int.TryParse(value, out int v) ? v : (int?)null;

        public RowModel Map(string[] values)
        {
            if (values == null || values.Length == 0)
                return null;
            int i = 0;
            return new RowModel()
            {
                Column = i < values.Length ? values[i++] : string.Empty,
                ColumnA = i < values.Length ? values[i++] : string.Empty,
                ColumnB = i < values.Length ? values[i++] : string.Empty,
                ColumnC = i < values.Length ? ConvertToNullableInt(values[i++]) : null,
                ColumnD = i < values.Length ? ConvertToNullableInt(values[i++]) : null,
                OtherColumn = i < values.Length ? values[i++] : string.Empty
            };
        }
    }
}
