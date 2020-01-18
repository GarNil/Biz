using Newtonsoft.Json;
using Xunit;

namespace Biz.Common.Test
{
    public class JsonTest
    {
        [Fact]
        public void CheckJsonResult()
        {
            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.None,
                NullValueHandling = NullValueHandling.Ignore
            };
            var jsonOk = new OutputRowModel { ErrorMessage = null, ConcatAB = "toto", LineNumber = 52, SumCD = 666, Type = KindOfType.ok };
            var result = JsonConvert.SerializeObject(jsonOk, settings);
            Assert.Equal(@"{""lineNumber"":52,""type"":""ok"",""concatAB"":""toto"",""sumCD"":666}", result);

            var jsonKo = new OutputRowModel { ErrorMessage = "Error", ConcatAB = null, LineNumber = 52, SumCD = null, Type = KindOfType.error };
            result = JsonConvert.SerializeObject(jsonKo, settings);
            Assert.Equal(@"{""lineNumber"":52,""type"":""error"",""errorMessage"":""Error""}", result);
        }
    }
}
