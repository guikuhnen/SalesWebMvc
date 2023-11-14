using SalesWebMvc.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalesWebMvc.Models.ViewModels
{
    public class SellerViewModel
    {
        public Seller Seller { get; set; }
        public ICollection<Department> Departments { get; set; }

        [Display(Name = "Department Name")]
        public string SellerDepartmentName { get; set; }
    }
}
