using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Patient_Manager.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:pg_stat_statements", ",,");

            migrationBuilder.CreateTable(
                name: "patients",
                columns: table => new
                {
                    patientid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    firstname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    lastname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    dateofbirth = table.Column<DateOnly>(type: "date", nullable: false),
                    sex = table.Column<char>(type: "character(1)", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("patients_pkey", x => x.patientid);
                });

            migrationBuilder.CreateTable(
                name: "checkups",
                columns: table => new
                {
                    checkupid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    patientid = table.Column<int>(type: "integer", nullable: false),
                    checkuptype = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    checkupdate = table.Column<DateOnly>(type: "date", nullable: false),
                    checkuptime = table.Column<TimeOnly>(type: "time without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("checkups_pkey", x => x.checkupid);
                    table.ForeignKey(
                        name: "checkups_patientid_fkey",
                        column: x => x.patientid,
                        principalTable: "patients",
                        principalColumn: "patientid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "medicalrecords",
                columns: table => new
                {
                    recordid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    patientid = table.Column<int>(type: "integer", nullable: false),
                    diseasename = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    startdate = table.Column<DateOnly>(type: "date", nullable: false),
                    enddate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("medicalrecords_pkey", x => x.recordid);
                    table.ForeignKey(
                        name: "medicalrecords_patientid_fkey",
                        column: x => x.patientid,
                        principalTable: "patients",
                        principalColumn: "patientid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "medicalfiles",
                columns: table => new
                {
                    fileid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    checkupid = table.Column<int>(type: "integer", nullable: false),
                    filepath = table.Column<string>(type: "text", nullable: false),
                    uploadedat = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("medicalfiles_pkey", x => x.fileid);
                    table.ForeignKey(
                        name: "medicalfiles_checkupid_fkey",
                        column: x => x.checkupid,
                        principalTable: "checkups",
                        principalColumn: "checkupid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "prescriptions",
                columns: table => new
                {
                    prescriptionid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    checkupid = table.Column<int>(type: "integer", nullable: false),
                    medication = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    dosage = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("prescriptions_pkey", x => x.prescriptionid);
                    table.ForeignKey(
                        name: "prescriptions_checkupid_fkey",
                        column: x => x.checkupid,
                        principalTable: "checkups",
                        principalColumn: "checkupid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_checkups_patientid",
                table: "checkups",
                column: "patientid");

            migrationBuilder.CreateIndex(
                name: "IX_medicalfiles_checkupid",
                table: "medicalfiles",
                column: "checkupid");

            migrationBuilder.CreateIndex(
                name: "IX_medicalrecords_patientid",
                table: "medicalrecords",
                column: "patientid");

            migrationBuilder.CreateIndex(
                name: "IX_prescriptions_checkupid",
                table: "prescriptions",
                column: "checkupid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "medicalfiles");

            migrationBuilder.DropTable(
                name: "medicalrecords");

            migrationBuilder.DropTable(
                name: "prescriptions");

            migrationBuilder.DropTable(
                name: "checkups");

            migrationBuilder.DropTable(
                name: "patients");
        }
    }
}
