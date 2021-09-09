using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Vavatech.Shop.TcpServer
{

    public class LoggerBackgroundService : BackgroundService
    {
        private readonly ILogger<LoggerBackgroundService> logger;

        public LoggerBackgroundService(ILogger<LoggerBackgroundService> logger)
        {
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(true)
            {
                if (stoppingToken.IsCancellationRequested)
                    break;

                // Logic
                logger.LogInformation("Logger {0}", DateTime.Now);

                await Task.Delay(TimeSpan.FromSeconds(1));

            }
        }
    }
}
