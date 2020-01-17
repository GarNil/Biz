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

            var jsonRows = CsvHelperProxy
                .ReadRows(args[0])
                .Select((v, i) => mapper.Map<RowModel>((i, v)))
                .Skip(1)
                .Where(m => m != null)
                .Select(mapper.Map<JsonRowModel>)
;

            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.None,
                NullValueHandling = NullValueHandling.Ignore
            };
            System.Console.WriteLine("[");
            string comma = string.Empty;
            foreach (var jsonRow in jsonRows)
            {
                var r = JsonConvert.SerializeObject(jsonRow, settings);
                System.Console.WriteLine($"{comma}{r}");
                if (comma == string.Empty)//It s ugly... shame on me... :(
                    comma = ",";
            }
            System.Console.WriteLine("]");
            System.Console.WriteLine("Press any key...");
            System.Console.ReadKey();
        }
    }
}
