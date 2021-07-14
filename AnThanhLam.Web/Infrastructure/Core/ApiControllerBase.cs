using AnThanhLam.Model.Models;
using AnThanhLam.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AnThanhLam.Web.Infrastructure.Core
{
    public class ApiControllerBase : ApiController
    {
        private IErrorService _errorService ;
        protected HttpResponseMessage CreateHttpResponse(HttpResponseMessage responseMessage, Func<HttpResponseMessage> func)
        {
            HttpResponseMessage response = null;
            try
            {
                response = func.Invoke();
            }
            catch (Exception ex)
            {
                LogError(ex);
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
