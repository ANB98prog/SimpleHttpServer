using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer
{
    public class StaticFileHandler : IHandler
    {
        public void Handle(Stream stream, Request request)
        {
            throw new NotImplementedException();
        }

        public Task HandleAsync(Stream stream, Request request)
        {
            throw new NotImplementedException();
        }
    }
}
