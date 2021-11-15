using CustomRoles.Data;
using CustomRoles.Data.ViewModels;
using CustomRoles.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomRoles.Repository
{
    public class ActionRepo : IActionRepo
    {
        private readonly CustomRolesDbContext _context;
        //private UserManager<Actions> _userManager;

        public ActionRepo(CustomRolesDbContext context
           // UserManager<Actions> userManager
           )
        {
            _context = context;
            //  _userManager = userManager;
        }

        public async Task<List<Actions>> GetAll()
        {
            return (await _context.Actions.Include(a => a.Page).ToListAsync());

        }

        public async Task<List<Page>> GetAllPages()
        {
            return (await _context.Pages.ToListAsync());

        }
        public Actions GetById(int Id)
        {
            return _context.Actions.Where(x => x.Id == Id).FirstOrDefault();
        }
        public async Task<List<Page>> GetPagesList()
        {
            return (await _context.Pages.ToListAsync());
        }


        public Page GetPageName(int Id)
        {
            return _context.Pages.Where(x => x.Id == Id).FirstOrDefault();
        }

        public void DeleteAction(Actions actions)
        {
            _context.Actions.Remove(actions);
            _context.SaveChanges();

        }
        public void SaveAction()
        {
            _context.SaveChangesAsync();

        }

        public async Task<Result> CreateAction([Bind("Id,PageId,ActionName,Name")] PageActionVM actionsVM)
        {
            Result r;

            Actions actions = new()
            {
                ActionName = actionsVM.ActionName,
                PageId = actionsVM.PageId
            };
            //IdentityResult result = 
            _context.Actions.Add(actions);

            await _context.SaveChangesAsync();
            r = new Result()
            {
                response = "OK",
                res = true
            };

            return r;
        }


        public async Task<Actions> EditAction(Actions action)
        {

            try
            {
                _context.Update(action);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException updateError)
            {
                if (!ActionExists(action.Id))
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

        public bool ActionExists(int id)
        {
            return _context.Actions.Any(e => e.Id == id);
        }

        public Task<Actions> EditAction(Action action)
        {
            throw new NotImplementedException();
        }
    }
}
