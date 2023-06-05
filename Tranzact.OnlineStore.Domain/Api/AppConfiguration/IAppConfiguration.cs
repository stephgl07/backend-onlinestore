using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tranzact.OnlineStore.Domain.Api.AppConfiguration
{
    public interface IAppConfiguration
    {
        string GetApiPromotionsUrl();
        string GetBlobStorageConnectionSring();
    }

}
