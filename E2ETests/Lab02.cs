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
    public class Lab02 : TestBase
    {
        public Lab02(WebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Index22()
        {
            var httpClient = CreateHttpClient();
            var httpResponseMessage = await httpClient.GetAsync("api/Lab022/Index1");
            var result = await httpResponseMessage.Content.ReadAsAsync<ServerHost>();
            result.Host.Should().Be("127.0.0.1");
        }
    }
}