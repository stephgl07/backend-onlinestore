using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranzact.OnlineStore.Domain.Api;

namespace Tranzact.OnlineStore.Infrastructure.Api
{
    public class HttpClientWrapper : HttpClient, IHttpClientWrapper
    {
        public HttpClientWrapper()
        {
        }
    }
}
