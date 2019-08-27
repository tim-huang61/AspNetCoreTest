using System.Net.Http;
using System.Threading.Tasks;
using AspNetCoreTest201908;
using AspNetCoreTest201908.Model;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace E2ETests
{
    public class Lab06 : TestBase
    {
        public Lab06(WebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Index1()
        {
            var httpClient = CreateHttpClient(config => { config.AddScoped<IHttpService, FakeHttpService>(); });
            var httpResponseMessage = await httpClient.GetAsync("api/Lab06/Index1");
            var result = await httpResponseMessage.Content.ReadAsAsync<AuthResult>();
            result.IsAuth.Should().BeTrue();
        }

        [Fact]
        public async Task Index1_1()
        {
            var httpClient = CreateHttpClient(config => { config.AddScoped<IHttpService, FakeFailedService>(); });
            var httpResponseMessage = await httpClient.GetAsync("api/Lab06/Index1");
            var result = await httpResponseMessage.Content.ReadAsAsync<AuthResult>();
            result.IsAuth.Should().BeFalse();
        }

        [Fact]
        public async Task HttpService_UnitTest()
        {
            var serviceCollection = new ServiceCollection()
                .AddHttpClient()
                .AddScoped<IHttpService, HttpService>();
            using (var serviceScope = serviceCollection.BuildServiceProvider().CreateScope())
            {
                var httpService = serviceScope.ServiceProvider.GetRequiredService<IHttpService>();
                var isAuthAsync = await httpService.IsAuthAsync();
                isAuthAsync.Should().BeTrue();
            }
        }
    }
}