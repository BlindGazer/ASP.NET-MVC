using System.ComponentModel;

namespace FastBus.DAL.Enums
{
    public enum StatusCar
    {
        [Description("Исправна")]
        Ok,
        [Description("Неисправна")]
        Defect,
        [Description("На ремонте")]
        Repear,
    }
}
