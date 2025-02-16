﻿// <auto-generated />
using System;
using CafeOrderManager.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CafeOrderManager.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20241125191333_SeedMenuItems")]
    partial class SeedMenuItems
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.13");

            modelBuilder.Entity("CafeOrderManager.Models.MenuDish", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Category")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("MenuDishes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Category = "Основные блюда",
                            Description = "Сочный стейк из мраморной говядины",
                            IsAvailable = true,
                            Name = "Стейк Рибай",
                            Price = 2500m
                        },
                        new
                        {
                            Id = 2,
                            Category = "Основные блюда",
                            Description = "Филе лосося с овощами",
                            IsAvailable = true,
                            Name = "Лосось на гриле",
                            Price = 1800m
                        },
                        new
                        {
                            Id = 3,
                            Category = "Основные блюда",
                            Description = "Классическая итальянская паста с беконом",
                            IsAvailable = true,
                            Name = "Паста Карбонара",
                            Price = 950m
                        },
                        new
                        {
                            Id = 4,
                            Category = "Основные блюда",
                            Description = "Нежное куриное филе в сливочном соусе",
                            IsAvailable = true,
                            Name = "Куриное филе с грибами",
                            Price = 850m
                        },
                        new
                        {
                            Id = 5,
                            Category = "Основные блюда",
                            Description = "С апельсиновым соусом и картофельным пюре",
                            IsAvailable = true,
                            Name = "Утиная грудка",
                            Price = 1600m
                        },
                        new
                        {
                            Id = 6,
                            Category = "Салаты",
                            Description = "Классический салат с куриным филе",
                            IsAvailable = true,
                            Name = "Цезарь с курицей",
                            Price = 650m
                        },
                        new
                        {
                            Id = 7,
                            Category = "Салаты",
                            Description = "Свежие овощи с сыром фета",
                            IsAvailable = true,
                            Name = "Греческий салат",
                            Price = 550m
                        },
                        new
                        {
                            Id = 8,
                            Category = "Салаты",
                            Description = "Тигровые креветки с авокадо",
                            IsAvailable = true,
                            Name = "Салат с креветками",
                            Price = 850m
                        },
                        new
                        {
                            Id = 9,
                            Category = "Салаты",
                            Description = "Традиционный русский салат",
                            IsAvailable = true,
                            Name = "Оливье",
                            Price = 450m
                        },
                        new
                        {
                            Id = 10,
                            Category = "Салаты",
                            Description = "Моцарелла с томатами и базиликом",
                            IsAvailable = true,
                            Name = "Капрезе",
                            Price = 600m
                        },
                        new
                        {
                            Id = 11,
                            Category = "Супы",
                            Description = "Традиционный украинский борщ со сметаной",
                            IsAvailable = true,
                            Name = "Борщ",
                            Price = 400m
                        },
                        new
                        {
                            Id = 12,
                            Category = "Супы",
                            Description = "Нежный суп из белых грибов",
                            IsAvailable = true,
                            Name = "Грибной крем-суп",
                            Price = 450m
                        },
                        new
                        {
                            Id = 13,
                            Category = "Супы",
                            Description = "Рыбный суп из трех видов рыб",
                            IsAvailable = true,
                            Name = "Уха",
                            Price = 500m
                        },
                        new
                        {
                            Id = 14,
                            Category = "Супы",
                            Description = "Острый тайский суп с морепродуктами",
                            IsAvailable = true,
                            Name = "Том Ям",
                            Price = 700m
                        },
                        new
                        {
                            Id = 15,
                            Category = "Супы",
                            Description = "Итальянский овощной суп",
                            IsAvailable = true,
                            Name = "Минестроне",
                            Price = 400m
                        },
                        new
                        {
                            Id = 16,
                            Category = "Закуски",
                            Description = "Хрустящий багет с лососем и сливочным сыром",
                            IsAvailable = true,
                            Name = "Брускетта с лососем",
                            Price = 450m
                        },
                        new
                        {
                            Id = 17,
                            Category = "Закуски",
                            Description = "Тонко нарезанная говядина с рукколой",
                            IsAvailable = true,
                            Name = "Карпаччо из говядины",
                            Price = 750m
                        },
                        new
                        {
                            Id = 18,
                            Category = "Закуски",
                            Description = "Хрустящие креветки с соусом тартар",
                            IsAvailable = true,
                            Name = "Креветки в кляре",
                            Price = 650m
                        },
                        new
                        {
                            Id = 19,
                            Category = "Закуски",
                            Description = "Ассорти из пяти видов сыра",
                            IsAvailable = true,
                            Name = "Сырная тарелка",
                            Price = 950m
                        },
                        new
                        {
                            Id = 20,
                            Category = "Закуски",
                            Description = "Нарезка из различных видов мяса",
                            IsAvailable = true,
                            Name = "Мясное ассорти",
                            Price = 1200m
                        },
                        new
                        {
                            Id = 21,
                            Category = "Десерты",
                            Description = "Классический итальянский десерт",
                            IsAvailable = true,
                            Name = "Тирамису",
                            Price = 400m
                        },
                        new
                        {
                            Id = 22,
                            Category = "Десерты",
                            Description = "Нью-Йоркский чизкейк",
                            IsAvailable = true,
                            Name = "Чизкейк",
                            Price = 450m
                        },
                        new
                        {
                            Id = 23,
                            Category = "Десерты",
                            Description = "С шариком ванильного мороженого",
                            IsAvailable = true,
                            Name = "Шоколадный фондан",
                            Price = 500m
                        },
                        new
                        {
                            Id = 24,
                            Category = "Десерты",
                            Description = "С малиновым соусом",
                            IsAvailable = true,
                            Name = "Панна котта",
                            Price = 400m
                        },
                        new
                        {
                            Id = 25,
                            Category = "Десерты",
                            Description = "Ассорти из сезонных фруктов",
                            IsAvailable = true,
                            Name = "Фруктовая тарелка",
                            Price = 600m
                        },
                        new
                        {
                            Id = 26,
                            Category = "Напитки",
                            Description = "Классический итальянский кофе",
                            IsAvailable = true,
                            Name = "Эспрессо",
                            Price = 150m
                        },
                        new
                        {
                            Id = 27,
                            Category = "Напитки",
                            Description = "С молочной пенкой",
                            IsAvailable = true,
                            Name = "Капучино",
                            Price = 200m
                        },
                        new
                        {
                            Id = 28,
                            Category = "Напитки",
                            Description = "200 мл",
                            IsAvailable = true,
                            Name = "Свежевыжатый апельсиновый сок",
                            Price = 250m
                        },
                        new
                        {
                            Id = 29,
                            Category = "Напитки",
                            Description = "С жасмином",
                            IsAvailable = true,
                            Name = "Зеленый чай",
                            Price = 150m
                        },
                        new
                        {
                            Id = 30,
                            Category = "Напитки",
                            Description = "С мятой и лаймом",
                            IsAvailable = true,
                            Name = "Лимонад домашний",
                            Price = 200m
                        });
                });

            modelBuilder.Entity("CafeOrderManager.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("CafeOrderManager.Models.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("MenuDishId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("OrderId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("MenuDishId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("CafeOrderManager.Models.OrderItem", b =>
                {
                    b.HasOne("CafeOrderManager.Models.MenuDish", "MenuDish")
                        .WithMany()
                        .HasForeignKey("MenuDishId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CafeOrderManager.Models.Order", null)
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MenuDish");
                });

            modelBuilder.Entity("CafeOrderManager.Models.Order", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}
