using Common.DataTransferObjects;
using Common.Domain;
using DataLayer.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesService _moviesService;

        public MoviesController(IMoviesService moviesService)
        {
            _moviesService = moviesService;
        }

        // GET: Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAllMovieDTO>>> GetMovies()
        {
            try
            {
                var movies = await _moviesService.GetAllMovies();
                return Ok(movies);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetOneMovieDTO>> GetMovie(string id)
        {
            try
            {
                var movie = await _moviesService.GetOneMovie(id);
                return Ok(movie);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // GET: Movies/BestMatchQuery?keywords=Action&keywords=Comedy
        [HttpGet("BestMatchQuery")]
        public async Task<ActionResult<IEnumerable<BestMatch>>> BestMatchQuery([FromQuery] string[] keywords)
        {
            try
            {
                var bestMatchQuery = await _moviesService.BestMatchQuery(keywords);
                return Ok(bestMatchQuery);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: Movies/ExactMatch?keywords=Action&keywords=Comedy
        [HttpGet("ExactMatch")]
        public async Task<ActionResult<IEnumerable<string>>> ExactMatchQuery([FromQuery] string[] keywords)
        {
            try
            {
                var exactMatchQuery = await _moviesService.ExactMatchQuery(keywords);
                return Ok(exactMatchQuery);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: Movies/FindSimilarMovies/13
        [HttpGet("FindSimilarMovies")]
        public async Task<ActionResult<IEnumerable<SimilarMovie>>> FindSimilarMovies([FromQuery] string movieId)
        {
            try
            {
                var similarMovies = await _moviesService.FindSimilarMovies(movieId);
                return Ok(similarMovies);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: Movies/WordToWord?keywords=Action&keywords=Comedy
        [HttpGet("WordToWords")]
        public async Task<ActionResult<IEnumerable<WordFrequency>>> WordToWordsQuery([FromQuery] string[] keywords)
        {
            try
            {
                var wordToWord = await _moviesService.WordToWordsQuery(keywords);
                return Ok(wordToWord);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<AddAndUpdateMovieDTO>> PutMovie([FromBody] Movie movie)
        {
            try
            {
                var putMovie = await _moviesService.UpdateMovie(movie);
                return Ok(putMovie);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AddAndUpdateMovieDTO>> PostMovie([FromBody] CreateMovieDTO movie)
        {
            try
            {
                var postMovie = await _moviesService.AddMovie(movie);
                return Ok(postMovie);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(string id)
        {
            try
            {
                var result = await _moviesService.DeleteMovie(id);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}