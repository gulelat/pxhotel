using System;
using System.Linq;
using System.Linq.Expressions;
using PX.Business.Models.Tags;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;
using AutoMapper;
using PX.EntityModel.Repositories;

namespace PX.Business.Services.Tags
{
    public class TagServices : ITagServices
    {
        private readonly ILocalizedResourceServices _localizedResourceServices;
        private readonly TagRepository _tagRepository;
        public TagServices(PXHotelEntities entities)
        {
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            _tagRepository = new TagRepository(entities);
        }

        #region Base
        public IQueryable<Tag> GetAll()
        {
            return _tagRepository.GetAll();
        }
        public IQueryable<Tag> Fetch(Expression<Func<Tag, bool>> expression)
        {
            return _tagRepository.Fetch(expression);
        }
        public Tag GetById(object id)
        {
            return _tagRepository.GetById(id);
        }
        public ResponseModel Insert(Tag tag)
        {
            return _tagRepository.Insert(tag);
        }
        public ResponseModel Update(Tag tag)
        {
            return _tagRepository.Update(tag);
        }
        public ResponseModel Delete(Tag tag)
        {
            return _tagRepository.Delete(tag);
        }
        public ResponseModel Delete(object id)
        {
            return _tagRepository.Delete(id);
        }
        #endregion

        #region Grid Search

        /// <summary>
        /// search the tags.
        /// </summary>
        /// <returns></returns>
        public JqGridSearchOut SearchTags(JqSearchIn si)
        {
            var tags = GetAll().Select(u => new TagModel
            {
                Id = u.Id,
                Name = u.Name,
                RecordActive = u.RecordActive,
                RecordOrder = u.RecordOrder,
                Created = u.Created,
                CreatedBy = u.CreatedBy,
                Updated = u.Updated,
                UpdatedBy = u.UpdatedBy
            });

            return si.Search(tags);
        }

        #endregion

        #region Grid Manage

        /// <summary>
        /// Manage tag
        /// </summary>
        /// <param name="operation">the operation</param>
        /// <param name="model">the tag model</param>
        /// <returns></returns>
        public ResponseModel ManageTag(GridOperationEnums operation, TagModel model)
        {
            ResponseModel response;
            Mapper.CreateMap<TagModel, Tag>();
            Tag tag;
            switch (operation)
            {
                case GridOperationEnums.Edit:
                    tag = _tagRepository.GetById(model.Id);
                    tag.Name = model.Name;
                    tag.RecordOrder = model.RecordOrder;
                    tag.RecordActive = model.RecordActive;
                    response = Update(tag);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::Tags:::Messages:::UpdateSuccessfully:::Update tag successfully.")
                        : _localizedResourceServices.T("AdminModule:::Tags:::Messages:::UpdateNotFounded:::Update tag failed. Please try again later."));
                
                case GridOperationEnums.Add:
                    tag = Mapper.Map<TagModel, Tag>(model);
                    response = Insert(tag);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::Tags:::Messages:::CreateSuccessfully:::Insert tag successfully.")
                        : _localizedResourceServices.T("AdminModule:::Tags:::Messages:::CreateFailure:::Insert tag failed. Please try again later."));
                
                case GridOperationEnums.Del:
                    response = Delete(model.Id);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::Tags:::Messages:::DeleteSuccessfully:::Delete tag successfully.")
                        : _localizedResourceServices.T("AdminModule:::Tags:::Messages:::DeleteFailure:::Delete tag failed. Please try again later."));
            }
            return new ResponseModel
            {
                Success = false,
                Message = _localizedResourceServices.T("AdminModule:::Tags:::Messages:::ObjectNotFound:::Tag not is founded")
            };
        }

        #endregion
    }
}
