using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FCM.API.REST.Contracts;
using Framework.Application.Contracts;
using FCM.Application;
using FCM.Application.Contracts;
using AutoMapper;
using Framework.Infrastruture;

namespace FCM.API.REST.Controllers
{
    [Route("api/[controller]")]
    public class ComponentController : RestBaseController
    {
        public ComponentController(IFCMApplication application, IMapper mapper, IRequestIdGenerator requestIdGenerator) : base(application, mapper, requestIdGenerator)
        {
        }

        // GET api/values
        [HttpGet]
        /// <summary>
        /// Returns all component configurations
        /// Requires Sysadmin role
        /// </summary>
        /// <returns></returns>
        public RestResponseWithData<IEnumerable<ComponentModel>> Get()
        {
            var inputEnvelop = this.GetInputEnvelop(new VoidDTO());
            var result = this.application.GetAllComponentOperation.Execute(inputEnvelop);
            return this.GetResponseObjectForData<IEnumerable<ComponentDTO>, IEnumerable<ComponentModel>>(result);
        }

        // POST api/values
        [HttpPost]
        /// <summary>
        /// Create or update a component configuration
        /// Requires Sysadmin role
        /// </summary>
        /// <returns></returns>
        public RestResponse Post([FromBody]ComponentModel value)
        {
            var inputEnvelop = this.GetInputEnvelop(this.mapper.Map<ComponentModel, ComponentDTO>(value));
            var result = this.application.AddOrUpdateComponentOperation.Execute(inputEnvelop);
            return this.GetResponseObject(result);
        }
        
        // DELETE api/values/5
        [HttpDelete("{componentName}")]
        /// <summary>
        /// Delete a component configuration
        /// Requires Sysadmin role
        /// </summary>
        /// <returns></returns>
        public RestResponse Delete(string componentName)
        {
            var result = this.application.RemoveComponentOperation.Execute(this.GetInputEnvelop(componentName));
            return this.GetResponseObject(result);
        }
    }
}
