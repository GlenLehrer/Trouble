using ZGT.Trouble.BL.Models;

namespace ZGT.Trouble.MAUI.UI
{
    public partial class App : Application
    {
        public static string SessionKey = "";
        public static Guid GameID { get; set; } = Guid.Empty;
        public static Guid PlayerID { get; set; } = Guid.Empty;
        public static string PlayerUserName { get; set; } = "";

        public static string hubAddress = "https://localhost:7009/uihub"; //Running locally depends on the local API project's port number. If remote server, change connection string

        public static string APIAddress = "https://localhost:7009/api/"; //Running locally depends on the local API project's port number.  If remote server, change connection string
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            Application.Current.UserAppTheme = AppTheme.Dark;
        }
        public static bool hasGameStarted(Game game) //Determines if a game has started.  If a game has started, there should be no new players joining.
        {
            //If HomeSquares are all full, no pieces have been moved.  Technically, a game could have started, but nothing has happened yet.
            //Method returns true if all pieces are on home squares and it is yellow's turn.
            if (
                game.PlayerTurn.ToLower().StartsWith("y") &&
                game.YellowHomeSquare1 == 1 &&
                game.YellowHomeSquare2 == 1 &&
                game.YellowHomeSquare3 == 1 &&
                game.YellowHomeSquare4 == 1 &&
                game.BlueHomeSquare1 == 1 &&
                game.BlueHomeSquare2 == 1 &&
                game.BlueHomeSquare3 == 1 &&
                game.BlueHomeSquare4 == 1 &&
                game.RedHomeSquare1 == 1 &&
                game.RedHomeSquare2 == 1 &&
                game.RedHomeSquare3 == 1 &&
                game.RedHomeSquare4 == 1 &&
                game.GreenHomeSquare1 == 1 &&
                game.GreenHomeSquare2 == 1 &&
                game.GreenHomeSquare3 == 1 &&
                game.GreenHomeSquare4 == 1
            )
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
