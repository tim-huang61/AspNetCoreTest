using AspNetCoreTest201908.Api.Lab01_ILogger;
using AspNetCoreTest201908.Model;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace UnitTests
{
    public class Lab01Tests
    {
        [Fact]
        public void Test_Index_Lab01Controller()
        {
            var logger = new NullLogger<Lab01Controller>();
            var lab00Controller = new Lab01Controller(logger);
            var actionResult = lab00Controller.Index1().As<OkObjectResult>();
            actionResult.Value.As<AuthResult>().IsAuth.Should().BeTrue();
        } 
    }
}