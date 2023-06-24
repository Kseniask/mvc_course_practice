namespace Vidly.ViewModels
{
    public class MovieFormViewModel
    {
        public List<Genre> Genres { get; set; }
        public int? Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name = "Genre")]
        [Required]
        public int? GenreId { get; set; }

        [Display(Name = "Release Date")]
        [Required]
        public DateTime? ReleaseDate { get; set; }

        [Display(Name = "Date Added")]
        [Required]
        public DateTime? DateAdded { get; set; }

        [Display(Name = "Number In Stock")]
        [Range(1,20)]
        [Required]
        public int? NumberInStock { get; set; }

        public string Title { get
            {
                return Id != 0 ? "Edit Movie" : "New Movie";
            }
        }

        public MovieFormViewModel()
        {
            Id = 0;
        }

        public MovieFormViewModel(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            DateAdded = movie.DateAdded;
            ReleaseDate = movie.ReleaseDate;
            NumberInStock = movie.NumberInStock;
        }
    }
}
