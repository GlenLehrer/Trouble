using System.Windows.Input;
using ZGT.Trouble.BL.Models;
using Newtonsoft.Json;
using ZGT.Trouble.MAUI.UI.Models;
using Org.BouncyCastle.Asn1.Ocsp;

namespace ZGT.Trouble.MAUI.UI.Views;
/*Page displays Game.  
 * The goal here is to have a room that displays a list of games to join to play or observe, and have a lobby chat feature with SignalR
 * In Order to display the games, I need to fetch the game data from the API.
 * Next to each game listed should be a button to join or observe a game.  Page takes a few moments to load
 */
public partial class JoinGameRoomPage : ContentPage
{
    bool NoRedirect = true;
    bool JoinNotCreate = true;
    int clicked = 0;
    public List<GameData> entities { get; set; } = new List<GameData>();
    string hubAddress = App.hubAddress;//Running locally depends on the local API project's port number
    string APIAddress = App.APIAddress;//Running locally depends on the local API project's port number
    public ICommand NavigateCommand { get; private set; }

    List<Game> gameList;
    List<PlayerGame> pgList;
    List<Player> playerList;

    public JoinGameRoomPage()
    {
        InitializeComponent();
        //NavigateCommand = new Command<Type>(
        //async (Type pageType) =>
        //{
        //    Page page = (Page)Activator.CreateInstance(pageType);
        //});

    }
    protected override async void OnAppearing()  //Every time page appears.
    {
        base.OnAppearing();
        NoRedirect = true;
        JoinNotCreate = true;
        App.GameID = Guid.Empty;
        BindingContext = this;
        //ConnectToChannel(); //Connect SignalR
        await Reload(); //Reload Page elements in case there are any new changes
        clicked = 0; //Reset variable. Clicked is for when OnDisappearing ends up calling itself.
                    //Redirecting the page in OnDisappearing makes the method call itself.
                    //Since I can't stop OnDisappearing from being called, I make sure that OnDisppearing only calls itself 1 additional time.
    }
    protected override async void OnDisappearing()
    { 

        entities  = new List<GameData>(); //reset values put onto JoinGamePage rows/cols

        clicked++; //OnDisappearing seems to end up calling itself when I have a page change in this message
                   //Example:  await Shell.Current.GoToAsync("///GamePlayPage"); Code ends up calling OnDisppearing.
                   //Clicked prevents extra calls by having a second call skip the if statements.
        base.OnDisappearing();
        if(!NoRedirect)
        {
            if (!JoinNotCreate && App.PlayerID != Guid.Empty && clicked == 1)
            {
                Game game = new Game()  //New Game.  All pieces start on their home squares.
                {
                    Id = Guid.NewGuid(),
                    PlayerTurn = "y",
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
                //test create game with all squares full
                //Game game = new Game()
                //{
                //    Id = Guid.NewGuid(),
                //    PlayerTurn = "y",
                //    DieRoll = 6,
                //    GameStartDate = DateTime.Now,
                //    GameComplete = 0,
                //    YellowHomeSquare1 = 1,
                //    YellowHomeSquare2 = 1,
                //    YellowHomeSquare3 = 1,
                //    YellowHomeSquare4 = 1,
                //    BlueHomeSquare1 = 1,
                //    BlueHomeSquare2 = 1,
                //    BlueHomeSquare3 = 1,
                //    BlueHomeSquare4 = 1,
                //    RedHomeSquare1 = 1,
                //    RedHomeSquare2 = 1,
                //    RedHomeSquare3 = 1,
                //    RedHomeSquare4 = 1,
                //    GreenHomeSquare1 = 1,
                //    GreenHomeSquare2 = 1,
                //    GreenHomeSquare3 = 1,
                //    GreenHomeSquare4 = 1,
                //    BlueStartSquare = "y",
                //    YellowStartSquare = "y",
                //    RedStartSquare = "y",
                //    GreenStartSquare = "y",
                //    Square1 = "y",
                //    Square2 = "y",
                //    Square3 = "y",
                //    Square4 = "y",
                //    Square5 = "y",
                //    Square6 = "y",
                //    Square7 = "y",
                //    Square8 = "y",
                //    Square9 = "y",
                //    Square10 = "y",
                //    Square11 = "y",
                //    Square12 = "y",
                //    Square13 = "y",
                //    Square14 = "y",
                //    Square15 = "y",
                //    Square16 = "y",
                //    Square17 = "y",
                //    Square18 = "y",
                //    Square19 = "y",
                //    Square20 = "y",
                //    Square21 = "y",
                //    Square22 = "y",
                //    Square23 = "y",
                //    Square24 = "y",
                //    YellowCenterSquare1 = 1,
                //    YellowCenterSquare2 = 1,
                //    YellowCenterSquare3 = 1,
                //    YellowCenterSquare4 = 1,
                //    BlueCenterSquare1 = 1,
                //    BlueCenterSquare2 = 1,
                //    BlueCenterSquare3 = 1,
                //    BlueCenterSquare4 = 1,
                //    RedCenterSquare1 = 1,
                //    RedCenterSquare2 = 1,
                //    RedCenterSquare3 = 1,
                //    RedCenterSquare4 = 1,
                //    GreenCenterSquare1 = 1,
                //    GreenCenterSquare2 = 1,
                //    GreenCenterSquare3 = 1,
                //    GreenCenterSquare4 = 1,
                //};
                //Create new game object in API
                HttpClient client = new HttpClient();
                bool rollback = false;
                string serializedObject = JsonConvert.SerializeObject(game);
                var content = new StringContent(serializedObject);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await client.PostAsync(new Uri(APIAddress + "game/" + rollback), content);
                string result = response.Content.ReadAsStringAsync().Result;
                result = result.Replace("\'", "").Replace("\\", "").Replace("\"", "").Trim();
                App.GameID = new Guid(result); //Assign GameID so that PlayGame page will load correct game

                //Add PlayerID and GameID to PlayerGameTable, so new Game has you assigned as a player.  Default PlayerColor is Yellow
                rollback = false;
                serializedObject = JsonConvert.SerializeObject(new PlayerGame { GameId = new Guid(result), PlayerId = App.PlayerID, PlayerColor = "yellow" });
                content = new StringContent(serializedObject);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(new Uri(APIAddress + "playergame/" + rollback), content);
                result = response.Content.ReadAsStringAsync().Result;
                //result = result.Replace("\'", "").Replace("\\", "").Replace("\"", "").Trim();

                await Shell.Current.GoToAsync("///GamePlayPage");

            }
            else if (JoinNotCreate && clicked == 1 && App.GameID != Guid.Empty) //&& App.PlayerID != Guid.Empty)
            {
                Game game = gameList.Where(data => data.Id == App.GameID).First();
                List<PlayerGame> limit = pgList.Where(pg => pg.GameId == App.GameID).ToList();
                List<Player> CurrentPlayers = new List<Player>();
                string currentColors = "";
                foreach (PlayerGame pg in limit)
                {
                    CurrentPlayers.Add(playerList.Where(pl => pl.Id == pg.PlayerId).First());
                    currentColors += pg.PlayerColor.Substring(0, 1).ToLower();
                }
                if (CurrentPlayers.Count >= 4 || App.hasGameStarted(game) || App.PlayerID == Guid.Empty)
                {
                    //Unable to Join Game.  4 Player Max.  Can't join a started game.  Join as Observer
                    await Shell.Current.GoToAsync("///GamePlayPage");
                }
                else if (CurrentPlayers.Count < 4 && App.PlayerID != Guid.Empty && CurrentPlayers.Find(p => p.Id == App.PlayerID) == null) //Only add to player Game table if player is not already part of game
                {
                    PlayerGame pg = new PlayerGame
                    {
                        IsComputerPlaying = false,
                        GameId = App.GameID,
                        PlayerId = App.PlayerID
                    };
                    //Join game.  Need to join with an appropriate unused color
                    if (!currentColors.Contains("y"))
                    {
                        pg.PlayerColor = "y";
                    }
                    else if (!currentColors.Contains("b"))
                    {
                        pg.PlayerColor = "b";
                    }
                    else if (!currentColors.Contains("r"))
                    {
                        pg.PlayerColor = "r";
                    }
                    else if (!currentColors.Contains("g"))
                    {
                        pg.PlayerColor = "g";
                    }
                    //Add PlayerID and GameID to PlayerGameTable, so new Game has you assigned as a player
                    HttpClient client = new HttpClient();
                    bool rollback = false;
                    var serializedObject = JsonConvert.SerializeObject(pg);
                    var content = new StringContent(serializedObject);
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    var response = await client.PostAsync(new Uri(APIAddress + "playergame/" + rollback), content);
                    string result = response.Content.ReadAsStringAsync().Result;
                    //result = result.Replace("\'", "").Replace("\\", "").Replace("\"", "").Trim();

                }
                //Need logic to determine if game has started or not.
                //If game has not started & is not full, join as a player
                //If game is full or has started, join as an observer
                await Shell.Current.GoToAsync("///GamePlayPage");
            }
            //else if (App.PlayerID == Guid.Empty) //&& clicked <= 1) //Must be LoggedIn to Join a Game
            //{
            //    await Shell.Current.GoToAsync("///LoginPage");
            //}
        }             
    }
    
    private async Task Reload()
    {
        entities = new List<GameData>();
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

            for(int i = 0; i < pgList.Count; i++) //Filling entities will all the data to display on the Page
            {
                //GameData object was made for displaying the data on the grid rows/cols on the JoinGameRoompage
                GameData dataInEntities = entities.Where(data => data.GameID == pgList[i].GameId).FirstOrDefault() ?? new GameData();
                if (dataInEntities.UserNamesAndColor != "")
                {
                    entities.Remove(dataInEntities); //Remove entity, to add more data to the UserNames and Color.  The same entity (row), will have UserNamesAndColors adde multiple times
                    string userName = playerList.Where(data => data.Id == pgList[i].PlayerId).First().UserName;
                    dataInEntities.UserNamesAndColor += userName + " :" + pgList[i].PlayerColor + " | "; //Adding to the UserNamesAndColors
                    entities.Add(dataInEntities);
                }
                else
                {
                    GameData gameData = new GameData();
                    
                    gameData.GameID = pgList[i].GameId;
                    gameData.IsGameStarted = "false";
                    Game game = gameList.Where(g => g.Id == pgList[i].GameId).FirstOrDefault();
                    gameData.IsGameStarted = App.hasGameStarted(game) ? "true" : "false";
                    string userName = playerList.Where(data => data.Id == pgList[i].PlayerId).First().UserName;
                    gameData.UserNamesAndColor += userName + " :" + pgList[i].PlayerColor + " | ";
                    entities.Add(gameData);
                }              
            }
            for(int i = 0; i < entities.Count; i++)
            {
                entities[i].GameNumber = i + 1;
            }
        }               
        await Rebind(); //Uses public List<GameData> entities { get; set; }, for the data it displays
    }
    private async Task Rebind()
    {
        //Rebind() adds controls to Page, JoinRoomPage is empty without this method running.
        Grid PageGrid = new Grid
        {
            BackgroundColor = Colors.MediumSeaGreen,
            RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto }
                },
            ColumnDefinitions =
                {
                    new ColumnDefinition { Width = GridLength.Auto },
                    new ColumnDefinition { Width = GridLength.Auto },
                    new ColumnDefinition { Width = GridLength.Auto },
                    new ColumnDefinition { Width = GridLength.Auto },
                    new ColumnDefinition { Width = GridLength.Auto }
                }
        };
        //PageGrid.AddWithSPan(control, row, col, rowspan, colspan);
        for (int i = 0; i < entities.Count; i++) //Add these controls for each row
        { 
            var command = new Command(o => App.GameID = entities[int.Parse(o.ToString())].GameID ); //Set global property so entire app knows the GameID
            PageGrid.AddWithSpan(new Label
            {
                Text = entities[i].GameNumber.ToString(), //Add a label with the Game Number
                FontAttributes = FontAttributes.Bold,
                //TextColor = Colors.Black,
                FontSize = 24,
                HorizontalOptions = LayoutOptions.Start,
                Padding = 5,
                Margin = 5,
            }, 1 + i, 1, 1, 1);
            PageGrid.AddWithSpan(new Editor
            {
                Text = entities[i].UserNamesAndColor + "\n", //Add a label with the Usernames and Color of each game
                //BackgroundColor = Colors.Gray,
                //TextColor = Colors.Black,
                FontSize = 12,
                HorizontalOptions = LayoutOptions.Start,
                IsReadOnly = true,
                Margin = 5,
            }, 1 + i, 2, 4, 1);
            PageGrid.AddWithSpan(new Label
            {
                Text = entities[i].IsGameStarted == "true" ? "Started" : "Unstarted", //Add a label saying if game has started.  If game has started, no new players can play.  Only observe.
                FontAttributes = FontAttributes.Italic,
                //TextColor = Colors.Black,
                FontSize = 18,
                //BackgroundColor = Colors.Gray,
                HorizontalOptions = LayoutOptions.Fill,
                Padding = 5,
                Margin = 5

            }, 1 + i, 3, 1, 1);
            Button btnJoinGame = new Button
            {
                Text = "JoinGame",  //Add a button to either Join a game or observe it.  If possible to join, button will have you join as an unused color by default
                FontAttributes = FontAttributes.Bold, 
                //TextColor = Colors.Black,
                //BackgroundColor = Colors.White,
                FontSize = 24,
                Command = command,
                CommandParameter = i,
                Padding = 2,
                Margin = 2,
                HorizontalOptions = LayoutOptions.Center
            };
            Button btnDelete = new Button
            {
                Text = "Delete",  //Add a button to either Join a game or observe it.  If possible to join, button will have you join as an unused color by default
                FontAttributes = FontAttributes.Bold,
                //TextColor = Colors.Black,
                //BackgroundColor = Colors.White,
                FontSize = 24,
                Command = command,
                CommandParameter = i,
                Padding = 2,
                Margin = 2,
                HorizontalOptions = LayoutOptions.Center
            };
            btnJoinGame.Clicked += BtnJoinGame_Clicked;
            btnDelete.Clicked += BtnDelete_Clicked;
            PageGrid.AddWithSpan(btnJoinGame, 1 + i, 4, 1, 1);
            PageGrid.AddWithSpan(btnDelete, 1 + i, 5, 1, 1);
        } //Ending Bracket of Creating rows and columns for the Grid Layout
        //views = new VerticalStackLayout()
        //{
        //    Margin = 20,
        //    Spacing = 6,
        //    BackgroundColor = Colors.White,
        //    WidthRequest = 1000,
        //    HeightRequest = 1500,
        //    VerticalOptions = LayoutOptions.FillAndExpand
        //}; //Adding Elements on page are ordered from top down (rather than being placed horizontally)
        Button CreateGame = new Button
        {
            Text = "Create Game",  //Single Button at top of page to create a new game
            //TextColor = Colors.Black,
            CharacterSpacing = 10,
            //BackgroundColor = Colors.Gray,
            Margin = 5,
            HorizontalOptions = LayoutOptions.Center,
        };
        CreateGame.Clicked += CreateGame_Clicked; //Create game click event

        //Removing elements from vertical stack layout, each time page reloads
        int? count = views.Children.Count; //Need to set this variable, because each time a child is remove from views.Children, views.Children.Count changes
        if(count != null && count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                views.Remove(views.Children[0]); //Children also changes its indexes after each.  I must delete each element at the 0 positon.
                                                //After 1 delete, element 1 will now be at element 0.
            }
        }
        //
        //Adding page elements to vertical stack layout
        views.Add(CreateGame); //Add Button to top of VerticalStackLayout
        views.Add(PageGrid); //Add elements in rows/cols grid to VerticalStackLayout

        this.Content = null;
        this.Content = scroll; //Add VerticalStackLayout, and All elements to the page

    }

    private void ContentPage_Loaded(object sender, EventArgs e) //Page Loads
    {
        Reload();
    }
    private async void BtnDelete_Clicked(object? sender, EventArgs e)
    {
        Game game = gameList.Where(data => data.Id == App.GameID).First();
        HttpClient client = new HttpClient();
        HttpResponseMessage response = client.DeleteAsync(APIAddress + "game/" + App.GameID + "/false").Result;
        App.GameID = Guid.Empty;
        await Reload();
    }
    private void BtnJoinGame_Clicked(object sender, EventArgs e)
    {
        NoRedirect = false;  //So, a user can click on another tab and is not forced into the GamePlayPage when the GameRoomPage disappears
                             //Like when a user clicks on another tab on the page that isn't the GamePlayPage     
        JoinNotCreate = true;
        OnDisappearing(); //Method called for Page to Disappear, OnDisappearing was overriden to redirect to other pages
    }

    private void CreateGame_Clicked(object sender, EventArgs e)
    {
        NoRedirect = false;//So, a user can click on another tab and is not forced into the GamePlayPage when the GameRoomPage disappears
                            //Like when a user clicks on another tab on the page that isn't the GamePlayPage
        JoinNotCreate = false;
        OnDisappearing(); //Method called for Page to Disappear, OnDisappearing was overriden to redirect to other pages
    }
}
