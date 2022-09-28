using ExpertPdf;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract.Domain.Entities;
using Tesseract.Domain.Interfaces;
using Tesseract.Service.Validators;

namespace Tesseract.Service.Services
{
    public class TesseractService : BaseService<ExtractionEntity>, ITesseractService
    {
        private readonly ILogger<TesseractService> _logger;
        private readonly IBaseService<ExtractionEntity> _baseService;
        private readonly IExtractionRepository _extractionRepository;

        public TesseractService(ILogger<TesseractService> logger,
                                IBaseService<ExtractionEntity> baseService,
                                IExtractionRepository extractionRepository) : base(extractionRepository)
        {
            _logger = logger;
            _baseService = baseService;
            _extractionRepository = extractionRepository;
        }

        public async Task<string> UploadedFile(IFormFile file)
        {
            try
            {
                string uniqueFileName = null;

                if (file != null && file.Length > 0)
                {

                    var pastaRaiz = Path.Combine("wwwroot", "Uploads");
                    if (!Directory.Exists(pastaRaiz))
                    {
                        Directory.CreateDirectory(pastaRaiz);
                    }

                    uniqueFileName = Guid.NewGuid().ToString();
                    string filePath = Path.Combine(pastaRaiz, uniqueFileName);
                    _logger.LogInformation($"Processing file: {file.FileName} renamed to: {uniqueFileName}");

                    if (!Directory.Exists(pastaRaiz))
                        Directory.CreateDirectory(pastaRaiz);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    var (textOcr, meanConfidence) = DecodeFile(filePath);

                    await _baseService.InsertAsync<ExtractionValidator>(new ExtractionEntity()
                    {
                        FileName = file.FileName,
                        TextOcr = textOcr,
                        MeanConfidence = meanConfidence,
                        DateRegister = DateTime.Now,
                        DateLastUpdate = DateTime.Now
                    });

                    return textOcr;
                }
                else
                {
                    throw new Exception("Uploading an image is mandatory!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error UploadedFile: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<ExtractionEntity>> GetExtractionsBetweenDateRegisterAsync(DateTime beginDate, DateTime endDate)
        {
            return await _extractionRepository.GetBetweenDatesRegister(beginDate, endDate.AddDays(1).AddSeconds(-1));
        }

        public async Task<IEnumerable<ExtractionEntity>> GetExtractionsBetweenDateRegisterAsync(float meanConfidence)
        {
            return await _extractionRepository.GetMeanConfidenceGreaterEqual(meanConfidence);
        }

        public async Task<IEnumerable<ExtractionEntity>> GetMeanConfidenceGreaterEqualAsync(float meanConfidence)
        {
            return await _extractionRepository.GetMeanConfidenceGreaterEqual(meanConfidence);
        }

        private (string, float) DecodeFile(string filePath)
        {
            _logger.LogInformation("DecodeFile Begin");
            _logger.LogInformation($"filePath: {filePath}");

            try
            {
                var (returnValue, meanConfidence) = GetDocReaderWithOcr(filePath, false);

                _logger.LogInformation("DecodeFile End");
                return (returnValue, meanConfidence);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error DecodeFile: {ex.Message}");
                throw;
            }
        }

        private (string, float) GetDocReaderWithOcr(string filePath, bool pdfToImage)
        {
            _logger.LogInformation("GetDocReaderWithOcr Begin");
            string pathFolderImg = string.Empty;
            string pathTemp = Path.Combine("wwwroot", "Uploads");
            float meanConfidence = float.MinValue;

            try
            {
                string getTextOcr = string.Empty;
                var initVars = new Dictionary<string, object>() {
            { "load_system_dawg", false },
            { "user_words_suffix", "por.user-words" },
            { "language_model_penalty_non_freq_dict_word", 1 },
            { "language_model_penalty_non_dict_word", 1 }};

                var pathLangTessdata = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Tessdata");
                using (var engine = new TesseractEngine(pathLangTessdata, "por", EngineMode.Default, Enumerable.Empty<string>(), initVars, false))
                {
                    string[] filesImg;

                    #region CONVERTER PDF TO IMAGE
                    if (pdfToImage)
                    {
                        var pdfToImageConverter = new PdfToImageConverter();

                        var guidFolder = Guid.NewGuid().ToString();

                        pathFolderImg = Path.Combine(pathTemp, guidFolder);

                        Directory.CreateDirectory(pathFolderImg);

                        pdfToImageConverter.ImagesPath = pathFolderImg;
                        //var image = pdfToImageConverter.ConvertToImages(filePath);

                        pdfToImageConverter.ConvertToImagesInFolder(filePath);

                        filesImg = Directory.GetFiles(pathFolderImg);
                    }
                    else
                    {
                        filesImg = Directory.GetFiles(pathTemp);
                    }
                    #endregion CONVERTER PDF TO IMAGE

                    foreach (var item in filesImg)
                    {
                        using (var img = Pix.LoadFromFile(item))
                        {
                            using (var page = engine.Process(img))
                            {
                                getTextOcr += page.GetText();
                                meanConfidence = page.GetMeanConfidence() * 100; //Percentagem de acertos
                            }
                        }
                    }
                }

                _logger.LogInformation("GetDocReaderWithOcr End");
                return (getTextOcr.Replace("\n", " ").Replace("\r", string.Empty), meanConfidence);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error GetDocReaderWithOcr: {ex.Message}");
                throw;
            }
            finally
            {
                if (!string.IsNullOrEmpty(pathFolderImg))
                    Directory.Delete(pathFolderImg, true);
                else
                    Directory.Delete(pathTemp, true);
            }
        }

        private int ExecuteTesseractProcess(string args)
        {
            _logger.LogInformation($"args: {args}");
            //método para testar o uso do tesseract via kestrel:
            //var teste = GetDocReaderWithOcr(args);

            var tesseractCreateInfo = new System.Diagnostics.ProcessStartInfo("tesseract", args)
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            };
            var tesseractProcess = Process.Start(tesseractCreateInfo);

            string output = tesseractProcess.StandardOutput.ReadToEnd();
            string error = tesseractProcess.StandardError.ReadToEnd();

            tesseractProcess.WaitForExit(1000 * 30);

            if (tesseractProcess.ExitCode != 0)
            {
                _logger.LogError($"Executed Process: {tesseractCreateInfo.FileName}");
                _logger.LogError($"Args: {tesseractCreateInfo.Arguments}");
                _logger.LogError($"Output: {output}");
                _logger.LogError($"Error: {error}");

                throw new InvalidOperationException($"Error on execute {tesseractCreateInfo.FileName} with args '{tesseractCreateInfo.Arguments}', exit code {tesseractProcess.ExitCode}");
            }
            else
            {
                _logger.LogInformation($"Executed Process: {tesseractCreateInfo.FileName}");
                _logger.LogInformation($"Args: {tesseractCreateInfo.Arguments}");
                _logger.LogInformation($"Output: {output}");
                _logger.LogInformation($"Error: {error}");
            }

            return tesseractProcess.ExitCode;
        }
    }
}
