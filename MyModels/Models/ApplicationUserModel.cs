using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MyAspNetCoreApp.MyModels.Models
{
    public class ApplicationUserModel : IdentityUser
    {
        [Required]
        public string? Name { get; set; }
        public string? StringAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }

        [ForeignKey("CompanyId")]  // ✅ FIXED FK NAME
        public int? CompanyId { get; set; }

        [ValidateNever]
        public virtual CompanyModel Company { get; set; }  // ✅ FIXED NAVIGATION PROPERTY NAME

    }
}
