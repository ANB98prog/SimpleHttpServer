using HttpServer;
using System.Net;
using System.Net.Sockets;

namespace SimpleHttpServer
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var host = new ServerHost();
            await host.StartAsync();
        }
    }
}