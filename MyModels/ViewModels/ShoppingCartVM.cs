using MyAspNetCoreApp.MyModels.Models;
namespace MyAspNetCoreApp.MyModels.ViewModels{
    public class ShoppingCartVM{
        public IEnumerable<ShoppingCartModel> ShoppingCartList {get;set;}

        public OrderHeaderModel OrderHeader{get; set;}

    }
}