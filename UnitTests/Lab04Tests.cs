using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreTest201908.Api.Lab03_IHostEnvironment;
using AspNetCoreTest201908.Api.Lab04_HttpContext;
using AspNetCoreTest201908.Model;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Xunit;

namespace UnitTests
{
    public class Lab04Tests
    {
        [Fact]
        public void Test_IsAuth_Should_Be_True()
        {
            var accessor = new HttpContextAccessor();
            accessor.HttpContext = new DefaultHttpContext();
            var user = new ClaimsPrincipal();
            user.AddIdentity(new ClaimsIdentity(null, "Bearer"));
            accessor.HttpContext.User = user;
            var lab03Controller = new Lab04Controller(accessor);
            var okObjectResult = lab03Controller.Index1().As<OkObjectResult>();
            var result = okObjectResult.Value.As<AuthResult>();
            result.IsAuth.Should().BeTrue();
        }

        [Fact]
        public void Test_IsAuth_Should_Be_False()
        {
            var accessor = new HttpContextAccessor();
            accessor.HttpContext = new DefaultHttpContext();
            accessor.HttpContext.User = new ClaimsPrincipal();
            accessor.HttpContext.User.AddIdentity(new ClaimsIdentity());
            var lab03Controller = new Lab04Controller(accessor);
            var okObjectResult = lab03Controller.Index1().As<OkObjectResult>();
            var result = okObjectResult.Value.As<AuthResult>();
            result.IsAuth.Should().BeFalse();
        }

        /// <summary>
        /// Microsoft.Extensions.Identity.Core
        /// </summary>
        [Fact]
        public void Test_Index2()
        {
            var accessor = new HttpContextAccessor();
            accessor.HttpContext = new DefaultHttpContext();
            accessor.HttpContext.User = new ClaimsPrincipal();
            var claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, "test mail"));
            claimsIdentity.AddClaim(new Claim("MyType", "test type"));
            accessor.HttpContext.User.AddIdentity(claimsIdentity);
            var lab03Controller = new Lab04Controller(accessor);
            var okObjectResult = lab03Controller.Index2().As<OkObjectResult>();
            var result = okObjectResult.Value.As<AuthClaim>();
            result.Email.Should().Be("test mail");
            result.MyType.Should().Be("test type");
        }

        [Fact]
        public void Test_Index3()
        {
            var accessor = new HttpContextAccessor();
            accessor.HttpContext = new DefaultHttpContext();
            accessor.HttpContext.Session = new TestSession();
            var lab03Controller = new Lab04Controller(accessor);
            var okObjectResult = lab03Controller.Index3().As<OkObjectResult>();
            var result = okObjectResult.Value.As<AuthUser>();
            result.User.Should().Be("tim");
        }

        [Fact]
        public void Test_Index4()
        {
            var accessor = new HttpContextAccessor();
            accessor.HttpContext = new DefaultHttpContext();
            accessor.HttpContext.Request.Cookies = new RequestCookieCollection(new Dictionary<string, string>
            {
                {"user", "tim"}
            });
            var lab03Controller = new Lab04Controller(accessor);
            var okObjectResult = lab03Controller.Index4().As<OkObjectResult>();
            var result = okObjectResult.Value.As<AuthUser>();
            result.User.Should().Be("tim");
        }
    }

    public class TestSession : ISession
    {
        public Task LoadAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public Task CommitAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public bool TryGetValue(string key, out byte[] value)
        {
            value = Encoding.UTF8.GetBytes("tim");

            return true;
        }

        public void Set(string key, byte[] value)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new System.NotImplementedException();
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public bool                IsAvailable { get; }
        public string              Id          { get; }
        public IEnumerable<string> Keys        { get; }
    }
}