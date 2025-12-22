using Serilog;
using System.Runtime.CompilerServices;


namespace App.Api.Extensitions
{
    public static class SerilogExtensition
    {
        public static IApplicationBuilder UseSerilogRequestResponseLogging(
         this IApplicationBuilder app)
        {
            app.UseSerilogRequestLogging(options =>
            {
                options.MessageTemplate =
                    "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";

                options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                {
                    diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
                    diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
                    diagnosticContext.Set("ClientIP",
                        httpContext.Connection.RemoteIpAddress?.ToString());
                };
            });

            return app;
        }
    }
}