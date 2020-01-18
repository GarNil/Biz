using AutoMapper;
using Biz.Common;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Biz.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                System.Console.WriteLine("Please a file path is necessary !");
                Environment.Exit(-1);
            }

            var mapper = new MapperConfiguration(cfg => _ = MapperHelper.Configure(cfg)).CreateMapper();

            var rows = new PlainTextFormatterEnumerableOutputRowModel().Serialize(CsvHelperProxy
                .ReadRows(args[0])
                .Skip(1)
                .Select((v, i) => mapper.Map<RowModel>((i, v)))
                .Select(mapper.Map<OutputRowModel>)
                );
            //var rows = new JsonFormatterEnumerableOutputRowModel().Serialize(CsvHelperProxy
            //    .ReadRows(args[0])
            //    .Skip(1)
            //    .Select((v, i) => mapper.Map<RowModel>((i, v)))
            //    .Select(mapper.Map<OutputRowModel>)
            //    );

            foreach (var row in rows)
            {
                System.Console.WriteLine($"{row}");
            }
            System.Console.WriteLine("Press any key...");
            System.Console.ReadKey();
        }
    }
}
