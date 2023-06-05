using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tranzact.OnlineStore.Domain.Models.Logger;

namespace Tranzact.OnlineStore.Domain.Services.Logger
{
    public interface ILoggerService 
    {
        void InsertLog(string logType, string message);
        Task InsertBlobLogInfo(BlobLogInfo blobInfo);
    }
}
