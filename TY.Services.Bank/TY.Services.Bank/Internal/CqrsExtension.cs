using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace TY.Services.Bank.Internal
{
    public static class CqrsExtension
    {
        public static void AddCommandQueryHandlers(this IServiceCollection services, Type handlerInterface)
        {
            var handlers = typeof(Startup).Assembly.GetTypes().Where(t =>
                t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface));

            foreach (var handler in handlers)
            {
                services.AddScoped(
                    handler.GetInterfaces()
                        .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface), handler);
            }
        }
    }
}