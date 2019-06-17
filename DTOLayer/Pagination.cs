namespace DTOLayer
{
    public class Pagination
    {
        private int _pageNumber ;

        public int PageNumber
        {
            get { return (_pageNumber -1) * PageSize; }

            set { _pageNumber = value; }
        }
        public int PageSize { get; set; }
        public int TotalOfSearch { get; set; }
        public int TotalCount => TotalOfSearch/PageSize;

        public Pagination(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
