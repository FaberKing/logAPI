using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using LogApi.Functions;

namespace LogApi.Services;

public class ResponseMiddleware
{
    private readonly RequestDelegate _next;

    public ResponseMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task Invoke(HttpContext context)
    {
        var originalBody = context.Response.Body;
        
        var responseBody = string.Empty;
        
        using (var memStream = new MemoryStream())
        {
            context.Response.Body = memStream;
            await _next(context);
            memStream.Position = 0;
            responseBody = new StreamReader(memStream).ReadToEnd();
        }

        var json = new
        {
            Data = responseBody.Encrypt(),
            ApiVersion = "V1"
        };
        
        var buffer = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(json));
        using (var output = new MemoryStream(buffer))
        {
            await output.CopyToAsync(originalBody);
        }

        context.Response.Body = originalBody;
    }
}