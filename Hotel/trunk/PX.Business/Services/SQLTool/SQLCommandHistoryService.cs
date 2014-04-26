using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using PX.Business.Models.SQLTool;
using PX.Business.Services.Localizes;
using PX.Business.Services.Settings;
using PX.Core.Configurations;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Environments;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;
using PX.EntityModel.Repositories;

namespace PX.Business.Services.SQLTool
{
    public class SQLCommandServices : ISQLCommandServices
    {
        private readonly ILocalizedResourceServices _localizedResourceServices;
        private readonly ISettingServices _settingServices;
        private readonly SQLCommandHistoryRepository _sqlCommandHistoryRepository;
        public SQLCommandServices()
        {
            _settingServices = HostContainer.GetInstance<ISettingServices>();
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            _sqlCommandHistoryRepository = new SQLCommandHistoryRepository();
        }

        #region Base
        public IQueryable<SQLCommandHistory> GetAll()
        {
            return _sqlCommandHistoryRepository.GetAll();
        }
        public IQueryable<SQLCommandHistory> Fetch(Expression<Func<SQLCommandHistory, bool>> expression)
        {
            return _sqlCommandHistoryRepository.Fetch(expression);
        }
        public SQLCommandHistory GetById(object id)
        {
            return _sqlCommandHistoryRepository.GetById(id);
        }
        public ResponseModel Insert(SQLCommandHistory sqlCommandHistory)
        {
            return _sqlCommandHistoryRepository.Insert(sqlCommandHistory);
        }
        public ResponseModel Update(SQLCommandHistory sqlCommandHistory)
        {
            return _sqlCommandHistoryRepository.Update(sqlCommandHistory);
        }
        public ResponseModel Delete(SQLCommandHistory sqlCommandHistory)
        {
            return _sqlCommandHistoryRepository.Delete(sqlCommandHistory);
        }
        public ResponseModel Delete(object id)
        {
            return _sqlCommandHistoryRepository.Delete(id);
        }
        #endregion

        #region Grid Search

        /// <summary>
        /// search the testimonials.
        /// </summary>
        /// <returns></returns>
        public JqGridSearchOut SearchCommands(JqSearchIn si)
        {
            var testimonials = GetAll().Select(c => new SQLCommandHistoryModel
            {
                Id = c.Id,
                Query = c.Query,
                RecordActive = c.RecordActive,
                RecordOrder = c.RecordOrder,
                Created = c.Created,
                CreatedBy = c.CreatedBy,
                Updated = c.Updated,
                UpdatedBy = c.UpdatedBy
            });

            return si.Search(testimonials);
        }

        #endregion

        #region Grid Manage

        /// <summary>
        /// Manage user group
        /// </summary>
        /// <param name="operation">the operation</param>
        /// <param name="model">the user group model</param>
        /// <returns></returns>
        public ResponseModel ManageSQLCommandHistory(GridOperationEnums operation, SQLCommandHistoryModel model)
        {
            ResponseModel response;
            AutoMapper.Mapper.CreateMap<SQLCommandHistoryModel, SQLCommandHistory>();
            SQLCommandHistory sqlCommandHistory;
            switch (operation)
            {
                case GridOperationEnums.Edit:
                    sqlCommandHistory = _sqlCommandHistoryRepository.GetById(model.Id);
                    sqlCommandHistory.Query = model.Query;
                    sqlCommandHistory.RecordOrder = model.RecordOrder;
                    sqlCommandHistory.RecordActive = model.RecordActive;
                    response = Update(sqlCommandHistory);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::SQLCommandHistorys:::Messages:::UpdateSuccessfully:::Update command successfully.")
                        : _localizedResourceServices.T("AdminModule:::SQLCommandHistorys:::Messages:::UpdateFailure:::Update command failed. Please try again later."));

                case GridOperationEnums.Add:
                    sqlCommandHistory = AutoMapper.Mapper.Map<SQLCommandHistoryModel, SQLCommandHistory>(model);
                    response = Insert(sqlCommandHistory);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::SQLCommandHistorys:::Messages:::CreateSuccessfully:::Create command successfully.")
                        : _localizedResourceServices.T("AdminModule:::SQLCommandHistorys:::Messages:::CreateFailure:::Create command failed. Please try again later."));

                case GridOperationEnums.Del:
                    response = Delete(model.Id);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::SQLCommandHistorys:::Messages:::DeleteSuccessfully:::Delete command successfully.")
                        : _localizedResourceServices.T("AdminModule:::SQLCommandHistorys:::Messages:::DeleteFailure:::Delete command failed. Please try again later."));
            }
            return new ResponseModel
            {
                Success = false,
                Message = _localizedResourceServices.T("AdminModule:::SQLCommandHistorys:::Messages:::ObjectNotFounded:::Command is not founded.")
            };
        }

        #endregion

        /// <summary>
        /// Get connection
        /// </summary>
        /// <returns></returns>
        public DbConnection GetConnection()
        {
            return _sqlCommandHistoryRepository.Connection();
        }

        /// <summary>
        /// Get connection string
        /// </summary>
        /// <returns></returns>
        public string GetConnectionString()
        {
            return _sqlCommandHistoryRepository.Connection().ConnectionString;
        }

        public ResponseModel Insert(SQLCommandHistoryModel historyModel)
        {
            var commandHistory = new SQLCommandHistory
                {
                    Query = historyModel.Query,
                    RecordActive = true
                };
            return Insert(commandHistory);
        }

        /// <summary>
        /// Save a SQL request into history for later use
        /// </summary>
        /// <param name="request"></param>
        public ResponseModel SaveCommand(SQLRequest request)
        {
            if (request.Query == null)
            {
                return new ResponseModel
                    {
                        Success = true
                    };
            }
            var last = GetLastCommand();
            if (last != null && last.Query == request.Query)
            {
                return new ResponseModel
                {
                    Success = true
                };
            }
            var history = new SQLCommandHistoryModel
            {
                Query = request.Query
            };
            var response = Insert(history);

            return response.SetMessage(response.Success ?
                _localizedResourceServices.T("AdminModule:::News:::Messages:::CreateSuccessfully:::Create news successfully.")
                : _localizedResourceServices.T("AdminModule:::News:::Messages:::CreateFailure:::Insert news failed. Please try again later."));
        }

        /// <summary>
        /// Get history request for current user
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<SQLCommandHistoryModel> GetHistories(int? index, int? pageSize)
        {
            if(!index.HasValue)
                index = _settingServices.GetSetting<int>(SettingNames.DefaultHistoryLength);
            if(!pageSize.HasValue)
                pageSize = _settingServices.GetSetting<int>(SettingNames.DefaultHistoryStart);

            var username = HttpContext.Current.User.Identity.Name;
            return Fetch(i => i.CreatedBy.Equals(username))
                .OrderByDescending(i => i.Created)
                .Skip(index.Value)
                .Take(pageSize.Value)
                .Select(
                    i => new SQLCommandHistoryModel { Id = i.Id, Query = i.Query, CreatedBy = i.CreatedBy });
        }

        public SQLCommandHistoryModel GetLastCommand()
        {
            return GetHistories(0, 1).FirstOrDefault();
        }
    }
}