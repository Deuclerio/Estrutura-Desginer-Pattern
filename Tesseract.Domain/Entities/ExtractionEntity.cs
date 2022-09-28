using System;

namespace Tesseract.Domain.Entities
{
    public class ExtractionEntity : BaseEntity
    {
        public string FileName { get; set; }
        public string TextOcr { get; set; }
        public float MeanConfidence { get; set; }
        public string Observacao { get; set; }
        public DateTime DateLastUpdate { get; set; }
        public DateTime DateRegister { get; set; }
    }
}
