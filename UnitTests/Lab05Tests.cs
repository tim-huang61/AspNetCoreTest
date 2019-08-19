using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTest201908.Api.Lab05_DbContext;
using AspNetCoreTest201908.Entity;
using AspNetCoreTest201908.Model;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Xunit;

namespace UnitTests
{
    public class Lab05Tests
    {
        [Fact]
        public async Task Test_Index1()
        {
            var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>();
//            var option = dbContextOptions.UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var option = dbContextOptions.UseSqlite("Data Source=:memory:").Options;
            var appDbContext = new AppDbContext(option);
            appDbContext.Database.OpenConnection();
            appDbContext.Database.EnsureCreated();
            var expected = new List<Profile>()
            {
                new Profile {Id = Guid.NewGuid(), Name = "Tim"}
            };
            appDbContext.Profile.AddRange(expected);
            appDbContext.SaveChanges();
            var lab05Controller = new Lab05Controller(appDbContext);
            var result = (await lab05Controller.Index1()).As<OkObjectResult>();
            result.Value.As<IEnumerable<Profile>>().Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Test_Index2()
        {
            var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>();
            var option = dbContextOptions.UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var appDbContext = new AppDbContext(option);   
            var lab05Controller = new Lab05Controller(appDbContext);
            lab05Controller.Index2(new ProfileDto {Name = "Tim"}).As<OkObjectResult>();

            appDbContext.Profile.First().Name.Should().Be("Tim");
        }
    }
}