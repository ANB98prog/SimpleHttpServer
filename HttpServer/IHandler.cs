namespace HttpServer
{
    public interface IHandler
    {
        public void Handle(Stream stream, Request request);
        public Task HandleAsync(Stream stream, Request request);
    }
}
