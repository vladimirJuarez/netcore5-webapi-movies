using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using back_end.DTOs;
using back_end.Entidades;
using back_end.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace back_end.Controllers
{
    [Route("api/actors")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ActorController> _logger;
        private readonly MoviesDbContext _context;
        private readonly ILocalFileStorage _localFileStorage;
        private readonly string _container = "actors";

        public ActorController(
            IMapper mapper,
            ILogger<ActorController> logger,
            MoviesDbContext context,
            ILocalFileStorage localFileStorage)
        {
            _mapper = mapper;
            _logger = logger;
            _context = context;
            _localFileStorage = localFileStorage;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ActorCreationDTO actorCreationDto)
        {
            var actor = _mapper.Map<Actor>(actorCreationDto);

            if (actorCreationDto.Picture != null)
            {
                actor.Picture = await _localFileStorage.SaveFile(_container, actorCreationDto.Picture);
            }
            await _context.Actors.AddAsync(actor);
            
            await _context.SaveChangesAsync();
            _logger.LogInformation("Actor was created");
            return NoContent();
        }
        
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<ActorDTO>>Get(int id)
        {
            var actor = await _context.Actors
                .FirstOrDefaultAsync(x => x.Id == id);

            if (actor is null)
            {
                return NotFound();
            }

            return _mapper.Map<ActorDTO>(actor);
        }
        
        [HttpGet]//api/genres
        //[HttpGet("listado")]
        //[ResponseCache(Duration = 60)]
        //[ServiceFilter(typeof(MyFilterAction))]
        public  async Task<ActionResult<List<ActorDTO>>> GetAll([FromQuery] PaginationDTO paginationDto)
        {
            var queryable = _context.Actors.AsQueryable();
            await HttpContext.InsertParamsHeaderPagination(queryable);
            var actors = await queryable.OrderBy(x => x.Name).Pagination(paginationDto).ToListAsync();
            return _mapper.Map<List<ActorDTO>>(actors);
            /*var genres = _repository.Get();

            if (genres is null)
            {
                _logger.LogError("No genres found");
                return BadRequest();
            }
            _logger.LogInformation("genres list was founded");
            return Ok(genres);*/
        }
        
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await _context.Actors.AnyAsync(x => x.Id == id);
            if (!exists)
            {
                return NotFound();
            }

            _context.Remove(new Actor() {Id = id});
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}