using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace CustomRoles.Data
{
    public partial class Business
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter Name")]
        public string Name { get; set; }
        public string TaxId { get; set; }

        public string Logo { get; set; }
        public string Website { get; set; }
        public bool IsDeleted { get; set; }
        [Required(ErrorMessage = "Please enter Email ID")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }



        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string SubDomain { get; set; }

        [Display(Name = "Is Vendor")]
        public bool IsVendor { get; set; }

        public int? VendorId { get; set; }

        public int StatusId { get; set; }

        public DateTime? DateAdded { get; set; }

      
       

    }
}
