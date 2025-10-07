using AuthSystem.Infrastructure.Context;
using AuthSystem.Infrastructure.Repositories.Implementation;
using Kwlc.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthSystem.Infrastructure.ServiceExtension
{
    public static class ServiceExtention
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IGenericQueryRepository<>), typeof(GenericQueryRepository<>));
            services.AddScoped(typeof(IGenericCommandRepository<>), typeof(GenericCommandRepository<>));
        }
    }
}
