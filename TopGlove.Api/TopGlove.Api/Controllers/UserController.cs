using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TopGlove.Api.Data;
using TopGlove.Api.Model;

namespace TopGlove.Api.Controllers
{
    [Route("api/[controller]/v1")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ProductQualityDbContext _dbContext;

        public UserController(ProductQualityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            if(!string.IsNullOrEmpty(loginModel.UserId))
            {
                var user = _dbContext.Users.FirstOrDefault(x => x.UserId == loginModel.UserId);

                if(user != null)
                {
                    return Ok(user);
                }
            }

            return Ok(false);
        }
    }
}
