using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookmarks_Blogs_BlogId",
                table: "Bookmarks");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookmarks_Users_SiteUserId",
                table: "Bookmarks");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailAuthenticators_Users_SiteUserId",
                table: "EmailAuthenticators");

            migrationBuilder.DropForeignKey(
                name: "FK_ForgottenPasswords_Users_SiteUserId",
                table: "ForgottenPasswords");

            migrationBuilder.DropForeignKey(
                name: "FK_OtpAuthenticators_Users_SiteUserId",
                table: "OtpAuthenticators");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_Users_SiteUserId",
                table: "RefreshTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Users_UserID",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogins_Users_SiteUserId",
                table: "UserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_UserOperationClaims_Users_SiteUserId",
                table: "UserOperationClaims");

            migrationBuilder.DropIndex(
                name: "IX_UserOperationClaims_SiteUserId",
                table: "UserOperationClaims");

            migrationBuilder.DropIndex(
                name: "IX_UserLogins_SiteUserId",
                table: "UserLogins");

            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_SiteUserId",
                table: "RefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_OtpAuthenticators_SiteUserId",
                table: "OtpAuthenticators");

            migrationBuilder.DropIndex(
                name: "IX_ForgottenPasswords_SiteUserId",
                table: "ForgottenPasswords");

            migrationBuilder.DropIndex(
                name: "IX_EmailAuthenticators_SiteUserId",
                table: "EmailAuthenticators");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "SiteUserId",
                table: "UserOperationClaims");

            migrationBuilder.DropColumn(
                name: "SiteUserId",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "SiteUserId",
                table: "OtpAuthenticators");

            migrationBuilder.DropColumn(
                name: "SiteUserId",
                table: "ForgottenPasswords");

            migrationBuilder.DropColumn(
                name: "SiteUserId",
                table: "EmailAuthenticators");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Reports",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_UserID",
                table: "Reports",
                newName: "IX_Reports_UserId");

            migrationBuilder.AddColumn<string>(
                name: "UserType",
                table: "Users",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserLogins",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Reports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AuthenticatorType", "Biography", "CreateUser", "CreatedDate", "DeleteUser", "DeletedDate", "Email", "FirstName", "IsActive", "IsVerified", "LastName", "PasswordHash", "PasswordSalt", "ProfileImageUrl", "UpdateUser", "UpdatedDate", "UserType", "VerifiedAt" },
                values: new object[] { 1, 0, "", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, "esquetta@gmail.com", "Admin", true, true, "Admin", new byte[] { 194, 135, 197, 103, 36, 243, 186, 74, 78, 246, 235, 118, 162, 22, 93, 195, 219, 123, 124, 214, 25, 78, 64, 128, 141, 165, 67, 190, 143, 209, 67, 61, 90, 217, 127, 123, 54, 151, 33, 218, 11, 20, 176, 66, 85, 64, 101, 234, 133, 193, 3, 107, 167, 19, 52, 62, 224, 173, 163, 31, 110, 208, 37, 217 }, new byte[] { 174, 194, 183, 186, 89, 253, 37, 214, 70, 124, 15, 122, 5, 183, 214, 159, 81, 121, 43, 238, 147, 163, 199, 113, 229, 40, 234, 150, 16, 73, 24, 30, 35, 81, 7, 173, 102, 44, 178, 45, 221, 194, 38, 42, 182, 190, 121, 51, 73, 138, 184, 23, 56, 38, 206, 96, 75, 111, 127, 122, 223, 55, 160, 173, 46, 100, 84, 113, 246, 189, 214, 124, 216, 104, 236, 234, 218, 92, 126, 155, 21, 173, 106, 172, 132, 209, 207, 139, 143, 113, 82, 22, 153, 250, 119, 191, 69, 68, 238, 229, 156, 173, 166, 23, 220, 214, 17, 107, 13, 59, 103, 64, 206, 231, 236, 224, 127, 243, 65, 6, 125, 19, 167, 246, 219, 143, 50, 154 }, "", 0, null, "SiteUser", null });

            migrationBuilder.CreateIndex(
                name: "IX_UserOperationClaims_UserId",
                table: "UserOperationClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_UserID",
                table: "Reports",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OtpAuthenticators_UserId",
                table: "OtpAuthenticators",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ForgottenPasswords_UserId",
                table: "ForgottenPasswords",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailAuthenticators_UserId",
                table: "EmailAuthenticators",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmarks_Blogs_BlogId",
                table: "Bookmarks",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmarks_Users_SiteUserId",
                table: "Bookmarks",
                column: "SiteUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailAuthenticators_Users_UserId",
                table: "EmailAuthenticators",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ForgottenPasswords_Users_UserId",
                table: "ForgottenPasswords",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OtpAuthenticators_Users_UserId",
                table: "OtpAuthenticators",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_Users_UserId",
                table: "RefreshTokens",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Users_UserID",
                table: "Reports",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Users_UserId",
                table: "Reports",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogins_Users_UserId",
                table: "UserLogins",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserOperationClaims_Users_UserId",
                table: "UserOperationClaims",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookmarks_Blogs_BlogId",
                table: "Bookmarks");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookmarks_Users_SiteUserId",
                table: "Bookmarks");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailAuthenticators_Users_UserId",
                table: "EmailAuthenticators");

            migrationBuilder.DropForeignKey(
                name: "FK_ForgottenPasswords_Users_UserId",
                table: "ForgottenPasswords");

            migrationBuilder.DropForeignKey(
                name: "FK_OtpAuthenticators_Users_UserId",
                table: "OtpAuthenticators");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_Users_UserId",
                table: "RefreshTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Users_UserID",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Users_UserId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogins_Users_UserId",
                table: "UserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_UserOperationClaims_Users_UserId",
                table: "UserOperationClaims");

            migrationBuilder.DropIndex(
                name: "IX_UserOperationClaims_UserId",
                table: "UserOperationClaims");

            migrationBuilder.DropIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins");

            migrationBuilder.DropIndex(
                name: "IX_Reports_UserID",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_OtpAuthenticators_UserId",
                table: "OtpAuthenticators");

            migrationBuilder.DropIndex(
                name: "IX_ForgottenPasswords_UserId",
                table: "ForgottenPasswords");

            migrationBuilder.DropIndex(
                name: "IX_EmailAuthenticators_UserId",
                table: "EmailAuthenticators");

            migrationBuilder.DropIndex(
                name: "IX_Comments_UserId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserLogins");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Reports");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Reports",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_UserId",
                table: "Reports",
                newName: "IX_Reports_UserID");

            migrationBuilder.AddColumn<int>(
                name: "SiteUserId",
                table: "UserOperationClaims",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SiteUserId",
                table: "RefreshTokens",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SiteUserId",
                table: "OtpAuthenticators",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SiteUserId",
                table: "ForgottenPasswords",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SiteUserId",
                table: "EmailAuthenticators",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: 1,
                column: "SiteUserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: 2,
                column: "SiteUserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 117, 66, 201, 254, 92, 255, 62, 82, 54, 4, 83, 255, 68, 155, 236, 78, 87, 172, 9, 144, 147, 120, 222, 95, 99, 81, 54, 100, 242, 210, 120, 2, 10, 77, 231, 118, 164, 161, 33, 123, 79, 56, 201, 229, 237, 211, 76, 159, 234, 251, 37, 143, 72, 32, 0, 16, 251, 178, 71, 16, 19, 128, 59, 175 }, new byte[] { 35, 186, 87, 214, 204, 144, 163, 191, 115, 97, 250, 3, 237, 191, 200, 56, 88, 86, 84, 125, 156, 176, 176, 201, 114, 217, 245, 39, 190, 151, 236, 47, 216, 135, 108, 178, 169, 106, 107, 3, 184, 112, 172, 96, 51, 127, 195, 33, 210, 193, 63, 100, 4, 76, 142, 125, 100, 54, 178, 58, 162, 16, 200, 52, 45, 40, 234, 14, 77, 51, 206, 46, 116, 182, 197, 47, 45, 59, 184, 116, 98, 135, 51, 139, 179, 56, 191, 136, 130, 251, 106, 92, 151, 244, 195, 18, 184, 202, 249, 53, 197, 168, 23, 115, 172, 211, 143, 101, 212, 165, 117, 45, 130, 221, 194, 71, 82, 242, 178, 22, 11, 239, 138, 91, 248, 193, 107, 141 } });

            migrationBuilder.CreateIndex(
                name: "IX_UserOperationClaims_SiteUserId",
                table: "UserOperationClaims",
                column: "SiteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_SiteUserId",
                table: "UserLogins",
                column: "SiteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_SiteUserId",
                table: "RefreshTokens",
                column: "SiteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OtpAuthenticators_SiteUserId",
                table: "OtpAuthenticators",
                column: "SiteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ForgottenPasswords_SiteUserId",
                table: "ForgottenPasswords",
                column: "SiteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailAuthenticators_SiteUserId",
                table: "EmailAuthenticators",
                column: "SiteUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmarks_Blogs_BlogId",
                table: "Bookmarks",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmarks_Users_SiteUserId",
                table: "Bookmarks",
                column: "SiteUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailAuthenticators_Users_SiteUserId",
                table: "EmailAuthenticators",
                column: "SiteUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ForgottenPasswords_Users_SiteUserId",
                table: "ForgottenPasswords",
                column: "SiteUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OtpAuthenticators_Users_SiteUserId",
                table: "OtpAuthenticators",
                column: "SiteUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_Users_SiteUserId",
                table: "RefreshTokens",
                column: "SiteUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Users_UserID",
                table: "Reports",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogins_Users_SiteUserId",
                table: "UserLogins",
                column: "SiteUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserOperationClaims_Users_SiteUserId",
                table: "UserOperationClaims",
                column: "SiteUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
