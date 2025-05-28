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
        private readonly IUsersService _usersService = usersService;
        private readonly ITokenService _tokenService = tokenService;

        /// <summary>
        /// Get all users (Admin only)
        /// </summary>
        /// <returns>List of users</returns>
        /// <response code="200">Success</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal error</response>
        [HttpGet]
        [Authorize(Roles = nameof(TypePermissionEnum.Admin))]
        public async Task<IActionResult> GetAll()
        {
            var users = await _usersService.GetAll();
            return Ok(users);
        }

        /// <summary>   
        /// Add a new user (Admin only)
        /// </summary>
        /// <remarks>Example: 
        /// {
        ///     "name": "admin",
        ///     "password": "123456",
        ///     "email": "admin@tech.com",
        ///     "permission": 1
        /// }
        /// </remarks>
        /// <param name="request">New user data</param>
        /// <returns>User created</returns>
        /// <response code="201">Created</response>
        /// <response code="400">Validation error</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal error</response>
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
        /// Update an existing user (Admin only)
        /// </summary>
        /// <param name="request">Updated user data</param>
        /// <param name="id">User ID</param>
        /// <returns>User updated</returns>
        /// <response code="200">Success</response>
        /// <response code="404">User not found</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal error</response>
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
        /// Delete a user by ID (Admin only)
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>User deleted</returns>
        /// <response code="200">Success</response>
        /// <response code="404">User not found</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal error</response>
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
        /// Get JWT token for user login
        /// </summary>
        /// <param name="user">User credentials</param>
        /// <returns>JWT token</returns>
        /// <response code="200">Token created</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal error</response>
        [HttpGet("token")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTokenUser([FromQuery] TokenRequest user)
        {
            var token = await _tokenService.ValidateInCacheToken(user);

            if (string.IsNullOrEmpty(token))
                return Unauthorized();

            return Ok(token);
        }
        [HttpGet("test-error")]
        [AllowAnonymous]
        public IActionResult ThrowError()
        {
            throw new Exception("Erro de teste para verificar o middleware.");
        }
    }
}
