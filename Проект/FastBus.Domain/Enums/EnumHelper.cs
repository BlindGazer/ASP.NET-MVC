using System.ComponentModel;
using System.Reflection;

namespace FastBus.Domain.Enums
{
    public class EnumHelper
    {
        public static string GetDescription(object enumValue)
        {
            FieldInfo fi = enumValue.GetType().GetField(enumValue.ToString());

            return fi?.GetCustomAttribute<DescriptionAttribute>(true)?.Description;
        }
    }
}