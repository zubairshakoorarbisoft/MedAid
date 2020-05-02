using Microsoft.EntityFrameworkCore.Migrations;

namespace MedAidAPI.Migrations
{
    public partial class StoreIdremovedfromalarmtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alarms_AlarmTypes_AlarmTypeId",
                table: "Alarms");

            migrationBuilder.DropForeignKey(
                name: "FK_Alarms_Stores_StoreId",
                table: "Alarms");

            migrationBuilder.DropIndex(
                name: "IX_Alarms_StoreId",
                table: "Alarms");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "Alarms");

            migrationBuilder.AlterColumn<int>(
                name: "AlarmTypeId",
                table: "Alarms",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Alarms_AlarmTypes_AlarmTypeId",
                table: "Alarms",
                column: "AlarmTypeId",
                principalTable: "AlarmTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alarms_AlarmTypes_AlarmTypeId",
                table: "Alarms");

            migrationBuilder.AlterColumn<int>(
                name: "AlarmTypeId",
                table: "Alarms",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "Alarms",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Alarms_StoreId",
                table: "Alarms",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alarms_AlarmTypes_AlarmTypeId",
                table: "Alarms",
                column: "AlarmTypeId",
                principalTable: "AlarmTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Alarms_Stores_StoreId",
                table: "Alarms",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
