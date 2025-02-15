using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MyAspNetCoreApp.Models;

namespace MyAspNetCoreApp.MyModels.Models{
public class ShoppingCartModel 
{
    [Key]
    public int Id{get; set;}

    public int ProductId{get; set;}
    [ForeignKey("ProductId")]
    [ValidateNever]
    public ProductModel Product{get;set;}

    [Range(1,1000,ErrorMessage ="please enter a value between 1 and 1000")]

    public int count{get; set;}

    public string ApplicationUserId{get; set;}
    [ForeignKey("ApplicationUserModelId")]
    [ValidateNever]
    public ApplicationUserModel ApplicationUser{get; set;}

    [NotMapped]
    public double Price{get; set;}
}

}