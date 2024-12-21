using System.Reflection;
using ZGT.Trouble.MAUI.UI.Views.MyMauiApp;
using Microsoft.Maui.Graphics.Platform;
using Newtonsoft.Json;
using ZGT.Trouble.BL.Models;
using Microsoft.AspNetCore.SignalR.Client;
using ZGT.Trouble.MAUI.UI.Models;



/*Page displays a game in session, 2-4 players..  
 * The goal here is to have a specific page where players can play a game against opponents or computers.  
 * Having chat feature on each individual game page with SignalR is also a good idea.
 * Currently, page only displays the game board.  More functionality is needed to play a game.
 */
namespace ZGT.Trouble.MAUI.UI.Views
{
    namespace MyMauiApp
    {       
        public class GraphicsDrawable : IDrawable //Method Draws Game Board onto the Screen
        {
            public static float xLabel;
            public static float yLabel;
            public static float widthLabel;
            public static float heightLabel;

            public static float resize = 1f;

            public static Game Game;
            public void Draw(ICanvas canvas, RectF dirtyRect)
            {
                String[] GameState = new String[60];
                (float x, float y)[] Coordinates = new (float x, float y)[60];
                float circleWidth = 10 * resize;
                float circleHeight = 10 * resize;
                Microsoft.Maui.Graphics.IImage image;
                Assembly assembly = GetType().GetTypeInfo().Assembly;
                //Drawing Text on Screen
                    //string[] names = Assembly.GetExecutingAssembly().GetManifestResourceNames();
                    //for(int i = 0; i < names.Length; i++)
                    //{
                    //    canvas.DrawString(names[i], 20, i * 10, HorizontalAlignment.Left);
                    //}

                using (Stream stream = assembly.GetManifestResourceStream("ZGT.Trouble.MAUI.UI.Resources.Images.board.png")) //Game Board Image
                {
                    image = PlatformImage.FromStream(stream);
                }

                if (image != null)
                {
                    //Points were made when resize intially was equal to 6.
                    //Point Coordinates assume a image dimensions 1687 x 1301, both divided by 6.
                    //Therefore, Starting with dividing image by 6 below is neccessary for correct draw coordinates.
                    //Coordinates also assume image starts at the 10x, 10y offset, which is the case in the DrawImage() 10, 10 numbers below
                    canvas.DrawImage(image, 10, 10, (image.Width / 6) * resize, (image.Height / 6) * resize);
                    
                    SolidPaint solidPaint = new SolidPaint(Colors.DarkRed); //Paint Color for when Drawing Rectangles/Circles
                    //Drawing example
                        //RectF solidRectangle = new RectF(10 + 1.35f, 10 + 8.68f, 15, 15);
                        //canvas.SetFillPaint(solidPaint, solidRectangle);
                        //canvas.FillRoundedRectangle(solidRectangle, 12);

                    RectF solidRectangle = new RectF(xLabel, yLabel, widthLabel, heightLabel); //For when using the Draw Buttons on Page
                    canvas.SetFillPaint(solidPaint, solidRectangle); //Add Paint
                    canvas.FillRoundedRectangle(solidRectangle, 12); //Draw Rectangle/Circle

                    solidPaint = new SolidPaint(Colors.Yellow);
                    //Points were made when resize intially was equal to 6.
                    //Therefore, dividing image by 6 above is neccessary for correct draw coordinates.
                    //Fill Yellow Home Squares
                        //solidRectangle = new RectF((resize * 14.5f) + 10, (resize * 36f) + 10, circleWidth, circleHeight);
                        //canvas.SetFillPaint(solidPaint, solidRectangle);
                        //canvas.FillRoundedRectangle(solidRectangle, 12);
                        //solidRectangle = new RectF((resize * 14.5f) + 10, (23f * resize) + 10, circleWidth, circleHeight);
                        //canvas.SetFillPaint(solidPaint, solidRectangle);
                        //canvas.FillRoundedRectangle(solidRectangle, 12);
                        //solidRectangle = new RectF((resize * 27f) + 10, (resize * 25.5f) + 10, circleWidth, circleHeight);
                        //canvas.SetFillPaint(solidPaint, solidRectangle);
                        //canvas.FillRoundedRectangle(solidRectangle, 12);
                        //solidRectangle = new RectF((resize * 29f) + 10, (resize * 11f) + 10, circleWidth, circleHeight);
                        //canvas.SetFillPaint(solidPaint, solidRectangle);
                        //canvas.FillRoundedRectangle(solidRectangle, 12);
                    //
                    if(Game != null) //Draw Rectangles/Circles for each square is filled, determined by Game data
                    {
                        GameState = MakeGameState(Game); //Make game object into an array
                        Coordinates = FillCoordinates(); //Give the Numbers the coordinates of each square is at
                        for (int i = 0; i < GameState.Length; i++) //For each square in the game object
                        {
                            if(GameState[i].Length >= 1) //If a square is full, must not be an emptyu string
                            {
                                switch (GameState[i].Substring(0, 1).ToLower()) //first character for color of game square
                                {
                                    case "y": //Game Square has a yellow piece in it
                                        //Draw a Yellow Square Rectangle, at the right coordinates
                                        solidPaint = new SolidPaint(Colors.Yellow);
                                        solidRectangle = new RectF((resize * (Coordinates[i].x - 10)) + 10, (resize * (Coordinates[i].y - 10)) + 10, circleWidth, circleHeight);
                                        canvas.SetFillPaint(solidPaint, solidRectangle);
                                        canvas.FillRoundedRectangle(solidRectangle, 12);
                                        break;
                                    case "b"://Game Square has a blue piece in it
                                        //Draw a Blue Square Rectangle, at the right coordinates
                                        solidPaint = new SolidPaint(Colors.DarkBlue);
                                        solidRectangle = new RectF((resize * (Coordinates[i].x - 10)) + 10, (resize * (Coordinates[i].y - 10)) + 10, circleWidth, circleHeight);
                                        canvas.SetFillPaint(solidPaint, solidRectangle);
                                        canvas.FillRoundedRectangle(solidRectangle, 12);
                                        break;
                                    case "r"://Game Square has a red piece in it
                                        //Draw a Red Square Rectangle, at the right coordinates
                                        solidPaint = new SolidPaint(Colors.DarkRed);
                                        solidRectangle = new RectF((resize * (Coordinates[i].x - 10)) + 10, (resize * (Coordinates[i].y - 10)) + 10, circleWidth, circleHeight);
                                        canvas.SetFillPaint(solidPaint, solidRectangle);
                                        canvas.FillRoundedRectangle(solidRectangle, 12);
                                        break;
                                    case "g"://Game Square has a green piece in it
                                        //Draw a Green Square Rectangle, at the right coordinates
                                        solidPaint = new SolidPaint(Colors.DarkGreen); 
                                        solidRectangle = new RectF((resize * (Coordinates[i].x - 10)) + 10, (resize * (Coordinates[i].y - 10)) + 10, circleWidth, circleHeight);
                                        canvas.SetFillPaint(solidPaint, solidRectangle);
                                        canvas.FillRoundedRectangle(solidRectangle, 12);
                                        break;
                                    default: break;
                                }
                            }                          
                        }
                    }
                }
            }
            private (float x, float y)[] FillCoordinates() //Coordinates for each circle/square on the game Image
            {
                (float x, float y)[] XYvalues = new (float x, float y)[60];
                XYvalues[0] = (67, 30);
                XYvalues[1] = (102, 28);
                XYvalues[2] = (126, 28);
                XYvalues[3] = (149.5f, 28.5f);
                XYvalues[4] = (175.5f, 28);
                XYvalues[5] = (202, 28.5f);
                XYvalues[6] = (225, 33);
                XYvalues[7] = (243, 59);
                XYvalues[8] = (247, 78);
                XYvalues[9] = (245.5f, 96.5f);
                XYvalues[10] = (247, 115);
                XYvalues[11] = (247, 135);
                XYvalues[12] = (247, 155);
                XYvalues[13] = (239, 169);
                XYvalues[14] = (224, 186.5f);
                XYvalues[15] = (198.5f, 189.5f);
                XYvalues[16] = (172, 188);
                XYvalues[17] = (146, 189);
                XYvalues[18] = (122, 188);
                XYvalues[19] = (98.5f, 188.5f);
                XYvalues[20] = (70, 186);
                XYvalues[21] = (49, 162);
                XYvalues[22] = (46.5f, 146);
                XYvalues[23] = (46.5f, 126);
                XYvalues[24] = (46.5f, 106);
                XYvalues[25] = (46, 87);
                XYvalues[26] = (46.5f, 68);
                XYvalues[27] = (47, 53);
                XYvalues[28] = (24.5f, 46);
                XYvalues[29] = (24.5f, 33);
                XYvalues[30] = (37, 35.5f);
                XYvalues[31] = (39, 22);
                XYvalues[32] = (37, 204);
                XYvalues[33] = (44, 188);
                XYvalues[34] = (27, 191);
                XYvalues[35] = (17, 179);
                XYvalues[36] = (265, 179);
                XYvalues[37] = (262.5f, 193.5f);
                XYvalues[38] = (250, 191);
                XYvalues[39] = (250, 204);
                XYvalues[40] = (250, 21);
                XYvalues[41] = (260.5f, 33);
                XYvalues[42] = (255.5f, 47);
                XYvalues[43] = (270, 45);

                XYvalues[44] = (80, 53);
                XYvalues[45] = (94, 64);
                XYvalues[46] = (109, 78);
                XYvalues[47] = (124, 88.5f);

                XYvalues[48] = (81, 157);
                XYvalues[49] = (95, 146);
                XYvalues[50] = (112, 135);
                XYvalues[51] = (126, 123);
                XYvalues[52] = (217, 164);
                XYvalues[53] = (203, 154);
                XYvalues[54] = (188, 140);
                XYvalues[55] = (174, 128);
                XYvalues[56] = (218, 56);
                XYvalues[57] = (205, 68);
                XYvalues[58] = (188, 79);
                XYvalues[59] = (174, 90);
                return XYvalues;
            }
            public String[] MakeGameState(Game game) //Game object with its squares listed in an Array Form.
            {
                String[] gameState = new String[60];
                gameState[0] = game.YellowStartSquare;
                gameState[1] = game.Square1;
                gameState[2] = game.Square2;
                gameState[3] = game.Square3; //Protected Square
                gameState[4] = game.Square4;
                gameState[5] = game.Square5;
                gameState[6] = game.Square6;

                gameState[7] = game.BlueStartSquare;
                gameState[8] = game.Square7;
                gameState[9] = game.Square8;
                gameState[10] = game.Square9;  //Protected Square
                gameState[11] = game.Square10;
                gameState[12] = game.Square11;
                gameState[13] = game.Square12;

                gameState[14] = game.RedStartSquare;
                gameState[15] = game.Square13;
                gameState[16] = game.Square14;
                gameState[17] = game.Square15;  //Protected Square
                gameState[18] = game.Square16;
                gameState[19] = game.Square17;
                gameState[20] = game.Square18;

                gameState[21] = game.GreenStartSquare;
                gameState[22] = game.Square19;
                gameState[23] = game.Square20;
                gameState[24] = game.Square21;  //Protected Square
                gameState[25] = game.Square22;
                gameState[26] = game.Square23;
                gameState[27] = game.Square24;

                gameState[28] = game.YellowHomeSquare1 == 1 ? "y" : "";
                gameState[29] = game.YellowHomeSquare2 == 1 ? "y" : "";
                gameState[30] = game.YellowHomeSquare3 == 1 ? "y" : "";
                gameState[31] = game.YellowHomeSquare4 == 1 ? "y" : "";
                gameState[32] = game.GreenHomeSquare1 == 1 ? "g" : "";
                gameState[33] = game.GreenHomeSquare2 == 1 ? "g" : "";
                gameState[34] = game.GreenHomeSquare3 == 1 ? "g" : "";
                gameState[35] = game.GreenHomeSquare4 == 1 ? "g" : "";
                gameState[36] = game.RedHomeSquare1 == 1 ? "r" : "";
                gameState[37] = game.RedHomeSquare2 == 1 ? "r" : "";
                gameState[38] = game.RedHomeSquare3 == 1 ? "r" : "";
                gameState[39] = game.RedHomeSquare4 == 1 ? "r" : "";
                gameState[40] = game.BlueHomeSquare1 == 1 ? "b" : "";
                gameState[41] = game.BlueHomeSquare2 == 1 ? "b" : "";
                gameState[42] = game.BlueHomeSquare3 == 1 ? "b" : "";
                gameState[43] = game.BlueHomeSquare4 == 1 ? "b" : "";

                gameState[44] = game.YellowCenterSquare1 == 1 ? "y" : "";
                gameState[45] = game.YellowCenterSquare2 == 1 ? "y" : "";
                gameState[46] = game.YellowCenterSquare3 == 1 ? "y" : "";
                gameState[47] = game.YellowCenterSquare4 == 1 ? "y" : "";
                gameState[48] = game.GreenCenterSquare1 == 1 ? "g" : "";
                gameState[49] = game.GreenCenterSquare2 == 1 ? "g" : "";
                gameState[50] = game.GreenCenterSquare3 == 1 ? "g" : "";
                gameState[51] = game.GreenCenterSquare4 == 1 ? "g" : "";
                gameState[52] = game.RedCenterSquare1 == 1 ? "r" : "";
                gameState[53] = game.RedCenterSquare2 == 1 ? "r" : "";
                gameState[54] = game.RedCenterSquare3 == 1 ? "r" : "";
                gameState[55] = game.RedCenterSquare4 == 1 ? "r" : "";
                gameState[56] = game.BlueCenterSquare1 == 1 ? "b" : "";
                gameState[57] = game.BlueCenterSquare2 == 1 ? "b" : "";
                gameState[58] = game.BlueCenterSquare3 == 1 ? "b" : "";
                gameState[59] = game.BlueCenterSquare4 == 1 ? "b" : "";

                return gameState;
            }
        }
        
    }

    public partial class GamePlayPage : ContentPage
    {
        string hubAddress = App.hubAddress;//Running locally depends on the local API project's port number
        string APIAddress = App.APIAddress;//Running locally depends on the local API project's port number
        string groupName; //For SignalR Group
        Game game;
        public List<Comment> comments { get; set; } = new List<Comment>();

        List<Game> gameList; //List of all games
        List<PlayerGame> pgList; //List of all PlayerGames
        List<Player> playerList; //List of all players
        List<(Player, string)> PlayersInThisGame; //List of players playing in the current game
        HubConnection _connection;
        public GamePlayPage()
        {
            InitializeComponent();           
            Draw.Clicked += Draw_Clicked; //Draw event button clicked
            Move.Clicked += Move_Clicked; //Move event button clicked
            btnResize.Clicked += Resize_Clicked; //Resize event button clicked
            SkipTurn.Clicked += SkipTurn_Clicked;
            btnAddComputer.Clicked += BtnAddComputer_Clicked;
            btnChat.Clicked += BtnChat_Clicked;
            //if(App.GameID != Guid.Empty)
            //{
            //    FillLabels();
            //    GraphicsDrawable.Game = game;
            //}
            //graphicsView.Drawable = new GraphicsDrawable();
            List<int> Numbers = new List<int>(60);
            for (int i = 0; i < 60; i++)
            {
                Numbers.Add(i);
            }
            pickerStart.ItemsSource = Numbers;
            pickerEnd.ItemsSource = Numbers;
            pickerStart.SelectedIndex = 0;
            pickerEnd.SelectedIndex = 0;

        }

        private async void BtnAddComputer_Clicked(object? sender, EventArgs e)
        {
            if (App.GameID == Guid.Empty)
            {
                await DisplayAlert("Error", "No game created to add a computer to", "Ok");
                return;
            }
            else if (game.GameComplete == 1)
            {
                await DisplayAlert("Winner", "Game has been won!", "Ok");
                return;
            }
            else if (App.hasGameStarted(game))
            {
                await DisplayAlert("Error", "Game has started.  Cannot Add new players.", "Ok");
                return;
            }
            else if (App.PlayerID == Guid.Empty)
            {
                await DisplayAlert("Error", "Must be logged in to add a computer player!", "Ok");
                return;
            }
            else
            {
                bool keepGoing = false;
                foreach (var item in PlayersInThisGame) //Make sure only the player whose is part of the game and of the right color can make a move
                {
                    if (item.Item1.Id == App.PlayerID)
                    {
                        if (game.PlayerTurn.Substring(0, 1).ToLower() == item.Item2.Substring(0, 1).ToLower())
                        {
                            keepGoing = true;
                        }
                    }

                }
                if (!keepGoing)
                {
                    await DisplayAlert("Errror", "Must be part of game to add a computer player", "Ok");
                    return;
                }
            }



            string ColorsInUse = "";
            string ComputerColor = "";
            for (int i = 0; i < PlayersInThisGame.Count; i++)
            {
                ColorsInUse += PlayersInThisGame[i].Item2.Trim().Substring(0, 1).ToLower();
            }
            if(ColorsInUse.Length >= 4)
            {
                await DisplayAlert("Error", "Already 4 players.  Cannot Add a Computer.", "Ok");
                return;
            }
            if (!ColorsInUse.Contains("y"))
            {
                ComputerColor = "y";
            }
            else if (!ColorsInUse.Contains("b"))
            {
                ComputerColor = "b";
            }
            else if (!ColorsInUse.Contains("r"))
            {
                ComputerColor = "r";
            }
            else if (!ColorsInUse.Contains("g"))
            {
                ComputerColor = "g";
            }
            //Add Computer PlayerID and GameID to PlayerGameTable, so new Game has you assigned as a player
            PlayerGame pg = new PlayerGame
            {
                IsComputerPlaying = true,
                GameId = App.GameID,
                PlayerId = new Guid("11111111-1111-1111-1111-111111111111"),
                PlayerColor = ComputerColor
            };
            
            HttpClient client = new HttpClient();
            bool rollback = false;
            var serializedObject = JsonConvert.SerializeObject(pg);
            var content = new StringContent(serializedObject);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(new Uri(APIAddress + "playergame/" + rollback), content);
            string result = response.Content.ReadAsStringAsync().Result;
            //result = result.Replace("\'", "").Replace("\\", "").Replace("\"", "").Trim();

            await FillLabels();
            UpdateOtherGameBoards();
        }

        private async void SkipTurn_Clicked(object? sender, EventArgs e)
        {
            if (App.GameID == Guid.Empty)
            {
                await DisplayAlert("Error", "Must be in a game to skip a move!", "Ok");
                return;
            }
            else if (App.PlayerID == Guid.Empty)
            {
                await DisplayAlert("Error", "Must be logged in to skip a move!", "Ok");
                return;
            }
            if (game != null && game.GameComplete == 1)
            {
                GraphicsDrawable.Game = game;  //Add game variable value, so Draw() method will draw every filled square on the Game Board              
                graphicsView.Drawable = new GraphicsDrawable();
                await DisplayAlert("Winner", "Game has been won!", "Ok");
                return;
            }

            game.Id = App.GameID;
            game = gameList.Where(g => g.Id == App.GameID).First();

            bool keepGoing = false;
            foreach (var item in PlayersInThisGame) //Make sure only the player whose is part of the game and of the right color can skip a turn
            {
                if (item.Item1.Id == App.PlayerID)
                {
                    if (game.PlayerTurn.Substring(0, 1).ToLower() == item.Item2.Substring(0, 1).ToLower())
                    {
                        keepGoing = true;
                    }
                }

            }
            if (!keepGoing)
            {
                await DisplayAlert("Errror", "You can't skip someone else's move", "Ok");
                return;
            }

            HttpClient client = new HttpClient();
            game.Id = App.GameID;
            game = gameList.Where(g => g.Id == App.GameID).First();

            string serializedObject = JsonConvert.SerializeObject(game);
            var content = new StringContent(serializedObject);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = client.PutAsync(new Uri(APIAddress + "Game/skip"), content).Result;
            string result = response.Content.ReadAsStringAsync().Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK && result == "true")
            {
                string responseBody = await client.GetStringAsync(new Uri(APIAddress + "game/"));
                if (responseBody != null)
                {
                    Title = "Game Data Retrieved";

                    gameList = JsonConvert.DeserializeObject<List<Game>>(responseBody);
                    game = gameList.Where(g => g.Id == App.GameID).First();
                    await FillLabels();
                    GraphicsDrawable.Game = game;
                    graphicsView.Drawable = new GraphicsDrawable();//Must make new GraphicsDrawable() and assign it the the xaml element graphicsView every time I want to change the Game Board.
                    UpdateOtherGameBoards();
                }
            }
        }

        private void StepperResize_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            DisplayStepper.Text = StepperResize.Value.ToString();
        }
        private void Resize_Clicked(object? sender, EventArgs e) //Resize the size of the Game Board and the layout containing it, after the resize button is clicked
        {
            GraphicsDrawable.resize = float.Parse(StepperResize.Value.ToString());
            float VHeight = 800 + 350 + (290 * float.Parse(StepperResize.Value.ToString())); ;
            float VWidth = 1400 + (250 * float.Parse(StepperResize.Value.ToString()));
            float GHeight = 250 * float.Parse(StepperResize.Value.ToString());
            float GWidth = 500 + (290 * float.Parse(StepperResize.Value.ToString()));

            Vlayout.HeightRequest = VHeight;  //Resizes the layout
            Vlayout.WidthRequest = VWidth;
            graphicsView.HeightRequest = GHeight; //Resizes the Game Board Image
            graphicsView.WidthRequest = GWidth;

            graphicsView.Drawable = new GraphicsDrawable();
            this.InvalidateMeasure(); //Needed on android so page reloads to change size whenever the graphicsView (Game Board) gets bigger


            //double maxHeight = DeviceDisplay.MainDisplayInfo.Height;
            //double maxWidth = DeviceDisplay.MainDisplayInfo.Width;
            //Vlayout.HeightRequest = VHeight < maxHeight ? VHeight : maxHeight - 200;
            //graphicsView.HeightRequest = GHeight < maxHeight - 700 ? GHeight : maxHeight - 700;
            //Vlayout.WidthRequest = VWidth < maxWidth ? VWidth : maxWidth;
            //graphicsView.WidthRequest = VWidth < maxWidth ? VWidth : maxWidth;
        }

        private async void Move_Clicked(object? sender, EventArgs e) //Make a move
        {
            await MakeMove();
            //GraphicsDrawable.Game = game;
            //graphicsView.Drawable = new GraphicsDrawable(); //Must make new GraphicsDrawable() and assign it the the xaml element graphicsView every time I want to change the Game Board.
        }

        private async Task MakeMove() //Add logic so only the player whose turn it is can move
        {
            if(App.GameID == Guid.Empty)
            {
                await DisplayAlert("Error", "No game is started!", "Ok");
                return;
            }
            if (App.PlayerID == Guid.Empty)
            {
                await DisplayAlert("Error", "Cannot move unless part of game!", "Ok");
                return;
            }
            HttpClient client = new HttpClient();

            int startSquare = int.Parse(pickerStart.ItemsSource[pickerStart.SelectedIndex].ToString());
            int endSquare = int.Parse(pickerEnd.ItemsSource[pickerEnd.SelectedIndex].ToString());
            game.Id = App.GameID;
            game = gameList.Where(g => g.Id == App.GameID).First();
            if (game.GameComplete == 1)
            {
                GraphicsDrawable.Game = game;  //Add game variable value, so Draw() method will draw every filled square on the Game Board              
                graphicsView.Drawable = new GraphicsDrawable();
                await DisplayAlert("Winner", "Game has been won!", "Ok");
                return;
            }
            bool keepGoing = false;
            foreach(var item in PlayersInThisGame) //Make sure only the player whose is part of the game and of the right color can make a move
            {
                if (item.Item1.Id == App.PlayerID)
                {
                    if(game.PlayerTurn.Substring(0,1).ToLower() == item.Item2.Substring(0, 1).ToLower())
                    {
                        keepGoing = true;
                    }
                }
                
            }
            if (!keepGoing)
            {
                await DisplayAlert("Errror", "You can't move for someone else", "Ok");
                return;
            }

            string serializedObject = JsonConvert.SerializeObject(game);
            var content = new StringContent(serializedObject);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = client.PutAsync(new Uri(APIAddress + "Game/" + startSquare + "/" + endSquare + "/1"), content).Result;
            string result = response.Content.ReadAsStringAsync().Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK && result == "true")
            {
                string responseBody = await client.GetStringAsync(new Uri(APIAddress + "game/"));
                if (responseBody != null)
                {
                    Title = "Game Data Retrieved";

                    gameList = JsonConvert.DeserializeObject<List<Game>>(responseBody);
                    game = gameList.Where(g => g.Id == App.GameID).First();

                    //Need to still be able to play if less than 4 players are playing.
                    //Would be best to rework this code and put it in the BL Managers / API, allowing for games with less than 4 players
                        //string CurrentColors = "";
                        //foreach (var item in PlayersInThisGame) 
                        //{
                        //    CurrentColors += item.Item2.Substring(0, 1).ToLower();
                        //}
                        //while(!CurrentColors.Contains(game.PlayerTurn.Substring(0, 1).ToLower()) 
                        //    && (CurrentColors.Contains("y") ||
                        //        CurrentColors.Contains("b") ||
                        //        CurrentColors.Contains("r") ||
                        //        CurrentColors.Contains("g")))
                        //{
                        //    if (game.PlayerTurn.Substring(0, 1).ToLower() == "y")
                        //        game.PlayerTurn = "b";
                        //    else if (game.PlayerTurn.Substring(0, 1).ToLower() == "b")
                        //        game.PlayerTurn = "r";
                        //    else if (game.PlayerTurn.Substring(0, 1).ToLower() == "r")
                        //        game.PlayerTurn = "g";
                        //    else if (game.PlayerTurn.Substring(0, 1).ToLower() == "g")
                        //        game.PlayerTurn = "y";
                        //}
                        //serializedObject = JsonConvert.SerializeObject(game);
                        //content = new StringContent(serializedObject);
                        //content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                        //response = client.PutAsync(new Uri(APIAddress + "Game/"+ App.GameID +"/false"), content).Result;
                        //result = response.Content.ReadAsStringAsync().Result;

                    await FillLabels();
                    GraphicsDrawable.Game = game;
                    graphicsView.Drawable = new GraphicsDrawable();//Must make new GraphicsDrawable() and assign it the the xaml element graphicsView every time I want to change the Game Board.                    
                    UpdateOtherGameBoards();
                    if (game.GameComplete == 1)
                    {
                        await DisplayAlert("Winner", "Game has been won!", "Ok");
                        return;
                    }
                }
                else
                {
                    await DisplayAlert("Errror", "Data Not Retrieved", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Errror", "Your Move is not Valid!", "Ok");
            }

        }
        private void Draw_Clicked(object? sender, EventArgs e) //Draw a cirlce/Rectangle on the Game Board.  Good for testing purposes.
        {
            GraphicsDrawable.xLabel = float.Parse(xLabel.Text);
            GraphicsDrawable.yLabel = float.Parse(yLabel.Text);
            GraphicsDrawable.widthLabel = float.Parse(widthLabel.Text);
            GraphicsDrawable.heightLabel = float.Parse(heightLabel.Text);
            graphicsView.Drawable = new GraphicsDrawable();
        }

        public async Task FillLabels()
        {
            HttpClient client = new HttpClient();
            string responseBody = await client.GetStringAsync(new Uri(APIAddress + "game/"));
            string responseBody2 = await client.GetStringAsync(new Uri(APIAddress + "playergame/"));
            string responseBody3 = await client.GetStringAsync(new Uri(APIAddress + "player/"));
            if (responseBody != null && responseBody2 != null && responseBody3 != null)
            {
                Title = "Game Data Retrieved";

                gameList = JsonConvert.DeserializeObject<List<Game>>(responseBody);
                pgList = JsonConvert.DeserializeObject<List<PlayerGame>>(responseBody2);
                playerList = JsonConvert.DeserializeObject<List<Player>>(responseBody3);

                PlayersInThisGame = new List<(Player, string)>();

                game = gameList.Where(g => g.Id == App.GameID).First(); //Get the current game, based on the global GameID property.  App.GameID was set on the JoinGameRoomPage.
                for (int i = 0; i < pgList.Count; i++)
                {
                    if (pgList[i].GameId == game.Id)
                    {   //Determine which players are playing in the game
                        PlayersInThisGame.Add((playerList.FirstOrDefault(p => p.Id == pgList[i].PlayerId), pgList[i].PlayerColor.ToString()));                    
                    }
                }
                DieRoll.Text = "Die Roll: " + game.DieRoll.ToString(); //Set the Die Roll Text Box based on the game.DieRoll Property
                switch (game.PlayerTurn.ToString().ToLower().Substring(0, 1))
                {
                    case "y":  //Set the color of the CurrentPlayer label, to show whose turn it is.
                        CurrentPlayer.BackgroundColor = Colors.Yellow;
                        break;
                    case "b":
                        CurrentPlayer.BackgroundColor = Colors.Blue;
                        break;
                    case "r":
                        CurrentPlayer.BackgroundColor = Colors.Red;
                        break;
                    case "g":
                        CurrentPlayer.BackgroundColor = Colors.Green;
                        break;
                }
                for (int i = 0; i < PlayersInThisGame.Count; i++)
                {
                    switch (PlayersInThisGame[i].Item2.ToLower().Substring(0, 1))
                    {
                        case "y": //Change the Player1 Label Text, to say who is Playing as yellow
                            Player1.Text = PlayersInThisGame[i].Item1.UserName;
                            break;
                        case "b": //Change the Player2 Label Text, to say who is Playing as blue
                            Player2.Text = PlayersInThisGame[i].Item1.UserName;
                            break;
                        case "r": //Change the Player3 Label Text, to say who is Playing as red
                            Player3.Text = PlayersInThisGame[i].Item1.UserName;
                            break;
                        case "g": //Change the Player4 Label Text, to say who is Playing as green
                            Player4.Text = PlayersInThisGame[i].Item1.UserName;
                            break;
                    }
                }
            }
        }
        protected override void OnDisappearing() //Every time leaving the page or clicking on a new tab
        {
            string groupName = "g" + App.GameID.ToString();
            if(_connection != null)
                _connection.StopAsync();

            base.OnDisappearing();
            //Reset data, so clicking on a new game will not show old Player Data
            Player1.Text = "";
            Player2.Text = "";
            Player3.Text = "";
            Player4.Text = "";
            TypeMessage.Text = "";
            DieRoll.Text = "Die Roll: ";
            CurrentPlayer.BackgroundColor = Colors.Yellow;
            PlayersInThisGame = new List<(Player, string)>();
            pickerStart.SelectedIndex = 0;
            pickerEnd.SelectedIndex = 0;
            comments = new List<Comment>();
            ShowChatMessage.Text = "";
            game = new Game();
            GraphicsDrawable.Game = null;
            GraphicsDrawable.resize = 1;
            GraphicsDrawable.xLabel = 0;
            GraphicsDrawable.yLabel = 0;
            GraphicsDrawable.widthLabel = 0;
            GraphicsDrawable.heightLabel = 0;
            App.GameID = Guid.Empty;
        }
        protected async override void OnAppearing() //Every time page appears
        {
            base.OnAppearing();
            pickerStart.SelectedIndex = 0;
            pickerEnd.SelectedIndex = 0;
            if (App.GameID != Guid.Empty)
            {
                await FillLabels();
                GraphicsDrawable.Game = game;  //Add game variable value, so Draw() method will draw every filled square on the Game Board              
            }
            graphicsView.Drawable = new GraphicsDrawable(); //Need to make a new graphics drawable to redraw Game Board
            await ConnectToChannel();
        }
        private async Task ConnectToChannel()
        {
            //connection = new SignalRConnection(hubAddress); //If I wanted to use the API Client Class in ZGT.Utility project
            //connection.Start();
            //HubConnection _connection = connection.HubConnection;

            _connection = new HubConnectionBuilder()
                .WithUrl(hubAddress)
                .Build();
            await _connection.StartAsync();
            groupName = "g" + App.GameID.ToString();
            await _connection.InvokeAsync("JoinGroup", groupName);
            _connection.On<string, string>("ReceiveMessage", (s1, s2) => UpdateMessages(s1, s2));
            _connection.On("UpdateGameBoard", async () => await UpdateGameBoard());
            _connection.On("GroupJoined", async() => await GroupJoined());
            await _connection.InvokeAsync("GroupJoined", groupName);
            //_connection.InvokeAsync("UpdateGameBoard", groupName);
            //_connection.InvokeAsync("SendMessage", groupName, App.PlayerUserName, "Message Text");
        }

        private async void BtnChat_Clicked(object? sender, EventArgs e)
        {
            if (App.PlayerID != Guid.Empty && !string.IsNullOrEmpty(App.PlayerUserName) && App.GameID != Guid.Empty && _connection != null)
            {
                await SendMessageToChannel(TypeMessage.Text);
                TypeMessage.Text = "";
            }
            else
            {
                await DisplayAlert("Error", "Must be Logged in & at a game to Chat!", "Ok");
            }
        }
        private async Task UpdateOtherGameBoards() //When I change my GameBoard
        {
            await _connection.InvokeAsync("UpdateGameBoard", groupName);
        }
        private async Task SendMessageToChannel(string message) //When I write a message
        {
            comments.Add(new Comment() { UserName = App.PlayerUserName, Message = message });

            string groupName = "g" + App.GameID.ToString();
            await _connection.InvokeAsync("SendMessage", groupName, App.PlayerUserName, message);
        }

        private void UpdateMessages(string name, string message) //When I recieve messages
        {
            MainThread.BeginInvokeOnMainThread(() => //Without running in the main thread, SignalR callback methods (or any method) cannot access the UI
            {
                try
                {

                    comments.Add(new Comment() { UserName = name, Message = message });
                    ShowChatMessage.Text += name + " : " + message + " \n";

                }
                catch(Exception ex)
                {
                }
            });


        }
        private async Task GroupJoined() //When I recieve messages
        {
            MainThread.BeginInvokeOnMainThread(async () => //Without running in the main thread, SignalR callback methods (or any method) cannot access the UI
            {
                if(App.GameID != Guid.Empty)
                    await FillLabels();
            });


        }
        private async Task UpdateGameBoard() //When I receive changes to Game Board from other players.
        {
            MainThread.BeginInvokeOnMainThread(async () => //Without running in the main thread, SignalR callback methods (or any method) cannot access the UI
            {
                HttpClient client = new HttpClient();
                string responseBody = await client.GetStringAsync(new Uri(APIAddress + "game/"));
                if (responseBody != null)
                {
                    Title = "Game Data Retrieved";

                    gameList = JsonConvert.DeserializeObject<List<Game>>(responseBody);
                    game = gameList.Where(g => g.Id == App.GameID).First();

                    await FillLabels();
                
                    GraphicsDrawable.Game = game;
                    graphicsView.Drawable = new GraphicsDrawable();//Must make new GraphicsDrawable() and assign it the the xaml element graphicsView every time I want to change the Game Board. 
                                
                }
            });
        }
    }

}
//int x = (int)dirtyRect.Width;
//int y = (int)dirtyRect.Height;


//canvas.FillColor = Colors.Yellow;
//canvas.FillRectangle(dirtyRect.Left, dirtyRect.Top, x / 2, (y / 11));
//canvas.FillColor = Colors.Blue;
//canvas.FillRectangle(dirtyRect.Right / 2, dirtyRect.Top, x / 2, (y / 11));
//canvas.FillColor = Colors.Green;
//canvas.FillRectangle(dirtyRect.Left, dirtyRect.Bottom - (y / 11), x / 2, (y / 11));
//canvas.FillColor = Colors.Red;
//canvas.FillRectangle(dirtyRect.Right / 2, dirtyRect.Bottom - (y / 11), x / 2, (y / 11));
///*
//canvas.FillRectangle(dirtyRect.Left, dirtyRect.Top, (x / 11), (y / 11));
//canvas.FillRectangle(dirtyRect.Left + (x / 11), dirtyRect.Top, (x / 11), (y / 11));
//canvas.FillRectangle(dirtyRect.Left + 2 * (x / 11), dirtyRect.Top, (x / 11), (y / 11));
//canvas.FillRectangle(dirtyRect.Left + 3 * (x / 11), dirtyRect.Top, (x / 11), (y / 11));
//canvas.FillRectangle(dirtyRect.Left + 4 * (x / 11), dirtyRect.Top, (x / 11), (y / 11));
//canvas.FillRectangle(dirtyRect.Left + 5 * (x / 11), dirtyRect.Top, (x / 11), (y / 11));
//canvas.FillColor = Colors.Blue;
//canvas.FillRectangle(dirtyRect.Left + 6 * (x / 11), dirtyRect.Top, (x / 11), (y / 11));
//canvas.FillRectangle(dirtyRect.Left + 7 * (x / 11), dirtyRect.Top, (x / 11), (y / 11));
//canvas.FillRectangle(dirtyRect.Left + 8 * (x / 11), dirtyRect.Top, (x / 11), (y / 11));
//canvas.FillRectangle(dirtyRect.Left + 9 * (x / 11), dirtyRect.Top, (x / 11), (y / 11));
//canvas.FillRectangle(dirtyRect.Left + 10 * (x / 11), dirtyRect.Top, (x / 11), (y / 11));
//*/
//canvas.StrokeColor = Colors.Black;
//canvas.StrokeSize = 2;

//canvas.DrawLine(dirtyRect.Left, dirtyRect.Top, dirtyRect.Right, dirtyRect.Top);
//canvas.DrawLine(dirtyRect.Left, dirtyRect.Top + (y / 11), dirtyRect.Right, dirtyRect.Top + (y / 11));

//canvas.DrawLine(dirtyRect.Left, dirtyRect.Bottom - (y / 11), dirtyRect.Right, dirtyRect.Bottom - (y / 11));
//canvas.DrawLine(dirtyRect.Left, dirtyRect.Bottom, dirtyRect.Right, dirtyRect.Bottom);