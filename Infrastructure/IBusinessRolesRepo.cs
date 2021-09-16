using CustomRoles.Data;
using CustomRoles.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomRoles.Infrastructure
{
    public interface IBusinessRolesRepo
    {
        bool businessRoleExists(int id);
        Task<Result> CreateRole([Bind(new[] { "Id,Name,Description,PageId,ActionId" })] BusinessRoles businessRole);
        Task<List<BusinessRolePermissions>> CreateRolesPermissions(List<RoleActionVM> roleActionVM);
        void DeleteRole(BusinessRoles businessRoles);
        Task<BusinessRoles> EditRole(BusinessRoles businessRoles);
        Task<List<BusinessRoles>> GetAll();
        Task<List<Actions>> GetAllActions();
        Business GetBusinessByUserId(string id);
        Task<List<Business>> GetBusinessList();
        BusinessRoles GetById(int Id);
        Task<List<BusinessRoles>> RolesGetByBusinessId(int id);
        void SaveRole();
    }
}