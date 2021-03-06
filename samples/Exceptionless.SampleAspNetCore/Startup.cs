﻿using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Exceptionless.SampleAspNetCore {
    public class Startup {
        public Startup(IHostingEnvironment env) {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) {
            app.UseExceptionless(Configuration);
            //OR
            //app.UseExceptionless(new ExceptionlessClient(c => c.ReadFromConfiguration(Configuration)));
            //OR
            //app.UseExceptionless("API_KEY_HERE");
            //OR
            //loggerFactory.AddExceptionless("API_KEY_HERE");
            //OR
            //loggerFactory.AddExceptionless((c) => c.ReadFromConfiguration(Configuration));

            loggerFactory.AddExceptionless();
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}