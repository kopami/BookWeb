using BookWebRazor.BusinessObjects.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWebRazor.Services.Interface
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts(string? includeProperties = null);
        Product? GetProductById(int id, string? includeProperties = null);
        bool CreateProduct(Product product, List<IFormFile>? files = null, string? rootPath = null);
        bool UpdateProduct(Product product, List<IFormFile>? files = null, string? rootPath = null);
        bool DeleteProduct(Product productToBeDeleted);
    }
}
