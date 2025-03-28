using System.Net;

namespace CachingProxyServer;

public class CachingProxyServer
{
    public CachingProxyServer(int port)
    {
        var listener = new HttpListener();
        listener.Prefixes.Add($"http://localhost:{port}");
        listener.Start();
    }
}