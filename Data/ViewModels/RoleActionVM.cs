

namespace CustomRoles.Data.ViewModels
{
    public class RoleActionVM
    {
        public int Id { get; set; }

        public string PageName { get; set; }


        public string ActionName { get; set; }
        public int PageId { get; set; }

        public bool Selected { get; set; }

        public int RoleId { get; set; }
    }
}
