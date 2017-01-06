namespace FastBus.DAL.Objects
{
    public class BaseQuery
    {
        public ColumnOrder OrderBy { get; set; }
        public Paging Paging { get; set; }
    }
}
