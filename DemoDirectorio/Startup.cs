using Directorio.Entities;
using Directorio.Repository;
using Directorio.Repository.Interfaces;
using Directorio.Services;
using Directorio.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace DemoDirectorio
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<IRepository<Usuario>, UsuarioRepository>();
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddMvc();

            //Enable Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1.0",
                    Title = "User Documentation API",
                    Description = "Documentation APIs to Use in DemoDirectory",
                    TermsOfService = "None",
                    Contact = new Contact()
                    {
                        Name = "Jorge Vargas Ipince",
                        Email = "jvargas.ipince@outlook.com",
                        Url = "http://jvargasi.com"
                    },                    
                });

                c.SwaggerDoc("v2", new Info
                {
                    Version = "v2.0",
                    Title = "User Documentation API 2.0",
                    Description = "Documentation APIs to Use in DemoDirectory  2.0",
                    TermsOfService = "None",
                    Contact = new Contact()
                    {
                        Name = "Jorge Vargas Ipince  2.0",
                        Email = "jvargas.ipince@outlook.com",
                        Url = "http://jvargasi.com"
                    },
                });


            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            //Enable Swagger & Swagger UI
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "User Documentation API");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "User Documentation API 2");
            });
            
        }
    }
}
