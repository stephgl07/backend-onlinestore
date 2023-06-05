using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranzact.OnlineStore.Domain.Models.DTOs;
using Tranzact.OnlineStore.Domain.Api.Wrappers;

namespace Tranzact.OnlineStore.Application.Handlers
{
    public class ApiResponseHandler : IApiResponseHandler
    {
        public async Task<IActionResult> HandleResponse<T>(Task<T> responseTask, string successMessage)
        {
            var response = await responseTask;
            var apiResponse = new ApiResponse<T>(response, successMessage);
            return new OkObjectResult(apiResponse);
        }
    }
}
