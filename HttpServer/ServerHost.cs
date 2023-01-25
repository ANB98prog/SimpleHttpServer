using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer
{
    public class ServerHost
    {
        public async Task StartAsync()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 12125);
            listener.Start();

            Console.WriteLine("Server started at http://localhost:12125");

            while (true)
            {
                using (var client = await listener.AcceptTcpClientAsync())
                using (var stream = client.GetStream())
                using (var reader = new StreamReader(stream))
                using (var writer = new StreamWriter(stream))
                {
                    var header = await reader.ReadLineAsync();

                    for (string data = null; data != string.Empty; data = await reader.ReadLineAsync())
                        ;

                    var request = RequestParser.Parse(header);

                    await ResponseWriter.WriteResponseStatusAsync(stream, HttpStatusCode.OK);

                    await writer.WriteLineAsync("Hello from server!");
                }
            }

        }
    }
}
