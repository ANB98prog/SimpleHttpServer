namespace HttpServer.Handlers
{
    public class StaticFileHandler : IHandler
    {
        private string _staticFilesPath;

        public StaticFileHandler(string staticFilesPath)
        {
            _staticFilesPath = staticFilesPath;
        }
        public void Handle(Stream stream, Request request)
        {
            Console.WriteLine($"Try to get file: '{request.url}'");

            var fullPath = Path.Combine(_staticFilesPath,
                                    string.Join(Path.DirectorySeparatorChar,
                                        request.url.Split('/', StringSplitOptions.RemoveEmptyEntries)));

            if (!File.Exists(fullPath))
            {
                Console.WriteLine("File not found!");
                ResponseWriter.WriteResponseStatus(System.Net.HttpStatusCode.NotFound, stream);
                using var writer = new StreamWriter(stream);
                writer.WriteLine($"File {Path.GetFileName(fullPath)} is not found!");
            }
            else
            {
                Console.WriteLine("File successfully got.");
                ResponseWriter.WriteResponseStatus(System.Net.HttpStatusCode.OK, stream);
                using (var fileStream = File.OpenRead(fullPath))
                {
                    fileStream.CopyTo(stream);
                }
            }
        }

        public async Task HandleAsync(Stream stream, Request request)
        {
            Console.WriteLine($"Try to get file: '{request.url}'");

            var fullPath = Path.Combine(_staticFilesPath,
                                    string.Join(Path.DirectorySeparatorChar,
                                        request.url.Split('/', StringSplitOptions.RemoveEmptyEntries)));

            if (!File.Exists(fullPath))
            {
                Console.WriteLine("File not found!");
                await ResponseWriter.WriteResponseStatusAsync(System.Net.HttpStatusCode.NotFound, stream);
                using var writer = new StreamWriter(stream);
                await writer.WriteLineAsync($"File {Path.GetFileName(fullPath)} is not found!");
            }
            else
            {
                Console.WriteLine("File successfully got.");
                await ResponseWriter.WriteResponseStatusAsync(System.Net.HttpStatusCode.OK, stream);
                using (var fileStream = File.OpenRead(fullPath))
                {
                    await fileStream.CopyToAsync(stream);
                }
            }
        }
    }
}
