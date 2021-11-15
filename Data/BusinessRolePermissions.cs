
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CustomRoles.Data
{
    public class BusinessRolePermissions
    {
        [Key]
        public int Id { get; set; }
        public int RoleId { get; set; }
        //public int PageId { get; set; }
        public int ActionId { get; set; }

        public bool IsDeleted { get; set; }
        [ForeignKey("RoleId")]
        public virtual BusinessRoles BusinessRoles { get; set; }

        [ForeignKey("ActionId")]
        public virtual Actions Actions { get; set; }
    }
}
