using CustomRoles.Data;
using CustomRoles.Data.ViewModels;
using CustomRoles.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomRoles.Repository
{
    public interface IBusinessRolesRepo
    {
        bool businessRoleExists(int id);
        Task<Result> CreateRole([Bind(new[] { "Id,Name,Description,PageId,ActionId" })] BusinessRoles businessRole);
        Task<List<BusinessRolePermissions>> CreateRolesPermissions(List<RoleActionVM> roleActionVM, List<BusinessRolePermissions> isSelectedPage, int roleId);
        void DeleteRole(BusinessRoles businessRoles);
        Task<List<BusinessRolePermissions>> DeleteRolesPermissions(List<RoleActionVM> roleActionVM, List<BusinessRolePermissions> isSelectedPage);
        Task<BusinessRoles> EditRole(BusinessRoles businessRoles);
        Task<List<Actions>> GetAllActions();
        BusinessRoles GetById(int Id);
        Task<List<BusinessRolePermissions>> IsPageSelected();
        Task<List<BusinessRoles>> RolesGetByBusinessId(int id);
        void SaveRole();
        Task<ApplicationUser> GetUserById(string Id);
    }
}