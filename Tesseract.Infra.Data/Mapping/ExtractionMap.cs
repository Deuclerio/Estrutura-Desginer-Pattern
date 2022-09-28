using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Tesseract.Domain.Entities;

namespace Tesseract.Repository.Mapping
{
    public class ExtractionMap : IEntityTypeConfiguration<ExtractionEntity>
    {
        public void Configure(EntityTypeBuilder<ExtractionEntity> builder)
        {

            builder.ToTable("Extraction");

            builder.HasKey(prop => prop.Id).HasName("Id");
            builder.Property(prop => prop.Id)
                   .HasColumnName("Id").ValueGeneratedOnAdd();

            builder.Property(prop => prop.FileName)
                    .HasConversion(prop => prop.ToString(), prop => prop)
                    .IsRequired()
                    .HasColumnName("FileName")
                    .HasColumnType("nvarchar(max)");

            builder.Property(prop => prop.TextOcr)
                    .HasConversion(prop => prop.ToString(), prop => prop)
                    .IsRequired()
                    .HasColumnName("TextOcr")
                    .HasColumnType("nvarchar(3000)");

            builder.Property(prop => prop.MeanConfidence)
                    .HasConversion(prop => prop, prop => prop)
                    .IsRequired()
                    .HasColumnName("MeanConfidence")
                    .HasColumnType("real");

            builder.Property(prop => prop.DateRegister)
                    .HasConversion(prop => prop, prop => prop)
                    .IsRequired()
                    .HasColumnName("DateRegister")
                    .HasColumnType("datetime2(7)");

            builder.Property(prop => prop.DateLastUpdate)
                    .HasConversion(prop => prop, prop => prop)
                    .IsRequired()
                    .HasColumnName("DateLastUpdate")
                    .HasColumnType("datetime2(7)");

            builder.Property(prop => prop.Observacao)
                    .HasConversion(prop => prop.ToString(), prop => prop)
                    .HasColumnName("Observacao")
                    .HasColumnType("varchar(3000)");
        }
    }
}
