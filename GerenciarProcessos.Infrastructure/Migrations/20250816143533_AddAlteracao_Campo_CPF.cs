using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciarProcessos.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAlteracao_Campo_CPF : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Documento",
                table: "Clientes",
                newName: "CPF");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CPF",
                table: "Clientes",
                newName: "Documento");
        }
    }
}
