using AnThanhLam.Model.Models;
using AnThanhLam.Service;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AnThanhLam.Web.Infrastructure.Core
{
    public class ApiControllerBase : ApiController
    {
        private IErrorService _errorService ;
        protected HttpResponseMessage CreateHttpResponse(HttpRequestMessage requestMessage, Func<HttpResponseMessage> func)
        {
            HttpResponseMessage response = null;
            try
            {
                response = func.Invoke();
            }
            catch (DbEntityValidationException dbVaEx)
            {
                foreach(var eve in dbVaEx.EntityValidationErrors)
                {
                    Trace.WriteLine($"Entity of type\"{eve.Entry.GetType().Name}\" in state \"{eve.Entry.State}\" has following validation errors:");
                    foreach(var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine($"Property: \"{ve.PropertyName}\", Error: {ve.ErrorMessage}\"");
                    }
                }    
                LogError(dbVaEx);
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, dbVaEx.InnerException.Message);
            }
            catch (DbUpdateException dbEx)
            {
                LogError(dbEx);
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, dbEx.InnerException.Message);
            }
            catch (Exception ex)
            {
                LogError(ex);
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            return response;
        }
        public ApiControllerBase(IErrorService errorService)
        {
            this._errorService = errorService;
        }

        private void LogError(Exception ex)
        {
            try
            {
                Error error = new Error();
                error.CreatedDate = DateTime.Now;
                error.Message = ex.Message;
                error.StackTrace = ex.StackTrace;
                _errorService.Create(error);
                _errorService.Save();
            }
            catch
            {

            }
        }
    }
}
