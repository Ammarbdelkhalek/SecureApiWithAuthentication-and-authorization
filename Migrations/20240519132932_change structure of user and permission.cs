using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecureApiWithAuthentication.Migrations
{
    /// <inheritdoc />
    public partial class changestructureofuserandpermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userPermissions_Users_userId",
                table: "userPermissions");

            migrationBuilder.DropIndex(
                name: "IX_userPermissions_userId",
                table: "userPermissions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_userPermissions_userId",
                table: "userPermissions",
                column: "userId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_userPermissions_Users_userId",
                table: "userPermissions",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
