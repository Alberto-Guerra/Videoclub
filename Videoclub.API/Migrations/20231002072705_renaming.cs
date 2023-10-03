using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Videoclub.API.Migrations
{
    public partial class renaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.RenameColumn(
              name: "passwordSalt",
              table: "Users",
              newName: "PasswordSalt");

            migrationBuilder.RenameColumn(
                name: "passwordHash",
                table: "Users",
                newName: "PasswordHash");            

            migrationBuilder.DropForeignKey(
                name: "FK_RentHistories_Movies_movie_id",
                table: "RentHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_RentHistories_Users_user_id",
                table: "RentHistories");

            migrationBuilder.RenameColumn(
                name: "movie_id",
                table: "RentHistories",
                newName: "MovieId");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "RentHistories",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RentHistories_movie_id",
                table: "RentHistories",
                newName: "IX_RentHistories_MovieId");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Action" },
                    { 2, "Comedy" },
                    { 3, "Drama" },
                    { 4, "Horror" },
                    { 5, "Thriller" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Available", "CategoryId", "Description", "PhotoURL", "Title" },
                values: new object[,]
                {
                    { 1, true, 3, "Some people are in prison and they try to scape", "https://m.media-amazon.com/images/M/MV5BNDE3ODcxYzMtY2YzZC00NmNlLWJiNDMtZDViZWM2MzIxZDYwXkEyXkFqcGdeQXVyNjAwNDUxODI@._V1_FMjpg_UX1000_.jpg", "The Shawshank Redemption" },
                    { 2, true, 1, "Oscar DiCaprio is in the woods and he is cold", "https://m.media-amazon.com/images/M/MV5BMDE5OWMzM2QtOTU2ZS00NzAyLWI2MDEtOTRlYjIxZGM0OWRjXkEyXkFqcGdeQXVyODE5NzE3OTE@._V1_.jpg", "The Revenant" },
                    { 3, true, 3, "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.", "https://m.media-amazon.com/images/M/MV5BM2MyNjYxNmUtYTAwNi00MTYxLWJmNWYtYzZlODY3ZTk3OTFlXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_FMjpg_UX1000_.jpg", "The Godfather" },
                    { 4, true, 1, "Some guys fight", "https://m.media-amazon.com/images/I/51v5ZpFyaFL._AC_.jpg", "Fight Club" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_RentHistories_Movies_MovieId",
                table: "RentHistories",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RentHistories_Users_UserId",
                table: "RentHistories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentHistories_Movies_MovieId",
                table: "RentHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_RentHistories_Users_UserId",
                table: "RentHistories");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.RenameColumn(
                name: "PasswordSalt",
                table: "Users",
                newName: "passwordSalt");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Users",
                newName: "passwordHash");

            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "RentHistories",
                newName: "movie_id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "RentHistories",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_RentHistories_MovieId",
                table: "RentHistories",
                newName: "IX_RentHistories_movie_id");

            migrationBuilder.AddForeignKey(
                name: "FK_RentHistories_Movies_movie_id",
                table: "RentHistories",
                column: "movie_id",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RentHistories_Users_user_id",
                table: "RentHistories",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
