using System.Net;

namespace CachingProxyServer;

public class Handler
{
    private CachingProxyServer _proxyServer;

    public void HandleCommand(string command)
    {
        var cmds = command.Split(" ");
        
        for (int i = 0; i < cmds.Length; i++)
        {
            switch (cmds[i])
            {
                case "--port" when i + 1 < cmds.Length:
                    _proxyServer = new CachingProxyServer(cmds[i + 1]);
                    break;
                case "--origin" when i + 1 < cmds.Length:
                    _proxyServer.HttpClient.BaseAddress = new Uri(cmds[i + 1]);
                    break;
            }
        }
    }

    public async Task HandleRequest()
    {
        var context = await _proxyServer.HttpListener.GetContextAsync();
        await _proxyServer.HandleRequest(context);
    }
    
}