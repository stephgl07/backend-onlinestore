using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tranzact.OnlineStore.Application.Handlers
{
    public interface IApiResponseHandler
    {
        Task<IActionResult> HandleResponse<T>(Task<T> responseTask, string successMessage);
    }
}
