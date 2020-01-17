using System;
using CsvHelper;
using System.IO;
using System.Linq;
using Xunit;

namespace Biz.Common.Test
{
    public class CsvHelperTest
    {
        [Theory]
        [InlineData(46594)]
        public void CheckReadRowsLowLevelCount(int expected)
        {
            var count = CsvHelperProxy.ReadRowsLowLevel(Path.Combine("Assets", "bigfile.csv")).Count();
            Assert.Equal(expected, count);
        }

        [Theory]
        [InlineData(46594)]
        public void CheckReadRowsCount(int expected)
        {
            var count = CsvHelperProxy.ReadRows(Path.Combine("Assets", "bigfile.csv")).Count();
            Assert.Equal(expected, count);

        }

        [Theory]
        [InlineData(46593)]
        public void CheckReadRowsWithMappingCount(int expected)
        {
            var mapper = new RowModelMapper();
            var count = CsvHelperProxy
                .ReadRows(Path.Combine("Assets", "bigfile.csv"))
                .Select(mapper.Map)
                .Skip(1)
                .Where(m => m != null)
                .Count();
            Assert.Equal(expected, count);
        }

        [Theory]
        [InlineData(46593)]
        public void CheckReadRowsLowLevelWithMappingCount(int expected)
        {
            var mapper = new RowModelMapper();
            var count = CsvHelperProxy
                .ReadRowsLowLevel(Path.Combine("Assets", "bigfile.csv"))
                .Select(mapper.Map)
                .Skip(1)
                .Where(m => m != null)
                .Count();
            Assert.Equal(expected, count);
        }

        [Theory]
        [InlineData(46593)]
        public void CheckReadWithMappingCount(int expected)
        {
            var count = CsvHelperProxy.Read<RowModel, RowModelMap>(Path.Combine("Assets", "bigfile.csv")).Count();
            Assert.Equal(expected, count);
        }

        [Fact]
        public void CheckResultsBetweenTwoMethods()
        {
            var filepath = Path.Combine("Assets", "bigfile.csv");
            
            var mapper = new RowModelMapper();

            var homeMade = CsvHelperProxy
                .ReadRows(filepath)
                .Select(mapper.Map)
                .Skip(1)
                .Where(m => m != null).ToList();

            var expected = CsvHelperProxy.Read<RowModel, RowModelMap>(filepath).ToList();
            foreach (var r in expected)
                Assert.True(homeMade.Remove(r), r.ToString() + " NotFound");
            Assert.Empty(homeMade);

            homeMade = CsvHelperProxy
                .ReadRowsLowLevel(filepath)
                .Select(mapper.Map)
                .Skip(1)
                .Where(m => m != null).ToList();
            foreach (var r in expected)
                Assert.True(homeMade.Remove(r), r.ToString() + " NotFound");
            Assert.Empty(homeMade);
        }
    }
}
