using MovieApp.Models.Dto.Movie;

namespace MovieApp.Models.Dto.Pager
{
    public class PagerVM
    {
        public List<UpdateMovie> Movies { get; set; }
        public UpdateMovie Movie { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalMovies { get; set; }
        public int TotalPages { get; set; }
    }
}
