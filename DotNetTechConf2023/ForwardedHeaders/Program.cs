using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

var app = builder.Build();

app.UseForwardedHeaders();

app.MapGet("/", (HttpContext context) =>
{
    return new
    {
        Ip = context.Features.Get<IHttpConnectionFeature>().RemoteIpAddress.ToString(),
        Ip2 = context.Connection.RemoteIpAddress.ToString(),
        OriginalIp = context.Request.Headers[ForwardedHeadersDefaults.XOriginalForHeaderName],
        ForwardedIp = context.Request.Headers[ForwardedHeadersDefaults.XForwardedForHeaderName],
    };
});

app.Run();