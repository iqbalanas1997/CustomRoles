
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace CustomRoles.Data
{
    public partial class Page
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter Path of page")]
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}
