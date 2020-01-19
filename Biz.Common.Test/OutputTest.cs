using System;
using System.Linq;
using Xunit;

namespace Biz.Common.Test
{
    public class OutputTest
    {
        [Fact]
        public void CheckCoverAllOutputsGetFormatter()
        {
            foreach (var outputType in Enum.GetValues(typeof(OutputType)).Cast<OutputType>())
            {
                var formatter = outputType.GetFormatter();
                Assert.NotNull(formatter);
                Assert.IsAssignableFrom<IFormatterEnumerable>(formatter);
            }
        }
    }
}
