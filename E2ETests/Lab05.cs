using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using AspNetCoreTest201908;
using AspNetCoreTest201908.Entity;
using AspNetCoreTest201908.Model;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
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
            // 不能透過Services取得,須透過create scope
            var profiles = new List<Profile>
            {
                new Profile
                {
                    Id = Guid.NewGuid(),
                    Name = "123"
                }
            };
            DbOperator(appDbContext =>
            {
                appDbContext.Profile.AddRange(profiles);
                appDbContext.SaveChanges();
            });

            var httpResponseMessage = await httpClient.GetAsync("api/Lab05/Index1");
            var result = await httpResponseMessage.Content.ReadAsAsync<List<Profile>>();
            result.Should().BeEquivalentTo(profiles);
        }

        [Fact]
        public async Task Index2()
        {
            var profileDto = new ProfileDto {Name = "Tim"};
            var httpClient = CreateHttpClient();
            await httpClient.PostAsJsonAsync("api/Lab05/Index2", profileDto);
            DbOperator(context =>
            {
                var profile = context.Profile.First();
                profile.Id.Should().NotBeEmpty();
                profile.Name.Should().Be("Tim");
            });
        }
    }
}