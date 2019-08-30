using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http;
using TY.Services.Bank.Cqrs.Handlers;
using TY.Services.Bank.Http;
using TY.Services.Bank.Internal;
using TY.Services.Bank.Services;
using TY.Services.Bank.Services.Interfaces;

namespace TY.Services.Bank
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
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Formatting = Formatting.Indented;
                    options.SerializerSettings.TypeNameHandling = TypeNameHandling.Objects;
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });

            services.AddTransient<HttpClient>();
            services.AddTransient<IRestClient, RestClient>();
            services.AddSingleton<IPasswordHasher, PasswordHasherService>();


            #region Cqrs Services

            services.AddCommandQueryHandlers(typeof(ICommandHandler<,>));
            services.AddCommandQueryHandlers(typeof(IQueryHandler<,>));

            #endregion

            services.AddLogging();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
