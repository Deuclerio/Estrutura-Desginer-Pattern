using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tesseract.Domain.Entities;

namespace Tesseract.Domain.Interfaces
{
    public interface IExtractionRepository : IBaseRepository<ExtractionEntity>
    {
        Task<IEnumerable<ExtractionEntity>> GetBetweenDatesRegister(DateTime startDate, DateTime endDate);
        Task<IEnumerable<ExtractionEntity>> GetMeanConfidenceGreaterEqual(float meanConfidence);
    }
}