using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VebTech.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class rename_table_UserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Roles_RolesRoleId",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Users_UsersUserId",
                table: "UserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole");

            migrationBuilder.RenameTable(
                name: "UserRole",
                newName: "UsersRoles");

            migrationBuilder.RenameIndex(
                name: "IX_UserRole_UsersUserId",
                table: "UsersRoles",
                newName: "IX_UsersRoles_UsersUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersRoles",
                table: "UsersRoles",
                columns: new[] { "RolesRoleId", "UsersUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UsersRoles_Roles_RolesRoleId",
                table: "UsersRoles",
                column: "RolesRoleId",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersRoles_Users_UsersUserId",
                table: "UsersRoles",
                column: "UsersUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersRoles_Roles_RolesRoleId",
                table: "UsersRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersRoles_Users_UsersUserId",
                table: "UsersRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersRoles",
                table: "UsersRoles");

            migrationBuilder.RenameTable(
                name: "UsersRoles",
                newName: "UserRole");

            migrationBuilder.RenameIndex(
                name: "IX_UsersRoles_UsersUserId",
                table: "UserRole",
                newName: "IX_UserRole_UsersUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole",
                columns: new[] { "RolesRoleId", "UsersUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Roles_RolesRoleId",
                table: "UserRole",
                column: "RolesRoleId",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Users_UsersUserId",
                table: "UserRole",
                column: "UsersUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
