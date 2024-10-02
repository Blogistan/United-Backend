using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class update1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "CreateUser", "CreatedDate", "DeleteUser", "DeletedDate", "Name", "UpdateUser", "UpdatedDate" },
                values: new object[] { 3, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, "User", 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 28, 221, 238, 245, 75, 65, 174, 19, 237, 61, 214, 100, 45, 66, 157, 34, 44, 204, 1, 157, 254, 42, 33, 58, 184, 223, 59, 230, 179, 195, 168, 83, 102, 129, 223, 131, 238, 12, 14, 42, 100, 251, 101, 237, 169, 19, 155, 234, 102, 237, 48, 62, 48, 234, 90, 20, 91, 56, 163, 83, 136, 116, 173, 221 }, new byte[] { 160, 143, 32, 102, 139, 148, 158, 21, 0, 130, 124, 159, 22, 75, 135, 213, 14, 191, 85, 66, 5, 122, 49, 85, 244, 136, 42, 209, 38, 126, 242, 139, 191, 32, 107, 187, 137, 72, 131, 98, 74, 86, 87, 239, 1, 18, 213, 224, 39, 84, 166, 52, 118, 209, 78, 233, 116, 119, 18, 60, 60, 115, 184, 71, 54, 55, 3, 65, 104, 31, 147, 63, 58, 179, 175, 230, 76, 243, 99, 214, 7, 222, 203, 159, 229, 240, 7, 225, 15, 29, 36, 71, 228, 227, 11, 74, 155, 94, 2, 222, 59, 45, 231, 78, 193, 124, 248, 143, 87, 231, 104, 169, 38, 191, 97, 82, 158, 91, 209, 216, 226, 72, 148, 164, 187, 250, 210, 54 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 126, 46, 232, 174, 23, 229, 84, 62, 149, 25, 126, 131, 170, 202, 93, 66, 116, 132, 67, 170, 8, 216, 135, 32, 139, 223, 38, 139, 245, 199, 19, 77, 3, 66, 11, 232, 30, 169, 109, 52, 49, 198, 237, 134, 60, 128, 188, 166, 153, 185, 21, 232, 157, 221, 18, 69, 74, 232, 23, 47, 238, 228, 154, 22 }, new byte[] { 104, 239, 166, 148, 168, 77, 78, 216, 24, 236, 3, 220, 121, 228, 204, 2, 111, 33, 15, 1, 82, 52, 208, 143, 107, 134, 205, 226, 168, 128, 101, 200, 214, 210, 84, 231, 21, 255, 83, 242, 143, 75, 19, 243, 130, 8, 98, 17, 188, 165, 30, 249, 188, 100, 249, 177, 63, 63, 3, 253, 191, 244, 208, 28, 88, 205, 196, 141, 149, 80, 229, 68, 97, 14, 188, 147, 2, 204, 84, 89, 83, 249, 108, 174, 23, 177, 135, 242, 68, 30, 243, 92, 174, 137, 142, 1, 162, 94, 239, 150, 61, 98, 96, 149, 191, 18, 84, 213, 81, 149, 0, 96, 28, 144, 127, 2, 216, 71, 138, 247, 28, 55, 52, 18, 42, 194, 162, 232 } });
        }
    }
}
