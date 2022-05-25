using CEDIS.Core.Pgsql.Domain;
using CEDIS.Core.Pgsql.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CEDIS.Core.Pgsql.Persistences
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BranchOrder>(x => x.HasKey(y => new { y.WarehouseId, y.BranchId, y.OrderId }));
            modelBuilder.Entity<BranchOrder>(x => x.HasIndex(y => new { y.BranchId, y.Reference }).IsUnique());
            modelBuilder.Entity<BranchOrderDetail>(x => x.HasKey(y => new { y.WarehouseId, y.BranchId, y.OrderId, y.PresentationId, y.ProductId }));

            modelBuilder.Entity<OrderHeader>(x =>
            {
                x.HasKey(y => new { y.WarehouseId, y.BranchId, y.OrderId, y.ZoneId });
                x.Property(x => x.StatusId).IsConcurrencyToken();
            });

            modelBuilder.Entity<OrderHeader>(x => x.Property(x => x.StatusId).IsConcurrencyToken());

            modelBuilder.Entity<OrderDetail>(x => x.HasKey(y => new { y.WarehouseId, y.BranchId, y.OrderId, y.ZoneId, y.PresentationId, y.ProductId }));

            modelBuilder.Entity<Box>(x => x.HasKey(y => new { y.WarehouseId, y.Id }));
            modelBuilder.Entity<BoxOrder>(x => x.HasKey(y => new { y.WarehouseId, y.BranchId, y.OrderId, y.ZoneId, y.BoxId }));
            modelBuilder.Entity<Branch>(x => x.HasKey(y => new { y.Id }));
            modelBuilder.Entity<Mode>(x => x.HasKey(y => new { y.Id }));

            modelBuilder.Entity<NoCoutnHistory>(x => x.HasKey(y => new { y.Id }));

            modelBuilder.Entity<Presentation>(x => x.HasKey(y => new { y.Id, y.ProductId }));

            modelBuilder.Entity<Product>(x => x.HasKey(y => y.Id));

            modelBuilder.Entity<Status>(x => x.HasKey(y => new { y.Id }));
            modelBuilder.Entity<Status>().HasData(
                new Status { Id = StatusEnum.Send, BackgroudColor = "fff2ed", Name = "ENVIADO POR LA TIENDA" },
                new Status { Id = StatusEnum.Managed, BackgroudColor = "ffeaca", Name = "GESTIONADO EN EL CENTRO" },
                new Status { Id = StatusEnum.Start, BackgroudColor = "ffe89a", Name = "INICIADO" },
                new Status { Id = StatusEnum.Asigned, BackgroudColor = "eaec62", Name = "ASIGNADO" },
                new Status { Id = StatusEnum.Picking, BackgroudColor = "b0f332", Name = "PICKING" },
                new Status { Id = StatusEnum.EndPicking, BackgroudColor = "00fa4c", Name = "PICKING FINALIZADO" },
                new Status { Id = StatusEnum.Billing, BackgroudColor = "00fa4c", Name = "FACTURACION" },
                new Status { Id = StatusEnum.OnDispatcht, BackgroudColor = "83c400", Name = "DESPACHADO" },
                new Status { Id = StatusEnum.Dispatched, BackgroudColor = "8f9100", Name = "ENVIADO ELECTRONICAMENTE" },
                new Status { Id = StatusEnum.Received, BackgroudColor = "806300", Name = "ENVIADO POR LA TIENDA" }
                );
            modelBuilder.Entity<StatusDetail>(x => x.HasKey(y => new { y.Id }));
            modelBuilder.Entity<StatusDetail>().HasData(
                new StatusDetail { Id = StatusDetailEnum.Pending, Name = "PENDIENTE" },
                new StatusDetail { Id = StatusDetailEnum.Complete, Name = "COMPLETO" },
                new StatusDetail { Id = StatusDetailEnum.Incomplete, Name = "INCOMPLETO" },
                new StatusDetail { Id = StatusDetailEnum.NoFind, Name = "NO ENCONTRADO" },
                new StatusDetail { Id = StatusDetailEnum.ErrorStock, Name = "ERROR EXISTENCIA" },
                new StatusDetail { Id = StatusDetailEnum.PendingProcess, Name = "PENDIENTE A PROCESAR" }
                );

            modelBuilder.Entity<Zones>(x => x.HasKey(y => new { y.WarehouseId, y.Id }));
            modelBuilder.Entity<UserZone>(x => x.HasKey(y => new { y.UserId, y.Id }));

            modelBuilder.Entity<Warehouse>(x => x.HasKey(y => y.Id));
            modelBuilder.Entity<Warehouse>(x => x.Property(y => y.Id).ValueGeneratedOnAdd());

            modelBuilder.Entity<PresentationWarehouse>(x => x.HasKey(y => new { y.WarehouseId, y.PresentationId, y.ProductId }));
        }

        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<PresentationWarehouse> PresentationWarehouses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<OrderHeader> Orders { get; set; }
        public DbSet<OrderDetail> Details { get; set; }
        public DbSet<BranchOrder> BranchOrder { get; set; }
        public DbSet<BranchOrderDetail> BranchOrderDetail { get; set; }
        public DbSet<Box> Boxes { get; set; }
        public DbSet<BoxOrder> BoxOrders { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Mode> Modes { get; set; }
        public DbSet<NoCoutnHistory> NoCoutns { get; set; }
        public DbSet<Presentation> Presentations { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<StatusDetail> StatusDetails { get; set; }
        public DbSet<Zones> Zones { get; set; }
        public DbSet<Units> Units { get; set; }
        public DbSet<UserZone> UserZones { get; set; }
    }
}
