using System;
using System.ComponentModel.DataAnnotations;

namespace FastBus.Web.Extensions
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class RangeUntilCurrentYearAttribute : RangeAttribute
    {
        public RangeUntilCurrentYearAttribute(int minimum) : base(minimum, DateTime.Now.Year)
        {
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class RangeAfterCurrentYearAttribute : RangeAttribute
    {
        public RangeAfterCurrentYearAttribute(int maximum = 3000) : base(DateTime.Now.Year, maximum)
        {
        }
    }
}