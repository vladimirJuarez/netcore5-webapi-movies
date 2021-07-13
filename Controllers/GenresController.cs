using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using back_end.DTOs;
using back_end.Entidades;
using back_end.Filters;
using back_end.Repositories;
using back_end.Utilities;
using back_end.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace back_end.Controllers
{
    [Route("api/genres")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GenresController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly ILogger<GenresController> _logger;
        private readonly MoviesDbContext _context;
        private readonly IMapper _mapper;

        public GenresController(IRepository repository, 
            ILogger<GenresController> logger, 
            MoviesDbContext context,
            IMapper mapper) {
            _repository = repository;
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        // GET
        [HttpGet]//api/genres
        //[HttpGet("listado")]
        //[ResponseCache(Duration = 60)]
        //[ServiceFilter(typeof(MyFilterAction))]
        public  async Task<ActionResult<List<GenreDTO>>> GetAll([FromQuery] PaginationDTO paginationDto)
        {
            var queryable = _context.Genres.AsQueryable();
            await HttpContext.InsertParamsHeaderPagination(queryable);
            var genres = await queryable.OrderBy(x => x.Name).Pagination(paginationDto).ToListAsync();
            return _mapper.Map<List<GenreDTO>>(genres);
            /*var genres = _repository.Get();

            if (genres is null)
            {
                _logger.LogError("No genres found");
                return BadRequest();
            }
            _logger.LogInformation("genres list was founded");
            return Ok(genres);*/
        }

        /*[HttpGet("{Id:int}/{Name=vlad}")]// api/genres/1
        public async Task<ActionResult<Genre>> Get(int id, string name)
        {
            _logger.LogInformation($"getting a genre by id {id}");
            var genre = await _repository.GetById(id);

            if (genre is null)
            {
                throw new ApplicationException($"Genre with id {id} was not found");
                return NotFound();
            }

            return genre;
        }*/
        
        //api/genres/id
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<GenreDTO>>Get(int id)
        {
            var genre = await _context.Genres
                .FirstOrDefaultAsync(x => x.Id == id);

            if (genre is null)
            {
                return NotFound();
            }

            return _mapper.Map<GenreDTO>(genre);
        }
        

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GenreCreationDTO genreCreationDto)
        {
            var genre = _mapper.Map<Genre>(genreCreationDto);
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
            
            return NoContent();
        }

        [HttpPut("{Id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] GenreCreationDTO genreCreationDto)
        {
            var genre = await _context.Genres
                .FirstOrDefaultAsync(x => x.Id == id);

            if (genre is null)
            {
                return NotFound();
            }

            genre = _mapper.Map(genreCreationDto, genre);

            await _context.SaveChangesAsync();
            return NoContent();

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await _context.Genres.AnyAsync(x => x.Id == id);
            if (!exists)
            {
                return NotFound();
            }

            _context.Remove(new Genre() {Id = id});
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}