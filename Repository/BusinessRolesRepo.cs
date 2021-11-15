using CustomRoles.Data;
using CustomRoles.Data.ViewModels;
using CustomRoles.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CustomRoles.Repository
{
    public class BusinessRolesRepo : IBusinessRolesRepo
    {
        private readonly CustomRolesDbContext _context;


        public BusinessRolesRepo(

            CustomRolesDbContext context
            )
        {
            //   this.roleManager = roleManager;
            _context = context;
        }


        public async Task<List<Actions>> GetAllActions()
        {
            return (await _context.Actions.Include(a => a.Page).ToListAsync());

        }

        public async Task<List<BusinessRolePermissions>> CreateRolesPermissions(List<RoleActionVM> roleActionVM, List<BusinessRolePermissions> isSelectedPage, int roleId)
        {
            for (int i = 0; i < roleActionVM.Count; i++)
            {
                if (roleActionVM[i].Selected == true)
                {
                    BusinessRolePermissions brolePremissions = new()
                    {
                        ActionId = roleActionVM[i].Id,
                        RoleId = roleActionVM[i].RoleId
                    };


                    var RolePermissions = isSelectedPage.Where(s => s.ActionId == brolePremissions.ActionId).FirstOrDefault();
                    if (RolePermissions == null)
                    {
                        _context.BusinessRolePermissions.Add(brolePremissions);

                    }
                    else
                    {
                        foreach (BusinessRolePermissions BusinessUserRolePermission in isSelectedPage)
                        {

                            var action = isSelectedPage.Where(s => s.ActionId == brolePremissions.ActionId).Where(r => r.RoleId == brolePremissions.RoleId).FirstOrDefault();
                            if (action == null)
                            {

                                _context.BusinessRolePermissions.Add(brolePremissions);

                            }

                        }
                    }


                }
            }

            await _context.SaveChangesAsync();
            return null;

        }

        public async Task<List<BusinessRolePermissions>> DeleteRolesPermissions(List<RoleActionVM> roleActionVM, List<BusinessRolePermissions> isSelectedPage)
        {
            for (int i = 0; i < roleActionVM.Count; i++)
            {
                if (roleActionVM[i].Selected == false)
                {
                    BusinessRolePermissions brolePremissions = new()
                    {
                        ActionId = roleActionVM[i].Id,
                        RoleId = roleActionVM[i].RoleId
                    };

                    foreach (BusinessRolePermissions BusinessUserRolePermission in isSelectedPage)
                    {

                        var action = isSelectedPage.Where(s => s.ActionId == brolePremissions.ActionId).Where(r => r.RoleId == brolePremissions.RoleId).FirstOrDefault();
                        if (action != null)
                        {
                            _context.BusinessRolePermissions.Remove(action);


                        }

                    }



                }
            }

            await _context.SaveChangesAsync();
            return null;

        }

        public BusinessRoles GetById(int Id)
        {
            return _context.BusinessRoles.Where(x => x.Id == Id).FirstOrDefault();
        }

      

       

        public async Task<List<BusinessRoles>> RolesGetByBusinessId(int id)
        {
            //var vendor = await _context.Vendor.FirstOrDefaultAsync(m => m.Id == id);

            return await _context.BusinessRoles.Where(x => x.BusinessId == id).ToListAsync();
        }

 

        public void DeleteRole(BusinessRoles businessRoles)
        {
            _context.BusinessRoles.Remove(businessRoles);
            _context.SaveChanges();

        }
        public void SaveRole()
        {
            _context.SaveChangesAsync();

        }

        public async Task<Result> CreateRole([Bind("Id,Name,Description,PageId,ActionId")] BusinessRoles businessRole)
        {
            Result r;
            
            //IdentityResult result = 
            _context.BusinessRoles.Add(businessRole);

             _context.SaveChanges();
            r = new Result()
            {
                response = "OK",
                res = true
            };

            return r;
        }

        public async Task<BusinessRoles> EditRole(BusinessRoles businessRoles)
        {

            try
            {
                _context.Update(businessRoles);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException updateError)
            {
                if (!businessRoleExists(businessRoles.Id))
                {
                    Console.WriteLine(updateError);
                    //return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return null;
        }

        public bool businessRoleExists(int id)
        {
            return _context.BusinessRoles.Any(e => e.Id == id);
        }


        public async Task<List<BusinessRolePermissions>> IsPageSelected()
        {

            List<BusinessRolePermissions> businessUserRolesPermission = _context.BusinessRolePermissions
            .Include(bur => bur.BusinessRoles).Include(bup => bup.Actions).ToList();

            //   List<BusinessRolePermissions> businessUserRolesPermission = await _context.BusinessRolePermissions.ToListAsync();

            return businessUserRolesPermission;
        }

        public async Task<ApplicationUser> GetUserById(string Id)
        {
            return _context.ApplicationUser.Where(x=>x.Id == Id).FirstOrDefault();
        }
    }


}
