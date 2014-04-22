using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PX.Business.Models.Testimonials;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;
using AutoMapper;
using PX.EntityModel.Repositories;

namespace PX.Business.Services.Testimonials
{
    public class TestimonialServices : ITestimonialServices
    {
        private readonly ILocalizedResourceServices _localizedResourceServices;
        public TestimonialServices()
        {
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
        }

        #region Base
        public IQueryable<Testimonial> GetAll()
        {
            return TestimonialRepository.GetAll();
        }
        public IQueryable<Testimonial> Fetch(Expression<Func<Testimonial, bool>> expression)
        {
            return TestimonialRepository.Fetch(expression);
        }
        public Testimonial GetById(object id)
        {
            return TestimonialRepository.GetById(id);
        }
        public ResponseModel Insert(Testimonial testimonial)
        {
            return TestimonialRepository.Insert(testimonial);
        }
        public ResponseModel Update(Testimonial testimonial)
        {
            return TestimonialRepository.Update(testimonial);
        }
        public ResponseModel Delete(Testimonial testimonial)
        {
            return TestimonialRepository.Delete(testimonial);
        }
        public ResponseModel Delete(object id)
        {
            return TestimonialRepository.Delete(id);
        }
        #endregion

        #region Grid Search

        /// <summary>
        /// search the testimonials.
        /// </summary>
        /// <returns></returns>
        public JqGridSearchOut SearchTestimonials(JqSearchIn si)
        {
            var testimonials = GetAll().Select(u => new TestimonialModel
            {
                Id = u.Id,
                Author = u.Author,
                Content = u.Content,
                AuthorDescription = u.AuthorDescription,
                AuthorImageUrl = u.AuthorImageUrl,
                RecordActive = u.RecordActive,
                RecordOrder = u.RecordOrder,
                Created = u.Created,
                CreatedBy = u.CreatedBy,
                Updated = u.Updated,
                UpdatedBy = u.UpdatedBy
            });

            return si.Search(testimonials);
        }

        #endregion

        #region Grid Manage

        /// <summary>
        /// Manage testimonial
        /// </summary>
        /// <param name="operation">the operation</param>
        /// <param name="model">the testimonial model</param>
        /// <returns></returns>
        public ResponseModel ManageTestimonial(GridOperationEnums operation, TestimonialModel model)
        {
            ResponseModel response;
            Mapper.CreateMap<TestimonialModel, Testimonial>();
            Testimonial testimonial;
            switch (operation)
            {
                case GridOperationEnums.Edit:
                    testimonial = TestimonialRepository.GetById(model.Id);
                    testimonial.Author = model.Author;
                    testimonial.Content = model.Content;
                    testimonial.AuthorDescription = model.AuthorDescription;
                    testimonial.RecordOrder = model.RecordOrder;
                    testimonial.RecordActive = model.RecordActive;
                    response = Update(testimonial);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::Testimonials:::Messages:::UpdateSuccessfully:::Update testimonial successfully.")
                        : _localizedResourceServices.T("AdminModule:::Testimonials:::Messages:::UpdateFailure:::Update testimonial failed. Please try again later."));

                case GridOperationEnums.Add:
                    testimonial = Mapper.Map<TestimonialModel, Testimonial>(model);
                    response = Insert(testimonial);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::Testimonials:::Messages:::CreateSuccessfully:::Create testimonial successfully.")
                        : _localizedResourceServices.T("AdminModule:::Testimonials:::Messages:::CreateFailure:::Insert testimonial failed. Please try again later."));

                case GridOperationEnums.Del:
                    response = Delete(model.Id);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::Testimonials:::Messages:::DeleteSuccessfully:::Delete testimonial successfully.")
                        : _localizedResourceServices.T("AdminModule:::Testimonials:::Messages:::DeleteFailure:::Delete testimonial failed. Please try again later."));
            }
            return new ResponseModel
            {
                Success = false,
                Message = _localizedResourceServices.T("AdminModule:::Testimonials:::Messages:::ObjectNotFounded:::Testimonial is not founded.")
            };
        }

        #endregion

        /// <summary>
        /// Get number of random testimonials
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<TestimonialModel> GetRandom(int count)
        {
            var data = new List<TestimonialModel>();
            var testimonials = GetAll().Select(t => new TestimonialModel
            {
                Author = t.Author,
                Content = t.Content
            }).ToList();

            for (var i = 0; i < count; i++)
            {
                if(testimonials.Count == 0)
                    break;
                var index = new Random(testimonials.Count).Next();
                data.Add(testimonials[index]);
                testimonials.Remove(testimonials[index]);
            }
            return data;
        }
    }
}
