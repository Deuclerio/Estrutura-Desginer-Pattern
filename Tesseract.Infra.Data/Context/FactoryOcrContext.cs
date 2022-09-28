using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net;

namespace Tesseract.Repository.Context
{
    public class FactoryOcrContext : IDesignTimeDbContextFactory<OcrContext>
    {
        public OcrContext CreateDbContext(string[] args)
        {
            var connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build()["ConnectionString"];

            var optionsBuilder = new DbContextOptionsBuilder<OcrContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new OcrContext(optionsBuilder.Options);
        }

    }
}
