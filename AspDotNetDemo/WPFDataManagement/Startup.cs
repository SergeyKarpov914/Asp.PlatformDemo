﻿
using Microsoft.Extensions.DependencyInjection;
using BlazorWebView;
using BlazorWasmDashboard.Shared;
using System.Net.Http;

namespace BlazorWebViewTutorial.WpfApp
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<HttpClient>();
        }

        /// <summary>
        /// Configure the app.
        /// </summary>
        /// <param name="app">The application builder for apps.</param>
        public void Configure(ApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}