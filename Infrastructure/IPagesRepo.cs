using CustomRoles.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomRoles.Infrastructure
{
    public interface IPagesRepo
    {
        Task<Result> CreateAction([Bind(new[] { "Id,ActionName" })] Page pages);
        void DeleteAction(Page pages);
        Task<Page> EditAction(Page pages);
        Task<Page> FindAction(int Id);
        Task<List<Page>> GetAll();
        Page GetById(int Id);
        bool PagesExists(int id);
        void SaveAction();
    }
}