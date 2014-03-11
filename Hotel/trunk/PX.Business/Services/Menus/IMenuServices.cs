using System.Linq;
using PX.Business.Models;
using PX.EntityModel;

namespace PX.Business.Services.Menus
{
    public interface IMenuServices
    {
        IQueryable<Menu> GetAll();
        Menu GetById(int id);
        ResponseModel Insert(Menu menu);
        ResponseModel Update(Menu menu);
        ResponseModel Delete(Menu menu);
        ResponseModel Delete(int id);
    }
}
