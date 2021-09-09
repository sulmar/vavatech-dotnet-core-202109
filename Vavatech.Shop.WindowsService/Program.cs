using System;
using System.IO;
using Topshelf;

namespace Vavatech.Shop.WindowsService
{
    class Program
    {
        static void Main(string[] args)
        {
            // dotnet add package Topshelf

            Console.WriteLine("Hello World!");

            HostFactory.Run(p =>
            {
                p.Service<LoggerService>();
                p.SetDisplayName("Vavatech Service");
                p.SetDescription("Opis usługi");
                p.StartAutomatically();
            });
        }
    }

    public class LoggerService : ServiceControl
    {
        public bool Start(HostControl hostControl)
        {
            Log("Started!");

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            Log("Stopped!");

            return true;
        }

        private void Log(string message)
        {
            File.AppendAllText(@"c:\temp\log.txt",  $"{DateTime.Now} {message} " + Environment.NewLine);
        }
    }


}
