using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace FastBus.Web.Extensions
{
    public static class ModelStateExtensions
    {
        public static string GetErrors(this ModelStateDictionary model, string separator = ", ")
        {
            return string.Join(separator, model.SelectMany(x => x.Value.Errors.Select(e => e.ErrorMessage)));
        }

        public static void AddIdentityErrors(this ModelStateDictionary model, IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                model.AddModelError("", error);
            }
        }
    }
}