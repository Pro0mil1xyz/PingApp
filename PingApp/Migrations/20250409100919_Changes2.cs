using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PingApp.Migrations
{
    /// <inheritdoc />
    public partial class Changes2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PingHistories_Devices_DeviceId",
                table: "PingHistories");

            migrationBuilder.AlterColumn<int>(
                name: "DeviceId",
                table: "PingHistories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "IPAddress",
                table: "PingHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_PingHistories_Devices_DeviceId",
                table: "PingHistories",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PingHistories_Devices_DeviceId",
                table: "PingHistories");

            migrationBuilder.DropColumn(
                name: "IPAddress",
                table: "PingHistories");

            migrationBuilder.AlterColumn<int>(
                name: "DeviceId",
                table: "PingHistories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PingHistories_Devices_DeviceId",
                table: "PingHistories",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
