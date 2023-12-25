using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

//builder.Configuration["ForwardedHeaders_Enabled"] = "true";

builder.Services.Configure<ForwardedHeadersOptions>(x =>
{
    x.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

var app = builder.Build();

app.UseForwardedHeaders();

app.MapGet("/", (HttpContext context) =>
{
    return new
    {
        Ip = context.Features.Get<IHttpConnectionFeature>().RemoteIpAddress.ToString(),
        OriginalIp = context.Request.Headers[ForwardedHeadersDefaults.XOriginalForHeaderName],
        ForwardedIp = context.Request.Headers[ForwardedHeadersDefaults.XForwardedForHeaderName],
    };
});

app.Run();