using AutoMapper;
using FCM.API.REST.Contracts;
using FCM.Application;
using Framework.Application.Contracts;
using Framework.DomainModel;
using Framework.Infrastruture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Internal.Networking;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FCM.API.REST.Controllers
{
    public class RestBaseController : Controller
    {
        protected readonly IFCMApplication application;
        protected readonly IMapper mapper;
        private IRequestIdGenerator requestIdGenerator;

        public RestBaseController(IFCMApplication application, IMapper mapper, IRequestIdGenerator requestIdGenerator)
        {
            this.application = application;
            this.mapper = mapper;
            this.requestIdGenerator = requestIdGenerator;
        }

        private string GetAuthenticationToken()
        {
            string token = null;
            string authorization = this.GetHeaderValue("Authorization");

            if (string.IsNullOrEmpty(authorization))
            {
                return null;
            }

            if (authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                token = authorization.Substring("Bearer ".Length).Trim();
            }

            return token;
        }

        private string GetHeaderValue(string headerName)
        {
            var value = Request.Headers[headerName];

            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            return value;
        }

        protected InputEnvelop<TDTO> GetInputEnvelop<TDTO>(TDTO data)
        {
            var requestID = this.requestIdGenerator.Generate();
            var authenticationToken = this.GetAuthenticationToken();
            var correlationId = this.GetHeaderValue("Correlation-Id");
            return new InputEnvelop<TDTO>(requestID, correlationId, authenticationToken, data);
        }

        protected RestResponseWithData<TModel> GetResponseObjectForData<TDTO, TModel>(OutputEnvelop<TDTO> outputEnvelop)
        {
            if (outputEnvelop.Errors != null && outputEnvelop.Errors.Any())
            {
                this.SetStatusCode(outputEnvelop);
            }

            return new RestResponseWithData<TModel>()
            {
                Data = this.mapper.Map<TDTO, TModel>(outputEnvelop.Data),
                Errors = outputEnvelop?.Errors?.ToArray()
            };
        }

        protected RestResponse GetResponseObject<TDTO>(OutputEnvelop<TDTO> outputEnvelop)
        {

            if(outputEnvelop.Errors != null && outputEnvelop.Errors.Any())
            {
                this.SetStatusCode(outputEnvelop);
            }

            return new RestResponse()
            {
                Errors = outputEnvelop?.Errors?.ToArray()
            };
        }

        private void SetStatusCode<TDTO>(OutputEnvelop<TDTO> outputEnvelop)
        {
            if (outputEnvelop.Errors.Contains(Error.AuthenticationError.Code))
            {
                Response.StatusCode = 401;
            }
            else if (outputEnvelop.Errors.Contains(Error.AuthorizationError.Code))
            {
                Response.StatusCode = 403;
            }
            else if (outputEnvelop.Errors.Contains(Error.Unexpected.Code))
            {
                Response.StatusCode = 500;
            }
            else
            {
                Response.StatusCode = 400;
            }
        }
    }
}
