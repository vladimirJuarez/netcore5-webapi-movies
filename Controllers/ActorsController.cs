using System.Threading.Tasks;
using AutoMapper;
using back_end.DTOs;
using back_end.Entidades;
using back_end.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}