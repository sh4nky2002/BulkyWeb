using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyAspNetCoreApp.Models;

namespace MyAspNetCoreApp.MyModels.ViewModels
{
    public class ProductVM
    {
        public ProductModel ProductModel { get; set; }
        
        [ValidateNever]

        public IEnumerable<SelectListItem> ItemList { get; set; }
    }
}

