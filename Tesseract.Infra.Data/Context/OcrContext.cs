using Microsoft.EntityFrameworkCore;
using Tesseract.Domain.Entities;
using Tesseract.Repository.Mapping;

namespace Tesseract.Repository.Context
{
    public class OcrContext : DbContext
    {
        public OcrContext(DbContextOptions<OcrContext> options) : base(options) { }

        public DbSet<ExtractionEntity> ExtractionEntity { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.HasDefaultSchema("Ocr");
            base.OnModelCreating(model);
            model.Entity<ExtractionEntity>(new ExtractionMap().Configure);

            //ConfigureExtractionEntity(model);
        }

        //private void ConfigureExtractionEntity(ModelBuilder construtorDeModelos)
        //{
        //    construtorDeModelos.Entity<ExtractionEntity>(x =>
        //    {
        //        x.ToTable("Extraction");
        //        x.HasKey(c => c.Id).HasName("Id");
        //        x.Property(c => c.Id).HasColumnName("Id").ValueGeneratedOnAdd();
        //        x.Property(c => c.FileName).HasColumnName("FileName").IsRequired().HasMaxLength(100000);
        //        x.Property(c => c.TextOcr).HasColumnName("TextOcr").IsRequired().HasMaxLength(3000);
        //        x.Property(c => c.MeanConfidence).HasColumnName("MeanConfidence").IsRequired();
        //        x.Property(c => c.DateRegister).HasColumnName("DateRegister").IsRequired();
        //        x.Property(c => c.DateLastUpdate).HasColumnName("DateLastUpdate").IsRequired();
        //    });
        //}
    }
}
