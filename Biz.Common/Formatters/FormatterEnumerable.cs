using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Biz.Common
{
    public abstract class FormatterEnumerable<T> : IFormatterEnumerable<T>
    {
        public abstract IEnumerable<string> Serialize(IEnumerable<T> rows);

        public IEnumerable<string> Serialize(IEnumerable rows)
            => Serialize(rows?.Cast<T>());
    }
}
