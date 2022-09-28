using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tesseract.Domain.Entities;

namespace Tesseract.Domain.Interfaces
{
    public interface ITesseractService : IBaseService<ExtractionEntity>
    {
        Task<string> UploadedFile(IFormFile file);
        Task<IEnumerable<ExtractionEntity>> GetExtractionsBetweenDateRegisterAsync(DateTime beginDate, DateTime endDate);
        Task<IEnumerable<ExtractionEntity>> GetMeanConfidenceGreaterEqualAsync(float meanConfidence);
    }
}
