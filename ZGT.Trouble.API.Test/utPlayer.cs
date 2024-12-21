namespace ZGT.Trouble.API.Test
{
    [TestClass]
    public class utPlayer : utBase
    {
        [TestMethod]
        public async Task LoadTestAsync()
        {
            await base.LoadTestAsync<Player>(3);
        }

        [TestMethod]
        public async Task LoadbyIdTestAsync()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            result.Add("Id", "c0e00542-a4bd-4088-b912-0dae885cb215");

            await base.LoadByIdTestAsync<Player>(result.First());
        }

        [TestMethod]
        public async Task InsertTestAsync()
        {

            Player player = new Player()
            {
               Id = Guid.NewGuid(),
               UserName = "Test",
               Password = "Test",
               Email = "Test",
               NumberOfWins = 1,
               DateJoined = DateTime.Now
            };
            await base.InsertTestAsync<Player>(player);
        }

        [TestMethod]
        public async Task UpdateTestAsync()
        {
            Player player = new Player()
            {
                Id = new Guid("c0e00542-a4bd-4088-b912-0dae885cb215"),
                UserName = "Test",
                Password = "Test",
                Email = "Test",
                NumberOfWins = 1,
                DateJoined = DateTime.Now
            };
            await base.UpdateTestAsync<Player>(new Guid("c0e00542-a4bd-4088-b912-0dae885cb215"), player);
        }

        [TestMethod]
        public async Task DeleteTestAsync()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            result.Add("Id", "c0e00542-a4bd-4088-b912-0dae885cb215");

            await base.DeleteTestAsync<Player>(result.First());
        }
    }
}