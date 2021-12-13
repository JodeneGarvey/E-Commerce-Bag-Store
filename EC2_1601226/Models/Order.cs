using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EC2_1601226.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [StringLength(50)]
        [Required(ErrorMessage = "Please enter your first name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(50)]
        [Required(ErrorMessage = "Please enter your last name")]
        public string LastName { get; set; }

        [Display(Name = "Address")]
        [StringLength(100)]
        [Required(ErrorMessage = "Please enter your address")]
        public string Address { get; set; }

        [Display(Name = "City")]
        [StringLength(50)]
        [Required(ErrorMessage = "Please enter your City")]
        public string City { get; set; }

        [Display(Name = "Country")]
        [StringLength(50)]
        [Required(ErrorMessage = "Please enter your Country")]
        public string Country { get; set; }

        [BindNever]
        [ScaffoldColumn(false)]
        public decimal Total { get; set; }

        public DateTime OrderDate { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }

    }
}
