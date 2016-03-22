namespace Example.Middleware
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Builder;
    using Microsoft.AspNet.Http;

    public class StandardHeaderMiddleware
    {
        private const string CorrelationId = "x-corr-id";
        private readonly RequestDelegate next;
        private readonly Stopwatch stopwatch;

        public StandardHeaderMiddleware(RequestDelegate next)
        {
            this.next = next;
            this.stopwatch = new Stopwatch();
        }

        public async Task Invoke(HttpContext context)
        {
            this.stopwatch.Start();

            if (!context.Request.Headers.ContainsKey(CorrelationId))
            {
                context.Request.Headers.Add(CorrelationId, Guid.NewGuid().ToString("D"));
            }

            context.Response.OnStarting(this.OnStartCallback, context);

            Debug.WriteLine("In header middleware..");
            await this.next.Invoke(context);
            Debug.WriteLine("Out of header middleware..");
        }

        private Task OnStartCallback(object state)
        {
            this.stopwatch.Stop();

            var context = state as HttpContext;

            context?.Response.Headers.Add("x-response-time", this.stopwatch.ElapsedMilliseconds.ToString("D"));
            context?.Response.Headers.Add(CorrelationId, context?.Request.Headers[CorrelationId].ToString());
            context?.Response.Headers.Remove("Server");

            return Task.FromResult(0);
        }
    }
}