using CustomRoles.Data;
using CustomRoles.Data.SPEntity;
using CustomRoles.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomRoles.Infrastructure
{
    public interface IBusinessUserRepo
    {
        Task<List<BusinessUserRoles>> CreateBusinessUserRoles(List<ManageBusinessUserRoleVM> businessUserRolesVM, List<BusinessUserRoles> SelectedRoles);
        void Delete(ApplicationUser applicationUser);

        Task<List<BusinessRoles>> GetAllBusinessRolesByBusinessId(int id);


        ApplicationUser GetById(string Id);
        Task<Result> Insert([Bind(new[] { "Id,UserName,Email,PhoneNumber,BusinessId,Password,ConfirmPassword" })] BusinessUserVM businessUserVM);

        void Save();
        Task<ApplicationUser> Update(string id, [Bind(new[] { "Id,UserName,Email,PhoneNumber,BusinessId" })] ApplicationUser applicationUser);
        Task<List<ApplicationUser>> UsersGetByBusinessId(int id);
        Task<List<BusinessUserRoles>> IsRoleSelected(string userId);

        Task<List<BusinessUserRoles>> DeleteBusinessUserRoles(List<ManageBusinessUserRoleVM> businessUserRolesVM, List<BusinessUserRoles> isSelect);
        Task<ApplicationUser> GetUserById(string Id);
        public int getBusinessId(string id);

    }
}