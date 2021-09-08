using Grpc.Core;
using Microsoft.Extensions.Logging;
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
    }
}
