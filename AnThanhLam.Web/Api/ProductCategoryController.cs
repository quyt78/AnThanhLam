using AnThanhLam.Model.Models;
using AnThanhLam.Service;
using AnThanhLam.Web.Infrastructure.Core;
using AnThanhLam.Web.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AnThanhLam.Web.Api
{
    [RoutePrefix("api/productcategory")]
    public class ProductCategoryController : ApiControllerBase
    {
        IProductCategoryService _productCategoryService;

        public ProductCategoryController(IErrorService errorService, IProductCategoryService productCategoryService) : base(errorService) {
            this._productCategoryService = productCategoryService;
        }

        [HttpGet]
        [Route("getall")]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _productCategoryService.GetAll();
                var reponseData = Mapper.Map<IEnumerable<ProductCategory>, List<ProductCategoryViewModel>>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, reponseData);
                return response;
            });
        }

    }
}
