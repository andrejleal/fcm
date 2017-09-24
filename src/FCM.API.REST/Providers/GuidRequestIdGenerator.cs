using Framework.Infrastruture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FCM.API.REST.Providers
{
    public class GuidRequestIdGenerator : IRequestIdGenerator
    {
        public string Generate()
        {
            return Guid.NewGuid().ToString().Replace("-","");
        }
    }
}
