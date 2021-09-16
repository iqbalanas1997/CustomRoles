using CustomRoles.Data;
using CustomRoles.Data.ViewModels;
using CustomRoles.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomRoles.Repository
{
    public class BusinessUserRepo : IBusinessUserRepo
    {
        private UserManager<ApplicationUser> _userManager;
        private CustomRolesDbContext _context;

        public BusinessUserRepo(UserManager<ApplicationUser> userManager, CustomRolesDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }


        public void Delete(ApplicationUser applicationUser)
        {
            _context.ApplicationUser.Remove(applicationUser);
        }

        public void Save()
        {
            _context.SaveChanges();

        }



        public async Task<List<ApplicationUser>> GetAll()
        {
            return (await _context.ApplicationUser.ToListAsync());
        }

        public async Task<List<BusinessUserRoles>> GetAllBusinessUserRoles()
        {
            return (await _context.BusinessUserRole.ToListAsync());
        }

        public async Task<List<BusinessRoles>> GetAllBusinessRoles()
        {
            return (await _context.BusinessRoles.ToListAsync());
        }

        //public async Task<BusinessUserRole> GetBusinessUserRole(string userId)
        //{
        //    return await _context.BusinessUserRole.Where(x => x.UserId == userId).FirstOrDefault();
        //}

        //public async Task<BusinessUserRole> GetRolebyUserId(string userId,int roleId);
        //{
        //    return _context.BusinessUserRole.Where(x => x.UserId == userId).FirstOrDefault();
        //}

        public async Task<BusinessUserRoles> IsInRole(string userId, int roleId)
        {
            return (_context.BusinessUserRole.Where(x => x.UserId == userId && x.RoleId == roleId).FirstOrDefault());
        }

        public async Task<ApplicationUser> GetUser(string userId)
        {
            return (await _userManager.FindByIdAsync(userId));
        }
        public ApplicationUser GetById(string Id)
        {
            //var vendor = await _context.Vendor.FirstOrDefaultAsync(m => m.Id == id);

            return _context.ApplicationUser.Where(x => x.Id == Id).FirstOrDefault();
        }
        public Business GetBusinessByUserId(string id)
        {
            //var user = _context.ApplicationUser.Where(x => x.Id == id).FirstOrDefault();
            //var businessId = user.BusinessId;
            //return _context.Business.Where(x => x.Id == businessId).FirstOrDefault();


            var user = _context.ApplicationUser.Include(a => a.Business).Where(x => x.Id == id).FirstOrDefault();
            return user.Business;


            //var user = _context.ApplicationUser.Include(a=>a.Business).ThenInclude(b=>b.BusinessLocations).Where(x => x.Id == id).FirstOrDefault();
            //return user.Business;

        }

        public async Task<List<BusinessRoles>> GetAllBusinessRolesByBusinessId(int id)
        {
            return (await _context.BusinessRoles.Where(x => x.BusinessId == id).ToListAsync());
        }

        public async Task<List<BusinessUserRoles>> CreateBusinessUserRoles(List<ManageBusinessUserRoleVM> businessUserRolesVM)
        {
            for (int i = 0; i < businessUserRolesVM.Count; i++)
            {
                if (businessUserRolesVM[i].selected == true)
                {
                    BusinessUserRoles bUserRole = new()
                    {
                        UserId = businessUserRolesVM[i].userId,
                        RoleId = businessUserRolesVM[i].roleId
                    };
                    _context.BusinessUserRole.Add(bUserRole);

                }
            }

            _context.SaveChanges();
            return null;

        }






        public async Task<List<ApplicationUser>> UsersGetByBusinessId(int id)
        {
            //var vendor = await _context.Vendor.FirstOrDefaultAsync(m => m.Id == id);

            return await _context.ApplicationUser.Where(x => x.BusinessId == id).ToListAsync();


        }
        public async Task<ApplicationUser> Update(string id, [Bind("Id,UserName,Email,PhoneNumber,BusinessId")] ApplicationUser applicationUser)
        {
            try
            {
                var usr = await _userManager.FindByIdAsync(applicationUser.Id);
                usr.UserName = applicationUser.UserName;
                usr.Email = applicationUser.Email;
                usr.PhoneNumber = applicationUser.PhoneNumber;
                await _userManager.UpdateAsync(usr);
                //await _userManager.UpdateAsync(applicationUser);
                // await _context.Update(applicationUser);
                //await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException updateError)
            {
                if (!BusinessUserExists(applicationUser.Id))
                {
                    Console.WriteLine(updateError);
                    //       return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return null;
        }



        public async Task<Result> Insert([Bind("Id,UserName,Email,PhoneNumber,BusinessId,Password,ConfirmPassword")] BusinessUserVM businessUserVM)
        {

            Result r;

            //ApplicationUser appuser = new ApplicationUser()
            //{
            //    UserName = businessUserVM.UserName,
            //    Email = businessUserVM.Email,
            //    PhoneNumber = businessUserVM.PhoneNumber,
            //    BusinessId = businessUserVM.BusinessId,
            //    PasswordHash = businessUserVM.Password
            //};

            // ApplicationUser buEntity = _context.Add(appuser).Entity;
            var user = new ApplicationUser { UserName = businessUserVM.UserName, Email = businessUserVM.Email, PhoneNumber = businessUserVM.PhoneNumber, BusinessId = businessUserVM.BusinessId };
            IdentityResult result = await _userManager.CreateAsync(user, businessUserVM.Password);


            if (result.Succeeded)
            {

                // await _signInManager.SignInAsync(user, false);
                await _userManager.AddToRoleAsync(user, "Business User");

                r = new Result()
                {
                    response = "ok",
                    res = true
                };


            }
            else
            {
                r = new Result()
                {
                    response = "failure",
                    res = false
                };


            }
            return r;
        }


        private bool BusinessUserExists(string id)
        {
            return _context.ApplicationUser.Any(e => e.Id == id);
        }



    }
}
