using System.Net.Http;
using AspNetCoreTest201908;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace E2ETests
{
    public class TestBase : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        protected TestBase(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        public HttpClient CreateHttpClient()
        {
            return _factory.CreateClient();
        }
    }
}