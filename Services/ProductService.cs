using PaymentForm.Models;

namespace PaymentForm.Services
{
    public class ProductService
    {
        private static readonly List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Time", Description = "We wish you a merry X-mas", Price = 1 },
        new Product { Id = 2, Name = "To", Description = "We wish you a merry X-mas", Price = 2 },
        new Product { Id = 3, Name = "Celebrate!", Description = "We wish you a merry X-mas\nAnd a happy New Year!", Price = 7 }
    };

        public List<Product> GetAllProducts() => _products;

        public Product GetProductById(int id) => _products.FirstOrDefault(p => p.Id == id);
    }
}
