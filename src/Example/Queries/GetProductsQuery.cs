namespace Example.Queries
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Example.Models;

    public interface IGetProductsQuery
    {
        Task<IEnumerable<Product>> Execute();
    }

    public class GetProductsQuery : IGetProductsQuery
    {
        public Task<IEnumerable<Product>> Execute()
        {
            return Task.FromResult(Enumerable.Empty<Product>());
        }
    }
}