using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContentVideo");

            migrationBuilder.DropTable(
                name: "VideoContents");

            migrationBuilder.AddColumn<int>(
                name: "ParentCommentId",
                table: "Comments",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentCommentId",
                table: "Comments");

            migrationBuilder.CreateTable(
                name: "ContentVideo",
                columns: table => new
                {
                    ContentsId = table.Column<int>(type: "int", nullable: false),
                    VideosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentVideo", x => new { x.ContentsId, x.VideosId });
                    table.ForeignKey(
                        name: "FK_ContentVideo_Contents_ContentsId",
                        column: x => x.ContentsId,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContentVideo_Videos_VideosId",
                        column: x => x.VideosId,
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideoContents",
                columns: table => new
                {
                    ContentId = table.Column<int>(type: "int", nullable: false),
                    VideoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoContents", x => new { x.ContentId, x.VideoId });
                    table.ForeignKey(
                        name: "FK_VideoContents_Contents_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VideoContents_Videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContentVideo_VideosId",
                table: "ContentVideo",
                column: "VideosId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoContents_VideoId",
                table: "VideoContents",
                column: "VideoId");
        }
    }
}
