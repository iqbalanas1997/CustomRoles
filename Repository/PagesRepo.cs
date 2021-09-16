
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using CustomRoles.Data;
using CustomRoles.Infrastructure;

namespace CustomRoles.Repository
{
    public class PagesRepo : IPagesRepo
    {
        private readonly CustomRolesDbContext _context;

        public PagesRepo(CustomRolesDbContext context)
        {
            _context = context;
        }
        public async Task<List<Page>> GetAll()
        {
            return (await _context.Pages.ToListAsync());

        }


        public Page GetById(int Id)
        {
            return _context.Pages.Where(x => x.Id == Id).FirstOrDefault();
        }

        public async Task<Page> FindAction(int Id)
        {
            return (await _context.Pages.FindAsync(Id));
        }


        public void DeleteAction(Page pages)
        {
            _context.Pages.Remove(pages);

        }
        public void SaveAction()
        {
            _context.SaveChangesAsync();

        }

        public async Task<Result> CreateAction([Bind("Id,ActionName")] Page pages)
        {
            Result r;


            //IdentityResult result = 
            _context.Pages.Add(pages);

            await _context.SaveChangesAsync();
            r = new Result()
            {
                response = "OK",
                res = true
            };

            return r;
        }


        public async Task<Page> EditAction(Page pages)
        {

            try
            {
                _context.Update(pages);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException updateError)
            {
                if (!PagesExists(pages.Id))
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

        public bool PagesExists(int id)
        {
            return _context.Pages.Any(e => e.Id == id);
        }
    }
}
