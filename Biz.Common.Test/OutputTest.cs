using Biz.Console;
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
            foreach (var outputType in Enum.GetValues(typeof(Program.OutputType)).Cast<Program.OutputType>())
            {
                var formatter = Program.GetFormatter(outputType);
                Assert.NotNull(formatter);
                Assert.IsAssignableFrom<IFormatterEnumerable>(formatter);
            }
        }
    }
}
