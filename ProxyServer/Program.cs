// See https://aka.ms/new-console-template for more information

using CachingProxyServer;

string? cmd = Console.ReadLine();

if (!string.IsNullOrEmpty(cmd))
{
    Handler handler = new Handler();
    handler.HandleCommand(cmd);
    
    while (true)
    {
        await handler.HandleRequest();
    }
}
