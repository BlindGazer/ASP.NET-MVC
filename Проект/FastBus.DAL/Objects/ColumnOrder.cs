using System.ComponentModel;

namespace FastBus.DAL.Objects
{
   public class ColumnOrder
    {
        [DisplayName("Сортировать")]
        public string Column { get; set; }
        [DisplayName("По убыванию")]
        public SortDirection Direction { get; set; }

        public override string ToString()
        {
            return $"{Column} {Direction.ToString().ToUpperInvariant()}";
        }
    }
}
