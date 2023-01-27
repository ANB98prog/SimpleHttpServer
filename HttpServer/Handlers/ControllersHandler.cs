using Newtonsoft.Json;
using System.Reflection;

namespace HttpServer.Handlers
{
    public class ControllersHandler : IHandler
    {
        private readonly Dictionary<string, Func<object>> _routes;

        public ControllersHandler(Assembly controllersAssembly)
        {
            _routes = controllersAssembly.GetTypes()
             .Where(x => typeof(IController).IsAssignableFrom(x))
                 .SelectMany(Controller => Controller.GetMethods()
                     .Select(Method => new { Controller, Method }))
                 .ToDictionary(
                     key => GetPath(key.Controller, key.Method),
                     value => GetEndpointMethod(value.Controller, value.Method)
                );
        }

        private string GetPath(Type controller, MethodInfo method)
        {
            string name = controller.Name;
            if (name.EndsWith("controller", StringComparison.InvariantCultureIgnoreCase))
                name = name.Substring(0, name.Length - "controller".Length);
            if (method.Name.Equals("index", StringComparison.InvariantCultureIgnoreCase))
                return "/" + name;
            return $"/{name}/{method.Name}";
        }

        private Func<object> GetEndpointMethod(Type controller, MethodInfo method) 
        {
            return () => method.Invoke(Activator.CreateInstance(controller), Array.Empty<object>());
        }

        public void Handle(Stream stream, Request request)
        {
            throw new NotImplementedException();
        }

        public async Task HandleAsync(Stream stream, Request request)
        {
            if (!_routes.TryGetValue(request.url, out var func))
            {
                await ResponseWriter.WriteResponseStatusAsync(System.Net.HttpStatusCode.NotFound, stream);
            }
            else
            {
                await ResponseWriter.WriteResponseStatusAsync(System.Net.HttpStatusCode.OK, stream);
                await WriteControllerResponseAsync(func(), stream);
            }
        }

        private async Task WriteControllerResponseAsync(object response, Stream stream)
        {
            if (response is string str)
            {
                using var writer = new StreamWriter(stream);
                await writer.WriteLineAsync(str);
            }
            else if(response is byte[] buffer)
            {
                await stream.WriteAsync(buffer, 0, buffer.Length);
            }
            else if(response is Task task)
            {
                await WriteControllerResponseAsync(task.GetType().GetProperty("Result").GetValue(task), stream);
            }
            else
            {
                await WriteControllerResponseAsync(JsonConvert.SerializeObject(response), stream);
            }
        }
    }
}
