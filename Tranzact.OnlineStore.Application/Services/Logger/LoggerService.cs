using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranzact.OnlineStore.Domain.FileStorage;
using Tranzact.OnlineStore.Domain.Models.Logger;
using Tranzact.OnlineStore.Domain.Services.Logger;

namespace Tranzact.OnlineStore.Application.Services.Logger
{
    public class LoggerService : ILoggerService
    {
        private readonly ILogger<LoggerService> _logger;
        private readonly IFileStorage _fileStorage;
        public LoggerService(ILogger<LoggerService> logger, IFileStorage fileStorage)
        {
            _logger = logger;
            _fileStorage = fileStorage;
        }
        public void InsertLog(string logType, string message)
        {
            switch (logType)
            {
                case "Error":
                    _logger.LogError(message);
                    break;
                case "Information":
                    _logger.LogInformation(message);
                    break;
                // Agrega más casos según los tipos de log que necesites
                default:
                    _logger.LogInformation(message);
                    break;
            }
        }

        public async Task InsertBlobLogInfo(BlobLogInfo blobInfo)
        {
            var strContent = JsonConvert.SerializeObject(blobInfo);
            var fileName = $"log-{DateTime.Now:yyyyMMdd-HHmmssfff}";
            string urlFile = await _fileStorage.SaveTextPlain(strContent, fileName, "timeresponse-api-onlinestore");
            _logger.LogInformation($"TimeResponseLog Url: {urlFile}");
        }
    }
}
