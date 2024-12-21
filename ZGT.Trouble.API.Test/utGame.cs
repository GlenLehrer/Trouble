using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ZGT.Trouble.API.Test
{
    [TestClass]
    public class utGame : utBase
    {
        [TestMethod]
        public async Task LoadTestAsync()
        {
            await base.LoadTestAsync<Game>(3);
        }

        [TestMethod]
        public async Task LoadbyIdTestAsync()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            result.Add("Id", "c0e00542-a4bd-4088-b912-0dae885cb214");

            await base.LoadByIdTestAsync<Game>(result.First());
        }

        [TestMethod]
        public async Task InsertTestAsync()
        {

            Game game = new Game()
            {
                Id = Guid.NewGuid(),
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
            await base.InsertTestAsync<Game>(game);
        }

        [TestMethod]
        public async Task UpdateTestAsync()
        {
            Game game = new Game()
            {
                Id = new Guid("c0e00542-a4bd-4088-b912-0dae885cb214"),
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
            await base.UpdateTestAsync<Game>(new Guid("c0e00542-a4bd-4088-b912-0dae885cb214"), game);
        }

        [TestMethod]
        public async Task DeleteTestAsync()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            result.Add("Id", "c0e00542-a4bd-4088-b912-0dae885cb214");

            await base.DeleteTestAsync<Game>(result.First());
        }

        [TestMethod]
        public async Task MakeMoveAsync()
        {
            try
            {       
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
                game.Id = new Guid("c0e00542-a4bd-4088-b912-0dae885cb214");
                int startSquare = 28;
                int endSquare = 0;

                string serializedObject = JsonConvert.SerializeObject(game);
                var content = new StringContent(serializedObject);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = client.PutAsync("Game/" + startSquare + "/" + endSquare + "/1", content).Result;
                string result = response.Content.ReadAsStringAsync().Result;
                
                Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK  && result == "true");
            }
            catch(Exception ex){
                Console.WriteLine(ex.InnerException.Message);


            }




        }
    }
}