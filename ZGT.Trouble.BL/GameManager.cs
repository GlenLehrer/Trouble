using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.SqlServer.Server;
using ZGT.Trouble.BL.Models;

namespace ZGT.Trouble.BL
{
    public class GameManager : GenericManager<tblGame>
    {
        public Guid ComputerGuid { get; set; } = new Guid("11111111-1111-1111-1111-111111111111");
        public GameManager(ILogger logger, DbContextOptions<TroubleEntities> options) : base(logger, options) { }
        public GameManager(DbContextOptions<TroubleEntities> options) : base(options) { }
        public GameManager() { }

        
        public async Task<Guid> InsertAsync(Game game, bool rollback = false)
        {
            try
            {
                tblGame row = Map<Game, tblGame>(game);
                return await base.InsertAsync(row, null, rollback);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> UpdateAsync(Game game, bool rollback = false)
        {
            try
            {
                tblGame row = Map<Game, tblGame>(game);
                return await base.UpdateAsync(row, rollback);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Game> LoadByIdAsync(Guid id)
        {
            try
            {
                //tblGame row = await base.LoadByIdAsync(id);
                //if (row != null)
                //    return Map<tblGame, Game>(row);
                //else
                //    throw new Exception("No Row");
                List<tblGame> rows = await base.LoadAsync(e => e.Id == id);
                if (rows[0] != null)
                    return Map<tblGame, Game>(rows[0]);
                else
                    throw new Exception("No Row");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<Game>> LoadAsync()
        {
            try
            {
                List<Game> rows = new List<Game>();

                (await base.LoadAsync()).ForEach(e => rows.Add(Map<tblGame, Game>(e)));
                return rows;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public override async Task<int> DeleteAsync(Guid id, bool rollback = false)
        {
            try
            {
                int results = 0;

                using (TroubleEntities dc = new TroubleEntities(options))
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblGame row = dc.Set<tblGame>().FirstOrDefault(t => t.Id == id);
                    List<tblPlayerGame> list = dc.Set<tblPlayerGame>().ToList().FindAll(pg => row.Id == pg.GameId);

                    if (row != null)
                    {
                        foreach (tblPlayerGame pg in list)
                        {
                            dc.Set<tblPlayerGame>().Remove(pg);
                            dc.SaveChanges();
                        }
                        dc.Set<tblGame>().Remove(row);
                        results = dc.SaveChanges();
                        if (rollback) transaction.Rollback();
                    }
                    else
                    {
                        throw new Exception("Row does not exist");
                    }

                }
                return results;
            }
            catch (Exception)
            {
                if (logger != null)
                    logger.LogWarning($"Delete {typeof(Game).Name}s - GenericManager");
                throw;
            }
        }
        public String[] MakeGameState(Game game)
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


        public bool IsMoveValid(Game game, int StartSquare, int EndSquare)
        {
            String[] gameState = MakeGameState(game);
            if (StartSquare > 59 || EndSquare > 59)
            {
                return false;
            }
            if (StartSquare == 0 && EndSquare != game.DieRoll)
            {
                return false; //Moves do not match dice roll
            }
            //Can a Dice roll move a piece from StartSquare to EndSquare, not calculating HomeSquares and CenterSquares. 
            if(StartSquare < 28 && EndSquare < 28 && ((EndSquare - StartSquare != game.DieRoll) 
                && StartSquare + EndSquare < 27)) //Possibility of going from from Square 27 to Square 1 is calculated in this line
            {
                    return false;  //Moves do not match dice roll.
            }

            if (gameState.ElementAt(StartSquare) != game.PlayerTurn || gameState.ElementAt(StartSquare) == gameState.ElementAt(EndSquare))
            {
                return false; //Must have piece of correct color on StartSquare.  Cannot move to a square that is already occupied by a piece of the same color.
            }
            if ((EndSquare == 3 || EndSquare == 9 || EndSquare == 15 || EndSquare == 21) && gameState.ElementAt(EndSquare) != "")
            {
                return false;  //Cannot move piece to occupied protected square
            }

            if ((EndSquare >= 28 && EndSquare <= 43) || //A Homesquare cannot be an endsquare
                ((StartSquare >= 28 && StartSquare <= 43) && game.DieRoll != 6)) //Can't move off a homesquare unless a 6 is rolled.
            {
                return false; //HomeSquare Error
            }

            if (game.PlayerTurn == "y")
            {
                if(EndSquare < StartSquare && !(StartSquare >= 28 && StartSquare <= 32))
                {
                    return false;  //Cannot move around the board a second time.  Must move piece into center region.
                }
                if(StartSquare >= 32 && StartSquare <= 43)
                {
                    return false;  //Can't move pieces on a different player's home squares.
                }
                if ((StartSquare >= 28 && StartSquare <=31) && EndSquare != 0)
                {
                    return false;  //Can only move from Yellow HomeSquare to Yellow Start Square
                }

                //Check to to make sure a CenterSquare Move is made only on my color
                if (StartSquare >= 44 && StartSquare <= 59 && !(StartSquare >=44 && StartSquare <= 47))
                {
                    return false;  //StartSquare cannot be on another color's HomeSquare
                }
                if (EndSquare >= 44 && EndSquare <= 59 && !(EndSquare >= 44 && EndSquare <= 47))
                {
                    return false;  //EndSquare cannot be on another color's HomeSquare
                }

                //Test to make sure a CenterSquare Move is valid
                if(StartSquare <= 27 && StartSquare + game.DieRoll > 27 
                    && (gameState.ElementAt(EndSquare) != "" || StartSquare + game.DieRoll - 27 > 4))
                {
                    return false;  //Cannot move more than 4 squares into the center.  Also, cannot move on an already occupied spot in the center.
                }

            }
            if (game.PlayerTurn == "b")
            {
                if (StartSquare <= 6 && EndSquare >= 7 && EndSquare < 56)
                {
                    return false;  //Cannot move around the board a second time.  Must move piece into center region.
                }
                if(StartSquare >= 28 && StartSquare <= 43 && !(StartSquare >= 40 && StartSquare <= 43))
                {
                    return false;  //Can't move pieces on a different player's home squares.
                }
                if ((StartSquare >= 40 && StartSquare <= 43) && EndSquare != 7)
                {
                    return false;  //Can only move from Blue HomeSquare to Blue Start Square
                }

                //Check to to make sure a CenterSquare Move is made only on my color
                if (StartSquare >= 44 && StartSquare <= 59 && !(StartSquare >= 56 && StartSquare <= 59))
                {
                    return false;  //StartSquare cannot be on another color's HomeSquare
                }
                if (EndSquare >= 44 && EndSquare <= 59 && !(EndSquare >= 56 && EndSquare <= 59))
                {
                    return false;  //EndSquare cannot be on another color's HomeSquare
                }

                //Test to make sure a CenterSquare Move is valid
                if (StartSquare <= 6 && StartSquare + game.DieRoll > 6
                    && (gameState.ElementAt(EndSquare) != "" || StartSquare + game.DieRoll - 6 > 4))
                {
                    return false;  //Cannot move more than 4 squares into the center.  Also, cannot move on an already occupied spot in the center.
                }
            }
            if (game.PlayerTurn == "r")
            {
                if (StartSquare <= 13 && EndSquare >= 14 && (EndSquare < 52 || EndSquare > 55))
                {
                    return false;  //Cannot move around the board a second time.  Must move piece into center region.
                }
                if (StartSquare >= 28 && StartSquare <= 43 && !(StartSquare >= 36 && StartSquare <= 39))
                {
                    return false;  //Can't move pieces on a different player's home squares.
                }
                if ((StartSquare >= 36 && StartSquare <= 39) && EndSquare != 14)
                {
                    return false;  //Can only move from Red HomeSquare to Red Start Square
                }
                //Check to to make sure a CenterSquare Move is made only on my color
                if (StartSquare >= 44 && StartSquare <= 59 && !(StartSquare >= 52 && StartSquare <= 55))
                {
                    return false;  //StartSquare cannot be on another color's HomeSquare
                }
                if (EndSquare >= 44 && EndSquare <= 59 && !(EndSquare >= 52 && EndSquare <= 55))
                {
                    return false;  //EndSquare cannot be on another color's HomeSquare
                }
                //Test to make sure a CenterSquare Move is valid

                if (StartSquare <= 13 && StartSquare + game.DieRoll > 13
                    && (gameState.ElementAt(EndSquare) != "" || StartSquare + game.DieRoll - 13 > 4))
                {
                    return false;  //Cannot move more than 4 squares into the center.  Also, cannot move on an already occupied spot in the center.
                }
            }
            if (game.PlayerTurn == "g")
            {
                if (StartSquare <= 20 && EndSquare >= 21 && (EndSquare < 48 || EndSquare > 51))
                {
                    return false;  //Cannot move around the board a second time.  Must move piece into center region.
                }
                if (StartSquare >= 28 && StartSquare <= 43 && !(StartSquare >= 32 && StartSquare <= 35))
                {
                    return false;  //Can't move pieces on a different player's home squares.
                }
                if ((StartSquare >= 32 && StartSquare <= 35) && EndSquare != 21)
                {
                    return false;  //Can only move from Green HomeSquare to Green Start Square
                }
                //Check to to make sure a CenterSquare Move is made only on my color
                if (StartSquare >= 44 && StartSquare <= 59 && !(StartSquare >= 48 && StartSquare <= 51))
                {
                    return false;  //StartSquare cannot be on another color's HomeSquare
                }
                if (EndSquare >= 44 && EndSquare <= 59 && !(EndSquare >= 48 && EndSquare <= 51))
                {
                    return false;  //EndSquare cannot be on another color's HomeSquare
                }

                //Test to make sure a CenterSquare Move is valid
                if (StartSquare <= 20 && StartSquare + game.DieRoll > 20
                && (gameState.ElementAt(EndSquare) != "" || StartSquare + game.DieRoll - 20 > 4))
                {
                    return false;  //Cannot move more than 4 squares into the center.  Also, cannot move on an already occupied spot in the center.
                }
            }

            //Returning false means move is invalid.  Returning true means move is valid
            return true;
        }

        public string GameWinner(Game game)
        {
            String[] gameState = MakeGameState(game);

            if (gameState[44] == "y" && gameState[45] == "y" && gameState[46] == "y" && gameState[47] == "y")
            {
                return "y";
            }
            if (gameState[48] == "g" && gameState[49] == "g" && gameState[50] == "g" && gameState[51] == "g")
            {
                return "g";
            }
            if (gameState[52] == "r" && gameState[53] == "r" && gameState[54] == "r" && gameState[55] == "r")
            {
                return "r";
            }
            if (gameState[56] == "b" && gameState[57] == "b" && gameState[58] == "b" && gameState[59] == "b")
            {
                return "b";
            }
            return "n"; //no winner
        }

        public async Task<bool> SkipMoveAsync(Game game)
        {  
            game.PlayerTurn = GetNextPlayer(game).Result;
            if (IsComputerPlayer(game))
            {
                await MakeComputerMoveAsync(game);
            }
            game.DieRoll = RollDice();
            await UpdateAsync(game);
            return true;
        }
        public async Task<bool> MakeMoveAsync(Game game, int startSquare, int endSquare)
        {
            switch (game.PlayerTurn.Trim().Substring(0, 1).ToLower())
            {
                case "y": game.PlayerTurn = "y";  break;
                case "b": game.PlayerTurn = "b"; break;
                case "r": game.PlayerTurn = "r"; break;
                case "g": game.PlayerTurn = "g"; break;
            }

            if (GameWinner(game) != "n")
            {
                game.GameComplete = 1;
                await UpdateAsync(game); // Save the changes
                return true;
            }
            // Roll the dice
            //game.DieRoll = RollDice(); //We would know the dice roll, before a player clicks on the squares they want to move to

            // Check if the move is valid
            if (!IsMoveValid(game, startSquare, endSquare))
            {
                return false; // Move is invalid, return false
            }

            // Update the game state to reflect the move
            String[] gameState = MakeGameState(game);

            //If a piece is moved upon, move it back to that color's homesquare
            if (gameState[endSquare] == "y")
            {
                if (gameState[28] == "")
                    gameState[28] = "y";
                else if (gameState[29] == "")
                    gameState[29] = "y";
                else if (gameState[30] == "")
                    gameState[30] = "y";
                else if (gameState[31] == "")
                    gameState[31] = "y";
            }
            if (gameState[endSquare] == "b")
            {
                if (gameState[40] == "")
                    gameState[40] = "b";
                else if (gameState[41] == "")
                    gameState[41] = "b";
                else if (gameState[42] == "")
                    gameState[42] = "b";
                else if (gameState[43] == "")
                    gameState[43] = "b";
            }
            if (gameState[endSquare] == "r")
            {
                if (gameState[36] == "")
                    gameState[36] = "r";
                else if (gameState[37] == "")
                    gameState[37] = "r";
                else if (gameState[38] == "")
                    gameState[38] = "r";
                else if (gameState[39] == "")
                    gameState[39] = "r";
            }
            if (gameState[endSquare] == "g")
            {
                if (gameState[32] == "")
                    gameState[32] = "g";
                else if (gameState[33] == "")
                    gameState[33] = "g";
                else if (gameState[34] == "")
                    gameState[34] = "g";
                else if (gameState[35] == "")
                    gameState[35] = "g";
            }
            gameState[endSquare] = gameState[startSquare]; // Move the piece
            gameState[startSquare] = ""; // Clear the start square

            // Update game object with new game state
            game = UpdateGameStateFromArray(game, gameState);


            // Switch the player's turn before update
            game.DieRoll = RollDice(); //Roll dice for next player
            game.PlayerTurn = GetNextPlayer(game).Result;
            if (IsComputerPlayer(game))
            {
                await MakeComputerMoveAsync(game);
                return true;
            }
            await UpdateAsync(game); // Save the changes


            ////// If the next player is a computer, make the computer move
            ////if (IsComputerPlayer(game.PlayerTurn)) // Checking to see if it's the computer's turn
            ////{
            ////    await MakeComputerMoveAsync(game);// If it is make the computer make a move.
            ////}
            if (GameWinner(game) != "n")
            {
                game.GameComplete = 1;
                await UpdateAsync(game); // Save the changes
                return true;
            }
            return true; // Move successful
        }

        private bool IsComputerPlayer(Game game)
        {
            // Still need to check to see if player is actually a computer or a player.
            PlayerGameManager manager = new PlayerGameManager(options);
            List<PlayerGame> results = manager.LoadAsync().Result.Where(pg => pg.GameId == game.Id).ToList();
            Guid CurrentPlayerID = results.Where(pg => pg.PlayerColor.Trim().Substring(0,1).ToLower() == game.PlayerTurn.Trim().Substring(0, 1).ToLower()).FirstOrDefault().PlayerId;

            if (CurrentPlayerID == ComputerGuid) //Computer Player has ID of ComputerGuid
                return true;
            else
                return false;
        }

        private Game UpdateGameStateFromArray(Game game, String[] gameState)
        {
            // Assign the game state directly to the properties if the game board.
            game.YellowStartSquare = gameState[0];
            game.Square1 = gameState[1];
            game.Square2 = gameState[2];
            game.Square3 = gameState[3];
            game.Square4 = gameState[4];
            game.Square5 = gameState[5];
            game.Square6 = gameState[6];

            game.BlueStartSquare = gameState[7];
            game.Square7 = gameState[8];
            game.Square8 = gameState[9];
            game.Square9 = gameState[10];
            game.Square10 = gameState[11];
            game.Square11 = gameState[12];
            game.Square12 = gameState[13];

            game.RedStartSquare = gameState[14];
            game.Square13 = gameState[15];
            game.Square14 = gameState[16];
            game.Square15 = gameState[17];
            game.Square16 = gameState[18];
            game.Square17 = gameState[19];
            game.Square18 = gameState[20];

            game.GreenStartSquare = gameState[21];
            game.Square19 = gameState[22];
            game.Square20 = gameState[23];
            game.Square21 = gameState[24];
            game.Square22 = gameState[25];
            game.Square23 = gameState[26];
            game.Square24 = gameState[27];

            // Update center squares as integers 0 for an empty square, 1 for occupied square
            game.YellowCenterSquare1 = gameState[44] == "y" ? 1 : 0;
            game.YellowCenterSquare2 = gameState[45] == "y" ? 1 : 0;
            game.YellowCenterSquare3 = gameState[46] == "y" ? 1 : 0;
            game.YellowCenterSquare4 = gameState[47] == "y" ? 1 : 0;

            game.GreenCenterSquare1 = gameState[48] == "g" ? 1 : 0;
            game.GreenCenterSquare2 = gameState[49] == "g" ? 1 : 0;
            game.GreenCenterSquare3 = gameState[50] == "g" ? 1 : 0;
            game.GreenCenterSquare4 = gameState[51] == "g" ? 1 : 0;

            game.RedCenterSquare1 = gameState[52] == "r" ? 1 : 0;
            game.RedCenterSquare2 = gameState[53] == "r" ? 1 : 0;
            game.RedCenterSquare3 = gameState[54] == "r" ? 1 : 0;
            game.RedCenterSquare4 = gameState[55] == "r" ? 1 : 0;

            game.BlueCenterSquare1 = gameState[56] == "b" ? 1 : 0;
            game.BlueCenterSquare2 = gameState[57] == "b" ? 1 : 0;
            game.BlueCenterSquare3 = gameState[58] == "b" ? 1 : 0;
            game.BlueCenterSquare4 = gameState[59] == "b" ? 1 : 0;

            //Update Home Squares
            game.YellowHomeSquare1 = gameState[28] == "y" ? 1 : 0;
            game.YellowHomeSquare2 = gameState[29] == "y" ? 1 : 0;
            game.YellowHomeSquare3 = gameState[30] == "y" ? 1 : 0;
            game.YellowHomeSquare4 = gameState[31] == "y" ? 1 : 0;
            game.GreenHomeSquare1 = gameState[32] == "g" ? 1 : 0;
            game.GreenHomeSquare2 = gameState[33] == "g" ? 1 : 0;
            game.GreenHomeSquare3 = gameState[34] == "g" ? 1 : 0;
            game.GreenHomeSquare4 = gameState[35] == "g" ? 1 : 0;
            game.RedHomeSquare1 = gameState[36] == "r" ? 1 : 0;
            game.RedHomeSquare2 = gameState[37] == "r" ? 1 : 0;
            game.RedHomeSquare3 = gameState[38] == "r" ? 1 : 0;
            game.RedHomeSquare4 = gameState[39] == "r" ? 1 : 0;
            game.BlueHomeSquare1 = gameState[40] == "b" ? 1 : 0;
            game.BlueHomeSquare2 = gameState[41] == "b" ? 1 : 0;
            game.BlueHomeSquare3 = gameState[42] == "b" ? 1 : 0;
            game.BlueHomeSquare4 = gameState[43] == "b" ? 1 : 0;

            return game;
        }
        private string GetNextPlayer(string currentPlayer)
        {
            if (currentPlayer == "y")
            {
                return "b"; // Yellow's turn goes to Blue
            }
            else if (currentPlayer == "b")
            {
                return "r"; // Blue's turn goes to Red
            }
            else if (currentPlayer == "r")
            {
                return "g"; // Red's turn goes to Green
            }
            else if (currentPlayer == "g")
            {
                return "y"; // Green's turn goes back to Yellow
            }
            return currentPlayer;
        }

        private async Task<string> GetNextPlayer(Game game)
        {
            // This function assumes player colors are in order: yellow, blue, red, green
            // Not sure what order we have the colors
            PlayerGameManager manager = new PlayerGameManager(options);
            List<PlayerGame> results = manager.LoadAsync().Result.Where(pg => pg.GameId == game.Id).ToList();
            string PlayerColors = "";
            foreach (PlayerGame pg in results)
            {
                PlayerColors += pg.PlayerColor.Trim().Substring(0, 1).ToLower();
            }
            string currentPlayer = game.PlayerTurn.Trim().Substring(0, 1).ToLower();
            if (game.PlayerTurn.Trim().Substring(0, 1).ToLower() == "y" ||
                game.PlayerTurn.Trim().Substring(0, 1).ToLower() == "b" ||
                game.PlayerTurn.Trim().Substring(0, 1).ToLower() == "r" ||
                game.PlayerTurn.Trim().Substring(0, 1).ToLower() == "g")
            {
                currentPlayer = GetNextPlayer(currentPlayer);
                while (!PlayerColors.Contains(currentPlayer))
                {
                    currentPlayer = GetNextPlayer(currentPlayer);
                }

            }
            game.PlayerTurn = currentPlayer;

                
            return currentPlayer; // If currentPlayer is invalid, return it unchanged
        }
        public async Task<(bool, string)> MakeComputerMoveAsync(Game game)
        {
            if (GameWinner(game) != "n")
            {
                game.GameComplete = 1;
                await UpdateAsync(game); // Save the changes
                return (true, "No Moves Available");
            }
            // Get all valid moves for the current player
            var validMoves = GetValidMoves(game);

            if (validMoves.Count == 0)
            {
                game.PlayerTurn = GetNextPlayer(game).Result;
                if (IsComputerPlayer(game))
                {
                    return await MakeComputerMoveAsync(game);
                }
                await UpdateAsync(game);
                game.DieRoll = RollDice();
                return (false, "No Moves Available"); // No valid moves available
            }

            // Choose a move randomly or we have to add logic to the computer to select a move that isn't random.
            var random = new Random();
            var selectedMove = validMoves[random.Next(validMoves.Count)];
            /*
            Console.WriteLine("*******************************");
            foreach ((int, int) move in validMoves)
            {
                Console.WriteLine($"Player: {game.PlayerTurn} || MaybeStart: {move.Item1} MaybeEnd: {move.Item2}");
            }
            Console.WriteLine("*******************************");
            */
            // Make the move
            return (await MakeMoveAsync(game, selectedMove.StartSquare, selectedMove.EndSquare), 
                $"StartSquare: {selectedMove.StartSquare}, EndSquare: {selectedMove.EndSquare}");
        }

        public List<(int StartSquare, int EndSquare)> GetValidMoves(Game game)
        {
            String[] gameState = MakeGameState(game);
            int[] PieceIndex = new int[gameState.Count()];
            string computerColor = game.PlayerTurn;

            int count = 0;
            for (int i = 0; i < gameState.Count(); i++)
            {
                if (gameState[i] == computerColor)
                {
                    PieceIndex[count] = i;
                    count++;
                }
            }
            List<(int StartSquare, int EndSquare)> validMoves = new List<(int, int)>();

            // Loop through all possible starting squares
            foreach (int startSquare in PieceIndex)
            {
                if (game.DieRoll == 6)
                {
                    //Yellow Home
                    if (startSquare >= 28 && startSquare <= 31)
                    {
                        if (IsMoveValid(game, startSquare, 0))
                        {
                            validMoves.Add((startSquare, 0));
                        }
                    }
                    //Green Home
                    if (startSquare >= 32 && startSquare <= 35)
                    {
                        if (IsMoveValid(game, startSquare, 21))
                        {
                            validMoves.Add((startSquare, 21));
                        }
                    }
                    //Red Home
                    if (startSquare >= 36 && startSquare <= 39)
                    {
                        if (IsMoveValid(game, startSquare, 14))
                        {
                            validMoves.Add((startSquare, 14));
                        }
                    }
                    //Blue Home
                    if (startSquare >= 40 && startSquare <= 43)
                    {
                        if (IsMoveValid(game, startSquare, 7))
                        {
                            validMoves.Add((startSquare, 7));
                        }
                    }
                }
                    if (startSquare + game.DieRoll <= 27)
                    {
                        int endSquare = startSquare + game.DieRoll;
                        // Validate move
                        if (IsMoveValid(game, startSquare, endSquare))
                        {
                            validMoves.Add((startSquare, endSquare));
                        }
                    }
                    else if(startSquare <= 27)
                    {
                        int endSquare = startSquare - 27 + game.DieRoll - 1;
                        // Validate move
                        if (IsMoveValid(game, startSquare, endSquare))
                        {
                            validMoves.Add((startSquare, endSquare));
                        }
                    }
                    if(computerColor == "y")
                    {
                        if(startSquare >= 44 && startSquare <= 47 && startSquare + game.DieRoll <= 47)
                        {
                            if (IsMoveValid(game, startSquare, startSquare + game.DieRoll))
                            {
                                validMoves.Add((startSquare, startSquare + game.DieRoll));
                            }
                        }
                        if(startSquare >= 22 && startSquare <= 27 && startSquare + game.DieRoll > 27)
                        {
                            if (IsMoveValid(game, startSquare, 43 + (startSquare + game.DieRoll - 27)))
                            {
                                validMoves.Add((startSquare, 43 + (startSquare + game.DieRoll - 27)));
                            }
                        }
                    }
                    if (computerColor == "b")
                    {
                        if (startSquare >= 56 && startSquare <= 59 && startSquare + game.DieRoll <= 59)
                        {
                            if (IsMoveValid(game, startSquare, startSquare + game.DieRoll))
                            {
                                validMoves.Add((startSquare, startSquare + game.DieRoll));
                            }
                        }
                        if (startSquare >= 1 && startSquare <= 6 && startSquare + game.DieRoll > 6)
                        {
                            if (IsMoveValid(game, startSquare, 55 + (startSquare + game.DieRoll - 6)))
                            {
                                validMoves.Add((startSquare, 55 + (startSquare + game.DieRoll - 6)));
                            }
                        }
                    }
                    if (computerColor == "r")
                    {
                        if (startSquare >= 52 && startSquare <= 55 && startSquare + game.DieRoll <= 55)
                        {
                            if (IsMoveValid(game, startSquare, startSquare + game.DieRoll))
                            {
                                validMoves.Add((startSquare, startSquare + game.DieRoll));
                            }
                        }
                        if (startSquare >= 8 && startSquare <= 13 && startSquare + game.DieRoll > 13)
                        {
                            if (IsMoveValid(game, startSquare, 51 + (startSquare + game.DieRoll - 13)))
                            {
                                validMoves.Add((startSquare, 51 + (startSquare + game.DieRoll - 13)));
                            }
                        }
                    }
                    if (computerColor == "g")
                    {
                        if (startSquare >= 48 && startSquare <= 51 && startSquare + game.DieRoll <= 51)
                        {
                            if (IsMoveValid(game, startSquare, startSquare + game.DieRoll))
                            {
                                validMoves.Add((startSquare, startSquare + game.DieRoll));
                            }
                        }
                        if (startSquare >= 15 && startSquare <= 20 && startSquare + game.DieRoll > 20)
                        {
                            if (IsMoveValid(game, startSquare, 47 + (startSquare + game.DieRoll - 20)))
                            {
                                validMoves.Add((startSquare, 47 + (startSquare + game.DieRoll - 20)));
                            }
                        }
                    }
            }

            return validMoves;

            //List<(int StartSquare, int EndSquare)> validMoves = new List<(int, int)>();

            //// Loop through all possible starting squares
            //for (int startSquare = 0; startSquare < 60; startSquare++)
            //{
            //    for (int dieRoll = 1; dieRoll <= 6; dieRoll++)
            //    {
            //        int endSquare = startSquare + dieRoll;

            //        // Validate move
            //        if (IsMoveValid(game, startSquare, endSquare))
            //        {
            //            validMoves.Add((startSquare, endSquare));
            //        }
            //    }
            //}

            //return validMoves;
        }

        public int RollDice()
        {
            Random random = new Random();
            return random.Next(1, 7); // Returns a random number between 1 and 6 for the diceroll.
        }
    }
}
