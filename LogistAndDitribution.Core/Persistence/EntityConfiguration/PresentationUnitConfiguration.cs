using LogistAndDistribution.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogistAndDitribution.Core.Persistence.EntityConfiguration
{
    public class PresentationUnitConfiguration : IEntityTypeConfiguration<PresentationUnit>
    {
        public void Configure(EntityTypeBuilder<PresentationUnit> builder)
        {
            builder.HasKey(x=>new {x.UnitId,x.PresentationId,x.ProductId,x.CompanyId });
        }
    }
}
