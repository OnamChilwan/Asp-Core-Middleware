namespace Example.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    using Example.Controllers;
    using Example.Models;
    using Example.Queries;

    using Microsoft.AspNet.Mvc;

    using Xunit;

    public class ProductControllerTests
    {
        [Fact]
        public async Task When_Successfully_Requesting_Products()
        {
            var subject = new ProductsController(new TestDoubleGetProductsQuery());
            var result = await subject.Get() as HttpOkObjectResult;
            var products = ((IEnumerable<Product>)result.Value).ToList();

            Assert.Equal(result.StatusCode, (int)HttpStatusCode.OK);

            Assert.Equal(products[0].Code, "ABC");
            Assert.Equal(products[0].Description, "Testing");
            Assert.Equal(products[0].Quantity, 1);

            Assert.Equal(products[1].Code, "XYZ");
            Assert.Equal(products[1].Description, "Testing");
            Assert.Equal(products[1].Quantity, 3);
        }

        private class TestDoubleGetProductsQuery : IGetProductsQuery
        {
            public Task<IEnumerable<Product>> Execute()
            {
                var products = new[]
                {
                    new Product { Code = "ABC", Description = "Testing", Quantity = 1 },
                    new Product { Code = "XYZ", Description = "Testing", Quantity = 3 }
                };

                return Task.FromResult((IEnumerable<Product>)products);
            }
        }
    }
}