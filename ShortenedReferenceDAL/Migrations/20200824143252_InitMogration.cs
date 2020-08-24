using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShortenedReferenceDAL.Migrations
{
    public partial class InitMogration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReferenceInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LongReference = table.Column<string>(nullable: false),
                    ShortenedReference = table.Column<string>(nullable: true),
                    CreatedData = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferenceInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Counters",
                columns: table => new
                {
                    ReferenceInfoId = table.Column<int>(nullable: false),
                    AmountClickLink = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counters", x => x.ReferenceInfoId);
                    table.ForeignKey(
                        name: "FK_Counters_ReferenceInfos_ReferenceInfoId",
                        column: x => x.ReferenceInfoId,
                        principalTable: "ReferenceInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Counters");

            migrationBuilder.DropTable(
                name: "ReferenceInfos");
        }
    }
}
