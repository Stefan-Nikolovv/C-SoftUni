
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace BookLibrary.Web.Infrastructure.Extentions
{
    public static class WEbApplicationBuilderExtentions
    {
        public static void AddApplicationServices(this IServiceCollection services, Type serviceType)
        {
            Assembly? serviceAssembly = Assembly.GetAssembly(serviceType);

            if (serviceAssembly == null)
            {
                throw new InvalidOperationException("Not valid assembly");
            }

            Type[] serviceTypes = serviceAssembly
                .GetTypes()
                .Where(t => t.Name.EndsWith("Service") && !t.IsInterface)
                .ToArray();

            foreach (Type implementationType in serviceTypes)
            {
                Type? interfaceType = implementationType.GetInterface($"I{implementationType.Name}");
                if (interfaceType == null)
                {
                    throw new InvalidOperationException("Not valid type");
                }

                services.AddScoped(interfaceType, implementationType);
            }

        }
    }
}
