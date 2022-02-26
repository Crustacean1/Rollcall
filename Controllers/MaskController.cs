using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Rollcall.Repositories;
using Rollcall.Services;
using Rollcall.Models;

namespace Rollcall.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MaskController : ControllerBase
    {
        private readonly MaskRepository _repository;
        private readonly ILogger<MaskController> _logger;
        private readonly IMealParserService _mealParser;
        public MaskController(MaskRepository repository, ILogger<MaskController> logger){
            _logger = logger;
            _repository  = repository;
        }
        [HttpPost, Authorize]
        [Route("{groupId}/{year}/{month}/{day}")]
        public ActionResult setMask(int groupId,int year,int month,int day, [FromBody] Dictionary<string,bool> meals){
        }
    }
}