using System.Collections.Generic;
using System.Configuration;
using System.Xml;
using AspNetCoreTest201908.Api.Lab02_IConfiguration;
using AspNetCoreTest201908.Model;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace UnitTests
{
    public class Lab02Tests
    {
        /// <summary>
        /// need to install Configuration Binder
        /// </summary>
        [Fact]
        public void Test_021_Index1()
        {
            var builder = new ConfigurationBuilder();
            var config = builder.AddInMemoryCollection(new Dictionary<string, string>
            {
                {"Server:Host", "127.0.0.1"}
            }).Build();
            var lab021Controller = new Lab021Controller(config);
            var result = lab021Controller.Index1().As<OkObjectResult>();
            var serverResult = result.Value.As<ServerResult>();
            serverResult.Host.Should().Be("127.0.0.1");
        }

        [Fact]
        public void Test_021_Index2()
        {
            var builder = new ConfigurationBuilder();
            var config = builder.AddInMemoryCollection(new Dictionary<string, string>
            {
                {"Server:Host", "127.0.0.1"}
            }).Build();
            var lab021Controller = new Lab021Controller(config);
            var result = lab021Controller.Index2().As<OkObjectResult>();
            var serverResult = result.Value.As<ServerResult>();
            serverResult.Host.Should().Be("127.0.0.1");
        }

        [Fact]
        public void Test_022_Index1()
        {
            var option = new OptionsWrapper<ServerHost>(new ServerHost
            {
                Host = "127.0.0.1"
            });
            var lab022Controller = new Lab022Controller(option);
            var result = lab022Controller.Index1().As<OkObjectResult>();
            var serverResult = result.Value.As<ServerResult>();
            serverResult.Host.Should().Be("127.0.0.1");
        }
    }
}