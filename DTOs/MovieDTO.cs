namespace Vidly.DTOs
{
    public class MovieDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public GenreDTO Genre { get; set; }
        public int GenreId { get; set; }

        [Display(Name = "Release Date")]
        [Required]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Date Added")]
        [Required]
        public DateTime DateAdded { get; set; }

        [Display(Name = "# In Stock")]
        [Required]
        [RegularExpression(@"^[1-20]{1,4}$", ErrorMessage = "The field Number In Stock must be between 1 and 20.")]
        public int NumberInStock { get; set; }
    }
}
