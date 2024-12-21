using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ZGT.Trouble.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : GenericController<Game, GameManager, TroubleEntities>
    {
        public GameController(ILogger<GameController> logger, DbContextOptions<TroubleEntities> options) : base(logger, options)
        {

        }

        /// <summary>
        /// Try to make a move
        /// </summary>
        /// <param name="game"></param>
        /// <param name="startSquare"></param>
        /// <param name="endSquare"></param>
        /// <returns></returns>
        [HttpPut("{startSquare}" + "/" + "{endSquare}" + "/1")]
        public async Task<ActionResult> MakeMoveAsync([FromBody] Game game, int startSquare, int endSquare)
        {
            try
            {
                var result = await base.manager.MakeMoveAsync(game, startSquare, endSquare);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Skip your move
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        [HttpPut("skip/")]
        public async Task<ActionResult> SkipMoveAsync([FromBody] Game game)
        {
            try
            {
                var result = await base.manager.SkipMoveAsync(game);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
