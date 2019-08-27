using System.Threading.Tasks;
using AspNetCoreTest201908.Model;

namespace E2ETests
{
    public class FakeFailedService : IHttpService
    {
        public Task<bool> IsAuthAsync()
        {
            return Task.FromResult(false);
        }
    }
}