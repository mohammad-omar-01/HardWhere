﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using infrastructure;

#nullable disable

namespace infrastructure.Migrations
{
    [DbContext(typeof(HardwhereDbContext))]
    [Migration("20231219114118_UserHasListOfProducts")]
    partial class UserHasListOfProducts
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Payment.PaymentGateWay", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PaymentGateWays");
                });

            modelBuilder.Entity("Domain.ProductNS.CategoreyImage", b =>
                {
                    b.Property<string>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CategoreyId")
                        .HasColumnType("int");

                    b.Property<string>("altText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("sourceUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("CategoreyId")
                        .IsUnique();

                    b.ToTable("CategoreyImages");
                });

            modelBuilder.Entity("Domain.ProductNS.GalleryImage", b =>
                {
                    b.Property<int>("GalleryImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GalleryImageId"));

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("SourceUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GalleryImageId");

                    b.HasIndex("ProductId");

                    b.ToTable("GalleryImages");
                });

            modelBuilder.Entity("Domain.ProductNS.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<decimal?>("AverageRating")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Height")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Length")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("OnSale")
                        .HasColumnType("bit");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("RawDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RawPrice")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RawRegularPrice")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RawSalePrice")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("RegularPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("ReviewCount")
                        .HasColumnType("int");

                    b.Property<decimal?>("SalePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sku")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int?>("StockQuantity")
                        .HasColumnType("int");

                    b.Property<string>("StockStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<decimal?>("Weight")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Width")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Domain.ProductNS.ProductCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProductCategories");
                });

            modelBuilder.Entity("Domain.ProductNS.ProductImage", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ImageId"));

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("SourceUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ImageId");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.ToTable("ProductImage");
                });

            modelBuilder.Entity("Domain.UserNS.Address", b =>
                {
                    b.Property<int>("AddressID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AddressID"));

                    b.Property<string>("Address1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Country")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Postcode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AddressID");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("Domain.UserNS.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BillingAddressID")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<int?>("ShippingAddressID")
                        .HasColumnType("int");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BillingAddressID");

                    b.HasIndex("ShippingAddressID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ProductProductCategory", b =>
                {
                    b.Property<int>("CategoriesId")
                        .HasColumnType("int");

                    b.Property<int>("ProductsProductId")
                        .HasColumnType("int");

                    b.HasKey("CategoriesId", "ProductsProductId");

                    b.HasIndex("ProductsProductId");

                    b.ToTable("ProductProductCategory");
                });

            modelBuilder.Entity("Domain.ProductNS.CategoreyImage", b =>
                {
                    b.HasOne("Domain.ProductNS.ProductCategory", "category")
                        .WithOne("CategoryImage")
                        .HasForeignKey("Domain.ProductNS.CategoreyImage", "CategoreyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("category");
                });

            modelBuilder.Entity("Domain.ProductNS.GalleryImage", b =>
                {
                    b.HasOne("Domain.ProductNS.Product", "Product")
                        .WithMany("GalleryImages")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Domain.ProductNS.Product", b =>
                {
                    b.HasOne("Domain.UserNS.User", null)
                        .WithMany("Products")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("Domain.ProductNS.ProductImage", b =>
                {
                    b.HasOne("Domain.ProductNS.Product", "Product")
                        .WithOne("ProductImage")
                        .HasForeignKey("Domain.ProductNS.ProductImage", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Domain.UserNS.User", b =>
                {
                    b.HasOne("Domain.UserNS.Address", "Billing")
                        .WithMany()
                        .HasForeignKey("BillingAddressID");

                    b.HasOne("Domain.UserNS.Address", "Shipping")
                        .WithMany()
                        .HasForeignKey("ShippingAddressID");

                    b.Navigation("Billing");

                    b.Navigation("Shipping");
                });

            modelBuilder.Entity("ProductProductCategory", b =>
                {
                    b.HasOne("Domain.ProductNS.ProductCategory", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.ProductNS.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.ProductNS.Product", b =>
                {
                    b.Navigation("GalleryImages");

                    b.Navigation("ProductImage");
                });

            modelBuilder.Entity("Domain.ProductNS.ProductCategory", b =>
                {
                    b.Navigation("CategoryImage")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.UserNS.User", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
