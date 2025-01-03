using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniProject.MVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class nullabletechnicine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaint_Techniciens_TechnicienId",
                table: "Complaint");

            migrationBuilder.AlterColumn<int>(
                name: "TechnicienId",
                table: "Complaint",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Complaint_Techniciens_TechnicienId",
                table: "Complaint",
                column: "TechnicienId",
                principalTable: "Techniciens",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaint_Techniciens_TechnicienId",
                table: "Complaint");

            migrationBuilder.AlterColumn<int>(
                name: "TechnicienId",
                table: "Complaint",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Complaint_Techniciens_TechnicienId",
                table: "Complaint",
                column: "TechnicienId",
                principalTable: "Techniciens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
