﻿// <auto-generated />
using System;
using BobBookstore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BobBookstore.Migrations
{
    [DbContext(typeof(UsedBooksContext))]
    partial class UsedBooksContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BobBookstore.Models.Book.Book", b =>
                {
                    b.Property<long>("Book_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AudioBook_Url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Back_Url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Front_Url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Genre_Id")
                        .HasColumnType("bigint");

                    b.Property<long?>("Genre_Id1")
                        .HasColumnType("bigint");

                    b.Property<long>("ISBN")
                        .HasColumnType("bigint");

                    b.Property<string>("Left_Url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("Publisher_Id")
                        .HasColumnType("bigint");

                    b.Property<string>("Right_Url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Summary_Url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("Type_Id")
                        .HasColumnType("bigint");

                    b.HasKey("Book_Id");

                    b.HasIndex("Genre_Id");

                    b.HasIndex("Genre_Id1");

                    b.HasIndex("Publisher_Id");

                    b.HasIndex("Type_Id");

                    b.ToTable("Book");
                });

            modelBuilder.Entity("BobBookstore.Models.Book.Condition", b =>
                {
                    b.Property<long>("Condition_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConditionName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Condition_Id");

                    b.ToTable("Condition");
                });

            modelBuilder.Entity("BobBookstore.Models.Book.Genre", b =>
                {
                    b.Property<long>("Genre_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Genre_Id");

                    b.ToTable("Genre");
                });

            modelBuilder.Entity("BobBookstore.Models.Book.Price", b =>
                {
                    b.Property<long>("Price_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("Book_Id")
                        .HasColumnType("bigint");

                    b.Property<long?>("Condition_Id")
                        .HasColumnType("bigint");

                    b.Property<double>("ItemPrice")
                        .HasColumnType("float");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Price_Id");

                    b.HasIndex("Book_Id");

                    b.HasIndex("Condition_Id");

                    b.ToTable("Price");
                });

            modelBuilder.Entity("BobBookstore.Models.Book.Publisher", b =>
                {
                    b.Property<long>("Publisher_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Publisher_Id");

                    b.ToTable("Publisher");
                });

            modelBuilder.Entity("BobBookstore.Models.Book.Type", b =>
                {
                    b.Property<long>("Type_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TypeName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Type_Id");

                    b.ToTable("Type");
                });

            modelBuilder.Entity("BobBookstore.Models.Carts.Cart", b =>
                {
                    b.Property<int>("Cart_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("Customer_Id")
                        .HasColumnType("bigint");

                    b.Property<string>("IP")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Cart_Id");

                    b.HasIndex("Customer_Id");

                    b.ToTable("Cart");
                });

            modelBuilder.Entity("BobBookstore.Models.Carts.CartItem", b =>
                {
                    b.Property<int>("CartItem_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("Book_Id")
                        .HasColumnType("bigint");

                    b.Property<int?>("Cart_Id")
                        .HasColumnType("int");

                    b.Property<long?>("Price_Id")
                        .HasColumnType("bigint");

                    b.HasKey("CartItem_Id");

                    b.HasIndex("Book_Id");

                    b.HasIndex("Cart_Id");

                    b.HasIndex("Price_Id");

                    b.ToTable("CartItem");
                });

            modelBuilder.Entity("BobBookstore.Models.Customer.Address", b =>
                {
                    b.Property<long>("Address_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AddressLine1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressLine2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("Customer_Id")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsPrimary")
                        .HasColumnType("bit");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("ZipCode")
                        .HasColumnType("bigint");

                    b.HasKey("Address_Id");

                    b.HasIndex("Customer_Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("BobBookstore.Models.Customer.Customer", b =>
                {
                    b.Property<long>("Customer_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Customer_Id");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("BobBookstore.Models.Order.Order", b =>
                {
                    b.Property<long>("Order_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("Address_Id")
                        .HasColumnType("bigint");

                    b.Property<long?>("Customer_Id")
                        .HasColumnType("bigint");

                    b.Property<string>("DeliveryDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("OrderStatus_Id")
                        .HasColumnType("bigint");

                    b.Property<double>("Subtotal")
                        .HasColumnType("float");

                    b.Property<double>("Tax")
                        .HasColumnType("float");

                    b.HasKey("Order_Id");

                    b.HasIndex("Address_Id");

                    b.HasIndex("Customer_Id");

                    b.HasIndex("OrderStatus_Id");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("BobBookstore.Models.Order.OrderStatus", b =>
                {
                    b.Property<long>("OrderStatus_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrderStatus_Id");

                    b.ToTable("OrderStatus");
                });

            modelBuilder.Entity("BobBookstore.Models.Book.Book", b =>
                {
                    b.HasOne("BobBookstore.Models.Book.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("Genre_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BobBookstore.Models.Book.Genre", null)
                        .WithMany()
                        .HasForeignKey("Genre_Id1");

                    b.HasOne("BobBookstore.Models.Book.Publisher", "Publisher")
                        .WithMany()
                        .HasForeignKey("Publisher_Id");

                    b.HasOne("BobBookstore.Models.Book.Type", "Type")
                        .WithMany()
                        .HasForeignKey("Type_Id");
                });

            modelBuilder.Entity("BobBookstore.Models.Book.Price", b =>
                {
                    b.HasOne("BobBookstore.Models.Book.Book", "Book")
                        .WithMany()
                        .HasForeignKey("Book_Id");

                    b.HasOne("BobBookstore.Models.Book.Condition", "Condition")
                        .WithMany()
                        .HasForeignKey("Condition_Id");
                });

            modelBuilder.Entity("BobBookstore.Models.Carts.Cart", b =>
                {
                    b.HasOne("BobBookstore.Models.Customer.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("Customer_Id");
                });

            modelBuilder.Entity("BobBookstore.Models.Carts.CartItem", b =>
                {
                    b.HasOne("BobBookstore.Models.Book.Book", "Book")
                        .WithMany()
                        .HasForeignKey("Book_Id");

                    b.HasOne("BobBookstore.Models.Carts.Cart", "Cart")
                        .WithMany()
                        .HasForeignKey("Cart_Id");

                    b.HasOne("BobBookstore.Models.Book.Price", "Price")
                        .WithMany()
                        .HasForeignKey("Price_Id");
                });

            modelBuilder.Entity("BobBookstore.Models.Customer.Address", b =>
                {
                    b.HasOne("BobBookstore.Models.Customer.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("Customer_Id");
                });

            modelBuilder.Entity("BobBookstore.Models.Order.Order", b =>
                {
                    b.HasOne("BobBookstore.Models.Customer.Address", "Address")
                        .WithMany()
                        .HasForeignKey("Address_Id");

                    b.HasOne("BobBookstore.Models.Customer.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("Customer_Id");

                    b.HasOne("BobBookstore.Models.Order.OrderStatus", "OrderStatus")
                        .WithMany()
                        .HasForeignKey("OrderStatus_Id");
                });
#pragma warning restore 612, 618
        }
    }
}
