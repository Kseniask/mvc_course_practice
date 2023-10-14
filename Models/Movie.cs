namespace Vidly.Models
{
	public class Movie
	{
		public int Id { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        [StringLength(255)]
        public string Name { get; set; }

        public Genre Genre { get; set; }

        public int GenreId { get; set; }

        [Display(Name = "Release Date")]
        [Required(ErrorMessage = "The Release Date field is required.")]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Date Added")]
        [Required(ErrorMessage = "The Date Added field is required.")]
        public DateTime DateAdded { get; set; }

        [Display(Name = "# In Stock")]
        [Required(ErrorMessage = "The Number In Stock field is required.")]
        [RegularExpression(@"^[1-20]{1,4}$", ErrorMessage = "The field Number In Stock must be between 1 and 20.")]
        public int NumberInStock { get; set; }

        public int NumberAvailable { get; set; }

    }


}

