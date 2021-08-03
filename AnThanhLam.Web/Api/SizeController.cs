using AnThanhLam.Model.Models;
using AnThanhLam.Service;
using AnThanhLam.Web.Infrastructure.Core;
using AnThanhLam.Web.Infrastructure.Extensions;
using AnThanhLam.Web.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace AnThanhLam.Web.Api
{
    [RoutePrefix("api/size")]
    public class SizeController : ApiControllerBase
    {
        #region Initialize
        private ISizeService _sizeService;

        public SizeController(IErrorService errorService, ISizeService sizeService) : base(errorService)
        {
            this._sizeService = sizeService;
        }

        #endregion

        [Route("getbyid/{id}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, string id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _sizeService.GetById(id);

                var responseData = Mapper.Map<Size, SizeViewModel>(model);

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _sizeService.GetAll(keyword);

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<Size>, IEnumerable<SizeViewModel>>(query);

                var paginationSet = new PaginationSet<SizeViewModel>()
                {
                    Items = responseData,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };
                var response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }


        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage request, SizeViewModel brandView)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var newSize = new Size();
                    newSize.UpdateSize(brandView);
                    newSize.CreatedDate = DateTime.Now;
                    _sizeService.Add(newSize);
                    _sizeService.Save();

                    var responseData = Mapper.Map<Size, SizeViewModel>(newSize);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, SizeViewModel sizeVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var dbSize = _sizeService.GetById(sizeVm.ID);

                    dbSize.UpdateSize(sizeVm);
                    dbSize.UpdatedDate = DateTime.Now;

                    _sizeService.Update(dbSize);
                    try
                    {
                        _sizeService.Save();
                    }
                    catch (DbEntityValidationException e)
                    {
                        foreach (var eve in e.EntityValidationErrors)
                        {
                            Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                eve.Entry.Entity.GetType().Name, eve.Entry.State);
                            foreach (var ve in eve.ValidationErrors)
                            {
                                Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                    ve.PropertyName, ve.ErrorMessage);
                            }
                        }
                        throw;
                    }


                    var responseData = Mapper.Map<Size, SizeViewModel>(dbSize);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage Delete(HttpRequestMessage request, string id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var oldSize = _sizeService.GetById(id);
                    _sizeService.Delete(oldSize);
                    _sizeService.Save();

                    var responseData = Mapper.Map<Size, SizeViewModel>(oldSize);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedSize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var listSize = new JavaScriptSerializer().Deserialize<List<string>>(checkedSize);
                    foreach (var item in listSize)
                    {
                        var oldSize = _sizeService.GetById(item);
                        _sizeService.Delete(oldSize);
                    }

                    _sizeService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listSize.Count);
                }

                return response;
            });
        }
    }
}
