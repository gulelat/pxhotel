using System.Linq;
using PX.EntityModel;

namespace PX.Business.Services.Menus
{
    public interface IMenuServices
    {
        IQueryable<Menu> GetAll();
        Menu GetById(int id);
        Menu Insert(Menu menu);
        Menu Update(Menu menu);
        bool Delete(Menu menu);
        bool Delete(int id);
    }
}
