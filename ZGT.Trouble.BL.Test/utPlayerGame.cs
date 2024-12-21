namespace ZGT.Trouble.BL.Test
{
    [TestClass]
    public class utPlayerGame : utBase
    {

        [TestMethod]
        public async Task LoadTest()
        {
            List<PlayerGame> playerGames = await new PlayerGameManager(options).LoadAsync();
            Assert.AreEqual(7, playerGames.Count);
        }

        [TestMethod]
        public async Task InsertTest()
        {
            PlayerGame playerGame = new PlayerGame()
            {
                Id = Guid.NewGuid(),
                PlayerId = (await new PlayerManager(options).LoadAsync())[0].Id,
                GameId = (await new GameManager(options).LoadAsync())[0].Id,
                IsComputerPlaying = false,
                PlayerColor = "y"
            };
            
            Guid result = await new PlayerGameManager(options).InsertAsync(playerGame, true);
            Assert.AreNotEqual(result, Guid.Empty);
        }


        [TestMethod]
        public async Task LoadByIDTest()
        {
            PlayerGame playerGame = (await new PlayerGameManager(options).LoadAsync())[0];

            Assert.AreEqual((await new PlayerGameManager(options).LoadByIdAsync(playerGame.Id)).Id, playerGame.Id);

        }

        [TestMethod]
        public async Task UpdateTest()
        {
            PlayerGame playerGame = (await new PlayerGameManager(options).LoadAsync())[0];
            playerGame.GameId = (await new GameManager(options).LoadAsync())[2].Id;

            Assert.IsTrue(new PlayerGameManager(options).UpdateAsync(playerGame, true).Result > 0);
        }

        [TestMethod]
        public async Task DeleteTest()
        {
            PlayerGame playerGame = (await new PlayerGameManager(options).LoadAsync())[0];

            Assert.IsTrue(new PlayerGameManager(options).DeleteAsync(playerGame.Id, true).Result > 0);

        }
    }
}
