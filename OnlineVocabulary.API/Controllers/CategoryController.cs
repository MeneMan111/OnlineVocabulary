using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using OnlineVocabulary.API.Model;
using OnlineVocabulary.API.Model.DTOs;
using OnlineVocabulary.API.Model.RequestCommands;
using OnlineVocabulary.API.Model.RequestModels;
using OnlineVocabulary.Domain.Services;
using OnlineVocabulary.Domain.Entities;

namespace OnlineVocabulary.API.Controllers
{
    public class CategoryController : ApiController
    {
        private readonly IVocabularyService _vocabularyService;

        public CategoryController(IVocabularyService vocabularyService) 
        {
            _vocabularyService = vocabularyService;
        }

        public PaginatedDTO<CategoryDTO> GetCategories(PaginatedRequestCommand cmd)
        {
            var сategories = _vocabularyService.GetCategories(cmd.Page, cmd.Take);

            var result = Mapper.Map<PaginatedList<Category>, PaginatedDTO<CategoryDTO>>(сategories);
            return result;
        }

        public PaginatedDTO<CategoryDTO> GetCategoriesBy(PaginatedRequestCommand cmd, string searchString)
        {
            var сategories = _vocabularyService.SearchCategoriesBy(cmd.Page, cmd.Take, searchString);

            var result = Mapper.Map<PaginatedList<Category>, PaginatedDTO<CategoryDTO>>(сategories);
            return result;
        }

        public CategoryDTO GetCategory(Guid key)
        {
            var сategory = _vocabularyService.GetCategory(key);
            if (сategory == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var result = Mapper.Map<Category, CategoryDTO>(сategory);
            return result;
        }


        public HttpResponseMessage PostCategory(CategoryRequestModel requestModel)
        {
            var categoryInstance = Mapper.Map<CategoryRequestModel, Category>(requestModel); 
            var createdCategoryResult = _vocabularyService.AddCategory(categoryInstance);

            if (!createdCategoryResult.IsSuccess)
            {
                return new HttpResponseMessage(HttpStatusCode.Conflict);
            }

            var response = Request.CreateResponse(
                                HttpStatusCode.Created,
                                Mapper.Map<Category, CategoryDTO>(createdCategoryResult.Entity)
                            );

            response.Headers.Location = new Uri(
                                Url.Link("DefaultHttpRoute",
                                new { key = createdCategoryResult.Entity.Id })
                                );

            return response;
        }

        public HttpResponseMessage PutCategory(Guid key, CategoryRequestModel requestModel)
        {
            var category = _vocabularyService.GetCategory(key);
            if (category == null)
            {

                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var categoryInstance = Mapper.Map<CategoryRequestModel, Category>(requestModel);
            var updatedCategoryResult = _vocabularyService.UpdateCategory(categoryInstance);
            if (!updatedCategoryResult.IsSuccess)
            {
                return new HttpResponseMessage(HttpStatusCode.Conflict);
            }

            var response = Request.CreateResponse(
                                HttpStatusCode.Created,
                                Mapper.Map<Category, CategoryDTO>(updatedCategoryResult.Entity)
                            );

            response.Headers.Location = new Uri(
                                Url.Link("DefaultHttpRoute",
                                new { key = updatedCategoryResult.Entity.Id })
                                );
            return response;
        }

        public HttpResponseMessage DeleteCategory(Guid key)
        {
            var category = _vocabularyService.GetCategory(key);
            if (category == null)
            {

                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var categoryRemoveResult = _vocabularyService.RemoveCategory(category);

            if (!categoryRemoveResult.IsSuccess)
            {

                return new HttpResponseMessage(HttpStatusCode.Conflict);
            }
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}
