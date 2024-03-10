

namespace HouseRentingSystem.Web.Infrastructure;

using HouseRentingSystem.Data.Models;
using HouseRentingSystem.Web.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

    public static class WebApplicationBuilderExtentions
    {
        public static void AddApplicationServices(this IServiceCollection services, Type serviceType)
        {
            Assembly? serviceAssembly = Assembly.GetAssembly(serviceType);

            if(serviceAssembly == null)
            {
                throw new InvalidOperationException("Not valid assembly");
            }

            Type[] serviceTypes = serviceAssembly
                .GetTypes()
                .Where(t => t.Name.EndsWith("Service") && !t.IsInterface)
                .ToArray();

            foreach(Type implementationType in serviceTypes)
            {
                Type? interfaceType = implementationType.GetInterface($"I{implementationType.Name}");
                if(interfaceType == null)
                {
                    throw new InvalidOperationException("Not valid type");
                }

                services.AddScoped(interfaceType, implementationType);
            }
            
        }
        public static IApplicationBuilder SeedAdministrator(this IApplicationBuilder App, string email)
        {
            using IServiceScope scopedServices = App.ApplicationServices.CreateScope();
                IServiceProvider serviceProvide = scopedServices.ServiceProvider;
       
           UserManager<User> userManager =
            serviceProvide.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole<Guid>> roleManger = 
            serviceProvide.GetRequiredService<RoleManager<IdentityRole<Guid>>>();


             Task.Run(async () =>
            {
                if(await roleManger.RoleExistsAsync("Administrator")) 
                {
                    return;
                }

                IdentityRole<Guid> role = new IdentityRole<Guid>("Administrator");

                await roleManger.CreateAsync(role);

                User AdminUser = await userManager.FindByEmailAsync(email);
                await userManager.AddToRoleAsync(AdminUser, "Administrator");
            })
            .GetAwaiter()
            .GetResult();

        return App;
        }
        public static IApplicationBuilder EnableOnlineUsersCheck(this IApplicationBuilder App)
        {
            return App.UseMiddleware<OnlineUsersMiddleware>();
        }
    }

