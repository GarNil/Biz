using System.Collections.Generic;
using System.Linq;

namespace Biz.Common
{
    public abstract class PlainTextFormatterEnumerable<T> : FormatterEnumerable<T>
    {
        public override IEnumerable<string> Serialize(IEnumerable<T> rows)
        {
            foreach (var row in rows.Where(r => r != null))
            {
                var r = row.ToString();
                if (!string.IsNullOrEmpty(r))
                    yield return row.ToString();
            }
        }
    }
}
