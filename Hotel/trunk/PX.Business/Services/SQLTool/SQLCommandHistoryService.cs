using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using PX.Business.Models.SQLTool;
using PX.Business.Services.Localizes;
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
        public SQLCommandServices()
        {
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
        }

        #region Base
        public IQueryable<SQLCommandHistory> GetAll()
        {
            return SQLCommandHistoryRepository.GetAll();
        }
        public IQueryable<SQLCommandHistory> Fetch(Expression<Func<SQLCommandHistory, bool>> expression)
        {
            return SQLCommandHistoryRepository.Fetch(expression);
        }
        public SQLCommandHistory GetById(object id)
        {
            return SQLCommandHistoryRepository.GetById(id);
        }
        public ResponseModel Insert(SQLCommandHistory sqlCommandHistory)
        {
            return SQLCommandHistoryRepository.Insert(sqlCommandHistory);
        }
        public ResponseModel Update(SQLCommandHistory sqlCommandHistory)
        {
            return SQLCommandHistoryRepository.Update(sqlCommandHistory);
        }
        public ResponseModel Delete(SQLCommandHistory sqlCommandHistory)
        {
            return SQLCommandHistoryRepository.Delete(sqlCommandHistory);
        }
        public ResponseModel Delete(object id)
        {
            return SQLCommandHistoryRepository.Delete(id);
        }
        #endregion

        #region Grid Search

        /// <summary>
        /// search the testimonials.
        /// </summary>
        /// <returns></returns>
        public JqGridSearchOut SearchCommands(JqSearchIn si)
        {
            var testimonials = GetAll().Select(c => new CommandHistory
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

        /// <summary>
        /// Get connection
        /// </summary>
        /// <returns></returns>
        public DbConnection GetConnection()
        {
            return SQLCommandHistoryRepository.Connection();
        }

        /// <summary>
        /// Get connection string
        /// </summary>
        /// <returns></returns>
        public string GetConnectionString()
        {
            return SQLCommandHistoryRepository.Connection().ConnectionString;
        }

        public ResponseModel Insert(CommandHistory history)
        {
            var commandHistory = new SQLCommandHistory
                {
                    Created = DateTime.Now,
                    CreatedBy = HttpContext.Current.User.Identity.Name,
                    Updated = DateTime.Now,
                    UpdatedBy = HttpContext.Current.User.Identity.Name,
                    Query = history.Query,
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
            var last = GetLastCommand(HttpContext.Current.User.Identity.Name);
            if (last != null && last.Query == request.Query)
            {
                return new ResponseModel
                {
                    Success = true
                };
            }
            var history = new CommandHistory
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
        public IEnumerable<CommandHistory> GetHistories(int index, int pageSize)
        {
            var username = HttpContext.Current.User.Identity.Name;
            return Fetch(i => i.CreatedBy.Equals(username))
                .OrderByDescending(i => i.Updated)
                .Skip(index)
                .Take(pageSize)
                .Select(
                    i => new CommandHistory { Id = i.Id, Query = i.Query, CreatedBy = i.CreatedBy, Updated = i.Updated });
        }

        public IEnumerable<SQLCommandHistory> GetHistories()
        {
            string user = HttpContext.Current.User.Identity.Name;
            return Fetch(i => i.CreatedBy == user).OrderByDescending(i => i.Updated);
        }

        public IQueryable<CommandHistory> GetHistories(string username, int start, int length)
        {
            return Fetch(i => i.CreatedBy == username)
                            .OrderByDescending(i => i.Updated)
                            .Skip(start)
                            .Take(length)
                            .Select(i => new CommandHistory
                            {
                                Id = i.Id,
                                Query = i.Query,
                                CreatedBy = i.CreatedBy,
                                Updated = i.Updated
                            });
        }

        public CommandHistory GetLastCommand(string username)
        {
            return GetHistories(username, 0, 1).FirstOrDefault();
        }
    }
}