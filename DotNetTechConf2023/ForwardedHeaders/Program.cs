using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration["ForwardedHeaders_Enabled"] = "true";

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