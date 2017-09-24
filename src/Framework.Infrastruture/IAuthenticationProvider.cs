using System.Security.Principal;

namespace Framework.Infrastruture
{
    public interface IAuthenticationProvider
    {
        IPrincipal Authenticate(string token);
        string GetSecuredToken(string token);
    }
}
