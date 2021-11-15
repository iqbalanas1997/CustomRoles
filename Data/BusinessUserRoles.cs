
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace CustomRoles.Data
{
    public partial class BusinessUserRoles
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int RoleId { get; set; }
        public bool IsDeleted { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("RoleId")]
        public virtual BusinessRoles BusinessRoles { get; set; }

    }
}
