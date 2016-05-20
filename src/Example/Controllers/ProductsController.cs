namespace Example.Controllers
{
    using System.Threading.Tasks;

    using Example.Queries;

    using Microsoft.AspNetCore.Mvc;

    [Route("api")]
    public class ProductsController
    {
        private readonly IGetProductsQuery getProductsQuery;

        public ProductsController(IGetProductsQuery getProductsQuery)
        {
            this.getProductsQuery = getProductsQuery;
        }

        [HttpGet("products")]
        public async Task<IActionResult> Get()
        {
            var products = await this.getProductsQuery.Execute();

            return new OkObjectResult(products);
        }
    }
}