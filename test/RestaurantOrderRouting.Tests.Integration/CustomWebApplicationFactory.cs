using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using RestaurantOrderRouting.Application;

namespace RestaurantOrderRouting.Tests.Integration
{
    /// <summary>
    /// Custom factory for bootstrapping an application in memory for functional end to end tests.
    /// </summary>
    public sealed class CustomWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(collection => { });
        }
    }
}
