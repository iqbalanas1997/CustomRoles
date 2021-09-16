using CustomRoles.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomRoles.Infrastructure
{
    public interface IUserControlRepo
    {
        Task<List<BusinessUserRoles>> GetAllBusinessUserRoles(string userId);
        Task<List<BusinessUserRoles>> GetAllPermissions(string UserId);
    }
}