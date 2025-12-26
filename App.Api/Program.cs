using App.Api.Extensitions;
using App.Api.MiddleWares;
using App.Repositories.Extensions;
using App.Services.Extensions;
using App.Services.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRepositories(builder.Configuration);
builder.Services.AddControllers(options => options.Filters.Add<FluentValidationFilter>());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddServices();
builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
builder.Host.UseSerilog((context, services, config) =>
    config.ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    );
builder.Services.ConfigureVersioning();
builder.Services.AddConfigureRateLimits();

var app = builder.Build();

app.UseRateLimiter();
app.UseMiddleware<CorrelationIdMiddleware>();
app.UseSerilogRequestResponseLogging();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WEBAPITRANING v1.0");
    c.SwaggerEndpoint("/swagger/v2/swagger.json", "WEBAPITRANING v2.0");
});
app.UseExceptionHandler(x => { });
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();