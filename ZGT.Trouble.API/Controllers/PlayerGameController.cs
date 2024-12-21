using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;

namespace ZGT.Trouble.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerGameController : GenericController<PlayerGame, PlayerGameManager, TroubleEntities>
    {
        public PlayerGameController(ILogger<PlayerGameController> logger, DbContextOptions<TroubleEntities> options) : base(logger, options)
        {

        }
    }
}
