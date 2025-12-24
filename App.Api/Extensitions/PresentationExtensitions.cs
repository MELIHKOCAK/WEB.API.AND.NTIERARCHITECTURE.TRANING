using Microsoft.AspNetCore.RateLimiting;
using Serilog;
using System.Runtime.CompilerServices;
using System.Threading.RateLimiting;


namespace App.Api.Extensitions
{
    public static class PresentationExtensitions
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
                    var correlationId = httpContext.Response.Headers["X-Correlation-ID"].FirstOrDefault()
                        ?? httpContext.TraceIdentifier;

                    diagnosticContext.Set("CorrelationId", correlationId);
                    diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
                    diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
                    diagnosticContext.Set("ClientIP",
                        httpContext.Connection.RemoteIpAddress?.ToString());
                };
            });

            return app;
        }

        public static IServiceCollection AddConfigureRateLimits(this IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                options.AddTokenBucketLimiter("Token", _options =>
                {
                    _options.TokenLimit = 15;
                    _options.ReplenishmentPeriod = TimeSpan.FromSeconds(3);
                    _options.TokensPerPeriod = 5;
                    _options.AutoReplenishment = true;
                    _options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    _options.QueueLimit = 5;

                });

                options.AddConcurrencyLimiter("Concurrency", _options =>
                {

                    _options.PermitLimit = 15;
                    _options.QueueLimit = 5;
                    _options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;

                });
            });

            return services;
        }
    }
}