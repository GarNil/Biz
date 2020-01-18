using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Biz.Common
{
    public abstract class JsonFormatterEnumerable<T> : IFormatterEnumerable<T>
        where T : class
    {
        private readonly JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            Formatting = Formatting.None,
            NullValueHandling = NullValueHandling.Ignore
        };

        public IEnumerable<string> Serialize(IEnumerable<T> rows)
        {
            yield return "[";
            string comma = string.Empty;
            foreach (var row in rows.Where(j => j != null))
            {
                yield return $"{comma}{JsonConvert.SerializeObject(row, _settings)}";
                if (comma == string.Empty)//It s ugly... shame on me... :(
                    comma = ",";
            }
            yield return "]";
        }
    }
}
