﻿using BookWebRazor.BusinessObjects.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWebRazor.Services.Interface
{
    public interface IProductImageService
    {
        int Remove(int id, string webRootPath);
        IEnumerable<ProductImage> GetProductImages(int productId);
        IEnumerable<ProductImage> GetAll();
    }
}
