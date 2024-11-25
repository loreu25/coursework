using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CafeOrderManager.Migrations
{
    /// <inheritdoc />
    public partial class SeedMenuItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MenuDishes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    Category = table.Column<string>(type: "TEXT", nullable: true),
                    IsAvailable = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuDishes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    MenuDishId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_MenuDishes_MenuDishId",
                        column: x => x.MenuDishId,
                        principalTable: "MenuDishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MenuDishes",
                columns: new[] { "Id", "Category", "Description", "IsAvailable", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Основные блюда", "Сочный стейк из мраморной говядины", true, "Стейк Рибай", 2500m },
                    { 2, "Основные блюда", "Филе лосося с овощами", true, "Лосось на гриле", 1800m },
                    { 3, "Основные блюда", "Классическая итальянская паста с беконом", true, "Паста Карбонара", 950m },
                    { 4, "Основные блюда", "Нежное куриное филе в сливочном соусе", true, "Куриное филе с грибами", 850m },
                    { 5, "Основные блюда", "С апельсиновым соусом и картофельным пюре", true, "Утиная грудка", 1600m },
                    { 6, "Салаты", "Классический салат с куриным филе", true, "Цезарь с курицей", 650m },
                    { 7, "Салаты", "Свежие овощи с сыром фета", true, "Греческий салат", 550m },
                    { 8, "Салаты", "Тигровые креветки с авокадо", true, "Салат с креветками", 850m },
                    { 9, "Салаты", "Традиционный русский салат", true, "Оливье", 450m },
                    { 10, "Салаты", "Моцарелла с томатами и базиликом", true, "Капрезе", 600m },
                    { 11, "Супы", "Традиционный украинский борщ со сметаной", true, "Борщ", 400m },
                    { 12, "Супы", "Нежный суп из белых грибов", true, "Грибной крем-суп", 450m },
                    { 13, "Супы", "Рыбный суп из трех видов рыб", true, "Уха", 500m },
                    { 14, "Супы", "Острый тайский суп с морепродуктами", true, "Том Ям", 700m },
                    { 15, "Супы", "Итальянский овощной суп", true, "Минестроне", 400m },
                    { 16, "Закуски", "Хрустящий багет с лососем и сливочным сыром", true, "Брускетта с лососем", 450m },
                    { 17, "Закуски", "Тонко нарезанная говядина с рукколой", true, "Карпаччо из говядины", 750m },
                    { 18, "Закуски", "Хрустящие креветки с соусом тартар", true, "Креветки в кляре", 650m },
                    { 19, "Закуски", "Ассорти из пяти видов сыра", true, "Сырная тарелка", 950m },
                    { 20, "Закуски", "Нарезка из различных видов мяса", true, "Мясное ассорти", 1200m },
                    { 21, "Десерты", "Классический итальянский десерт", true, "Тирамису", 400m },
                    { 22, "Десерты", "Нью-Йоркский чизкейк", true, "Чизкейк", 450m },
                    { 23, "Десерты", "С шариком ванильного мороженого", true, "Шоколадный фондан", 500m },
                    { 24, "Десерты", "С малиновым соусом", true, "Панна котта", 400m },
                    { 25, "Десерты", "Ассорти из сезонных фруктов", true, "Фруктовая тарелка", 600m },
                    { 26, "Напитки", "Классический итальянский кофе", true, "Эспрессо", 150m },
                    { 27, "Напитки", "С молочной пенкой", true, "Капучино", 200m },
                    { 28, "Напитки", "200 мл", true, "Свежевыжатый апельсиновый сок", 250m },
                    { 29, "Напитки", "С жасмином", true, "Зеленый чай", 150m },
                    { 30, "Напитки", "С мятой и лаймом", true, "Лимонад домашний", 200m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_MenuDishId",
                table: "OrderItems",
                column: "MenuDishId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "MenuDishes");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
