using Microsoft.Extensions.DependencyInjection;

namespace ProductService.Grpc.Init
{
    public static class DataLoaderInstaller
    {
        public static IServiceCollection AddProductDemoInitializer(this IServiceCollection services)
        {
            services.AddScoped<DataLoader>();
            return services;
        }
    }
}