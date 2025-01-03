using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniProject.MVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class ComplainSp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "MenPrice",
                table: "Complaint",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "TechnicienId",
                table: "Complaint",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ComplaintSpareParts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComplaintId = table.Column<int>(type: "int", nullable: false),
                    SparePartId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintSpareParts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComplaintSpareParts_Complaint_ComplaintId",
                        column: x => x.ComplaintId,
                        principalTable: "Complaint",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ComplaintSpareParts_SpareParts_SparePartId",
                        column: x => x.SparePartId,
                        principalTable: "SpareParts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Techniciens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Techniciens", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Complaint_TechnicienId",
                table: "Complaint",
                column: "TechnicienId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintSpareParts_ComplaintId",
                table: "ComplaintSpareParts",
                column: "ComplaintId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintSpareParts_SparePartId",
                table: "ComplaintSpareParts",
                column: "SparePartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Complaint_Techniciens_TechnicienId",
                table: "Complaint",
                column: "TechnicienId",
                principalTable: "Techniciens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaint_Techniciens_TechnicienId",
                table: "Complaint");

            migrationBuilder.DropTable(
                name: "ComplaintSpareParts");

            migrationBuilder.DropTable(
                name: "Techniciens");

            migrationBuilder.DropIndex(
                name: "IX_Complaint_TechnicienId",
                table: "Complaint");

            migrationBuilder.DropColumn(
                name: "MenPrice",
                table: "Complaint");

            migrationBuilder.DropColumn(
                name: "TechnicienId",
                table: "Complaint");
        }
    }
}
