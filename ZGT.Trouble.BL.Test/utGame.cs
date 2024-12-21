using System.Diagnostics;
using System.Runtime.InteropServices;
using ZGT.Trouble.BL.Models;

namespace ZGT.Trouble.BL.Test
{
    [TestClass]
    public class utGame : utBase
    {

        [TestMethod]
        public async Task LoadTest()
        {
            List<Game> games = await new GameManager(options).LoadAsync();
            Assert.AreEqual(3, games.Count);
        }

        [TestMethod]
        public async Task InsertTest()
        {
            Game game = new Game()
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
            };

            Guid result = await new GameManager(options).InsertAsync(game, true);
            Assert.AreNotEqual(result, Guid.Empty);
        }


        [TestMethod]
        public async Task LoadByIDTest()
        {
            Game game = (await new GameManager(options).LoadAsync())[0];

            Assert.AreEqual((await new GameManager(options).LoadByIdAsync(game.Id)).Id, game.Id);

        }

        [TestMethod]
        public async Task UpdateTest()
        {
            Game game = (await new GameManager(options).LoadAsync())[0];
            //game.Id = new Guid("1afc7f5c-ffa0-4741-81cf-f12eAAb822bf");
            game.PlayerTurn = "Test";

            Assert.IsTrue(new GameManager(options).UpdateAsync(game, true).Result > 0);
        }

        [TestMethod]
        public async Task DeleteTest()
        {
            Game game = (await new GameManager(options).LoadAsync())[0];
            //List<PlayerGame> list = await new PlayerGameManager(options).LoadAsync();
            //foreach (PlayerGame pg in list)
            //{
            //    if (pg.GameId == game.Id)
            //        await new PlayerGameManager(options).DeleteAsync(pg.Id, false);
            //}
            Assert.IsTrue(new GameManager(options).DeleteAsync(game.Id, true).Result > 0);

        }

        [TestMethod]
        public async Task ComputerMakeMove()
        {
            GameManager manager = new GameManager(options);
            //Turn Order:  Yellow, Blue, Red, Green
            Game game = new Game()
            {
                PlayerTurn = "y",
                DieRoll = 6,
                GameComplete = 0,
                GameStartDate = DateTime.Now,
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

                YellowStartSquare = "",
                BlueStartSquare = "",
                RedStartSquare = "",
                GreenStartSquare = "",

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
                Square24 = ""
            };
            game.Id = (Guid)manager.LoadAsync().Result.FirstOrDefault().Id;
            await manager.UpdateAsync(game);

            while(manager.GameWinner(game) == "n")
            {
                game.DieRoll = manager.RollDice();
                int Die = game.DieRoll;
                string player = game.PlayerTurn;
                string y = (await manager.MakeComputerMoveAsync(game)).Item2;
                Console.WriteLine(player + ": " + "DieRoll: " + Die + " " +  y);               
            }

            Console.WriteLine($"The Winner is: {manager.GameWinner(game)}");
            Assert.IsTrue(true);
                        
        }



        [TestMethod]
        public async Task MakeAMove()
        {

            GameManager manager = new GameManager(options);
            //Turn Order:  Yellow, Blue, Red, Green
            Game game = new Game()
            {
                PlayerTurn = "y",
                DieRoll = 6,
                GameComplete = 0,
                GameStartDate = DateTime.Now,
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

                YellowStartSquare = "",
                BlueStartSquare = "",
                RedStartSquare = "",
                GreenStartSquare = "",

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

                Square1 = "",
                Square2 = "y",
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
                Square24 = ""
            };
            game.Id = (Guid)manager.LoadAsync().Result.FirstOrDefault().Id;
            manager.UpdateAsync(game);
            
            game.DieRoll = 6;
            
            Assert.IsFalse(manager.MakeMoveAsync(game, 1, 48).Result, "Yellow Can't move to a Green Center Square"); //To GreenCenterSquare1
            game.DieRoll = 6;
            game.PlayerTurn = "y";
            Assert.IsFalse(manager.MakeMoveAsync(game, 1, 52).Result, "Yellow Can't move to a Red Center Square"); //To RedCenterSquare1
            game.DieRoll = 6;
            game.PlayerTurn = "y";
            Assert.IsFalse(manager.MakeMoveAsync(game, 1, 56).Result, "Yellow Can't move to a Blue Center Square");//To BlueCenterSquare1
            game.DieRoll = 6;
            game.Square19 = "y";
            game.PlayerTurn = "y";
            Assert.IsFalse(manager.MakeMoveAsync(game, 22, 28).Result, "Can't move on the board off to the a Home Square"); //To YellowHomeSquare1
            game.DieRoll = 6;
            game.Square19 = "";
            game.Square22 = "y";
            game.PlayerTurn = "y";
            Assert.IsFalse(manager.MakeMoveAsync(game, 25, 32).Result, "Can't move on the board off to the a Green Home Square");//To GreenHomeSquare1
            game.DieRoll = 6;
            game.Square22 = "";
            game.YellowHomeSquare3 = 1;
            game.PlayerTurn = "y";
            Assert.IsFalse(manager.MakeMoveAsync(game, 30, 37).Result, "Can't move on the board off to the a Red Home Square");//To RedHomeSquare1
            game.DieRoll = 6;
            game.PlayerTurn = "y";
            Assert.IsFalse(manager.MakeMoveAsync(game, 1, 40).Result, "Can't move on the board off to the a Blue Home Square");//To BlueHomeSquare1
            game.DieRoll = 6;
            game.PlayerTurn = "y";

            game.Square2 = "y";
            Assert.IsFalse(manager.MakeMoveAsync(game, 2, 9).Result, "Can't move greater than Dice Roll");
            game.DieRoll = 6;
            game.PlayerTurn = "y";
            Assert.IsFalse(manager.MakeMoveAsync(game, 2, 7).Result, "Can't move less than Dice Roll");
            game.DieRoll = 6;
            game.Square7 = "g";
            game.PlayerTurn = "y";
            Assert.IsTrue(manager.MakeMoveAsync(game, 2, 8).Result, "Proper move should work");
            game.DieRoll = 6;
            game.PlayerTurn = "y";
            Assert.IsTrue(manager.LoadByIdAsync(game.Id).Result.Square7 == "y", "y should replace g with proper move");



            game.DieRoll = 6;
            game.Square24 = "y";
            game.PlayerTurn = "y";
            Assert.IsFalse(manager.MakeMoveAsync(game, 27, 5).Result, "Can't go around the board twice");
            game.PlayerTurn = "y";
            game.DieRoll = 6;
            game.YellowStartSquare = "";
            Assert.IsTrue(manager.MakeMoveAsync(game, 28, 0).Result, "HomeSquare1 should be able to go to Start Square");
            game.DieRoll = 6;
            game.YellowStartSquare = "";
            game.PlayerTurn = "y";
            Assert.IsTrue(manager.MakeMoveAsync(game, 29, 0).Result, "HomeSquare2 should be able to go to Start Square");
            game.DieRoll = 6;
            game.YellowStartSquare = "";
            game.PlayerTurn = "y";
            Assert.IsTrue(manager.MakeMoveAsync(game, 30, 0).Result, "HomeSquare3 should be able to go to Start Square");
            game.DieRoll = 6;
            game.YellowStartSquare = "";
            game.PlayerTurn = "y";
            Assert.IsTrue(manager.MakeMoveAsync(game, 31, 0).Result, "HomeSquare4 should be able to go to Start Square");
            game.DieRoll = 6;
            game.YellowHomeSquare4 = 1;
            game.PlayerTurn = "y";
            Assert.IsFalse(manager.MakeMoveAsync(game, 31, 0).Result, "HomeSquare4 should not be able to go to filled Start Square");

            game.DieRoll = 6;
            game.PlayerTurn = "b";
            game.Square6 = "b";            
            Assert.IsFalse(manager.MakeMoveAsync(game, 6, 12).Result, "Can't go around the board twice");
            game.DieRoll = 2;
            game.PlayerTurn = "b";
            Assert.IsTrue(manager.MakeMoveAsync(game, 6, 57).Result, "Should make it in Center Square");
            game.PlayerTurn = "r";
            game.Square12 = "r";
            game.DieRoll = 6;
            Assert.IsFalse(manager.MakeMoveAsync(game, 13, 19).Result, "Can't go around the board twice");
            game.DieRoll = 2;
            game.PlayerTurn = "r";
            Assert.IsTrue(manager.MakeMoveAsync(game, 13, 53).Result, "Should make it in Center Square");
            game.PlayerTurn = "g";
            game.Square18 = "g";
            game.DieRoll = 6;
            Assert.IsFalse(manager.MakeMoveAsync(game, 20, 26).Result, "Can't go around the board twice");
            game.DieRoll = 2;
            game.PlayerTurn = "g";
            Assert.IsTrue(manager.MakeMoveAsync(game, 20, 49).Result, "Should make it in Center Square");
            game.DieRoll = 6;
            game.PlayerTurn = "y";
            game.DieRoll = 6;
            game.YellowStartSquare = "";
            game.Square22 = "y";
            game.Square21 = "y";
            game.Square20 = "y";
            game.Square19 = "y";
            Assert.IsTrue(manager.MakeMoveAsync(game, 25, 47).Result, "Making it to the center square");
            game.PlayerTurn = "y";
            game.DieRoll = 6;
            Assert.IsTrue(manager.MakeMoveAsync(game, 24, 46).Result, "Making it to the center square");
            game.PlayerTurn = "y";
            game.DieRoll = 6;
            Assert.IsTrue(manager.MakeMoveAsync(game, 23, 45).Result, "Making it to the center square");
            game.PlayerTurn = "y";
            game.DieRoll = 6;
            Assert.IsTrue(manager.MakeMoveAsync(game, 22, 44).Result, "Making it to the center square");
            game.PlayerTurn = "y";
            //Test if game is over, Last 4 Tests filled in Yellow's Center Squares
            Assert.IsTrue((manager.GameWinner(game) == "y"));
        }

    }
}
