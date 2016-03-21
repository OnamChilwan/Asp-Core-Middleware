namespace Example.Tests.Middleware
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Example.Middleware;

    using Microsoft.AspNet.Builder;
    using Microsoft.AspNet.TestHost;

    using Xunit;

    public class StandardHeaderMiddlewareTests
    {
        [Fact]
        public async Task When_Performing_A_Request_Then_Standard_Headers_Are_Returned()
        {
            HttpResponseMessage result;

            using (var server = TestServer.Create((app) => { app.UseMiddleware<StandardHeaderMiddleware>(); }))
            {
                result = await server.CreateRequest("/").GetAsync();
            }

            Guid value;
            Assert.NotEmpty(result.Headers.Where(x => x.Key == "x-response-time"));
            Assert.NotEmpty(result.Headers.Where(x => x.Key == "x-corr-id"));
            Assert.True(Guid.TryParse(result.Headers.First(x => x.Key == "x-corr-id").Value.First(), out value));
            Assert.Empty(result.Headers.Where(x => x.Key == "Server"));
        }
    }
}