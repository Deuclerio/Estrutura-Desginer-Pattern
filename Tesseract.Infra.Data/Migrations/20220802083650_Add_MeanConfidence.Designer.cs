﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tesseract.Repository.Context;

namespace Tesseract.Repository.Migrations
{
    [DbContext(typeof(OcrContext))]
    [Migration("20220802083650_Add_MeanConfidence")]
    partial class Add_MeanConfidence
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Ocr")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Tesseract.Domain.ExtractionEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateLastUpdate")
                        .HasColumnType("datetime2")
                        .HasColumnName("DateLastUpdate");

                    b.Property<DateTime>("DateRegister")
                        .HasColumnType("datetime2")
                        .HasColumnName("DateRegister");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(100000)
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("FileName");

                    b.Property<float>("MeanConfidence")
                        .HasColumnType("real")
                        .HasColumnName("MeanConfidence");

                    b.Property<string>("TextOcr")
                        .IsRequired()
                        .HasMaxLength(3000)
                        .HasColumnType("nvarchar(3000)")
                        .HasColumnName("TextOcr");

                    b.HasKey("Id")
                        .HasName("Id");

                    b.ToTable("Extraction");
                });
#pragma warning restore 612, 618
        }
    }
}