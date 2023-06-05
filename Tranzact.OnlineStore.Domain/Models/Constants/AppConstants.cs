using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tranzact.OnlineStore.Domain.Models.Constants
{

    public static class AppConstants
    {
        private static IConfiguration _configuration;

        public static void SetConfiguration(IConfiguration configuration) => _configuration = configuration;

        public static string ApiPromotionsUrl => _configuration.GetSection("ExternalConnections")["ApiPromotions"];
        public static string ConnectionString => _configuration.GetConnectionString("DB_OnlineStore");
    }
}
