using Microsoft.EntityFrameworkCore;
using CafeOrderManager.Models;
using System.IO;
using System;

namespace CafeOrderManager.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<MenuDish> MenuDishes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public DatabaseContext()
        {
            Database.EnsureCreated(); // Only create if doesn't exist
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CafeDB.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MenuDish>()
                .HasKey(m => m.Id);

            modelBuilder.Entity<Order>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => oi.Id);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.MenuDish)
                .WithMany()
                .HasForeignKey(oi => oi.MenuDishId);

            // Seed menu items
            var menuItems = new[]
            {
                // Основные блюда
                new MenuDish { Id = 1, Name = "Стейк Рибай", Description = "Сочный стейк из мраморной говядины", Price = 2500m, Category = "Основные блюда", IsAvailable = true },
                new MenuDish { Id = 2, Name = "Лосось на гриле", Description = "Филе лосося с овощами", Price = 1800m, Category = "Основные блюда", IsAvailable = true },
                new MenuDish { Id = 3, Name = "Паста Карбонара", Description = "Классическая итальянская паста с беконом", Price = 950m, Category = "Основные блюда", IsAvailable = true },
                new MenuDish { Id = 4, Name = "Куриное филе с грибами", Description = "Нежное куриное филе в сливочном соусе", Price = 850m, Category = "Основные блюда", IsAvailable = true },
                new MenuDish { Id = 5, Name = "Утиная грудка", Description = "С апельсиновым соусом и картофельным пюре", Price = 1600m, Category = "Основные блюда", IsAvailable = true },
                
                // Салаты
                new MenuDish { Id = 6, Name = "Цезарь с курицей", Description = "Классический салат с куриным филе", Price = 650m, Category = "Салаты", IsAvailable = true },
                new MenuDish { Id = 7, Name = "Греческий салат", Description = "Свежие овощи с сыром фета", Price = 550m, Category = "Салаты", IsAvailable = true },
                new MenuDish { Id = 8, Name = "Салат с креветками", Description = "Тигровые креветки с авокадо", Price = 850m, Category = "Салаты", IsAvailable = true },
                new MenuDish { Id = 9, Name = "Оливье", Description = "Традиционный русский салат", Price = 450m, Category = "Салаты", IsAvailable = true },
                new MenuDish { Id = 10, Name = "Капрезе", Description = "Моцарелла с томатами и базиликом", Price = 600m, Category = "Салаты", IsAvailable = true },
                
                // Супы
                new MenuDish { Id = 11, Name = "Борщ", Description = "Традиционный украинский борщ со сметаной", Price = 400m, Category = "Супы", IsAvailable = true },
                new MenuDish { Id = 12, Name = "Грибной крем-суп", Description = "Нежный суп из белых грибов", Price = 450m, Category = "Супы", IsAvailable = true },
                new MenuDish { Id = 13, Name = "Уха", Description = "Рыбный суп из трех видов рыб", Price = 500m, Category = "Супы", IsAvailable = true },
                new MenuDish { Id = 14, Name = "Том Ям", Description = "Острый тайский суп с морепродуктами", Price = 700m, Category = "Супы", IsAvailable = true },
                new MenuDish { Id = 15, Name = "Минестроне", Description = "Итальянский овощной суп", Price = 400m, Category = "Супы", IsAvailable = true },
                
                // Закуски
                new MenuDish { Id = 16, Name = "Брускетта с лососем", Description = "Хрустящий багет с лососем и сливочным сыром", Price = 450m, Category = "Закуски", IsAvailable = true },
                new MenuDish { Id = 17, Name = "Карпаччо из говядины", Description = "Тонко нарезанная говядина с рукколой", Price = 750m, Category = "Закуски", IsAvailable = true },
                new MenuDish { Id = 18, Name = "Креветки в кляре", Description = "Хрустящие креветки с соусом тартар", Price = 650m, Category = "Закуски", IsAvailable = true },
                new MenuDish { Id = 19, Name = "Сырная тарелка", Description = "Ассорти из пяти видов сыра", Price = 950m, Category = "Закуски", IsAvailable = true },
                new MenuDish { Id = 20, Name = "Мясное ассорти", Description = "Нарезка из различных видов мяса", Price = 1200m, Category = "Закуски", IsAvailable = true },
                
                // Десерты
                new MenuDish { Id = 21, Name = "Тирамису", Description = "Классический итальянский десерт", Price = 400m, Category = "Десерты", IsAvailable = true },
                new MenuDish { Id = 22, Name = "Чизкейк", Description = "Нью-Йоркский чизкейк", Price = 450m, Category = "Десерты", IsAvailable = true },
                new MenuDish { Id = 23, Name = "Шоколадный фондан", Description = "С шариком ванильного мороженого", Price = 500m, Category = "Десерты", IsAvailable = true },
                new MenuDish { Id = 24, Name = "Панна котта", Description = "С малиновым соусом", Price = 400m, Category = "Десерты", IsAvailable = true },
                new MenuDish { Id = 25, Name = "Фруктовая тарелка", Description = "Ассорти из сезонных фруктов", Price = 600m, Category = "Десерты", IsAvailable = true },
                
                // Напитки
                new MenuDish { Id = 26, Name = "Эспрессо", Description = "Классический итальянский кофе", Price = 150m, Category = "Напитки", IsAvailable = true },
                new MenuDish { Id = 27, Name = "Капучино", Description = "С молочной пенкой", Price = 200m, Category = "Напитки", IsAvailable = true },
                new MenuDish { Id = 28, Name = "Свежевыжатый апельсиновый сок", Description = "200 мл", Price = 250m, Category = "Напитки", IsAvailable = true },
                new MenuDish { Id = 29, Name = "Зеленый чай", Description = "С жасмином", Price = 150m, Category = "Напитки", IsAvailable = true },
                new MenuDish { Id = 30, Name = "Лимонад домашний", Description = "С мятой и лаймом", Price = 200m, Category = "Напитки", IsAvailable = true }
            };

            modelBuilder.Entity<MenuDish>().HasData(menuItems);
        }
    }
}
