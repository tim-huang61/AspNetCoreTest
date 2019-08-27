using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AspNetCoreTest201908;
using AspNetCoreTest201908.Entity;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace E2ETests
{
    public class Lab05 : TestBase
    {
        public Lab05(WebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Index()
        {
            var httpClient = CreateHttpClient();
            var httpResponseMessage = await httpClient.GetAsync("api/Lab05/Index1");
            var result = await httpResponseMessage.Content.ReadAsAsync<List<Profile>>();
            result.Count.Should().NotBe(0);
        }
    }
}