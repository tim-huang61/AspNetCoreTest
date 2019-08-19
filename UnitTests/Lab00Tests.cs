using AspNetCoreTest201908.Api.Lab00;
using AspNetCoreTest201908.Model;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace UnitTests
{
    public class Lab00Tests
    {
        [Fact]
        public void Test_Index_Lab00Controller()
        {
            var lab00Controller = new Lab00Controller();
            var actionResult = lab00Controller.Index().As<OkObjectResult>();
            actionResult.Value.As<AuthResult>().IsAuth.Should().BeTrue();
        }
    }
}