using Biz.Common;
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
            var mapper = new RowModelMapper();

            var csvRows = CsvHelperProxy
                .ReadRows(args[0])
                .Select(mapper.Map)
                .Skip(1)
                .Where(m => m != null);

            foreach (var csvRow in csvRows)
            {
                if (csvRow.ColumnC.HasValue && csvRow.ColumnD.HasValue && csvRow.ColumnC.Value + csvRow.ColumnD.Value > 100)
                    System.Console.WriteLine(csvRow.ColumnA + csvRow.ColumnB);
            }
            System.Console.WriteLine("Press any key...");
            System.Console.ReadKey();
        }
    }
}
