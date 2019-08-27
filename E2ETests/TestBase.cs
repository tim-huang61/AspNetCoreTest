using System;
using System.Net.Http;
using AspNetCoreTest201908;
using AspNetCoreTest201908.Entity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace E2ETests
{
    public class TestBase : IClassFixture<WebApplicationFactory<Startup>> 
    {
        private readonly WebApplicationFactory<Startup> _factory;
        protected WebApplicationFactory<Startup> AppWebHost;

        protected TestBase(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        protected HttpClient CreateHttpClient()
        {
            // 透過WithWebHostBuilder來建立需要對映的service
            AppWebHost = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(config=>
                {
                    config.AddDbContext<AppDbContext>(
//                        option => option.UseInMemoryDatabase("memory")
                        option => option.UseSqlite("DataSource=name")
                        );

                    var serviceProvider = config.BuildServiceProvider();
                    using (var serviceScope = serviceProvider.CreateScope())
                    {
                        var appDbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
                        appDbContext.Database.EnsureDeleted();
                        appDbContext.Database.EnsureCreated();
                        appDbContext.Database.ExecuteSqlCommand(
                            @"CREATE VIEW VProfile AS SELECT Name From Profile");
                    }
                });
            });
            
            return AppWebHost.CreateClient();
        }

        protected void DbOperator(Action<AppDbContext> action)
        {
            using (var serviceScope = AppWebHost.Server.Host.Services.CreateScope())
            {
                // GetRequiredService 如果沒有註冊會直接噴exception.
                var appDbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
                action.Invoke(appDbContext);
            }
        }
    }
}