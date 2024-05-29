﻿// <auto-generated />
using System;
using FoodDeliveryWebApp.context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FoodDeliveryWebApp.Migrations
{
    [DbContext(typeof(FoodAppContext))]
    [Migration("20240528044439_feedback")]
    partial class feedback
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.30")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FoodDeliveryWebApp.models.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("FoodDeliveryWebApp.models.Cart", b =>
                {
                    b.Property<int>("CartId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.HasKey("CartId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("FoodDeliveryWebApp.models.CartDetails", b =>
                {
                    b.Property<int>("CartDetailsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartDetailsId"), 1L, 1);

                    b.Property<int>("CartId")
                        .HasColumnType("int");

                    b.Property<int>("FId")
                        .HasColumnType("int");

                    b.Property<int?>("MenuFId")
                        .HasColumnType("int");

                    b.Property<int>("Qty_ordered")
                        .HasColumnType("int");

                    b.Property<int>("Total")
                        .HasColumnType("int");

                    b.HasKey("CartDetailsId");

                    b.HasIndex("CartId");

                    b.HasIndex("MenuFId");

                    b.ToTable("CartDetails");
                });

            modelBuilder.Entity("FoodDeliveryWebApp.models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("FoodDeliveryWebApp.models.FbComment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommentId"), 1L, 1);

                    b.Property<int>("AdminId")
                        .HasColumnType("int");

                    b.Property<string>("CommentText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FbId")
                        .HasColumnType("int");

                    b.HasKey("CommentId");

                    b.ToTable("FbComment");
                });

            modelBuilder.Entity("FoodDeliveryWebApp.models.Feedback", b =>
                {
                    b.Property<int>("FbId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FbId"), 1L, 1);

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CommentId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("FId")
                        .HasColumnType("int");

                    b.Property<int?>("FbCommentCommentId")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.HasKey("FbId");

                    b.HasIndex("CommentId");

                    b.HasIndex("FbCommentCommentId");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("FoodDeliveryWebApp.models.Menu", b =>
                {
                    b.Property<int>("FId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FId"), 1L, 1);

                    b.Property<string>("FName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuantityInStock")
                        .HasColumnType("int");

                    b.Property<int>("UnitPrice")
                        .HasColumnType("int");

                    b.HasKey("FId");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("FoodDeliveryWebApp.models.Order", b =>
                {
                    b.Property<int>("OId")
                        .HasColumnType("int");

                    b.Property<int>("CartId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("FoodDeliveryWebApp.models.OrderDetails", b =>
                {
                    b.Property<int>("OrderDetailsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderDetailsId"), 1L, 1);

                    b.Property<int>("FId")
                        .HasColumnType("int");

                    b.Property<int?>("MenuFId")
                        .HasColumnType("int");

                    b.Property<int>("OId")
                        .HasColumnType("int");

                    b.Property<int?>("OrderOId")
                        .HasColumnType("int");

                    b.Property<int>("Qty_ordered")
                        .HasColumnType("int");

                    b.Property<int>("Total")
                        .HasColumnType("int");

                    b.HasKey("OrderDetailsId");

                    b.HasIndex("MenuFId");

                    b.HasIndex("OrderOId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("FoodDeliveryWebApp.models.Payment", b =>
                {
                    b.Property<int>("PayId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PayId"), 1L, 1);

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("OId")
                        .HasColumnType("int");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PayId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("FoodDeliveryWebApp.models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AdminId")
                        .HasColumnType("int");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordHashKey")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FoodDeliveryWebApp.models.Cart", b =>
                {
                    b.HasOne("FoodDeliveryWebApp.models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("FoodDeliveryWebApp.models.CartDetails", b =>
                {
                    b.HasOne("FoodDeliveryWebApp.models.Cart", null)
                        .WithMany("CartDetails")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodDeliveryWebApp.models.Menu", "Menu")
                        .WithMany()
                        .HasForeignKey("MenuFId");

                    b.Navigation("Menu");
                });

            modelBuilder.Entity("FoodDeliveryWebApp.models.Feedback", b =>
                {
                    b.HasOne("FoodDeliveryWebApp.models.Menu", "Menu")
                        .WithMany()
                        .HasForeignKey("CommentId");

                    b.HasOne("FoodDeliveryWebApp.models.FbComment", "FbComment")
                        .WithMany()
                        .HasForeignKey("FbCommentCommentId");

                    b.Navigation("FbComment");

                    b.Navigation("Menu");
                });

            modelBuilder.Entity("FoodDeliveryWebApp.models.Order", b =>
                {
                    b.HasOne("FoodDeliveryWebApp.models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("FoodDeliveryWebApp.models.OrderDetails", b =>
                {
                    b.HasOne("FoodDeliveryWebApp.models.Menu", "Menu")
                        .WithMany()
                        .HasForeignKey("MenuFId");

                    b.HasOne("FoodDeliveryWebApp.models.Order", null)
                        .WithMany("Details")
                        .HasForeignKey("OrderOId");

                    b.Navigation("Menu");
                });

            modelBuilder.Entity("FoodDeliveryWebApp.models.Payment", b =>
                {
                    b.HasOne("FoodDeliveryWebApp.models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("FoodDeliveryWebApp.models.User", b =>
                {
                    b.HasOne("FoodDeliveryWebApp.models.Admin", "Admin")
                        .WithMany()
                        .HasForeignKey("AdminId");

                    b.HasOne("FoodDeliveryWebApp.models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.Navigation("Admin");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("FoodDeliveryWebApp.models.Cart", b =>
                {
                    b.Navigation("CartDetails");
                });

            modelBuilder.Entity("FoodDeliveryWebApp.models.Order", b =>
                {
                    b.Navigation("Details");
                });
#pragma warning restore 612, 618
        }
    }
}
