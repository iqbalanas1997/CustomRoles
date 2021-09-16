
using System.ComponentModel.DataAnnotations;


namespace CustomRoles.Data.ViewModels
{
    public class PageActionVM
    {
        public int Id { get; set; }

        public string PageName { get; set; }

        [Required(ErrorMessage = "Please Enter Action Name")]
        public string ActionName { get; set; }
        public int PageId { get; set; }
    }
}
