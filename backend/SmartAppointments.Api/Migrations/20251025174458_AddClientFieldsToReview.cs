using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartAppointments.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddClientFieldsToReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Providers_Users_UserId",
                table: "Providers");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Appointments_AppointmentId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Providers_UserId",
                table: "Providers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Providers");

            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Availabilities");

            migrationBuilder.RenameColumn(
                name: "AppointmentId",
                table: "Reviews",
                newName: "ProviderId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_AppointmentId",
                table: "Reviews",
                newName: "IX_Reviews_ProviderId");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Providers",
                newName: "Specialty");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Providers",
                newName: "Name");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Reviews",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<double>(
                name: "Rating",
                table: "Providers",
                type: "double",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AddColumn<string>(
                name: "ContactInfo",
                table: "Providers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Availabilities",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TimeSlot",
                table: "Availabilities",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Providers_ProviderId",
                table: "Reviews",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Providers_ProviderId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ContactInfo",
                table: "Providers");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "TimeSlot",
                table: "Availabilities");

            migrationBuilder.RenameColumn(
                name: "ProviderId",
                table: "Reviews",
                newName: "AppointmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_ProviderId",
                table: "Reviews",
                newName: "IX_Reviews_AppointmentId");

            migrationBuilder.RenameColumn(
                name: "Specialty",
                table: "Providers",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Providers",
                newName: "Category");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Reviews",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<decimal>(
                name: "Rating",
                table: "Providers",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Providers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DayOfWeek",
                table: "Availabilities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "EndTime",
                table: "Availabilities",
                type: "time(6)",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "StartTime",
                table: "Availabilities",
                type: "time(6)",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.CreateIndex(
                name: "IX_Providers_UserId",
                table: "Providers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Providers_Users_UserId",
                table: "Providers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Appointments_AppointmentId",
                table: "Reviews",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
