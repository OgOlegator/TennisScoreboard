namespace TennisScoreboard.WebApp.Models
{
    public class PageViewModel
    {
        public int Number { get; private set; }

        public int Total { get; private set; }

        public PageViewModel(int countEntries, int pageNumber, int pageSize)
        {
            Number = pageNumber;
            Total = (int)Math.Ceiling(countEntries / (double)pageSize);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (Number > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (Number < Total);
            }
        }
    }
}
