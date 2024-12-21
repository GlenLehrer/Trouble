using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGT.Trouble.MAUI.UI.Models
{
    public class GameData //Model to display Data on the JoinGameRoomPage.  Displays data for each row and column on the grid.
    {
        public Guid GameID { get; set; }
        public int GameNumber { get; set; }
        public string UserNamesAndColor { get; set; } = "";
        public string IsGameStarted { get; set; } = "";

    }
}
