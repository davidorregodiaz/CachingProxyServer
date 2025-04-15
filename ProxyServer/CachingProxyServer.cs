using System.Collections.Concurrent;
using System.Net;
using System.Text;

namespace CachingProxyServer;

public class CachingProxyServer
{
    public readonly HttpClient HttpClient;
    public readonly HttpListener HttpListener;
    private ConcurrentDictionary<string,string> _cache = new();
    
    
    public CachingProxyServer(string port)
    {
        HttpClient = new HttpClient();
        HttpListener = new HttpListener();
        HttpListener.Prefixes.Add($"http://localhost:{port}/");
        HttpListener.Start();
    }

    public async Task HandleRequest(HttpListenerContext context)
    {
        var cacheKey = $"{context.Request.HttpMethod}:{context.Request.Url.PathAndQuery}";

        if (_cache.TryGetValue(cacheKey, out string? response))
        {
            Console.WriteLine("CACHE --- HIT");
            context.Response.Headers.Add("Cache-Control", "HIT");
            context.Response.StatusCode = 200;
            var responseBytes = Encoding.UTF8.GetBytes(response);
            context.Response.ContentLength64 = responseBytes.Length;
            await context.Response.OutputStream.WriteAsync(responseBytes, 0, responseBytes.Length);
            context.Response.Close();
        }
        else
        {
            var fullTargetUri = new Uri(HttpClient.BaseAddress.AbsoluteUri + context.Request.Url.PathAndQuery);
            var request = new HttpRequestMessage(new HttpMethod(context.Request.HttpMethod), fullTargetUri);

            var httpResponseMessage = await HttpClient.SendAsync(request);

            var responseBody = await httpResponseMessage.Content.ReadAsStringAsync();
            _cache.TryAdd(cacheKey,responseBody);

            Console.WriteLine($"CACHE MISS: {cacheKey}");

            context.Response.Headers.Add("Cache-Control", "MISS");
            context.Response.StatusCode = (int)httpResponseMessage.StatusCode;
            var responseBytes = Encoding.UTF8.GetBytes(responseBody);
            context.Response.ContentLength64 = responseBytes.Length;
            await context.Response.OutputStream.WriteAsync(responseBytes, 0, responseBytes.Length);
            context.Response.Close();
        }
        
    }
    
}