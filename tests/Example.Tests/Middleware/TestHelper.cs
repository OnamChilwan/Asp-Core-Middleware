namespace Example.Tests.Middleware
{
    using System;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;

    public static class TestHelper
    {
        public static TestServer CreateServer(Action<IApplicationBuilder> app)
        {
            var builder = new WebHostBuilder()
                .Configure(app);

            return new TestServer(builder);
        }
    }
}