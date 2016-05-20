namespace Example.Tests.Middleware
{
    using System;
    using System.Net;
    using System.Threading.Tasks;

    using Example.Middleware;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;

    using Xunit;

    public class GlobalExceptionMiddlewareTests
    {
        [Fact]
        public async Task When_An_Exception_Is_Raised_Then_Handle_And_Return_Server_Error()
        {
            using (var server = TestHelper.CreateServer(
                app =>
                {
                    app.UseMiddleware<GlobalExceptionMiddleware>();
                    app.UseMiddleware<FakeMiddlware>();
                }))
            {
                var result = await server.CreateRequest("/").GetAsync();

                Assert.Equal(result.StatusCode, HttpStatusCode.InternalServerError);
            }
        }

        private class FakeMiddlware
        {
            public FakeMiddlware(RequestDelegate next)
            {
            }

            public async Task Invoke(HttpContext context)
            {
                throw new Exception();
            }
        }
    }
}