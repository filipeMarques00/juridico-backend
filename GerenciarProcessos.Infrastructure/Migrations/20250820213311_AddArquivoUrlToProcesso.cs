using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciarProcessos.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddArquivoUrlToProcesso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ArquivoUrl",
                table: "Processos",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArquivoUrl",
                table: "Processos");
        }
    }
}
