namespace Example
{
    using Example.Middleware;
    using Example.Queries;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvcCore()
                .AddJsonFormatters();

            services.AddTransient<IGetProductsQuery, GetProductsQuery>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionMiddleware>();
            app.UseMiddleware<StandardHeaderMiddleware>();
            app.UseMvc();
        }
    }
}