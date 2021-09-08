using Bogus;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Vavatech.Shop.GrpcServer;

namespace Vavatech.Shop.GrpcService.Services
{
                                    // {csharp_namespace}.{service}.{service}Base
    public class MyTrackingService : Vavatech.Shop.GrpcServer.TrackingService.TrackingServiceBase
    {
        private readonly ILogger<MyTrackingService> logger;

        public MyTrackingService(ILogger<MyTrackingService> logger)
        {
            this.logger = logger;
        }

        public override Task<AddLocationResponse> AddLocation(AddLocationRequest request, ServerCallContext context)
        {
            logger.LogInformation($"Reqest {request.Name} {request.Latitude} {request.Longitude} {request.Speed} {request.Direction}");

            var response = new AddLocationResponse { IsConfirmed = true };

            return Task.FromResult(response);
        }

        public override async Task Subscribe(SubscribeRequest request, IServerStreamWriter<SubscribeResponse> responseStream, ServerCallContext context)
        {
            var locations = new Faker<SubscribeResponse>()
                .RuleFor(p => p.Name, f => f.Vehicle.Model())
                .RuleFor(p => p.Latitude, f => (float)f.Address.Latitude())
                .RuleFor(p => p.Longitude, f => (float)f.Address.Longitude())
                .RuleFor(p => p.Speed, f => f.Random.Int(0, 190))
                .RuleFor(p => p.Direction, f => f.Random.Float())
                .GenerateForever();

            locations = locations.Where(p => p.Speed > request.Speed);

            foreach (var locaton in locations)
            {
                await responseStream.WriteAsync(locaton);

                logger.LogInformation($"Reqest {locaton.Name} {locaton.Latitude} {locaton.Longitude} {locaton.Speed} {locaton.Direction}");

                await Task.Delay(TimeSpan.FromMilliseconds(10));
            }

        }
    }
}
