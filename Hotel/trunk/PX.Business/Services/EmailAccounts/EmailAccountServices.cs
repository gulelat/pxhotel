using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using PX.Business.Models.EmailAccounts;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;
using PX.EntityModel.Repositories;

namespace PX.Business.Services.EmailAccounts
{
    public class EmailAccountServices : IEmailAccountServices
    {
        private readonly ILocalizedResourceServices _localizedResourceServices;
        private readonly EmailAccountRepository _emailAccountRepository;
        public EmailAccountServices(PXHotelEntities entities)
        {
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            _emailAccountRepository = new EmailAccountRepository(entities);
        }

        #region Base
        public IQueryable<EmailAccount> GetAll()
        {
            return _emailAccountRepository.GetAll();
        }
        public IQueryable<EmailAccount> Fetch(Expression<Func<EmailAccount, bool>> expression)
        {
            return _emailAccountRepository.Fetch(expression);
        }
        public EmailAccount FetchFirst(Expression<Func<EmailAccount, bool>> expression)
        {
            return _emailAccountRepository.FetchFirst(expression);
        }
        public EmailAccount GetById(object id)
        {
            return _emailAccountRepository.GetById(id);
        }
        public ResponseModel Insert(EmailAccount emailAccount)
        {
            return _emailAccountRepository.Insert(emailAccount);
        }
        public ResponseModel Update(EmailAccount emailAccount)
        {
            return _emailAccountRepository.Update(emailAccount);
        }
        public ResponseModel Delete(EmailAccount emailAccount)
        {
            return _emailAccountRepository.Delete(emailAccount);
        }
        public ResponseModel Delete(object id)
        {
            return _emailAccountRepository.Delete(id);
        }
        public ResponseModel InactiveRecord(int id)
        {
            return _emailAccountRepository.InactiveRecord(id);
        }
        #endregion

        #region Grid Search

        /// <summary>
        /// search the email accounts.
        /// </summary>
        /// <returns></returns>
        public JqGridSearchOut SearchEmailAccounts(JqSearchIn si)
        {
            var emailAccounts = GetAll().Select(u => new EmailAccountModel
            {
                Id = u.Id,
                Email = u.Email,
                DisplayName = u.DisplayName,
                IsDefault = u.IsDefault,
                RecordActive = u.RecordActive,
                RecordOrder = u.RecordOrder,
                Created = u.Created,
                CreatedBy = u.CreatedBy,
                Updated = u.Updated,
                UpdatedBy = u.UpdatedBy
            });

            return si.Search(emailAccounts);
        }

        #endregion

        #region Manage

        /// <summary>
        /// Get email account manage model for edit/create
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EmailAccountManageModel GetEmailAccountManageModel(int? id = null)
        {
            var emailAccount = GetById(id);
            if (emailAccount != null)
            {
                return new EmailAccountManageModel
                {
                    Id = emailAccount.Id,
                    Email = emailAccount.Email,
                    DisplayName = emailAccount.DisplayName,
                    Host = emailAccount.Host,
                    Port = emailAccount.Port,
                    UserName = emailAccount.UserName,
                    Password = emailAccount.Password,
                    EnableSsl = emailAccount.EnableSsl,
                    UseDefaultCredentials = emailAccount.UseDefaultCredentials,
                    IsDefault = emailAccount.IsDefault
                };
            }
            return new EmailAccountManageModel
            {
                IsDefault = false
            };
        }

        /// <summary>
        /// Save email account
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResponseModel SaveEmailAccount(EmailAccountManageModel model)
        {
            ResponseModel response;
            var emailAccount = GetById(model.Id);
            if (emailAccount != null)
            {
                emailAccount.Email = model.Email;
                emailAccount.DisplayName = model.DisplayName;
                emailAccount.Host = model.Host;
                emailAccount.Port = model.Port;
                emailAccount.UserName = model.UserName;
                emailAccount.Password = model.Password;
                emailAccount.EnableSsl = model.EnableSsl;
                emailAccount.UseDefaultCredentials = model.UseDefaultCredentials;
                emailAccount.IsDefault = model.IsDefault;

                response = Update(emailAccount);
                return response.SetMessage(response.Success ?
                    _localizedResourceServices.T("AdminModule:::EmailAccounts:::Messages:::UpdateSuccessfully:::Update email account successfully.")
                    : _localizedResourceServices.T("AdminModule:::EmailAccounts:::Messages:::UpdateFailure:::Update email account failed. Please try again later."));
            }
            Mapper.CreateMap<EmailAccountManageModel, EmailAccount>();
            emailAccount = Mapper.Map<EmailAccountManageModel, EmailAccount>(model);
            response = Insert(emailAccount);
            return response.SetMessage(response.Success ?
                _localizedResourceServices.T("AdminModule:::EmailAccounts:::Messages:::CreateSuccessfully:::Create email account successfully.")
                : _localizedResourceServices.T("AdminModule:::EmailAccounts:::Messages:::CreateFailure:::Create email account failed. Please try again later."));
        }

        #endregion

        /// <summary>
        /// Mark email account as default
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseModel MarkAsDefault(int id)
        {
            var image = GetById(id);
            if (image != null)
            {
                var query = string.Format("Update EmailAccounts Set IsDefault = 0");
                var response = _emailAccountRepository.ExcuteSql(query);
                if (response.Success)
                {
                    image.IsDefault = true;
                    response = Update(image);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::EmailAccounts:::Messages:::MarkDefaultSuccessfully:::Mark email account as default successfully.")
                        : _localizedResourceServices.T("AdminModule:::EmailAccounts:::Messages:::MarkDefaultFailure:::Mark email account as default failed. Please try again later."));
                }
            }
            return new ResponseModel
            {
                Success = false,
                Message = _localizedResourceServices.T("AdminModule:::EmailAccounts:::Messages:::ObjectNotFounded:::Email account is not founded.")
            };
        }

        public ResponseModel SendTestEmail(TestEmailModel model)
        {
            throw new NotImplementedException();
        }
    }
}
