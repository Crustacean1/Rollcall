using Microsoft.AspNetCore.Mvc;

using Rollcall.Models;
using Rollcall.Repositories;
using Rollcall.Services;

namespace Rollcall.Controllers{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase{
        private readonly ILogger<UserController> _logger;
        private readonly AuthorizationService _authService;
        private readonly UserRepository _repository;
        public UserController(ILogger<UserController> logger,UserRepository repository,AuthorizationService authService) {
            _logger = logger;
            _repository = repository;
            _authService = authService;
        }
        [HttpPut]
        public async Task<ActionResult> RegisterUser([FromBody] UserRegistrationDto userDto){
            _logger.LogInformation("Reached registration endpoint");
            var newUser = _authService.CreateUser(userDto);
            _logger.LogInformation($"new login: {newUser.Login}");
            
            _repository.AddUser(newUser);
            await _repository.SaveChangesAsync();
            return CreatedAtAction(null,null);
        }

        [HttpPost]
        public ActionResult LoginUser([FromBody] UserRegistrationDto userDto){
            _logger.LogInformation("Reached logging endpoint");
            var token = _authService.AuthorizeUser(userDto);
            if(token == null){
                return Unauthorized();
            }
            return Ok(new TokenDto{Token = token});
        }
    }
}