namespace ZGT.Trouble.PL.Test
{
    [TestClass]
    public class utPlayer : utBase<tblPlayer>
    {
        [TestMethod]
        public void LoadTest()
        {
            int expected = 4;
            var players = base.LoadTest();
            Assert.AreEqual(expected, players.Count);
        }

        [TestMethod]
        public void LoadByIDTest()
        {
            tblPlayer row = base.LoadTest().FirstOrDefault();
            Assert.IsNotNull(row);
        }

        [TestMethod]
        public void InsertTest()
        {
            int rowsAffected = InsertTest(new tblPlayer
            {
                Id = Guid.NewGuid(),
                UserName = "Test",
                Password = "Test",
                Email = "Test",
                NumberOfWins = 99,
                DateJoined = DateTime.Now
            });

            Assert.AreEqual(1, rowsAffected);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblPlayer row = base.LoadTest().FirstOrDefault(x => x.Id == new Guid("c0e00542-a4bd-4088-b912-0dae885cb214"));
            if (row != null)
            {
                row.UserName = "BillyBobJoe";
                row.Password = "Billy";
                row.Email = "Test@test.com";
                row.DateJoined = DateTime.Now;
                row.NumberOfWins = 1;
                int rowsAffected = UpdateTest(row);
                Assert.AreEqual(1, rowsAffected);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            utBase<tblPlayerGame> playerGame = new utBase<tblPlayerGame>();
            List<tblPlayerGame> list = playerGame.LoadTest().FindAll(x => x.PlayerId == new Guid("c0e00542-a4bd-4088-b912-0dae885cb214"));
            if (list != null)
            {
                foreach (tblPlayerGame item in list)
                {
                    if (item != null)
                    {
                        base.dc.Set<tblPlayerGame>().Remove(item);
                        dc.SaveChanges();
                    }
                }
            }
            tblPlayer row = base.LoadTest().FirstOrDefault(x => x.Id == new Guid("c0e00542-a4bd-4088-b912-0dae885cb214"));
            if (row != null)
            {
                base.dc.Set<tblPlayer>().Remove(row);
                int rowsAffected = dc.SaveChanges();
                Assert.IsTrue(rowsAffected == 1);
            }
        }
        [TestMethod]
        public void ForeignKeyTest()
        {
            try //Trying to delete a row that exists as a foreign key in the PlayerGame Table. Should fail.
            {
                tblPlayer row = base.LoadTest().FirstOrDefault(x => x.Id == new Guid("c0e00542-a4bd-4088-b912-0dae885cb214"));
                if (row != null)
                {
                    int rowsAffected = DeleteTest(row);
                    Assert.IsTrue(false);
                }
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }
    }
}