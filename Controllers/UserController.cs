using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Rollcall.Models;
using Rollcall.Repositories;
using Rollcall.Services;

namespace Rollcall.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly AuthorizationService _authService;
        private readonly UserRepository _repository;
        public UserController(ILogger<UserController> logger, UserRepository repository, AuthorizationService authService)
        {
            _logger = logger;
            _repository = repository;
            _authService = authService;
        }

        [HttpPost]
        public async Task<ActionResult> LoginUser([FromBody] UserRegistrationDto userDto)
        {
            _logger.LogInformation("Reached logging endpoint");
            if (_authService.ShouldRegisterUser(userDto))
            {
                await _authService.CreateUser(userDto);
            }
            var token = _authService.AuthorizeUser(userDto);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(new TokenDto { Token = token });
        }
        [HttpGet,Authorize]
        public ActionResult CheckTokenValidity(){
            return Ok();
        }
    }
}