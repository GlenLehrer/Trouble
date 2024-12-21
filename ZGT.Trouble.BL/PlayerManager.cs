using Microsoft.SqlServer.Server;

namespace ZGT.Trouble.BL
{
    public class PlayerManager : GenericManager<tblPlayer>
    {
        public PlayerManager(ILogger logger, DbContextOptions<TroubleEntities> options) : base(logger, options) { }
        public PlayerManager(DbContextOptions<TroubleEntities> options) : base(options) { }
        public PlayerManager() { }


        public async Task<Guid> InsertAsync(Player player, bool rollback = false)
        {
            try
            {
                tblPlayer row = Map<Player, tblPlayer>(player);
                return await base.InsertAsync(row, e => e.UserName == player.UserName || e.Email == player.Email, rollback);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> UpdateAsync(Player player, bool rollback = false)
        {
            try
            {
                tblPlayer row = Map<Player, tblPlayer>(player);
                return await base.UpdateAsync(row, rollback);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Player> LoadByIdAsync(Guid id)
        {
            try
            {
                //tblPlayer row = await base.LoadByIdAsync(id);
                //if (row != null)
                //    return Map<tblPlayer, Player>(row);
                //else
                //    throw new Exception("No Row");
                List<tblPlayer> rows = await base.LoadAsync(e => e.Id == id);
                if (rows[0] != null)
                    return Map<tblPlayer, Player>(rows[0]);
                else
                    throw new Exception("No Row");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<Player>> LoadAsync()
        {
            try
            {
                List<Player> rows = new List<Player>();

                (await base.LoadAsync()).ForEach(e => rows.Add(Map<tblPlayer, Player>(e)));
                return rows;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public override async Task<int> DeleteAsync(Guid id, bool rollback = false)
        {
            try
            {
                int results = 0;

                using (TroubleEntities dc = new TroubleEntities(options))
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblPlayer row = dc.Set<tblPlayer>().FirstOrDefault(t => t.Id == id);
                    List<tblPlayerGame> list = dc.Set<tblPlayerGame>().ToList().FindAll(pg => row.Id == pg.PlayerId);

                    if (row != null)
                    {
                        foreach (tblPlayerGame pg in list)
                        {
                            dc.Set<tblPlayerGame>().Remove(pg);
                            dc.SaveChanges();
                        }
                        dc.Set<tblPlayer>().Remove(row);
                        results = dc.SaveChanges();
                        if (rollback) transaction.Rollback();
                    }
                    else
                    {
                        throw new Exception("Row does not exist");
                    }

                }
                return results;
            }
            catch (Exception)
            {
                if (logger != null)
                    logger.LogWarning($"Delete {typeof(Game).Name}s - GenericManager");
                throw;
            }
        }
        /******Copied and Pasted From Player Login in Trouble**********/
        public class LoginFailureException : Exception
        {
            public LoginFailureException() : base("Cannot log in with these credentials.  Your IP address has been saved.")
            {
            }

            public LoginFailureException(string message) : base(message)
            {
            }
        }

            public static string GetHash(string Password)
            {
                using (var hasher = new System.Security.Cryptography.SHA1Managed())
                {
                    var hashbytes = System.Text.Encoding.UTF8.GetBytes(Password);
                    return Convert.ToBase64String(hasher.ComputeHash(hashbytes));
                }
            }

        public async Task Seed()
            {
                List<Player> Players = await LoadAsync();

                foreach (Player Player in Players)
                {
                    if (Player.Password.Length != 28)
                    {
                        await UpdateAsync(Player);
                    }
                }

                if (Players.Count == 0)
                {
                    // Hardcord a couple of Players with hashed passwords
                    await InsertAsync(new Player { UserName = "bfoote", Email = "Brian@brian", NumberOfWins = 0, DateJoined = DateTime.Now, Password = "maple" });
                    await InsertAsync(new Player { UserName = "kvicchiollo", Email = "Ken@ken", NumberOfWins = 0, DateJoined = DateTime.Now, Password = "password" });
                }
            }

            public async Task<Guid> LoginAsync(Player Player)
            {
                try
                {
                    if (!string.IsNullOrEmpty(Player.UserName))
                    {
                        if (!string.IsNullOrEmpty(Player.Password))
                        {
                            using (TroubleEntities dc = new TroubleEntities(options))
                            {
                                tblPlayer Playerrow = dc.tblPlayer.FirstOrDefault(u => u.UserName == Player.UserName);

                                if (Playerrow != null)
                                {
                                    // check the password
                                    if (Playerrow.Password == Player.Password)//GetHash(Player.Password))
                                    {
                                        // Login was successfull
                                        Player.Id = Playerrow.Id;
                                        Player.NumberOfWins = Playerrow.NumberOfWins;
                                        Player.Email = Playerrow.Email;
                                        Player.UserName = Playerrow.UserName;
                                        Player.Password = Playerrow.Password;
                                        Player.DateJoined = Playerrow.DateJoined;

                                        return Player.Id;
                                    }
                                    else
                                    {
                                        throw new LoginFailureException("Cannot log in with these credentials.  Your IP address has been saved.");
                                    }
                                }
                                else
                                {
                                    throw new Exception("Player could not be found.");
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("Password was not set.");
                        }
                    }
                    else
                    {
                        throw new Exception("User name was not set.");
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
}
