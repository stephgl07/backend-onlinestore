using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tranzact.OnlineStore.Domain.Api.ApiService
{
    public interface IApiService
    {
        Task<string> SendGetRequestAsync(string url);
        Task<string> SendPostRequestAsync(string url, string requestData);
        Task<string> SendPutRequestAsync(string url, string id, string updatedData);
        Task<string> SendDeleteRequestAsync(string url, string id);
    }
}
