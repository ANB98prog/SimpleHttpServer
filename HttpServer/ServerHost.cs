using System.Net.Sockets;
using System.Net;
using HttpServer.Handlers;

namespace HttpServer
{
    public class ServerHost
    {
        private readonly IHandler _handler;

        public ServerHost(IHandler handler)
        {
            _handler = handler;
        }

        public async Task StartAsync()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 12125);
            listener.Start();

            Console.WriteLine("Server started at http://localhost:12125");

            while (true)
            {
                var client = await listener.AcceptTcpClientAsync();
                var _ = ProcessClientAsync(client);
            }
        }

        private async Task ProcessClientAsync(TcpClient client)
        {
            try
            {
                using (client)
                using (var stream = client.GetStream())
                using (var reader = new StreamReader(stream))
                {
                    var header = await reader.ReadLineAsync();

                    for (string data = null; data != string.Empty; data = await reader.ReadLineAsync())
                        ;

                    var request = RequestParser.Parse(header);

                    await _handler.HandleAsync(stream, request);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}