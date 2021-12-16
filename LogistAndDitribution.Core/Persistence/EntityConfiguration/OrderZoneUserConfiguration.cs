using LogistAndDistribution.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogistAndDitribution.Core.Persistence.EntityConfiguration
{
    public class OrderZoneUserConfiguration : IEntityTypeConfiguration<OrderZoneUser>
    {
        public void Configure(EntityTypeBuilder<OrderZoneUser> builder)
        {
            builder.HasKey(x => new { x.OrderHeaderId, x.ZoneId, x.CompanyId });
            builder.HasOne(x => x.Zone).WithOne().OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.User).WithOne().OnDelete(DeleteBehavior.NoAction);
        }
    }
}
