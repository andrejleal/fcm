using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace FCM.DomainModel.Entities
{
    public class Principal : IPrincipal, IIdentity
    {

        public IIdentity Identity
        {
            get
            {
                return this;
            }
        }

        public string AuthenticationType
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsAuthenticated
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }
        public ExternalSystem Application { get; private set; }

        public Principal(ExternalSystem application)
        {
            this.IsAuthenticated = true;
            this.Application = application;
        }

        public bool IsInRole(string role)
        {
            if (role == Consts.Roles.SysAdmin && this.Application.IsSysAdmin)
            {
                return true;
            }
            return false;
        }
    }
}
