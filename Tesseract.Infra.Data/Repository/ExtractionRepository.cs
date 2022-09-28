using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tesseract.Repository.Context;
using Tesseract.Domain.Entities;
using Tesseract.Domain.Interfaces;

namespace Tesseract.Repository.Repository
{
    public class ExtractionRepository : BaseRepository<ExtractionEntity>, IExtractionRepository
    {
        private readonly DbSet<ExtractionEntity> _extractionEntity;

        public ExtractionRepository(OcrContext context) : base(context)
        {
            _extractionEntity = context.Set<ExtractionEntity>();
        }

        public async Task<IEnumerable<ExtractionEntity>> GetBetweenDatesRegister(DateTime startDate, DateTime endDate)
        {
            return await _extractionEntity.Where(x => x.DateRegister >= startDate && x.DateRegister <= endDate).ToListAsync();
        }

        public async Task<IEnumerable<ExtractionEntity>> GetMeanConfidenceGreaterEqual(float meanConfidence)
        {
            return await _extractionEntity.Where(x => x.MeanConfidence >= meanConfidence).ToListAsync();
        }
    }
}
