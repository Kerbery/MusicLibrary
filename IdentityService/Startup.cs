// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityService.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityService
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var assembly = typeof(Program).Assembly.GetName().Name;

            // uncomment, if you want to add an MVC-based UI
            //services.AddControllersWithViews();

            var env = System.Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER");

            var connString = env == "true" ?  "DockerConnection": "DefaultConnection";

            var defaultConnString = Configuration.GetConnectionString(connString);

            //SeedData.EnsureSeedData(defaultConnString);

            services.AddDbContext<AspNetIdentityDbContext>(options =>
            {
                options.UseSqlServer(defaultConnString, opt => opt.MigrationsAssembly(assembly));
            });

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AspNetIdentityDbContext>();

            var builder = services.AddIdentityServer(options =>
            {
                // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                options.EmitStaticAudienceClaim = true;
            })
                .AddAspNetIdentity<IdentityUser>()
                .AddConfigurationStore((options) =>
                {
                    options.ConfigureDbContext = b =>
                    b.UseSqlServer(defaultConnString, opt => opt.MigrationsAssembly(assembly));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b =>
                    b.UseSqlServer(defaultConnString, opt => opt.MigrationsAssembly(assembly));

                })

                //.AddInMemoryIdentityResources(Config.IdentityResources)
                //.AddInMemoryApiScopes(Config.ApiScopes)
                //.AddInMemoryClients(Config.Clients)
                //.AddTestUsers(Config.TestUsers)

                .AddDeveloperSigningCredential();

            // not recommended for production - you need to store your key material somewhere secure
            builder.AddDeveloperSigningCredential();

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseIdentityServer();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
