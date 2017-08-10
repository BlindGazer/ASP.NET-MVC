using System.Security.Principal;
using Microsoft.AspNet.Identity;

namespace FastBus.Web.Extensions
{
    public static class IdentityExtensions
    {
        public static int GetUserIdInt(this IIdentity identity)
        {
            return int.TryParse(identity.GetUserId<string>(), out int id) ? id : 0;
        }
    }
}