namespace HttpServer
{
    internal static class RequestParser
    {
        public static Request Parse(string header)
        {

            if (string.IsNullOrWhiteSpace(header))
            {
                throw new ArgumentNullException(nameof(header));
            }

            var parsed = header.Split(' ');
            return new Request(parsed[1], GetMethod(parsed[0]));
        }

        private static HttpMethod GetMethod(string stringMethod)
        {
            switch (stringMethod)
            {
                case "GET":
                    return HttpMethod.Get;
                case "POST":
                    return HttpMethod.Post;
                case "PUT":
                    return HttpMethod.Put;
                case "DELETE":
                    return HttpMethod.Delete;
                default:
                    return HttpMethod.Get;
            }
        }
    }
}
