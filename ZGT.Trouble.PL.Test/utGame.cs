namespace ZGT.Trouble.PL.Test
{
    [TestClass]
    public class utGame : utBase<tblGame>
    {
        [TestMethod]
        public void LoadTest()
        {
            int expected = 3;
            var Games = base.LoadTest();
            Assert.AreEqual(expected, Games.Count());

        }

        [TestMethod]
        public void LoadByIDTest()
        {
            tblGame row = base.LoadTest().FirstOrDefault();
            Assert.IsNotNull(row);
        }


        [TestMethod]
        public void InsertTest()
        {
            int rowsAffected = InsertTest(new tblGame
            {
                Id = Guid.NewGuid(),
                PlayerTurn = "Yellow",
                DieRoll = 6,
                GameStartDate = DateTime.Now,
                GameComplete = 0,
                YellowHomeSquare1 = 1,
                YellowHomeSquare2 = 1,
                YellowHomeSquare3 = 1,
                YellowHomeSquare4 = 1,
                BlueHomeSquare1 = 1,
                BlueHomeSquare2 = 1,
                BlueHomeSquare3 = 1,
                BlueHomeSquare4 = 1,
                RedHomeSquare1 = 1,
                RedHomeSquare2 = 1,
                RedHomeSquare3 = 1,
                RedHomeSquare4 = 1,
                GreenHomeSquare1 = 1,
                GreenHomeSquare2 = 1,
                GreenHomeSquare3 = 1,
                GreenHomeSquare4 = 1,
                BlueStartSquare = "",
                YellowStartSquare = "",
                RedStartSquare = "",
                GreenStartSquare = "",
                Square1 = "",
                Square2 = "",
                Square3 = "",
                Square4 = "",
                Square5 = "",
                Square6 = "",
                Square7 = "",
                Square8 = "",
                Square9 = "",
                Square10 = "",
                Square11 = "",
                Square12 = "",
                Square13 = "",
                Square14 = "",
                Square15 = "",
                Square16 = "",
                Square17 = "",
                Square18 = "",
                Square19 = "",
                Square20 = "",
                Square21 = "",
                Square22 = "",
                Square23 = "",
                Square24 = "",
                YellowCenterSquare1 = 0,
                YellowCenterSquare2 = 0,
                YellowCenterSquare3 = 0,
                YellowCenterSquare4 = 0,
                BlueCenterSquare1 = 0,
                BlueCenterSquare2 = 0,
                BlueCenterSquare3 = 0,
                BlueCenterSquare4 = 0,
                RedCenterSquare1 = 0,
                RedCenterSquare2 = 0,
                RedCenterSquare3 = 0,
                RedCenterSquare4 = 0,
                GreenCenterSquare1 = 0,
                GreenCenterSquare2 = 0,
                GreenCenterSquare3 = 0,
                GreenCenterSquare4 = 0,
            });

            Assert.AreEqual(1, rowsAffected);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblGame row = base.LoadTest().FirstOrDefault(x => x.Id == new Guid("c0e00542-a4bd-4088-b912-0dae885cb214"));
            if (row != null)
            {
                row.DieRoll = 5;
                row.GameComplete = 0;
                int rowsAffected = UpdateTest(row);
                Assert.AreEqual(1, rowsAffected);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            
            utBase<tblPlayerGame> playerGame = new utBase<tblPlayerGame>();
            List<tblPlayerGame> list = playerGame.LoadTest().FindAll(x => x.GameId == new Guid("c0e00542-a4bd-4088-b912-0dae885cb214"));
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
            tblGame row = base.LoadTest().FirstOrDefault(x => x.Id == new Guid("c0e00542-a4bd-4088-b912-0dae885cb214"));
            if (row != null)
            {
               base.dc.Set<tblGame>().Remove(row);
               int rowsAffected = dc.SaveChanges();
               Assert.IsTrue(rowsAffected == 1);
            }
        }
        [TestMethod]
        public void ForeignKeyTest()
        {
            try //Trying to delete a row that exists as a foreign key in the PlayerGame Table. Should fail.
            {
                tblGame row = base.LoadTest().FirstOrDefault(x => x.Id == new Guid("c0e00542-a4bd-4088-b912-0dae885cb214"));
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