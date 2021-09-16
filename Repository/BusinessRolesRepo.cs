using CustomRoles.Data;
using CustomRoles.Data.ViewModels;
using CustomRoles.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<BusinessRoles>> GetAll()
        {
            return (await _context.BusinessRoles.ToListAsync());
        }

        public async Task<List<Actions>> GetAllActions()
        {
            return (await _context.Actions.Include(a => a.Page).ToListAsync());

        }

        public async Task<List<BusinessRolePermissions>> CreateRolesPermissions(List<RoleActionVM> roleActionVM)
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
                    _context.BusinessRolePermissions.Add(brolePremissions);

                }
            }

            _context.SaveChanges();
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


        public Business GetBusinessByUserId(string id)
        {
            var user = _context.ApplicationUser.Where(x => x.Id == id).FirstOrDefault();
            var businessId = user.BusinessId;
            return _context.Business.Where(x => x.Id == businessId).FirstOrDefault();

        }


        public void DeleteRole(BusinessRoles businessRoles)
        {
            _context.BusinessRoles.Remove(businessRoles);

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

            await _context.SaveChangesAsync();
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

        public async Task<List<Business>> GetBusinessList()
        {
            return (await _context.Business.ToListAsync());
        }






    }
}
