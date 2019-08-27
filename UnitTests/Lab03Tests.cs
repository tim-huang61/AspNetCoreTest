using AspNetCoreTest201908.Api.Lab03_IHostEnvironment;
using AspNetCoreTest201908.Model;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace UnitTests
{
    public class Lab03Tests
    {
        [Fact]
        public void Test_1()
        {
            var hostingEnvironment = new HostingEnvironment();
            var lab03Controller = new Lab03Controller(hostingEnvironment);
            var okObjectResult = lab03Controller.Index1().As<OkObjectResult>();
            var result = okObjectResult.Value.As<EnvResult>();
            result.Env.Should().Be("Dev");
        }

        [Fact]
        public void Test_2()
        {
            var hostingEnvironment = new HostingEnvironment();
            hostingEnvironment.EnvironmentName = "Prod";
            var lab03Controller = new Lab03Controller(hostingEnvironment);
            var okObjectResult = lab03Controller.Index1().As<OkObjectResult>();
            var result = okObjectResult.Value.As<EnvResult>();
            result.Env.Should().Be("Prod");
        }
    }
}