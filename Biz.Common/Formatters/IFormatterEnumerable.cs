using System.Collections;
using System.Collections.Generic;

namespace Biz.Common
{
    public interface IFormatterEnumerable
    {
        IEnumerable<string> Serialize(IEnumerable rows);
    }


    public interface IFormatterEnumerable<T> : IFormatterEnumerable
    {
        IEnumerable<string> Serialize(IEnumerable<T> rows);
    }
}
