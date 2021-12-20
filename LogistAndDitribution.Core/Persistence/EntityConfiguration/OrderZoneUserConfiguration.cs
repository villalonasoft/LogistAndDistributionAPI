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
            builder.HasKey(x => new { x.ZoneId,x.OrderHeaderId,x.CompanyId });
            builder.HasOne(x => x.Zone).WithMany().OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.User).WithMany().OnDelete(DeleteBehavior.NoAction);
        }
    }
}
