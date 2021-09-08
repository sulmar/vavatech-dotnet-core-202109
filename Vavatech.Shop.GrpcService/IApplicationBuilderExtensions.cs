using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Vavatech.Shop.GrpcService
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseStaticFilesProtos(this IApplicationBuilder app, string root, string endpoint = "/proto")
        {
            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings.Clear();
            provider.Mappings[".proto"] = "text/plain";

            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = endpoint,
                FileProvider = new PhysicalFileProvider(root),
                ContentTypeProvider = provider
            });

            return app;
        }
    }
}
