using System.Collections.Generic;

namespace FastBus.DAL.Objects
{
    public class QueryResult<T>
    {
        public List<T> Data { get; set; }
        public int Total { get; set; }
        public int TotalFiltered { get; set; }

        public QueryResult()
        {
            Total = 0;
            TotalFiltered = 0;
            Data = new List<T>();
        }
    }
}
