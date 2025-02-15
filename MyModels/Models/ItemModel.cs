using System;
using System.ComponentModel.DataAnnotations;
namespace MyAspNetCoreApp.Models;
public class ItemModel
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    

    [Range(0, 10000)]
    public decimal Price { get; set; }

    [Range(0, 100)]
    public decimal Discount { get; set; }

    public bool IsPublished { get; set; }

    public bool DisplayInHomepage { get; set; }
}
