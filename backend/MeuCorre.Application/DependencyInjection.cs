using System.Reflection;
using MeuCorre.Application.UseCases.Contas.Queries;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace MeuCorre.Application
{
   
        public static class DependencyInjection
        {
            public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
            {
                services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ListarContasQueryHandler).Assembly));
                return services;

            }
        }
    

}
