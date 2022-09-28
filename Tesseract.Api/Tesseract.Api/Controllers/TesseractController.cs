using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tesseract.Domain.Entities;
using Tesseract.Domain.Interfaces;
using Tesseract.Service.Validators;

namespace Tesseract.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TesseractController : ControllerBase
    {
        private readonly ITesseractService _tesseractService;

        public TesseractController(ITesseractService tesseractService)
        {
            _tesseractService = tesseractService;
        }

        [HttpGet]
        [Route("get")]
        [SwaggerOperation("Get an extraction")]
        public async Task<IActionResult> GetExtraction(long id)
        {
            try
            {
                return Ok(await _tesseractService.GetAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getList")]
        [SwaggerOperation("Get to extracts list")]
        public async Task<IActionResult> GetListExtraction()
        {
            try
            {
                return Ok(await _tesseractService.GetListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetExtractionsBetweenDateRegister")]
        [SwaggerOperation("Get extractions between two dates")]
        public async Task<IActionResult> GetExtractionsBetweenDateRegister(DateTime beginDate, DateTime endDate)
        {
            try
            {
                return Ok(await _tesseractService.GetExtractionsBetweenDateRegisterAsync(beginDate, endDate));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetMeanConfidenceGreaterEqual")]
        [SwaggerOperation("Get Confidence Average Greater Equal")]
        public async Task<IActionResult> GetMeanConfidenceGreaterEqual(float meanConfidence)
        {
            try
            {
                return Ok(await _tesseractService.GetMeanConfidenceGreaterEqualAsync(meanConfidence));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("ocr-by-upload")]
        [SwaggerOperation("Captures the text from the image, saves the information in the database and returns the text.")]
        public async Task<IActionResult> OcrByUploadAsync(IFormFile file)
        {
            try
            {
                var returnValue = await _tesseractService.UploadedFile(file);
                return Ok(returnValue);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete")]
        [SwaggerOperation("Delete an extract record")]
        public async Task<IActionResult> DeleteExtraction(long id)
        {
            try
            {
                await _tesseractService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("update")]
        [SwaggerOperation("Update an extract record")]
        public async Task<IActionResult> UpdateExtraction([FromBody] ExtractionEntity extractionEntity)
        {
            try
            {
                await _tesseractService.UpdateAsync<ExtractionValidator>(extractionEntity);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
