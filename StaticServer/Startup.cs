using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace StaticServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCompression(options =>
            {
                options.Providers.Clear();
                options.Providers.Add<GzipCompressionProvider>(); // GZIP compression only for test
                options.EnableForHttps = true;
            });

            services.Configure<GzipCompressionProviderOptions>(options =>
            {

                options.Level = System.IO.Compression.CompressionLevel.Fastest;
            });
        }

        public void Configure(IApplicationBuilder app, Microsoft.Extensions.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseResponseCompression(); // Compression
            app.UseDefaultFiles();        // Serving index.html by default
            app.UseStaticFiles();         // Static files support  
        }
    }
}
