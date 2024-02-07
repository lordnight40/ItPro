using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItPro.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedStoredProcedures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = GetSqlRaw("GetBirthdayReceiptSumByClients.sql");
            migrationBuilder.Sql(sql);

            sql = GetSqlRaw("GetHourlyAverageSumByStatus.sql");
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }

        private static string GetSqlRaw(string fileName)
        {
            var baseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "StoredProcedures");
            var path = Path.Combine(baseDirectory, fileName);

            return File.ReadAllText(path);
        }
    }
}
