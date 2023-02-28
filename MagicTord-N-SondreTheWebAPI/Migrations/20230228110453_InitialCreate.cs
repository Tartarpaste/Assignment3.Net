using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicTord_N_SondreTheWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    CharacterID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PictureURL = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.CharacterID);
                });

            migrationBuilder.CreateTable(
                name: "Franchises",
                columns: table => new
                {
                    FranchiseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Franchises", x => x.FranchiseID);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    MovieID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ReleaseYear = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Director = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PictureURL = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TrailerURL = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    FranchiseID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.MovieID);
                    table.ForeignKey(
                        name: "FK_Movies_Franchises_FranchiseID",
                        column: x => x.FranchiseID,
                        principalTable: "Franchises",
                        principalColumn: "FranchiseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterMovie",
                columns: table => new
                {
                    MovieID = table.Column<int>(type: "int", nullable: false),
                    CharacterID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterMovie", x => new { x.MovieID, x.CharacterID });
                    table.ForeignKey(
                        name: "FK_CharacterMovie_Characters_CharacterID",
                        column: x => x.CharacterID,
                        principalTable: "Characters",
                        principalColumn: "CharacterID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterMovie_Movies_MovieID",
                        column: x => x.MovieID,
                        principalTable: "Movies",
                        principalColumn: "MovieID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Characters",
                columns: new[] { "CharacterID", "Alias", "FullName", "Gender", "PictureURL" },
                values: new object[,]
                {
                    { 1, "The Cocaine Bear", "Boris", "Female", "https://static01.nyt.com/images/2022/12/01/lens/01xp-cocainebear/01xp-cocainebear-mediumSquareAt3X.jpg" },
                    { 2, "The Cocaine", "Carol Cocaine", "Female", "https://i2-prod.chroniclelive.co.uk/incoming/article18860908.ece/ALTERNATES/s615/1_snr_nec_010920dale_01.jpg" },
                    { 3, "The King", "Eger Taronton", "Male", "https://i2-prod.chroniclelive.co.uk/incoming/article18860908.ece/ALTERNATES/s615/1_snr_nec_010920dale_01.jpg" },
                    { 4, "Furiosa", "Tharlize Cheron", "Female", "https://www.indiewire.com/wp-content/uploads/2021/05/furiosa-movie.png?w=780" }
                });

            migrationBuilder.InsertData(
                table: "Franchises",
                columns: new[] { "FranchiseID", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "The Noroff Cinematic Universe", "NCU" },
                    { 2, "Zisson Zinematic Zeries", "ZZZ" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "MovieID", "Director", "FranchiseID", "Genre", "MovieTitle", "PictureURL", "ReleaseYear", "TrailerURL" },
                values: new object[,]
                {
                    { 1, "Nicholas Dean Nutz", 1, "Action/Comedy", "Cocain Bear", "https://movies.universalpictures.com/media/01-cb-dm-mobile-banner-1080x745-pl-f01-112222-638e6de200084-1.jpg", "2023", "https://www.youtube.com/watch?v=DuWEEKeJLMI" },
                    { 2, "Nicholas Dean Nutz", 1, "Action/Comedy", "Tetris", "https://static2.tribute.ca/poster/160x236/tetris-apple-tv-167535.jpg", "2023", "https://www.youtube.com/watch?v=-BLM1naCfME" },
                    { 3, "Zackary Zuckerberg", 2, "Adventure", "Zathura", "https://www.themoviedb.org/t/p/original/gtlqXDVrF8E59Se2LKnBdbTw6oa.jpg", "2005", "https://www.youtube.com/watch?v=zNxm_obDpNU" }
                });

            migrationBuilder.InsertData(
                table: "CharacterMovie",
                columns: new[] { "CharacterID", "MovieID" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 2, 2 },
                    { 3, 2 },
                    { 1, 3 },
                    { 4, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterMovie_CharacterID",
                table: "CharacterMovie",
                column: "CharacterID");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_FranchiseID",
                table: "Movies",
                column: "FranchiseID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterMovie");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Franchises");
        }
    }
}
