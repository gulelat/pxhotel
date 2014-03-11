using System;
using System.Linq;
using PX.Business.Models;
using PX.EntityModel;
using PX.EntityModel.Framework.Repositories;

namespace PX.Business.Services.Menus
{
    public class MenuServices : IMenuServices
    {
        #region Base

        #endregion
        public IQueryable<Menu> GetAll()
        {
            return MenuRepository.GetAll();
        }
        public Menu GetById(int id)
        {
            return MenuRepository.GetById(id);
        }

        public ResponseModel Insert(Menu menu)
        {
            var response = new ResponseModel();
            try
            {
                MenuRepository.Insert(menu);
                response.Success = true;
            }
            catch (Exception exception)
            {
                response.Success = false;
                response.Message = exception.Message;
            }
            return response;
        }
        public ResponseModel Update(Menu user)
        {
            var response = new ResponseModel();
            try
            {
                MenuRepository.Update(user);
                response.Success = true;
            }
            catch (Exception exception)
            {
                response.Success = false;
                response.Message = exception.Message;
            }
            return response;
        }
        public ResponseModel Delete(Menu menu)
        {
            var response = new ResponseModel();
            try
            {
                MenuRepository.Delete(menu);
                response.Success = true;
            }
            catch (Exception exception)
            {
                response.Success = false;
                response.Message = exception.Message;
            }
            return response;
        }
        public ResponseModel Delete(int id)
        {
            var response = new ResponseModel();
            try
            {
                MenuRepository.Delete(id);
                response.Success = true;
            }
            catch (Exception exception)
            {
                response.Success = false;
                response.Message = exception.Message;
            }
            return response;
        }
    }
}
