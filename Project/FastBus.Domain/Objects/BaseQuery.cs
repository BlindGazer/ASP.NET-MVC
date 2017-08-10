namespace FastBus.Domain.Objects
{
    public class BaseQuery
    {
        public ColumnOrder OrderBy { get; set; }
        public Paging Paging { get; set; }

        public BaseQuery()
        {
            Paging = new Paging();
        }
    }
}
