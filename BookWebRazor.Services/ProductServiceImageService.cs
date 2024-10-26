using BookWebRazor.BusinessObjects.Model;
using BookWebRazor.Repositories.Interface;
using BookWebRazor.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BookWebRazor.Services
{
    public class ProductImageService : IProductImageService
    {
        private readonly IProductImageRepository _productImageRepository;

        public ProductImageService(IProductImageRepository productImageRepository)
        {
            _productImageRepository = productImageRepository;
        }

        public IEnumerable<ProductImage> GetAll() => _productImageRepository.GetAll();

        public IEnumerable<ProductImage> GetProductImages(int productId)
        {
            return _productImageRepository.GetAll(image => image.ProductId == productId);
        }

        public int Remove(int id, string webRootPath)
        {
            ProductImage? imageToBeDeleted = _productImageRepository.Get(image => image.Id == id);
            if (imageToBeDeleted != null)
            {
                var productId = imageToBeDeleted.ProductId;
                if (!string.IsNullOrEmpty(imageToBeDeleted.ImageUrl))
                {
                    //delete the old image
                    var oldImagePath = Path.Combine(
                        path1: webRootPath,
                        path2: imageToBeDeleted.ImageUrl.TrimStart(Path.DirectorySeparatorChar));

                    if (File.Exists(oldImagePath))
                    {
                        File.Delete(oldImagePath);
                    }
                }
                _productImageRepository.Delete(imageToBeDeleted);

                return imageToBeDeleted.ProductId;
            }
            return 0;
        }
    }
}
