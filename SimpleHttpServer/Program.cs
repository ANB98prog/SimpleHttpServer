using HttpServer;
using HttpServer.Handlers;
using System.Net;
using System.Net.Sockets;

namespace SimpleHttpServer
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var host = new ServerHost(new ControllersHandler(typeof(Program).Assembly));
            await host.StartAsync();
        }
    }
}