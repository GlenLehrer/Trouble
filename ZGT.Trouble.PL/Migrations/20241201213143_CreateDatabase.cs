using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ZGT.Trouble.PL.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblGame",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerTurn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DieRoll = table.Column<int>(type: "int", nullable: false),
                    GameStartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    GameComplete = table.Column<int>(type: "int", nullable: false),
                    YellowHomeSquare1 = table.Column<int>(type: "int", nullable: false),
                    YellowHomeSquare2 = table.Column<int>(type: "int", nullable: false),
                    YellowHomeSquare3 = table.Column<int>(type: "int", nullable: false),
                    YellowHomeSquare4 = table.Column<int>(type: "int", nullable: false),
                    YellowStartSquare = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Square1 = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Square2 = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Square3 = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Square4 = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Square5 = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Square6 = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    YellowCenterSquare1 = table.Column<int>(type: "int", nullable: false),
                    YellowCenterSquare2 = table.Column<int>(type: "int", nullable: false),
                    YellowCenterSquare3 = table.Column<int>(type: "int", nullable: false),
                    YellowCenterSquare4 = table.Column<int>(type: "int", nullable: false),
                    GreenHomeSquare1 = table.Column<int>(type: "int", nullable: false),
                    GreenHomeSquare2 = table.Column<int>(type: "int", nullable: false),
                    GreenHomeSquare3 = table.Column<int>(type: "int", nullable: false),
                    GreenHomeSquare4 = table.Column<int>(type: "int", nullable: false),
                    GreenStartSquare = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Square7 = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Square8 = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Square9 = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Square10 = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Square11 = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Square12 = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    GreenCenterSquare1 = table.Column<int>(type: "int", nullable: false),
                    GreenCenterSquare2 = table.Column<int>(type: "int", nullable: false),
                    GreenCenterSquare3 = table.Column<int>(type: "int", nullable: false),
                    GreenCenterSquare4 = table.Column<int>(type: "int", nullable: false),
                    RedHomeSquare1 = table.Column<int>(type: "int", nullable: false),
                    RedHomeSquare2 = table.Column<int>(type: "int", nullable: false),
                    RedHomeSquare3 = table.Column<int>(type: "int", nullable: false),
                    RedHomeSquare4 = table.Column<int>(type: "int", nullable: false),
                    RedStartSquare = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Square13 = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Square14 = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Square15 = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Square16 = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Square17 = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Square18 = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    RedCenterSquare1 = table.Column<int>(type: "int", nullable: false),
                    RedCenterSquare2 = table.Column<int>(type: "int", nullable: false),
                    RedCenterSquare3 = table.Column<int>(type: "int", nullable: false),
                    RedCenterSquare4 = table.Column<int>(type: "int", nullable: false),
                    BlueHomeSquare1 = table.Column<int>(type: "int", nullable: false),
                    BlueHomeSquare2 = table.Column<int>(type: "int", nullable: false),
                    BlueHomeSquare3 = table.Column<int>(type: "int", nullable: false),
                    BlueHomeSquare4 = table.Column<int>(type: "int", nullable: false),
                    BlueStartSquare = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Square19 = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Square20 = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Square21 = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Square22 = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Square23 = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Square24 = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    BlueCenterSquare1 = table.Column<int>(type: "int", nullable: false),
                    BlueCenterSquare2 = table.Column<int>(type: "int", nullable: false),
                    BlueCenterSquare3 = table.Column<int>(type: "int", nullable: false),
                    BlueCenterSquare4 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblGame_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblPlayer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: false),
                    Password = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: false),
                    Email = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: false),
                    NumberOfWins = table.Column<int>(type: "int", nullable: false),
                    DateJoined = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPlayer_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblPlayerGame",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsComputerPlaying = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPlayerGame_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblPlayerGame_GameId",
                        column: x => x.GameId,
                        principalTable: "tblGame",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tblPlayerGame_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "tblPlayer",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "tblGame",
                columns: new[] { "Id", "BlueCenterSquare1", "BlueCenterSquare2", "BlueCenterSquare3", "BlueCenterSquare4", "BlueHomeSquare1", "BlueHomeSquare2", "BlueHomeSquare3", "BlueHomeSquare4", "BlueStartSquare", "DieRoll", "GameComplete", "GameStartDate", "GreenCenterSquare1", "GreenCenterSquare2", "GreenCenterSquare3", "GreenCenterSquare4", "GreenHomeSquare1", "GreenHomeSquare2", "GreenHomeSquare3", "GreenHomeSquare4", "GreenStartSquare", "PlayerTurn", "RedCenterSquare1", "RedCenterSquare2", "RedCenterSquare3", "RedCenterSquare4", "RedHomeSquare1", "RedHomeSquare2", "RedHomeSquare3", "RedHomeSquare4", "RedStartSquare", "Square1", "Square10", "Square11", "Square12", "Square13", "Square14", "Square15", "Square16", "Square17", "Square18", "Square19", "Square2", "Square20", "Square21", "Square22", "Square23", "Square24", "Square3", "Square4", "Square5", "Square6", "Square7", "Square8", "Square9", "YellowCenterSquare1", "YellowCenterSquare2", "YellowCenterSquare3", "YellowCenterSquare4", "YellowHomeSquare1", "YellowHomeSquare2", "YellowHomeSquare3", "YellowHomeSquare4", "YellowStartSquare" },
                values: new object[,]
                {
                    { new Guid("4a28deab-4f05-4cd9-a129-3a1de188fe21"), 0, 0, 0, 0, 1, 1, 1, 1, "", 1, 0, new DateTime(2024, 12, 1, 15, 31, 42, 305, DateTimeKind.Local).AddTicks(642), 0, 0, 0, 0, 1, 1, 1, 1, "", "Yellow", 0, 0, 0, 0, 1, 1, 1, 1, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 0, 0, 0, 0, 1, 1, 1, 1, "" },
                    { new Guid("c0e00542-a4bd-4088-b912-0dae885cb214"), 0, 0, 0, 0, 1, 1, 1, 1, "", 6, 0, new DateTime(2024, 12, 1, 15, 31, 42, 305, DateTimeKind.Local).AddTicks(568), 0, 0, 0, 0, 1, 1, 1, 1, "", "Yellow", 0, 0, 0, 0, 1, 1, 1, 1, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 0, 0, 0, 0, 1, 1, 1, 1, "" },
                    { new Guid("f582eaf5-0f38-44db-be25-3c0bafdf2e18"), 0, 0, 0, 0, 1, 1, 1, 1, "", 3, 0, new DateTime(2024, 12, 1, 15, 31, 42, 305, DateTimeKind.Local).AddTicks(670), 0, 0, 0, 0, 1, 1, 1, 1, "", "Yellow", 0, 0, 0, 0, 1, 1, 1, 1, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 0, 0, 0, 0, 1, 1, 1, 1, "" }
                });

            migrationBuilder.InsertData(
                table: "tblPlayer",
                columns: new[] { "Id", "DateJoined", "Email", "NumberOfWins", "Password", "UserName" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2024, 12, 1, 15, 31, 42, 304, DateTimeKind.Local).AddTicks(7321), "computer@computer", 0, "12345", "Computer" },
                    { new Guid("756e7429-79d3-4179-8e38-7b8516893c07"), new DateTime(2024, 12, 1, 15, 31, 42, 304, DateTimeKind.Local).AddTicks(7313), "Brian@Brian", 0, "maple", "Brian" },
                    { new Guid("ae3c2b78-e1b0-4956-adf1-34bf834e7583"), new DateTime(2024, 12, 1, 15, 31, 42, 304, DateTimeKind.Local).AddTicks(7309), "Zach@Zach", 0, "123", "Zach" },
                    { new Guid("c0e00542-a4bd-4088-b912-0dae885cb215"), new DateTime(2024, 12, 1, 15, 31, 42, 304, DateTimeKind.Local).AddTicks(7193), "Glen@Glen", 0, "12345", "Glen" }
                });

            migrationBuilder.InsertData(
                table: "tblPlayerGame",
                columns: new[] { "Id", "GameId", "IsComputerPlaying", "PlayerColor", "PlayerId" },
                values: new object[,]
                {
                    { new Guid("0fe79625-d6ed-469a-9c08-c719f3245eb3"), new Guid("f582eaf5-0f38-44db-be25-3c0bafdf2e18"), false, "y", new Guid("756e7429-79d3-4179-8e38-7b8516893c07") },
                    { new Guid("7f626c72-4be5-4a0a-b8a4-b8c34dc69137"), new Guid("c0e00542-a4bd-4088-b912-0dae885cb214"), false, "b", new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("bb3e8235-c3b0-4835-a86e-da9d51fca467"), new Guid("c0e00542-a4bd-4088-b912-0dae885cb214"), false, "r", new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("c0e00542-a4bd-4088-b912-0dae885cb216"), new Guid("c0e00542-a4bd-4088-b912-0dae885cb214"), false, "y", new Guid("c0e00542-a4bd-4088-b912-0dae885cb215") },
                    { new Guid("ce896b31-2ef3-40f7-99ff-2464fcac01f4"), new Guid("c0e00542-a4bd-4088-b912-0dae885cb214"), false, "g", new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("d82b820e-dfea-4934-b877-155ef5ea36e6"), new Guid("4a28deab-4f05-4cd9-a129-3a1de188fe21"), false, "y", new Guid("ae3c2b78-e1b0-4956-adf1-34bf834e7583") },
                    { new Guid("f9ab93cd-3225-4ee6-89c3-c887dec88595"), new Guid("4a28deab-4f05-4cd9-a129-3a1de188fe21"), false, "b", new Guid("11111111-1111-1111-1111-111111111111") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblPlayerGame_GameId",
                table: "tblPlayerGame",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPlayerGame_PlayerId",
                table: "tblPlayerGame",
                column: "PlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblPlayerGame");

            migrationBuilder.DropTable(
                name: "tblGame");

            migrationBuilder.DropTable(
                name: "tblPlayer");
        }
    }
}
