using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicTrack.Migrations
{
    public partial class remove_dictionary_playlist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrackPosition",
                table: "PlayLists");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrackPosition",
                table: "PlayLists",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
