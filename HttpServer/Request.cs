namespace HttpServer
{
    public record Request(string url, HttpMethod httpMethod);
}