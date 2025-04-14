using System.Net;

namespace CachingProxyServer;

public class CachingProxyServer
{
    private readonly HttpClient _httpClient;
    private readonly HttpListener _httpListener;
    private Dictionary<string,string> _caches = new();
    
    
    public CachingProxyServer(int port)
    {
        _httpClient = new HttpClient();
        _httpListener = new HttpListener();
        _httpListener.Prefixes.Add($"http://localhost:{port}/");
        _httpListener.Start();
    }
}