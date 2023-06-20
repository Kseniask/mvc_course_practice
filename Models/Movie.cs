namespace Vidly.Models
{
	public class Movie
	{
		public int Id { get; set; }

        public string Name { get; set; }

        public Genre Genre { get; set; }

        public int GenreId { get; set; }

        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; }

        [Display(Name = "# In Stock")]
        public int NumberInStock { get; set; }

    }


}

