using LogistAndDistribution.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogistAndDitribution.Core.Persistence.EntityConfiguration
{
    public class StockConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.HasKey(x=>new {x.ZoneId,x.UnitId,x.PresentationId,x.ProductId,x.CompanyId });
            builder.HasOne(x => x.Presentation).WithOne().OnDelete(DeleteBehavior.NoAction);
        }
    }
}
