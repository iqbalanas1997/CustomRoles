
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace CustomRoles.Data
{
    public partial class Actions
    {
        [Key]
        public int Id { get; set; }

        public int PageId { get; set; }

        [Required(ErrorMessage = "Please Enter Action Name")]
        public string ActionName { get; set; }

        [ForeignKey("PageId")]
        public virtual Page Page { get; set; }
    }
}
