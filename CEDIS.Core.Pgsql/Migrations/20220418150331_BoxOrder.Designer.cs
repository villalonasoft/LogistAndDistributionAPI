﻿// <auto-generated />
using System;
using CEDIS.Core.Pgsql.Persistences;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CEDIS.Core.Pgsql.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220418150331_BoxOrder")]
    partial class BoxOrder
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.23")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("CEDIS.Core.Pgsql.Domain.Box", b =>
                {
                    b.Property<int>("WarehouseId")
                        .HasColumnName("warehouseid")
                        .HasColumnType("integer");

                    b.Property<int>("Id")
                        .HasColumnName("id")
                        .HasColumnType("integer");

                    b.HasKey("WarehouseId", "Id")
                        .HasName("pk_boxes");

                    b.ToTable("boxes");
                });

            modelBuilder.Entity("CEDIS.Core.Pgsql.Domain.BoxOrder", b =>
                {
                    b.Property<int>("WarehouseId")
                        .HasColumnName("warehouseid")
                        .HasColumnType("integer");

                    b.Property<int>("BranchId")
                        .HasColumnName("branchid")
                        .HasColumnType("integer");

                    b.Property<int>("OrderId")
                        .HasColumnName("orderid")
                        .HasColumnType("integer");

                    b.Property<int>("ZoneId")
                        .HasColumnName("zoneid")
                        .HasColumnType("integer");

                    b.Property<int>("BoxId")
                        .HasColumnName("boxid")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Date")
                        .HasColumnName("date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("StatusId")
                        .HasColumnName("statusid")
                        .HasColumnType("integer");

                    b.HasKey("WarehouseId", "BranchId", "OrderId", "ZoneId", "BoxId")
                        .HasName("pk_boxorders");

                    b.HasIndex("WarehouseId", "BoxId")
                        .HasName("ix_boxorders_warehouseid_boxid");

                    b.ToTable("boxorders");
                });

            modelBuilder.Entity("CEDIS.Core.Pgsql.Domain.Branch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ApiKey")
                        .HasColumnName("apikey")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("pk_branches");

                    b.ToTable("branches");
                });

            modelBuilder.Entity("CEDIS.Core.Pgsql.Domain.BranchOrder", b =>
                {
                    b.Property<int>("WarehouseId")
                        .HasColumnName("warehouseid")
                        .HasColumnType("integer");

                    b.Property<int>("BranchId")
                        .HasColumnName("branchid")
                        .HasColumnType("integer");

                    b.Property<int>("OrderId")
                        .HasColumnName("orderid")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Date")
                        .HasColumnName("date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Mode")
                        .HasColumnName("mode")
                        .HasColumnType("text");

                    b.Property<string>("Reference")
                        .HasColumnName("reference")
                        .HasColumnType("text");

                    b.Property<int>("StatusId")
                        .HasColumnName("statusid")
                        .HasColumnType("integer");

                    b.HasKey("WarehouseId", "BranchId", "OrderId")
                        .HasName("pk_branchorder");

                    b.HasIndex("StatusId")
                        .HasName("ix_branchorder_statusid");

                    b.HasIndex("BranchId", "Reference")
                        .IsUnique()
                        .HasName("ix_branchorder_branchid_reference");

                    b.ToTable("branchorder");
                });

            modelBuilder.Entity("CEDIS.Core.Pgsql.Domain.BranchOrderDetail", b =>
                {
                    b.Property<int>("WarehouseId")
                        .HasColumnName("warehouseid")
                        .HasColumnType("integer");

                    b.Property<int>("BranchId")
                        .HasColumnName("branchid")
                        .HasColumnType("integer");

                    b.Property<int>("OrderId")
                        .HasColumnName("orderid")
                        .HasColumnType("integer");

                    b.Property<int>("PresentationId")
                        .HasColumnName("presentationid")
                        .HasColumnType("integer");

                    b.Property<int>("ProductId")
                        .HasColumnName("productid")
                        .HasColumnType("integer");

                    b.Property<decimal>("Cost")
                        .HasColumnName("cost")
                        .HasColumnType("numeric");

                    b.Property<int>("Factor")
                        .HasColumnName("factor")
                        .HasColumnType("integer");

                    b.Property<int>("OrderedQuantity")
                        .HasColumnName("orderedquantity")
                        .HasColumnType("integer");

                    b.Property<int>("UnitId")
                        .HasColumnName("unitid")
                        .HasColumnType("integer");

                    b.HasKey("WarehouseId", "BranchId", "OrderId", "PresentationId", "ProductId")
                        .HasName("pk_branchorderdetail");

                    b.HasIndex("UnitId")
                        .HasName("ix_branchorderdetail_unitid");

                    b.HasIndex("WarehouseId", "PresentationId", "ProductId")
                        .HasName("ix_branchorderdetail_warehouseid_presentationid_productid");

                    b.ToTable("branchorderdetail");
                });

            modelBuilder.Entity("CEDIS.Core.Pgsql.Domain.Mode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<char>("Abrebiature")
                        .HasColumnName("abrebiature")
                        .HasColumnType("character(1)");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("pk_modes");

                    b.ToTable("modes");
                });

            modelBuilder.Entity("CEDIS.Core.Pgsql.Domain.NoCoutnHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Cant")
                        .HasColumnName("cant")
                        .HasColumnType("integer");

                    b.Property<string>("Code")
                        .HasColumnName("code")
                        .HasColumnType("character varying(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Cost")
                        .HasColumnName("cost")
                        .HasColumnType("text");

                    b.Property<int>("CountCant")
                        .HasColumnName("countcant")
                        .HasColumnType("integer");

                    b.Property<string>("Date")
                        .HasColumnName("date")
                        .HasColumnType("text");

                    b.Property<string>("ProductName")
                        .HasColumnName("productname")
                        .HasColumnType("text");

                    b.Property<int>("Refpedido")
                        .HasColumnName("refpedido")
                        .HasColumnType("integer");

                    b.Property<string>("Status")
                        .HasColumnName("status")
                        .HasColumnType("text");

                    b.Property<int>("Stock")
                        .HasColumnName("stock")
                        .HasColumnType("integer");

                    b.Property<string>("User")
                        .HasColumnName("user")
                        .HasColumnType("character varying(25)")
                        .HasMaxLength(25);

                    b.HasKey("Id")
                        .HasName("pk_nocoutns");

                    b.ToTable("nocoutns");
                });

            modelBuilder.Entity("CEDIS.Core.Pgsql.Domain.OrderDetail", b =>
                {
                    b.Property<int>("WarehouseId")
                        .HasColumnName("warehouseid")
                        .HasColumnType("integer");

                    b.Property<int>("BranchId")
                        .HasColumnName("branchid")
                        .HasColumnType("integer");

                    b.Property<int>("OrderId")
                        .HasColumnName("orderid")
                        .HasColumnType("integer");

                    b.Property<int>("ZoneId")
                        .HasColumnName("zoneid")
                        .HasColumnType("integer");

                    b.Property<int>("PresentationId")
                        .HasColumnName("presentationid")
                        .HasColumnType("integer");

                    b.Property<int>("ProductId")
                        .HasColumnName("productid")
                        .HasColumnType("integer");

                    b.Property<decimal>("Cost")
                        .HasColumnName("cost")
                        .HasColumnType("numeric");

                    b.Property<int>("CountedQuantity")
                        .HasColumnName("countedquantity")
                        .HasColumnType("integer");

                    b.Property<int>("QuantityAvailable")
                        .HasColumnName("quantityavailable")
                        .HasColumnType("integer");

                    b.Property<int>("StatusId")
                        .HasColumnName("statusid")
                        .HasColumnType("integer");

                    b.Property<int>("UnitId")
                        .HasColumnName("unitid")
                        .HasColumnType("integer");

                    b.HasKey("WarehouseId", "BranchId", "OrderId", "ZoneId", "PresentationId", "ProductId")
                        .HasName("pk_details");

                    b.HasIndex("StatusId")
                        .HasName("ix_details_statusid");

                    b.HasIndex("UnitId")
                        .HasName("ix_details_unitid");

                    b.HasIndex("WarehouseId", "PresentationId", "ProductId")
                        .HasName("ix_details_warehouseid_presentationid_productid");

                    b.ToTable("details");
                });

            modelBuilder.Entity("CEDIS.Core.Pgsql.Domain.OrderHeader", b =>
                {
                    b.Property<int>("WarehouseId")
                        .HasColumnName("warehouseid")
                        .HasColumnType("integer");

                    b.Property<int>("BranchId")
                        .HasColumnName("branchid")
                        .HasColumnType("integer");

                    b.Property<int>("OrderId")
                        .HasColumnName("orderid")
                        .HasColumnType("integer");

                    b.Property<int>("ZoneId")
                        .HasColumnName("zoneid")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateEnd")
                        .HasColumnName("dateend")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DateInit")
                        .HasColumnName("dateinit")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("ModeId")
                        .HasColumnName("modeid")
                        .HasColumnType("integer");

                    b.Property<int>("StatusId")
                        .IsConcurrencyToken()
                        .HasColumnName("statusid")
                        .HasColumnType("integer");

                    b.Property<int?>("UserId")
                        .HasColumnName("userid")
                        .HasColumnType("integer");

                    b.HasKey("WarehouseId", "BranchId", "OrderId", "ZoneId")
                        .HasName("pk_orders");

                    b.HasIndex("BranchId")
                        .HasName("ix_orders_branchid");

                    b.HasIndex("ModeId")
                        .HasName("ix_orders_modeid");

                    b.HasIndex("StatusId")
                        .HasName("ix_orders_statusid");

                    b.HasIndex("UserId")
                        .HasName("ix_orders_userid");

                    b.HasIndex("WarehouseId", "ZoneId")
                        .HasName("ix_orders_warehouseid_zoneid");

                    b.ToTable("orders");
                });

            modelBuilder.Entity("CEDIS.Core.Pgsql.Domain.Presentation", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("id")
                        .HasColumnType("integer");

                    b.Property<int>("ProductId")
                        .HasColumnName("productid")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.HasKey("Id", "ProductId")
                        .HasName("pk_presentations");

                    b.HasIndex("ProductId")
                        .HasName("ix_presentations_productid");

                    b.ToTable("presentations");
                });

            modelBuilder.Entity("CEDIS.Core.Pgsql.Domain.PresentationWarehouse", b =>
                {
                    b.Property<int>("WarehouseId")
                        .HasColumnName("warehouseid")
                        .HasColumnType("integer");

                    b.Property<int>("PresentationId")
                        .HasColumnName("presentationid")
                        .HasColumnType("integer");

                    b.Property<int>("ProductId")
                        .HasColumnName("productid")
                        .HasColumnType("integer");

                    b.Property<char>("Bandeja")
                        .HasColumnName("bandeja")
                        .HasColumnType("character(1)");

                    b.Property<int>("Pasillo")
                        .HasColumnName("pasillo")
                        .HasColumnType("integer");

                    b.Property<int>("Tramo")
                        .HasColumnName("tramo")
                        .HasColumnType("integer");

                    b.Property<int>("Ubitramo")
                        .HasColumnName("ubitramo")
                        .HasColumnType("integer");

                    b.Property<int>("ZoneId")
                        .HasColumnName("zoneid")
                        .HasColumnType("integer");

                    b.HasKey("WarehouseId", "PresentationId", "ProductId")
                        .HasName("pk_presentationwarehouses");

                    b.HasIndex("PresentationId", "ProductId")
                        .HasName("ix_presentationwarehouses_presentationid_productid");

                    b.HasIndex("WarehouseId", "ZoneId")
                        .HasName("ix_presentationwarehouses_warehouseid_zoneid");

                    b.ToTable("presentationwarehouses");
                });

            modelBuilder.Entity("CEDIS.Core.Pgsql.Domain.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("pk_products");

                    b.ToTable("products");
                });

            modelBuilder.Entity("CEDIS.Core.Pgsql.Domain.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("id")
                        .HasColumnType("integer");

                    b.Property<bool>("Friday")
                        .HasColumnName("friday")
                        .HasColumnType("boolean");

                    b.Property<bool>("Monday")
                        .HasColumnName("monday")
                        .HasColumnType("boolean");

                    b.Property<bool>("Saturday")
                        .HasColumnName("saturday")
                        .HasColumnType("boolean");

                    b.Property<bool>("Sunday")
                        .HasColumnName("sunday")
                        .HasColumnType("boolean");

                    b.Property<bool>("Thursday")
                        .HasColumnName("thursday")
                        .HasColumnType("boolean");

                    b.Property<bool>("Tuesday")
                        .HasColumnName("tuesday")
                        .HasColumnType("boolean");

                    b.Property<bool>("Wednesday")
                        .HasColumnName("wednesday")
                        .HasColumnType("boolean");

                    b.HasKey("Id")
                        .HasName("pk_schedules");

                    b.ToTable("schedules");
                });

            modelBuilder.Entity("CEDIS.Core.Pgsql.Domain.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("BackgroudColor")
                        .HasColumnName("backgroudcolor")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("pk_statuses");

                    b.ToTable("statuses");
                });

            modelBuilder.Entity("CEDIS.Core.Pgsql.Domain.StatusDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("pk_statusdetails");

                    b.ToTable("statusdetails");
                });

            modelBuilder.Entity("CEDIS.Core.Pgsql.Domain.Units", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("pk_units");

                    b.ToTable("units");
                });

            modelBuilder.Entity("CEDIS.Core.Pgsql.Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnName("password")
                        .HasColumnType("text");

                    b.Property<string>("Salt")
                        .HasColumnName("salt")
                        .HasColumnType("text");

                    b.Property<bool>("Status")
                        .HasColumnName("status")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasColumnName("username")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.ToTable("users");
                });

            modelBuilder.Entity("CEDIS.Core.Pgsql.Domain.UserZone", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnName("userid")
                        .HasColumnType("integer");

                    b.Property<int>("Id")
                        .HasColumnName("id")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Date")
                        .HasColumnName("date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnName("isactive")
                        .HasColumnType("boolean");

                    b.Property<int>("WarehouseId")
                        .HasColumnName("warehouseid")
                        .HasColumnType("integer");

                    b.Property<int>("ZoneId")
                        .HasColumnName("zoneid")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "Id")
                        .HasName("pk_userzones");

                    b.HasIndex("WarehouseId", "ZoneId")
                        .HasName("ix_userzones_warehouseid_zoneid");

                    b.ToTable("userzones");
                });

            modelBuilder.Entity("CEDIS.Core.Pgsql.Domain.Warehouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("pk_warehouses");

                    b.ToTable("warehouses");
                });

            modelBuilder.Entity("CEDIS.Core.Pgsql.Domain.Zones", b =>
                {
                    b.Property<int>("WarehouseId")
                        .HasColumnName("warehouseid")
                        .HasColumnType("integer");

                    b.Property<int>("Id")
                        .HasColumnName("id")
                        .HasColumnType("integer");

                    b.Property<int>("FinPasillo")
                        .HasColumnName("finpasillo")
                        .HasColumnType("integer");

                    b.Property<int>("FinTramo")
                        .HasColumnName("fintramo")
                        .HasColumnType("integer");

                    b.Property<int>("InitPasillo")
                        .HasColumnName("initpasillo")
                        .HasColumnType("integer");

                    b.Property<int>("InitTramo")
                        .HasColumnName("inittramo")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.HasKey("WarehouseId", "Id")
                        .HasName("pk_zones");

                    b.ToTable("zones");
                });

            modelBuilder.Entity("CEDIS.Core.Pgsql.Domain.Box", b =>
                {
                    b.HasOne("CEDIS.Core.Pgsql.Domain.Warehouse", "Warehouse")
                        .WithMany()
                        .HasForeignKey("WarehouseId")
                        .HasConstraintName("fk_boxes_warehouses_warehouseid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CEDIS.Core.Pgsql.Domain.BoxOrder", b =>
                {
                    b.HasOne("CEDIS.Core.Pgsql.Domain.Box", "Box")
                        .WithMany()
                        .HasForeignKey("WarehouseId", "BoxId")
                        .HasConstraintName("fk_boxorders_boxes_warehouseid_boxid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CEDIS.Core.Pgsql.Domain.OrderHeader", "OrderHeader")
                        .WithMany()
                        .HasForeignKey("WarehouseId", "BranchId", "OrderId", "ZoneId")
                        .HasConstraintName("fk_boxorders_orders_warehouseid_branchid_orderid_zoneid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CEDIS.Core.Pgsql.Domain.BranchOrder", b =>
                {
                    b.HasOne("CEDIS.Core.Pgsql.Domain.Branch", "Branch")
                        .WithMany()
                        .HasForeignKey("BranchId")
                        .HasConstraintName("fk_branchorder_branches_branchid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CEDIS.Core.Pgsql.Domain.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .HasConstraintName("fk_branchorder_statuses_statusid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CEDIS.Core.Pgsql.Domain.Warehouse", "Warehouse")
                        .WithMany()
                        .HasForeignKey("WarehouseId")
                        .HasConstraintName("fk_branchorder_warehouses_warehouseid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CEDIS.Core.Pgsql.Domain.BranchOrderDetail", b =>
                {
                    b.HasOne("CEDIS.Core.Pgsql.Domain.Units", "Unit")
                        .WithMany()
                        .HasForeignKey("UnitId")
                        .HasConstraintName("fk_branchorderdetail_units_unitid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CEDIS.Core.Pgsql.Domain.BranchOrder", "BranchOrder")
                        .WithMany("BranchOrderDetails")
                        .HasForeignKey("WarehouseId", "BranchId", "OrderId")
                        .HasConstraintName("fk_branchorderdetail_branchorder_warehouseid_branchid_orderid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CEDIS.Core.Pgsql.Domain.PresentationWarehouse", "PresentationWarehouse")
                        .WithMany()
                        .HasForeignKey("WarehouseId", "PresentationId", "ProductId")
                        .HasConstraintName("fk_branchorderdetail_presentationwarehouses_warehouseid_presen~")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CEDIS.Core.Pgsql.Domain.OrderDetail", b =>
                {
                    b.HasOne("CEDIS.Core.Pgsql.Domain.StatusDetail", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .HasConstraintName("fk_details_statusdetails_statusid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CEDIS.Core.Pgsql.Domain.Units", "Units")
                        .WithMany()
                        .HasForeignKey("UnitId")
                        .HasConstraintName("fk_details_units_unitid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CEDIS.Core.Pgsql.Domain.PresentationWarehouse", "PresentationWarehouse")
                        .WithMany()
                        .HasForeignKey("WarehouseId", "PresentationId", "ProductId")
                        .HasConstraintName("fk_details_presentationwarehouses_warehouseid_presentationid_p~")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CEDIS.Core.Pgsql.Domain.OrderHeader", "OrderHeader")
                        .WithMany("OrderDetails")
                        .HasForeignKey("WarehouseId", "BranchId", "OrderId", "ZoneId")
                        .HasConstraintName("fk_details_orders_warehouseid_branchid_orderid_zoneid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CEDIS.Core.Pgsql.Domain.OrderHeader", b =>
                {
                    b.HasOne("CEDIS.Core.Pgsql.Domain.Branch", "Branch")
                        .WithMany("OrderHeaders")
                        .HasForeignKey("BranchId")
                        .HasConstraintName("fk_orders_branches_branchid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CEDIS.Core.Pgsql.Domain.Mode", "Mode")
                        .WithMany("OrderHeaders")
                        .HasForeignKey("ModeId")
                        .HasConstraintName("fk_orders_modes_modeid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CEDIS.Core.Pgsql.Domain.Status", "Status")
                        .WithMany("OrderHeaders")
                        .HasForeignKey("StatusId")
                        .HasConstraintName("fk_orders_statuses_statusid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CEDIS.Core.Pgsql.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_orders_users_userid");

                    b.HasOne("CEDIS.Core.Pgsql.Domain.Zones", "Zones")
                        .WithMany("OrderHeaders")
                        .HasForeignKey("WarehouseId", "ZoneId")
                        .HasConstraintName("fk_orders_zones_warehouseid_zoneid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CEDIS.Core.Pgsql.Domain.BranchOrder", "BranchOrder")
                        .WithMany("OrderHeaders")
                        .HasForeignKey("WarehouseId", "BranchId", "OrderId")
                        .HasConstraintName("fk_orders_branchorder_warehouseid_branchid_orderid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CEDIS.Core.Pgsql.Domain.Presentation", b =>
                {
                    b.HasOne("CEDIS.Core.Pgsql.Domain.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .HasConstraintName("fk_presentations_products_productid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CEDIS.Core.Pgsql.Domain.PresentationWarehouse", b =>
                {
                    b.HasOne("CEDIS.Core.Pgsql.Domain.Warehouse", "Warehouse")
                        .WithMany()
                        .HasForeignKey("WarehouseId")
                        .HasConstraintName("fk_presentationwarehouses_warehouses_warehouseid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CEDIS.Core.Pgsql.Domain.Presentation", "Presentation")
                        .WithMany("PresentationWarehouses")
                        .HasForeignKey("PresentationId", "ProductId")
                        .HasConstraintName("fk_presentationwarehouses_presentations_presentationid_product~")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CEDIS.Core.Pgsql.Domain.Zones", "Zones")
                        .WithMany()
                        .HasForeignKey("WarehouseId", "ZoneId")
                        .HasConstraintName("fk_presentationwarehouses_zones_warehouseid_zoneid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CEDIS.Core.Pgsql.Domain.Schedule", b =>
                {
                    b.HasOne("CEDIS.Core.Pgsql.Domain.Branch", "Branch")
                        .WithOne("Schedule")
                        .HasForeignKey("CEDIS.Core.Pgsql.Domain.Schedule", "Id")
                        .HasConstraintName("fk_schedules_branches_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CEDIS.Core.Pgsql.Domain.UserZone", b =>
                {
                    b.HasOne("CEDIS.Core.Pgsql.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_userzones_users_userid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CEDIS.Core.Pgsql.Domain.Zones", "Zones")
                        .WithMany()
                        .HasForeignKey("WarehouseId", "ZoneId")
                        .HasConstraintName("fk_userzones_zones_warehouseid_zoneid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CEDIS.Core.Pgsql.Domain.Zones", b =>
                {
                    b.HasOne("CEDIS.Core.Pgsql.Domain.Warehouse", "WareHouse")
                        .WithMany()
                        .HasForeignKey("WarehouseId")
                        .HasConstraintName("fk_zones_warehouses_warehouseid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
