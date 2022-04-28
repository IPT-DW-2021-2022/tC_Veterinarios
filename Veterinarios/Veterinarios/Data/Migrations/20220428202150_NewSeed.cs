using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Veterinarios.Data.Migrations
{
    public partial class NewSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Donos",
                columns: new[] { "Id", "Email", "NIF", "Nome", "Sexo" },
                values: new object[,]
                {
                    { 1, null, "813635582", "Luís Freitas", "M" },
                    { 2, null, "854613462", "Andreia Gomes", "F" },
                    { 3, null, "265368715", "Cristina Sousa", "F" },
                    { 4, null, "835623190", "Sónia Rosa", "F" }
                });

            migrationBuilder.InsertData(
                table: "Animais",
                columns: new[] { "Id", "DonoFK", "Especie", "Foto", "Nome", "Peso", "Raca" },
                values: new object[,]
                {
                    { 1, 1, "Cão", "animal1.jpg", "Bubi", 24.0, "Pastor Alemão" },
                    { 2, 3, "Cão", "animal2.jpg", "Pastor", 50.0, "Serra Estrela" },
                    { 3, 2, "Cão", "animal3.jpg", "Tripé", 4.0, "Serra Estrela" },
                    { 4, 3, "Cavalo", "animal4.jpg", "Saltador", 580.0, "Lusitana" },
                    { 5, 3, "Gato", "animal5.jpg", "Tareco", 1.0, "siamês" },
                    { 6, 2, "Cão", "animal6.jpg", "Cusca", 45.0, "Labrador" },
                    { 7, 4, "Cão", "animal7.jpg", "Morde Tudo", 39.0, "Dobermann" },
                    { 8, 2, "Cão", "animal8.jpg", "Forte", 20.0, "Rottweiler" },
                    { 9, 3, "Vaca", "animal9.jpg", "Castanho", 652.0, "Mirandesa" },
                    { 10, 1, "Gato", "animal10.jpg", "Saltitão", 2.0, "Persa" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Animais",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Animais",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Animais",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Animais",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Animais",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Animais",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Animais",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Animais",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Animais",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Animais",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Donos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Donos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Donos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Donos",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
