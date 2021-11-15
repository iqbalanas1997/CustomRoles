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

            var Roles = new List<string>();
            var RolesId = new List<int>();
            var pagePermissions = new List<String>();
            int BusinessId = 0;
           
            for (int i = 0; i < businessUserRoles.Count; i++)
            {
                for (int j = 0; j < businessUserRoles[i].BusinessRoles.BusinessRolePermissions.Count; j++)
                {
                    pagePermissions.Add(businessUserRoles[i].BusinessRoles.BusinessRolePermissions[j].Actions.Page.Name + businessUserRoles[i].BusinessRoles.BusinessRolePermissions[j].Actions.ActionName);
                }

                Roles.Add(businessUserRoles[i].BusinessRoles.Name);
                RolesId.Add(businessUserRoles[i].BusinessRoles.Id);
                BusinessId = businessUserRoles[i].BusinessRoles.BusinessId;
            };


            UserSession sess = new UserSession();
            sess.PagePermissions = pagePermissions;
            sess.BusinessUserRoles = Roles;
        
            sess.BusinessUserRolesID = RolesId;
            if (BusinessId == 0)
            {
              var user =  _context.ApplicationUser.Where(x => x.Id == loggedInUserId).FirstOrDefault();
                sess.BusinessId = (int)user.BusinessId;
            }
            else
            {
                sess.BusinessId = BusinessId;
            }
             
            context.Session.SetObjectAsJson("userObject", sess);
            return null;
        }

       
}
}
