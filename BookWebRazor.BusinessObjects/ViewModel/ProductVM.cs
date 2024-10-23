using BookWebRazor.BusinessObjects.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace BookWebRazor.BusinessObjects.ViewModel
{
    public class ProductVM
    {
        public Product Product { get; set; } = new();

        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; } = new List<SelectListItem>();
    }
}
