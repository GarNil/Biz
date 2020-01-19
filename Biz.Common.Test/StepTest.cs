using AutoMapper;
using Biz.Console;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace Biz.Common.Test
{
    public class StepTest
    {
        [Theory]
        [InlineData(1, "bigfile.csv", "step1.txt")]
        [InlineData(2, "bigfile.csv", "step2.txt")]//=>not json ext to avoid auto formatting
        [InlineData(3, "bigfile.csv", "step3.txt")]//=>not xml ext to avoid auto formatting
        public void CheckStep(int stepNumber, string inputFileName, string expectedFileName)
        {
            var mapper = new MapperConfiguration(cfg => _ = MapperHelper.Configure(cfg)).CreateMapper();
            var formatter = Program.GetFormatter((Program.OutputType)stepNumber);
            var rows = formatter.Serialize(
                            CsvHelperProxy.ReadRows(Path.Combine("Assets", inputFileName))
                            .Skip(1)
                            .Select((v, i) => mapper.Map<RowModel>((i, v)))
                            .Select(mapper.Map<OutputRowModel>)
                );

            //using (var s = new StreamWriter(File.OpenWrite(@"c:\temp\totototo.xml")))
            //{
            //    foreach (var row in rows)
            //        s.WriteLine(row);
            //}
            var expectedRows = new List<string>();
            using (var stream = new StreamReader(File.OpenRead(Path.Combine("Assets", expectedFileName))))
            {
                while (!stream.EndOfStream)
                    expectedRows.Add(stream.ReadLine());
            }

            var expectedEnumerator = expectedRows.GetEnumerator();
            foreach (var row in rows)
            {
                Assert.True(expectedEnumerator.MoveNext(), $"{stepNumber} : too many rows received !");
                Assert.Equal(expectedEnumerator.Current, row);
            }
            Assert.False(expectedEnumerator.MoveNext(), $"{stepNumber} : not enough rows received !");
        }
    }
}
