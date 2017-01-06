namespace FastBus.DAL.Objects
{
    public class Paging
    {
        public int Skip => (Page - 1) * Length;
        public int Length { get; set; }
        public int Page { get; set; }

        public Paging()
        {
            Length = 4;
            Page = 1;
        }
    }
}
