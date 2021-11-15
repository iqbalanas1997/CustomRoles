using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace CustomRoles.Data
{
    public partial class BusinessRoles
    {
        [Key]
        public int Id { get; set; }

        public int BusinessId { get; set; }
        [Required(ErrorMessage = "Please Enter Role Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        [ForeignKey("BusinessId")]
        public virtual Business Business { get; set; }

        public virtual List<BusinessRolePermissions> BusinessRolePermissions { get; set; }
    }
}
