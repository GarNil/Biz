using System.Collections.Generic;

namespace Biz.Common
{
    public interface IFormatterEnumerable<T> where T : class
    {
        public abstract IEnumerable<string> Serialize(IEnumerable<T> rows);
    }
}
