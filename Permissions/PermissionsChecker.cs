using CustomRoles.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Procurement.Extensions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CustomRoles.Sessions;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CustomRoles.Permissions
{
    public class PermissionsChecker : PageModel
    {
        private readonly CustomRolesDbContext _context;
        public PermissionsChecker(CustomRolesDbContext context)
        {
            _context = context;
        }

        public PermissionsChecker()
        {
        }
        public async Task<String> AccessToUser(string loggedInUserId, HttpContext context)
        {
            List<BusinessUserRoles> businessUserRoles =  _context.BusinessUserRole
       .Include(bur => bur.BusinessRoles)
       .ThenInclude(br => br.BusinessRolePermissions)
       .ThenInclude(brp => brp.Actions)
       .ThenInclude(a => a.Page)
       .Where(bur => bur.UserId == loggedInUserId).ToList();

            var pagePermissions = new List<String>();

            for (int i = 0; i < businessUserRoles.Count; i++)
            {
                pagePermissions.Add(businessUserRoles[i].BusinessRoles.BusinessRolePermissions[i].Actions.Page.Name + businessUserRoles[i].BusinessRoles.BusinessRolePermissions[i].Actions.ActionName);
            }
            UserSession sess = new UserSession();
            sess.PagePermissions = pagePermissions;
            context.Session.SetObjectAsJson("userObject", sess);
            return null;
        }

       
}
}
