﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OnlineStore.Api.Models.Data;

#nullable disable

namespace OnlineStore.Api.Migrations
{
    [DbContext(typeof(OnlineStoreContext))]
    [Migration("20240224083525_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("OnlineStore.Api.Models.Category", b =>
                {
                    b.Property<int>("Categoryid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("categoryid");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Categoryid"));

                    b.Property<string>("Nameofcategory")
                        .HasColumnType("text")
                        .HasColumnName("nameofcategory");

                    b.HasKey("Categoryid")
                        .HasName("categories_pkey");

                    b.ToTable("categories", (string)null);
                });

            modelBuilder.Entity("OnlineStore.Api.Models.Customer", b =>
                {
                    b.Property<int>("Customerid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("customerid");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Customerid"));

                    b.Property<string>("Firdname")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("firdname");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("firstname");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("lastname");

                    b.Property<string>("Phone")
                        .HasMaxLength(24)
                        .HasColumnType("character varying(24)")
                        .HasColumnName("phone");

                    b.HasKey("Customerid")
                        .HasName("customers_pkey");

                    b.ToTable("customers", (string)null);
                });

            modelBuilder.Entity("OnlineStore.Api.Models.Order", b =>
                {
                    b.Property<int>("Orderid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("orderid");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Orderid"));

                    b.Property<int>("Customerid")
                        .HasColumnType("integer")
                        .HasColumnName("customerid");

                    b.Property<DateOnly?>("Orderdate")
                        .HasColumnType("date")
                        .HasColumnName("orderdate");

                    b.HasKey("Orderid")
                        .HasName("orders_pkey");

                    b.HasIndex("Customerid");

                    b.ToTable("orders", (string)null);
                });

            modelBuilder.Entity("OnlineStore.Api.Models.Orderposition", b =>
                {
                    b.Property<int>("Orderpositionsid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("orderpositionsid");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Orderpositionsid"));

                    b.Property<int>("Orderid")
                        .HasColumnType("integer")
                        .HasColumnName("orderid");

                    b.Property<int>("Productid")
                        .HasColumnType("integer")
                        .HasColumnName("productid");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer")
                        .HasColumnName("quantity");

                    b.Property<decimal>("Unitprice")
                        .HasColumnType("money")
                        .HasColumnName("unitprice");

                    b.HasKey("Orderpositionsid")
                        .HasName("orderpositions_pkey");

                    b.HasIndex("Orderid");

                    b.HasIndex("Productid");

                    b.ToTable("orderpositions", (string)null);
                });

            modelBuilder.Entity("OnlineStore.Api.Models.Product", b =>
                {
                    b.Property<int>("Productid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("productid");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Productid"));

                    b.Property<int>("Categoryid")
                        .HasColumnType("integer")
                        .HasColumnName("categoryid");

                    b.Property<string>("Productname")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("productname");

                    b.Property<decimal?>("Unitprice")
                        .HasColumnType("money")
                        .HasColumnName("unitprice");

                    b.Property<int?>("Unitsinstock")
                        .HasColumnType("integer")
                        .HasColumnName("unitsinstock");

                    b.HasKey("Productid")
                        .HasName("products_pkey");

                    b.HasIndex("Categoryid");

                    b.ToTable("products", (string)null);
                });

            modelBuilder.Entity("OnlineStore.Api.Models.Order", b =>
                {
                    b.HasOne("OnlineStore.Api.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("Customerid")
                        .IsRequired()
                        .HasConstraintName("orders_customerid_fkey");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("OnlineStore.Api.Models.Orderposition", b =>
                {
                    b.HasOne("OnlineStore.Api.Models.Order", "Order")
                        .WithMany("Orderpositions")
                        .HasForeignKey("Orderid")
                        .IsRequired()
                        .HasConstraintName("orderpositions_orderid_fkey");

                    b.HasOne("OnlineStore.Api.Models.Product", "Product")
                        .WithMany("Orderpositions")
                        .HasForeignKey("Productid")
                        .IsRequired()
                        .HasConstraintName("orderpositions_productid_fkey");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("OnlineStore.Api.Models.Product", b =>
                {
                    b.HasOne("OnlineStore.Api.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("Categoryid")
                        .IsRequired()
                        .HasConstraintName("products_categoryid_fkey");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("OnlineStore.Api.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("OnlineStore.Api.Models.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("OnlineStore.Api.Models.Order", b =>
                {
                    b.Navigation("Orderpositions");
                });

            modelBuilder.Entity("OnlineStore.Api.Models.Product", b =>
                {
                    b.Navigation("Orderpositions");
                });
#pragma warning restore 612, 618
        }
    }
}
