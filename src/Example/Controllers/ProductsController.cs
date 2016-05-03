namespace Example.Controllers
{
    using System.Threading.Tasks;

    using Example.Queries;

    using Microsoft.AspNet.Mvc;

    [Route("api")]
    public class ProductsController : Controller
    {
        private readonly IGetProductsQuery getProductsQuery;

        public ProductsController(IGetProductsQuery getProductsQuery)
        {
            this.getProductsQuery = getProductsQuery;
        }

        [HttpGet("products")]
        public async Task<IActionResult> Get()
        {
            return this.Ok(await this.getProductsQuery.Execute());
        }
    }
}