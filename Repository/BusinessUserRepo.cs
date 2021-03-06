using CustomRoles.Data;
using CustomRoles.Data.SPEntity;
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
            _context.SaveChanges();
        }

        public async void Save()
        {
           await _context.SaveChangesAsync();

        }


    



        public async Task<List<BusinessUserRoles>> IsRoleSelected(string userId)
        {

            List<BusinessUserRoles> businessUserRoles = _context.BusinessUserRole
            .Include(bur => bur.BusinessRoles)
            .Where(bur => bur.UserId == userId).ToList();

            return businessUserRoles;
        }
      
        public ApplicationUser GetById(string Id)
        {
            //var vendor = await _context.Vendor.FirstOrDefaultAsync(m => m.Id == id);

            return _context.ApplicationUser.Where(x => x.Id == Id).FirstOrDefault();
        }

        public async Task<List<BusinessRoles>> GetAllBusinessRolesByBusinessId(int id)
        {
            return (await _context.BusinessRoles.Where(x => x.BusinessId == id).ToListAsync());
        }

        public async Task<List<BusinessUserRoles>> CreateBusinessUserRoles(List<ManageBusinessUserRoleVM> businessUserRolesVM, List<BusinessUserRoles> SelectedRoles)
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


                    var b = SelectedRoles.Where(s => s.RoleId == bUserRole.RoleId).FirstOrDefault();
                    if (b == null)
                    {
                        _context.BusinessUserRole.Add(bUserRole);

                    }
                    else
                    {
                        foreach (BusinessUserRoles manageBusinessUserRole in SelectedRoles)
                        {
                            var a = SelectedRoles.Where(s => s.RoleId == bUserRole.RoleId).FirstOrDefault();
                            if (a == null)
                            {
                                _context.BusinessUserRole.Add(bUserRole);
                            }
                           
                        }
                    }

                }
               
                }

            await _context.SaveChangesAsync();
            return null;

        }


        public async Task<List<BusinessUserRoles>> DeleteBusinessUserRoles(List<ManageBusinessUserRoleVM> businessUserRolesVM, List<BusinessUserRoles> isSelect)
        {



            for (int i = 0; i < businessUserRolesVM.Count; i++)
            {
                if (businessUserRolesVM[i].selected == false)
                {
                    BusinessUserRoles bUserRole = new()
                    {
                        
                        UserId = businessUserRolesVM[i].userId,
                        RoleId = businessUserRolesVM[i].roleId
                    };


                    foreach (BusinessUserRoles manageBusinessUserRole in isSelect)
                    {
                        var a = isSelect.Where(s => s.RoleId == bUserRole.RoleId).FirstOrDefault();
                        if (a != null)
                        {
                            _context.BusinessUserRole.Remove(a);
                          
                        }
                       
                    }
                   
                }
             
            }
            await _context.SaveChangesAsync();
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

        public async Task<ApplicationUser> GetUserById(string Id)
        {
            return _context.ApplicationUser.Where(x => x.Id == Id).FirstOrDefault();
        }

        public async Task<Result> Insert([Bind("Id,UserName,Email,PhoneNumber,BusinessId,Password,ConfirmPassword")] BusinessUserVM businessUserVM)
        {

            Result r;



            var user = new ApplicationUser {
                UserName = businessUserVM.UserName,
                Email = businessUserVM.Email,
                PhoneNumber = businessUserVM.PhoneNumber,
                BusinessId = businessUserVM.BusinessId 
            };
            IdentityResult result = await _userManager.CreateAsync(user, businessUserVM.Password);


            var roleId = _context.DefaultRole.FromSqlRaw($"selectDefaultRoleId {businessUserVM.BusinessId}").ToList();

            for (int i = 0; i < roleId.Count; i++)
            {
                var businessUserRole = new BusinessUserRoles
                {
                    UserId = user.Id,
                    RoleId = roleId[i].Id
                };
                _context.BusinessUserRole.Add(businessUserRole);
            }

            await _context.SaveChangesAsync();
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
        public int getBusinessId(string id)
        {
            //var a = _context.GetBusinessId.FromSqlRaw($"SelectBusinessId {id}").ToList();
            var a = _context.ApplicationUser.Where(x => x.Id == id).Select(b=>b.BusinessId).FirstOrDefault();
            int bid = (int)a;
            return bid;
        }

        private bool BusinessUserExists(string id)
        {
            return _context.ApplicationUser.Any(e => e.Id == id);
        }

       
    }
}
