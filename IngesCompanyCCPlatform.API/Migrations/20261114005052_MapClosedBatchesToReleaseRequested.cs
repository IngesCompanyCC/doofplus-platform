using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IngesCompanyCCPlatform.API.Migrations
{
    /// <inheritdoc />
    public partial class MapClosedBatchesToReleaseRequested : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE production_batches SET status = 'ReleaseRequested' WHERE status = 'Closed';");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE production_batches SET status = 'Closed' WHERE status IN ('ReleaseRequested', 'Released');");
        }
    }
}
