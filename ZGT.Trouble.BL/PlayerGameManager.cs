using Microsoft.SqlServer.Server;

namespace ZGT.Trouble.BL
{
    public class PlayerGameManager : GenericManager<tblPlayerGame>
    {
        public PlayerGameManager(ILogger logger, DbContextOptions<TroubleEntities> options) : base(logger, options) { }
        public PlayerGameManager(DbContextOptions<TroubleEntities> options) : base(options) { }
        public PlayerGameManager() { }


        public async Task<Guid> InsertAsync(PlayerGame playerGame, bool rollback = false)
        {
            try
            {
                tblPlayerGame row = Map<PlayerGame, tblPlayerGame>(playerGame);
                return await base.InsertAsync(row, e => e.PlayerId == playerGame.PlayerId && e.GameId == playerGame.GameId && e.PlayerColor == playerGame.PlayerColor, rollback);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> UpdateAsync(PlayerGame playerGame, bool rollback = false)
        {
            try
            {
                tblPlayerGame row = Map<PlayerGame, tblPlayerGame>(playerGame);
                return await base.UpdateAsync(row, rollback);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<PlayerGame> LoadByIdAsync(Guid id)
        {
            try
            {
                //tblPlayerGame row = await base.LoadByIdAsync(id);
                //if (row != null)playe
                //    return Map<tblPlayerGame, PlayerGame>(row);
                //else
                //    throw new Exception("No Row");
                List<tblPlayerGame> rows = await base.LoadAsync(e => e.Id == id);
                if (rows[0] != null)
                    return Map<tblPlayerGame, PlayerGame>(rows[0]);
                else
                    throw new Exception("No Row");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<PlayerGame>> LoadAsync()
        {
            try
            {
                List<PlayerGame> rows = new List<PlayerGame>();

                (await base.LoadAsync()).ForEach(e => rows.Add(Map<tblPlayerGame, PlayerGame>(e)));
                return rows;
            }
            catch (Exception)
            {

                throw;
            }
        }
        
    }
}
