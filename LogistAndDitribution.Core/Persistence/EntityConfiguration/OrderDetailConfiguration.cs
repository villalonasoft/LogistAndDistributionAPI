using LogistAndDistribution.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogistAndDitribution.Core.Persistence.EntityConfiguration
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(x=>new {x.ZoneId, x.UnitId,x.PresentationId,x.ProductId, x.OrderHeaderId,x.CompanyId });
            builder.HasOne(x => x.Stock).WithMany().OnDelete(DeleteBehavior.NoAction);
        }
    }
}
