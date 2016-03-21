namespace Example.Middleware
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Builder;
    using Microsoft.AspNet.Http;

    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                Debug.WriteLine("In error handling middleware..");
                await this.next.Invoke(context);
                Debug.WriteLine("Out of error handling middleware..");
            }
            catch (Exception exception)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                Debug.WriteLine(exception.Message);
            }
        }
    }
}