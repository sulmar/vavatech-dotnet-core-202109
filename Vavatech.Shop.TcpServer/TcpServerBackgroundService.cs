using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Vavatech.Shop.TcpServer
{
    public class TcpServerOptions
    {
        public string Host { get; set; }
        public int Port { get; set; }

        public TcpServerOptions()
        {
            Host = "127.0.0.1";
            Port = 9000;
        }
    }

    public class TcpServerBackgroundService : BackgroundService
    {
        private readonly TcpServerOptions options;
        private readonly ILogger<TcpServerBackgroundService> logger;

        public TcpServerBackgroundService(IOptions<TcpServerOptions> options, ILogger<TcpServerBackgroundService> logger)
        {
            this.options = options.Value;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            TcpListener listener = new TcpListener(IPAddress.Parse(options.Host), options.Port);
            listener.Start();

            logger.LogInformation("Now listening on port: {0}", options.Port);

            while (!stoppingToken.IsCancellationRequested)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();

                logger.LogInformation("Client connected. Waiting for request...");

                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    while (client.Connected)
                    {
                        var request = await reader.ReadLineAsync();

                        logger.LogInformation("Request {0}", request);

                        var response = $"ECHO {request}"; // <-- logic

                        await writer.WriteLineAsync(response);

                        writer.AutoFlush = true;

                    }
                }

            }

        }
    }
}
