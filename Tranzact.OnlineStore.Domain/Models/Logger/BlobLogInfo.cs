using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tranzact.OnlineStore.Domain.Models.Logger
{
    public class BlobLogInfo
    {
        public string Endpoint { get; set; }
        public string ResponseCode { get; set; }
        public DateTime DateRequest { get; set; }
        public string TimeRequest { get; set; }
    }
}
