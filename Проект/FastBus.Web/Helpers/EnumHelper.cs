using System.ComponentModel;
using System.Reflection;

namespace FastBus.Web.Helpers
{
    public class EnumHelper
    {
        public static string GetDescription(object enumValue)
        {
            FieldInfo fi = enumValue.GetType().GetField(enumValue.ToString());

            if (fi != null)
            {
                DescriptionAttribute attr = fi.GetCustomAttribute<DescriptionAttribute>(true);
                if (attr != null)
                {
                    return attr.Description;
                }
            }
            return null;
        }
    }
}