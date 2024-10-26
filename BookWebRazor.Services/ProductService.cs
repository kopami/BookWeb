using BookWebRazor.BusinessObjects.Model;
using BookWebRazor.Repositories;
using BookWebRazor.Repositories.Interface;
using BookWebRazor.Services.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWebRazor.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService()
        {
            _productRepository = new ProductRepository();
        }

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public bool CreateProduct(Product product, List<IFormFile>? files = null, string? rootPath = null)
        {
            bool isSuccess = _productRepository.Add(product);
            if (isSuccess && files != null && rootPath != null)
            {
                HandleUploadFile(product, files, rootPath);
                _productRepository.Update(product);
                return true;
            }
            if (isSuccess)
            {
                return true;
            }
            return false;
        }

        public bool DeleteProduct(Product productToBeDeleted)
        {
            return _productRepository.Delete(productToBeDeleted);
        }

        public Product? GetProductById(int id, string? includeProperties = null)
        {
            return _productRepository.Get(p => p.Id == id, includeProperties: includeProperties);
        }

        public IEnumerable<Product> GetProducts(string? includeProperties = null)
        {
            return _productRepository.GetAll(includeProperties: includeProperties);
        }

        public bool UpdateProduct(Product product, List<IFormFile>? files = null, string? rootPath = null)
        {
            bool isSuccess = _productRepository.Update(product);
            if (isSuccess && files != null && rootPath != null)
            {
                HandleUploadFile(product, files, rootPath);
                _productRepository.Update(product);
                return true;
            }
            if (isSuccess)
            {
                return true;
            }
            return false;
        }

        private void HandleUploadFile(Product product, List<IFormFile> files, string wwwRootPath)
        {
            foreach (IFormFile file in files)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productPath = Path.Combine("images", "products", $"product-{product.Id}");
                string finalPath = Path.Combine(wwwRootPath, productPath);

                if (!Directory.Exists(finalPath))
                {
                    Directory.CreateDirectory(finalPath);
                }

                using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                ProductImage image = new()
                {
                    ImageUrl = $"{Path.DirectorySeparatorChar}{Path.Combine(productPath, fileName)}",
                    ProductId = product.Id,
                };

                if (product.ProductImages == null)
                {
                    product.ProductImages = new List<ProductImage>();
                }
                product.ProductImages.Add(image);
            }
        }
    }
}
