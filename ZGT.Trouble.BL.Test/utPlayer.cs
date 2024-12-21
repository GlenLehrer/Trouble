namespace ZGT.Trouble.BL.Test
{
    [TestClass]
    public class utPlayer : utBase
    {

        [TestMethod]
        public async Task LoadTest()
        {
            List<Player> players = await new PlayerManager(options).LoadAsync();
            Assert.AreEqual(4, players.Count);
        }

        [TestMethod]
        public async Task InsertTest()
        {
            Player player = new Player()
            {
                Id = Guid.NewGuid(),
                UserName = "NewUsername",
                Password = "Test123",
                Email = "unknown@unknown",
                NumberOfWins = 0,
                DateJoined = DateTime.Now,
            };

            Guid result = await new PlayerManager(options).InsertAsync(player, true);
            Assert.AreNotEqual(result, Guid.Empty);
        }


        [TestMethod]
        public async Task LoadByIDTest()
        {
            Player player = (await new PlayerManager(options).LoadAsync()).Find(e => e.UserName == "Glen");

            Assert.AreEqual((await new PlayerManager(options).LoadByIdAsync(player.Id)).Id, player.Id);

        }

        [TestMethod]
        public async Task UpdateTest()
        {
            Player player = (await new PlayerManager(options).LoadAsync())[0];
            //player.Id = new Guid("1afc7f5c-ffa0-4741-81cf-f12eAAb822bf");
            player.DateJoined = DateTime.Now;

            Assert.IsTrue(new PlayerManager(options).UpdateAsync(player, true).Result > 0);
        }

        [TestMethod]
        public async Task DeleteTest()
        {
            Player player = (await new PlayerManager(options).LoadAsync())[0];
           // List<PlayerGame> list = await new PlayerGameManager(options).LoadAsync();
            //PlayerGameManager playerGameManager = new PlayerGameManager(options);
            //foreach (PlayerGame pg in list)
            //{
            //    if(pg.PlayerId == player.Id)
            //        await playerGameManager.DeleteAsync(pg.Id, true);
            //}          
            Assert.IsTrue(new PlayerManager(options).DeleteAsync(player.Id, true).Result > 0);

        }
    }
}
