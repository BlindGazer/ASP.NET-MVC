using System.ComponentModel;

namespace FastBus.Domain.Objects
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

        public ColumnOrder() { }

        public ColumnOrder(string column, SortDirection direction = SortDirection.Asc)
        {
            Column = column;
            Direction = direction;
        }
    }
}
