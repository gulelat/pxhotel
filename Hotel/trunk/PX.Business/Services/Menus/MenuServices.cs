using System.Linq;
using PX.EntityModel;
using PX.EntityModel.Framework.Repositories;

namespace PX.Business.Services.Menus
{
    public class MenuServices : IMenuServices
    {
        public IQueryable<Menu> GetAll()
        {
            return MenuRepository.GetAll();
        }
        public Menu GetById(int id)
        {
            return MenuRepository.GetById(id);
        }
        public Menu Insert(Menu menu)
        {
            return MenuRepository.Insert(menu);
        }
        public Menu Update(Menu menu)
        {
            return MenuRepository.Update(menu);
        }
        public bool Delete(Menu menu)
        {
            return MenuRepository.Delete(menu);
        }
        public bool Delete(int id)
        {
            var menu = GetById(id);
            if(menu != null)
            {
                return Delete(menu);
            }
            return false;
        }
    }
}
