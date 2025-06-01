using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tech.Domain.Interfaces.Services;
using Tech.Domain.Requests;

namespace Tech.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController(IGameService gameService) : Controller
    {
        private readonly IGameService _gameService = gameService;

        /// <summary>
        /// Get all games
        /// </summary>
        /// <returns>List of games</returns>
        /// <response code="200">Success</response>
        /// <response code="500">Internal error</response>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var games = await _gameService.GetAll();
            return Ok(games);
        }

        /// <summary>
        /// Get a game by ID
        /// </summary>
        /// <param name="id">Game ID</param>
        /// <returns>Game object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Game not found</response>
        /// <response code="500">Internal error</response>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var game = await _gameService.GetId(id);
            if (game == null)
                return NotFound();

            return Ok(game);
        }

        /// <summary>
        /// Create a new game
        /// </summary>
        /// <param name="request">Game object</param>
        /// <remarks>Example: 
        /// {
        ///     "nome": "FIFA 24",
        ///     "categoria": "Sports",
        ///     "plataforma": "PlayStation",
        ///     "dataLancamento": "2024-09-01"
        /// }
        /// </remarks>
        /// <returns>Game created</returns>
        /// <response code="201">Created</response>
        /// <response code="400">Invalid data</response>
        /// <response code="500">Internal error</response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddGame([FromBody] GameRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _gameService.AddGame(request);
            return Created("", result);
        }

        /// <summary>
        /// Update an existing game
        /// </summary>
        /// <param name="request">Updated game data</param>
        /// <param name="id">Game ID</param>
        /// <returns>Updated game</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Game not found</response>
        /// <response code="500">Internal error</response>
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateGame([FromBody] GameRequest request, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _gameService.GetId(id);
            if (existing == null)
                return NotFound();

            var updated = await _gameService.UpdateGame(request, id);
            return Ok(updated);
        }

        /// <summary>
        /// Delete a game by ID
        /// </summary>
        /// <param name="id">Game ID</param>
        /// <returns>Game deleted</returns>
        /// <response code="200">Deleted</response>
        /// <response code="404">Game not found</response>
        /// <response code="500">Internal error</response>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteGame([FromRoute] int id)
        {
            var existing = await _gameService.GetId(id);
            if (existing == null)
                return NotFound();

            var deleted = await _gameService.Delete(id);
            return Ok(new { message = "Game deleted successfully", deleted });
        }
    }
}
