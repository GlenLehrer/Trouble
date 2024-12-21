namespace ZGT.Trouble.API.Test
{
    [TestClass]
    public class utPlayerGame : utBase
    {
        [TestMethod]
        public async Task LoadTestAsync()
        {
            await base.LoadTestAsync<PlayerGame>(3);
        }

        [TestMethod]
        public async Task LoadbyIdTestAsync()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            result.Add("Id", "c0e00542-a4bd-4088-b912-0dae885cb216");

            await base.LoadByIdTestAsync<PlayerGame>(result.First());
        }

        [TestMethod]
        public async Task InsertTestAsync()
        {

            PlayerGame playerGame = new PlayerGame()
            {
                Id = Guid.NewGuid(),
                PlayerId = new Guid("c0e00542-a4bd-4088-b912-0dae885cb215"),
                GameId = new Guid("c0e00542-a4bd-4088-b912-0dae885cb214"),
                IsComputerPlaying = false,
                PlayerColor = "y",
            };
            await base.InsertTestAsync<PlayerGame>(playerGame);
        }

        // Doesn't make sense to have an update playergame.
        //[TestMethod]
        //public async Task UpdateTestAsync()
        //{
        //    PlayerGame playerGame = new PlayerGame()
        //    {

        //    };
        //    await base.UpdateTestAsync<PlayerGame>(new Guid("c0e00542-a4bd-4088-b912-0dae885cb216"), playerGame);
        //}

        [TestMethod]
        public async Task DeleteTestAsync()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            result.Add("Id", "c0e00542-a4bd-4088-b912-0dae885cb216");

            await base.DeleteTestAsync<PlayerGame>(result.First());
        }
    }
}