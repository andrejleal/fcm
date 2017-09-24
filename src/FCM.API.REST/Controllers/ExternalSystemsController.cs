using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FCM.API.REST.Contracts;
using FCM.Application;
using Framework.Application.Contracts;
using FCM.Application.Contracts;
using System;
using AutoMapper;
using Framework.Infrastruture;

namespace FCM.API.REST.Controllers
{
    [Route("api/[controller]")]
    public class ExternalSystemsController : RestBaseController
    {
        public ExternalSystemsController(IFCMApplication application, IMapper mapper, IRequestIdGenerator requestIdGenerator) : base(application, mapper, requestIdGenerator)
        {
        }

        // GET api/values
        [HttpGet]
        /// <summary>
        /// Returns all external systems.
        /// Requires Sysadmin role
        /// </summary>
        /// <returns></returns>
        public RestResponseWithData<IEnumerable<ExternalSystemResponseModel>> Get()
        {
            var inputEnvelop = this.GetInputEnvelop(new VoidDTO());
            var result = this.application.GetAllExternalSystemOperation.Execute(inputEnvelop);
            return this.GetResponseObjectForData<IEnumerable<ExternalSystemOutputDTO>, IEnumerable<ExternalSystemResponseModel>>(result);
        }

        [HttpGet("components")]
        /// <summary>
        /// Returns all component configurations for the external system associated to the authentication token
        /// </summary>
        /// <returns></returns>
        public RestResponseWithData<IEnumerable<ComponentModel>> GetExternalSystemComponents()
        {
            var inputEnvelop = this.GetInputEnvelop(new VoidDTO());
            var result = this.application.GetComponentsForExternalSystemOperation.Execute(inputEnvelop);
            return this.GetResponseObjectForData<IEnumerable<ComponentDTO>, IEnumerable<ComponentModel>>(result);
        }

        // POST api/values
        [HttpPost]
        /// <summary>
        /// Add an external system
        /// Requires Sysadmin role
        /// </summary>
        /// <returns></returns>
        public RestResponse Post([FromBody]ExternalSystemModel value)
        {
            var inputDTO = this.mapper.Map<ExternalSystemModel, ExternalSystemDTO> (value);
            var inputEnvelop = this.GetInputEnvelop(inputDTO);
            var result = this.application.AddOrUpdateExternalSystemOperation.Execute(inputEnvelop);
            return this.GetResponseObject(result);
        }

        // DELETE api/values/5
        /// <summary>
        /// Delete an external system
        /// Requires Sysadmin role
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{externalSystemName}")]
        public RestResponse Delete(string externalSystemName)
        {
            var result = this.application.RemoveExternalSystemOperation.Execute(this.GetInputEnvelop(externalSystemName));
            return this.GetResponseObject(result);
        }
    }
}
