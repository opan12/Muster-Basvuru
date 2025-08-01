using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusterıBasvuru.Migrations
{
    /// <inheritdoc />
    public partial class sa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ErrorLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StackTrace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    logId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MusteriBasvuru_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Basvuru_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Açıklama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kayit_Yapan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kayit_Zaman = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.logId);
                });

            migrationBuilder.CreateTable(
                name: "MusteriBasvuru",
                columns: table => new
                {
                    Basvuru_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MusteriBasvuru_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MusteriNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BasvuruDurum = table.Column<int>(type: "int", nullable: false),
                    Basvurutipi = table.Column<int>(type: "int", nullable: false),
                    BasvuruTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HataAciklama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kayit_Zaman = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Kayit_Yapan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kayit_Durum = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusteriBasvuru", x => x.Basvuru_UID);
                });

            migrationBuilder.CreateTable(
                name: "Musteriİletisim",
                columns: table => new
                {
                    IletısımId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MusteriBasvuru_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Basvuru_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GönderilmeSeklı = table.Column<bool>(type: "bit", nullable: false),
                    Acıklama = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musteriİletisim", x => x.IletısımId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    MusteriBasvuru_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MusteriNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TCKimlikNO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DogumTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.MusteriBasvuru_UID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminUser");

            migrationBuilder.DropTable(
                name: "ErrorLogs");

            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "MusteriBasvuru");

            migrationBuilder.DropTable(
                name: "Musteriİletisim");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
