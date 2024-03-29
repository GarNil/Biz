﻿using AutoMapper;
using Biz.Common;
using Fclp;
using Serilog;
using System;

namespace Biz.Console
{
    public  class Program
    {
        public class ApplicationArguments
        {
            public string File { get; set; }
            public OutputType OutputType { get; set; }
        }

        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            var p = new FluentCommandLineParser<ApplicationArguments>();

            p.Setup(arg => arg.File)
             .As('f', "file") 
             .Required();

            p.Setup(arg => arg.OutputType)
             .As('o', "output_type")
             .SetDefault(OutputType.PlainText);

            var result = p.Parse(args);

            if (result.HasErrors)
            {
                Environment.Exit(-1);
            }
            Run(p.Object);
        }

        public static void Run(ApplicationArguments arg) 
        {
            var mapper = new MapperConfiguration(cfg => _ = MapperHelper.Configure(cfg)).CreateMapper();

            var rows = arg.OutputType.GetFormatter().Serialize(CsvHelperProxy
                .ReadRows(arg.File).ToResult(mapper)
                );

            foreach (var row in rows)
            {
                System.Console.WriteLine($"{row}");
            }
            System.Console.WriteLine("Press any key...");
            System.Console.ReadKey();
        }
    }
}
