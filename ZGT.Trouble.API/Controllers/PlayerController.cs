using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ZGT.Trouble.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : GenericController<Player, PlayerManager, TroubleEntities>
    {
        public PlayerController(ILogger<PlayerController> logger, DbContextOptions<TroubleEntities> options) : base(logger, options)
        {

        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        [HttpPost("login/")]
        public async Task<ActionResult> Login([FromBody] Player player)
        {
            try
            {
                var result = (await manager.LoginAsync(player));
                return Ok(result);

                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
