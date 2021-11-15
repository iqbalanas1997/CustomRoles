

using CustomRoles.Data;
using System.Collections.Generic;

namespace CustomRoles.Sessions
{
    public class UserSession
    {
        public List<string> PagePermissions { get; set; }
        public List<string> BusinessUserRoles { get; set; }
        public List<int> BusinessUserRolesID { get; set; }
        public int BusinessId { get; set; }

    }
}
