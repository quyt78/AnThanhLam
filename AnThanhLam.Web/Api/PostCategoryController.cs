using AnThanhLam.Model.Models;
using AnThanhLam.Service;
using AnThanhLam.Web.Infrastructure.Core;
using AnThanhLam.Web.Models;
using AutoMapper;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AnThanhLam.Web.Infrastructure.Extensions;

namespace AnThanhLam.Web.Api
{
    [RoutePrefix("api/postcategory")]
    public class PostCategoryController : ApiControllerBase
    {
        IPostCategoryService _postCategoryService;

        public PostCategoryController(IErrorService errorService, IPostCategoryService postCategoryService) : base(errorService)
        {
            this._postCategoryService = postCategoryService;
        }
        [Route("getall")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {

                var listCategory = _postCategoryService.GetAll();

                var listPostcategory = Mapper.Map<List<PostCategoryViewModel>>(listCategory);
                HttpResponseMessage reponse = request.CreateResponse(HttpStatusCode.OK, listPostcategory);

                return reponse;
            });
        }

        [Route("add")]
        public HttpResponseMessage Post(HttpRequestMessage request, PostCategoryViewModel postCategoryVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage reponse = null;
                if (ModelState.IsValid)
                {
                    request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    PostCategory newPostCategory = new PostCategory();
                    newPostCategory.UpdatePostCategory(postCategoryVM);
                    var category = _postCategoryService.Add(newPostCategory);
                    _postCategoryService.Save();

                    reponse = request.CreateResponse(HttpStatusCode.Created, category);
                }
                return reponse;
            });
        }

        [Route("update")]
        public HttpResponseMessage Put(HttpRequestMessage request, PostCategoryViewModel postCategory)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage reponse = null;
                if (ModelState.IsValid)
                {
                    request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var postCategoryDb = _postCategoryService.GetById(postCategory.ID);
                    postCategoryDb.UpdatePostCategory(postCategory);
                    _postCategoryService.Update(postCategoryDb);
                    _postCategoryService.Save();

                    reponse = request.CreateResponse(HttpStatusCode.OK);
                }
                return reponse;
            });
        }

        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage reponse = null;
                if (ModelState.IsValid)
                {
                    request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var category = _postCategoryService.Delete(id);
                    _postCategoryService.Save();

                    reponse = request.CreateResponse(HttpStatusCode.OK, category);
                }
                return reponse;
            });
        }
    }
}
