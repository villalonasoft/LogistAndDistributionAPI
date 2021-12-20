﻿// <auto-generated />
using System;
using LogistAndDitribution.Core.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LogistAndDitribution.Core.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20211216202231_IndexZoneUser")]
    partial class IndexZoneUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LogistAndDistribution.Models.Domain.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("LogistAndDistribution.Models.Domain.Customer", b =>
                {
                    b.Property<int>("PersonTypeId")
                        .HasColumnType("int");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<int>("CreditDay")
                        .HasColumnType("int");

                    b.Property<decimal>("CreditLimit")
                        .HasColumnType("decimal(14, 2)");

                    b.Property<string>("LargeName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PersonTypeId", "PersonId", "CompanyId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("PersonId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("LogistAndDistribution.Models.Domain.OrderDetail", b =>
                {
                    b.Property<int>("ZoneId")
                        .HasColumnType("int");

                    b.Property<int>("UnitId")
                        .HasColumnType("int");

                    b.Property<int>("PresentationId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("OrderHeaderId")
                        .HasColumnType("int");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<int>("CuantityOrder")
                        .HasColumnType("int");

                    b.Property<int>("CuantityPicked")
                        .HasColumnType("int");

                    b.HasKey("ZoneId", "UnitId", "PresentationId", "ProductId", "OrderHeaderId", "CompanyId");

                    b.HasIndex("OrderHeaderId", "CompanyId");

                    b.HasIndex("ZoneId", "UnitId", "PresentationId", "ProductId", "CompanyId");

                    b.ToTable("OrderDetail");
                });

            modelBuilder.Entity("LogistAndDistribution.Models.Domain.OrderHeader", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("InitDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Mount")
                        .HasColumnType("decimal(14, 2)");

                    b.Property<int>("OrderTypeId")
                        .HasColumnType("int");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<int>("PersonTypeId")
                        .HasColumnType("int");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<string>("Reference")
                        .HasColumnType("nvarchar(25)")
                        .HasMaxLength(25);

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id", "CompanyId");

                    b.HasIndex("OrderTypeId");

                    b.HasIndex("PersonTypeId", "PersonId", "CompanyId");

                    b.ToTable("OrderHeaders");
                });

            modelBuilder.Entity("LogistAndDistribution.Models.Domain.OrderType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OrderTypes");
                });

            modelBuilder.Entity("LogistAndDistribution.Models.Domain.OrderZoneUser", b =>
                {
                    b.Property<int>("ZoneId")
                        .HasColumnType("int");

                    b.Property<int>("OrderHeaderId")
                        .HasColumnType("int");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<bool>("IsFinish")
                        .HasColumnType("bit");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ZoneId", "OrderHeaderId", "CompanyId");

                    b.HasIndex("OrderHeaderId", "CompanyId");

                    b.HasIndex("UserId", "CompanyId");

                    b.HasIndex("ZoneId", "CompanyId");

                    b.ToTable("OrderZoneUsers");
                });

            modelBuilder.Entity("LogistAndDistribution.Models.Domain.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Document")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Document")
                        .IsUnique()
                        .HasFilter("[Document] IS NOT NULL");

                    b.ToTable("People");
                });

            modelBuilder.Entity("LogistAndDistribution.Models.Domain.PersonType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PersonTypes");
                });

            modelBuilder.Entity("LogistAndDistribution.Models.Domain.Presentation", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id", "ProductId", "CompanyId");

                    b.HasIndex("ProductId", "CompanyId");

                    b.ToTable("Presentations");
                });

            modelBuilder.Entity("LogistAndDistribution.Models.Domain.PresentationUnit", b =>
                {
                    b.Property<int>("UnitId")
                        .HasColumnType("int");

                    b.Property<int>("PresentationId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.HasKey("UnitId", "PresentationId", "ProductId", "CompanyId");

                    b.HasIndex("PresentationId", "ProductId", "CompanyId");

                    b.ToTable("PresentationUnits");
                });

            modelBuilder.Entity("LogistAndDistribution.Models.Domain.Product", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id", "CompanyId");

                    b.HasIndex("CompanyId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("LogistAndDistribution.Models.Domain.Stock", b =>
                {
                    b.Property<int>("ZoneId")
                        .HasColumnType("int");

                    b.Property<int>("UnitId")
                        .HasColumnType("int");

                    b.Property<int>("PresentationId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<decimal>("Cant")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ZoneId", "UnitId", "PresentationId", "ProductId", "CompanyId");

                    b.HasIndex("ZoneId", "CompanyId");

                    b.HasIndex("UnitId", "PresentationId", "ProductId", "CompanyId")
                        .IsUnique();

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("LogistAndDistribution.Models.Domain.Unit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("InventoryFactor")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Units");
                });

            modelBuilder.Entity("LogistAndDistribution.Models.Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id", "CompanyId");

                    b.HasIndex("CompanyId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LogistAndDistribution.Models.Domain.Zone", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HallEnd")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HallInit")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id", "CompanyId");

                    b.HasIndex("CompanyId");

                    b.ToTable("Zones");
                });

            modelBuilder.Entity("LogistAndDistribution.Models.Domain.Customer", b =>
                {
                    b.HasOne("LogistAndDistribution.Models.Domain.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LogistAndDistribution.Models.Domain.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LogistAndDistribution.Models.Domain.PersonType", "PersonType")
                        .WithMany()
                        .HasForeignKey("PersonTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LogistAndDistribution.Models.Domain.OrderDetail", b =>
                {
                    b.HasOne("LogistAndDistribution.Models.Domain.OrderHeader", "OrderHeader")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderHeaderId", "CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LogistAndDistribution.Models.Domain.Stock", "Stock")
                        .WithMany()
                        .HasForeignKey("ZoneId", "UnitId", "PresentationId", "ProductId", "CompanyId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("LogistAndDistribution.Models.Domain.OrderHeader", b =>
                {
                    b.HasOne("LogistAndDistribution.Models.Domain.OrderType", "OrderType")
                        .WithMany()
                        .HasForeignKey("OrderTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LogistAndDistribution.Models.Domain.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("PersonTypeId", "PersonId", "CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LogistAndDistribution.Models.Domain.OrderZoneUser", b =>
                {
                    b.HasOne("LogistAndDistribution.Models.Domain.OrderHeader", "OrderHeader")
                        .WithMany()
                        .HasForeignKey("OrderHeaderId", "CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LogistAndDistribution.Models.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId", "CompanyId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("LogistAndDistribution.Models.Domain.Zone", "Zone")
                        .WithMany()
                        .HasForeignKey("ZoneId", "CompanyId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("LogistAndDistribution.Models.Domain.Presentation", b =>
                {
                    b.HasOne("LogistAndDistribution.Models.Domain.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId", "CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LogistAndDistribution.Models.Domain.PresentationUnit", b =>
                {
                    b.HasOne("LogistAndDistribution.Models.Domain.Unit", "Unit")
                        .WithMany()
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LogistAndDistribution.Models.Domain.Presentation", "Presentation")
                        .WithMany()
                        .HasForeignKey("PresentationId", "ProductId", "CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LogistAndDistribution.Models.Domain.Product", b =>
                {
                    b.HasOne("LogistAndDistribution.Models.Domain.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LogistAndDistribution.Models.Domain.Stock", b =>
                {
                    b.HasOne("LogistAndDistribution.Models.Domain.Zone", "Zone")
                        .WithMany()
                        .HasForeignKey("ZoneId", "CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LogistAndDistribution.Models.Domain.PresentationUnit", "Presentation")
                        .WithOne()
                        .HasForeignKey("LogistAndDistribution.Models.Domain.Stock", "UnitId", "PresentationId", "ProductId", "CompanyId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("LogistAndDistribution.Models.Domain.Unit", b =>
                {
                    b.HasOne("LogistAndDistribution.Models.Domain.Unit", "Child")
                        .WithMany()
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("LogistAndDistribution.Models.Domain.User", b =>
                {
                    b.HasOne("LogistAndDistribution.Models.Domain.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LogistAndDistribution.Models.Domain.Zone", b =>
                {
                    b.HasOne("LogistAndDistribution.Models.Domain.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
