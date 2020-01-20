using System;
using System.Collections.Generic;
using System.Text;

namespace Biz.Common
{
    public class RowModelMapper : ICsvRowMapper<RowModel>
    {
        public RowModel Map((string[] v, int i) value)
        {
            var values = value.v;
            if (values == null || values.Length == 0)
                return null;
            int i = 0;
            return new RowModel()
            {
                Column = i < values.Length ? values[i++] : string.Empty,
                ColumnA = i < values.Length ? values[i++] : string.Empty,
                ColumnB = i < values.Length ? values[i++] : string.Empty,
                ColumnC = i < values.Length ? MapperHelper.ConvertToNullableInt(values[i++]) : null,
                ColumnD = i < values.Length ? MapperHelper.ConvertToNullableInt(values[i++]) : null,
                OtherColumn = i < values.Length ? values[i++] : string.Empty
            };
        }
    }
}
