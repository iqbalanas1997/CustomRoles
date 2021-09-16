

namespace CustomRoles.Data.ViewModels
{
    public class ManageBusinessUserRoleVM
    {
        public string userId { get; set; }
        public int roleId { get; set; }
        public int businessId { get; set; }
        public string roleName { get; set; }
        public bool selected { get; set; }
    }
}
