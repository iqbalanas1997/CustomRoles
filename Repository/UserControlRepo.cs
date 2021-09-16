using CustomRoles.Data;
using CustomRoles.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomRoles.Repository
{
    public class UserControlRepo : IUserControlRepo
    {
        private readonly CustomRolesDbContext _context;

        public UserControlRepo(CustomRolesDbContext context)
        {
            _context = context;
        }
        public async Task<List<BusinessUserRoles>> GetAllBusinessUserRoles(string userId)
        {
            var a = await _context.BusinessUserRole.Include(a => a.BusinessRoles).Where(s => s.UserId == userId).ToListAsync();
            return a;
        }
        public async Task<List<BusinessUserRoles>> GetAllPermissions(string UserId)
        {
            List<BusinessUserRoles> businessUserRoles = await _context.BusinessUserRole
                .Include(bur => bur.BusinessRoles)
                .ThenInclude(br => br.BusinessRolePermissions)
                .ThenInclude(brp => brp.Actions)
                .ThenInclude(a => a.Page)
                .Where(bur => bur.UserId == UserId).ToListAsync();


            return businessUserRoles;
        }

  



    }
}
