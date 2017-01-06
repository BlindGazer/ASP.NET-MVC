using System.ComponentModel;

namespace FastBus.DAL.Enums
{
    public enum TypeReview
    {
        [Description("Жалоба")]
        Сomplaint,
        [Description("Предложение")]
        Suggestion,
        [Description("Отзыв")]
        Review,
        [Description("Другое")]
        Other
    }
}
