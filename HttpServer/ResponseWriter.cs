using System.Net;

namespace HttpServer
{
    internal static class ResponseWriter
    {
        internal static void WriteResponseStatus(HttpStatusCode statusCode, Stream stream)
        {
            using StreamWriter writer = new StreamWriter(stream, leaveOpen: true);
            writer.WriteLine(GetResponseText(statusCode));
            writer.WriteLine();
        }

        internal static async Task WriteResponseStatusAsync(HttpStatusCode statusCode, Stream stream)
        {
            using StreamWriter writer = new StreamWriter(stream, leaveOpen: true);
            await writer.WriteLineAsync(GetResponseText(statusCode));
            await writer.WriteLineAsync();
        }

        private static string GetResponseText(HttpStatusCode code)
        {
            return $"HTTP/1.1 {(int)code} {code}";
        }
    }
}
