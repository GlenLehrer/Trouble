namespace ZGT.Trouble.PL.Test
{
    [TestClass]
    public class utPlayerGame : utBase<tblPlayerGame>
    {
        [TestMethod]
        public void LoadTest()
        {
            int expected = 7;
            var playerGames = base.LoadTest();
            Assert.AreEqual(expected, playerGames.Count());
        }

        [TestMethod]
        public void InsertTest()
        {
            utBase<tblPlayer> player = new utBase<tblPlayer>();
            List<tblPlayer> list = player.LoadTest();
            utBase<tblGame> game = new utBase<tblGame>();
            List<tblGame> list2 = game.LoadTest();

            int rowsAffected = InsertTest(new tblPlayerGame
            {
                Id = Guid.NewGuid(),
                PlayerId = list[0].Id,
                GameId = list2[0].Id,
                IsComputerPlaying = false,
                PlayerColor = "y"
            });

            Assert.AreEqual(1, rowsAffected);
        }

        [TestMethod]
        public void UpdateTest()
        {
            utBase<tblPlayer> player = new utBase<tblPlayer>();
            List<tblPlayer> list = player.LoadTest();

            tblPlayerGame row = base.LoadTest().FirstOrDefault();
            if (row != null)
            {
                row.PlayerId = list[0].Id;
                int rowsAffected = UpdateTest(row);
                Assert.AreEqual(1, rowsAffected);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            tblPlayerGame row = base.LoadTest().FirstOrDefault();
            if (row != null)
            {
                int rowsAffected = DeleteTest(row);
                Assert.IsTrue(rowsAffected == 1);
            }
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            tblPlayerGame row = base.LoadTest().FirstOrDefault();
            Assert.IsNotNull(row);
        }

        [TestMethod]
        public void ForeignKeyTest()
        {
            utBase<tblPlayer> player = new utBase<tblPlayer>();
            List<tblPlayer> list = player.LoadTest();
            utBase<tblGame> game = new utBase<tblGame>();
            List<tblGame> list2 = game.LoadTest();
            try
            {
                int rowsAffected = InsertTest(new tblPlayerGame
                {
                    Id = Guid.NewGuid(),
                    PlayerId = Guid.NewGuid(),
                    GameId = list2[0].Id
                });

                rowsAffected = InsertTest(new tblPlayerGame
                {
                    Id = Guid.NewGuid(),
                    PlayerId = list[0].Id,
                    GameId = Guid.NewGuid()
                });

               Assert.IsTrue(false);
            }
            catch
            {
                Assert.IsTrue(true);
            }          
        }
    }
}