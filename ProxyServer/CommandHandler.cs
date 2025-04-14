namespace CachingProxyServer;

public class CommandHandler
{
    private CachingProxyServer _proxyServer;
    public CommandHandler()
    {
    }

    public string Handle(string command)
    {
        string [] cmds = command.Split(" ");
        
        for (int i = 0; i < cmds.Length; i++)
        {
            switch (cmds[i])
            {
                case "--port" when i + 1 < cmds.Length:
                    _proxyServer = new CachingProxyServer(cmds[i + 1]);
                    break;
                case "--origin" when i + 1 < cmds.Length:
                    _proxyServer.RemoteUrl = new Uri(cmds[i + 1]);
                    break;
                default:
                    return "Error parsing command";
            }
        }

        return "";
    }
    
}