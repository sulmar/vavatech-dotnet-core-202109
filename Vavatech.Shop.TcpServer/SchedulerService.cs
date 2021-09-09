using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Vavatech.Shop.TcpServer
{
    public class SchedulerService : IHostedService
    {
        private readonly Timer timer;

        public SchedulerService()
        {
            timer = new Timer(DoWork);
        }

        private void DoWork(object state)
        {
            throw new NotImplementedException();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
