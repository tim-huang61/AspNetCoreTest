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
    // IClassFixture 類似setup 
    // WebApplicationFactory : 建立內建web application
    public class Lab00 : TestBase
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public Lab00(WebApplicationFactory<Startup> factory) : base(factory)
        {
            _factory = factory;
        }

        // host建立記憶體
        [Fact]
        public async Task Test()
        {
            var httpClient = CreateHttpClient(config => { config.AddScoped<IHttpService, FakeHttpService>(); });
            var httpResponseMessage = await httpClient.GetAsync("api/Lab00/Index");
            var result = await httpResponseMessage.Content.ReadAsAsync<AuthResult>();
            result.IsAuth.Should().BeTrue();
        }
    }
}