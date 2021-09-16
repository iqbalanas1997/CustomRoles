using CustomRoles.Data;
using CustomRoles.Data.ViewModels;

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CustomRoles.Infrastructure
{
    public interface IActionRepo
    {
        bool ActionExists(int id);
        Task<Result> CreateAction([Bind(new[] { "Id,PageId,ActionName,Name" })] PageActionVM actionsVM);
        void DeleteAction(Actions actions);
        Task<Actions> EditAction(Actions action);
        Task<List<Actions>> GetAll();
        Task<List<Page>> GetAllPages();
        Actions GetById(int Id);
        Page GetPageName(int Id);
        Task<List<Page>> GetPagesList();
        void SaveAction();
    }
}
