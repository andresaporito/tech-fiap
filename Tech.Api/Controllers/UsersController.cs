using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tech.Domain.Enums;
using Tech.Domain.Interfaces.Services;
using Tech.Domain.Interfaces.Token;
using Tech.Domain.Requests;

namespace Tech.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(IUsersService usersService, ITokenService tokenService) : Controller
    {
        private readonly ITokenService _tokenService = tokenService;

        private readonly IUsersService _usersService = usersService;
        /// <summary>
        /// Required token
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Success</response>
        /// <response code="400">Not Authorize</response>
        /// <response code="500">Erro</response>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var find = await _usersService.GetAll();
            var current = User;
            return Ok(find);
        }

        /// <summary>   
        /// Add new user
        /// </summary>
        /// <remarks>Ex: de requisicao. 
        /// {
        ///     Name: "ex",
        ///     Pass: "123",
        ///     TypePermission: 1
        /// }
        /// </remarks>
        /// <returns></returns>
        /// <param name="request">object user</param>
        /// <response code="201">Created</response>
        /// <response code="400">Not Authorize</response>
        /// <response code="500">Error</response>
        [HttpPost]
        [Authorize(Roles = nameof(TypePermissionEnum.Admin))]
        public async Task<IActionResult> AddUser([FromBody] NewUsersRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _usersService.AddUsers(request);

            return Created("", response);
        }
        /// <summary>
        /// Update exists user
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Created</response>
        /// <response code="404">Not Authorize</response>
        /// <response code="500">Error</response>
        [HttpPut("{id}")]
        [Authorize(Roles = nameof(TypePermissionEnum.Admin))]
        public async Task<IActionResult> UpdateUser([FromBody] UsersRequest request, [FromRoute] int id)
        {
            var find = await _usersService.GetId(id);

            if (find == null)
                return NotFound();

            await _usersService.UpdateUsers(request, id);

            return Ok(find);
        }
        /// <summary>
        /// Delete user
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Created</response>
        /// <response code="404">Not Authorize</response>
        /// <response code="500">Error</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(TypePermissionEnum.Admin))]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            var find = await _usersService.GetId(id);

            if (find == null)
                return NotFound();

            await _usersService.Delete(id);

            return Ok(find);
        }
        /// <summary>
        /// get token from user 
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Created</response>
        /// <response code="401">Not Authorize</response>
        /// <response code="500">Error</response>
        [HttpGet("token")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTokenUser([FromQuery] TokenRequest user)
        {
            var token = await _tokenService.ValidateInCacheToken(user);

            if (string.IsNullOrEmpty(token))
                return Unauthorized();

            return Ok(token);
        }


    }
}
