using AutoMapper;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Vidly.DTOs;

namespace Vidly.Controllers.Api
{
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;
        public MoviesController(IConfiguration configuration, IMapper mapper)
        {
            _context = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>(), configuration);
            _mapper = mapper;
        }
        //GET /api/movies
        public IEnumerable<MovieDTO> GetMovies()
        {
            return _context.Movies.ToList().Select(_mapper.Map<Movie, MovieDTO>);
        }

        //GET /api/movie
        [HttpGet("{id}")]
        public ActionResult<MovieDTO> GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null) return NotFound();

            return Ok(_mapper.Map<Movie, MovieDTO>(movie));
        }

        // POST /api/movie
        public ActionResult<MovieDTO> CreateMovie(MovieDTO movieDTO)
        {
            if (!ModelState.IsValid) return BadRequest();
            var movie = _mapper.Map<MovieDTO, Movie>(movieDTO);
            _context.Movies.Add(movie);
            _context.SaveChanges();
            movieDTO.Id = movie.Id; 
            return Created(new Uri(Request.GetDisplayUrl() + "/" + movie.Id), movieDTO);
        }

        // PUT /api/movie/1
        [HttpPut]
        public void UpdateMovie(int id, MovieDTO movieDTO) {
            if (!ModelState.IsValid) throw new BadHttpRequestException("Invalid entry");

            var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movieInDb == null) throw new BadHttpRequestException("Movie not found");

            _mapper.Map(movieDTO, movieInDb);
            _context.SaveChanges();
        }

        // DELETE /api/movie/1
        [HttpDelete]
        public void DeleteMovie(int id)
        {
            if (!ModelState.IsValid) throw new BadHttpRequestException("Invalid entry");

            var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movieInDb == null) throw new BadHttpRequestException("Movie not found");

            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();
        }

    }
}
