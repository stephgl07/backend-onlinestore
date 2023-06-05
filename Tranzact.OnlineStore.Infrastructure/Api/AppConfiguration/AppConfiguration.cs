using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranzact.OnlineStore.Domain.Api.AppConfiguration;

namespace Tranzact.OnlineStore.Infrastructure.Api.AppConfiguration
{
    public class AppConfiguration : IAppConfiguration
    {
        private readonly IConfiguration _configuration;

        public AppConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetApiPromotionsUrl()
        {
            return _configuration.GetSection("ExternalConnections")["ApiPromotions"];
        }
    }
}
