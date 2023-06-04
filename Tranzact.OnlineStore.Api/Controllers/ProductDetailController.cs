using Microsoft.AspNetCore.Mvc;
using Tranzact.OnlineStore.Application.Handlers;
using Tranzact.OnlineStore.Domain.Services.ProductDetails;

namespace Tranzact.OnlineStore.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductDetailController : Controller
    {
        private readonly IProductDetailService _productDetailService;
        private readonly IApiResponseHandler _apiResponseHandler;

        public ProductDetailController(IProductDetailService productDetailService, IApiResponseHandler apiResponseHandler)
        {
            _productDetailService = productDetailService;
            _apiResponseHandler = apiResponseHandler;
        }

        [HttpGet]
        [Route("GetDetailById")]
        public async Task<IActionResult> GetDetailById([FromQuery] int DetailId)
        {
            var asdas = await _apiResponseHandler.HandleResponse(_productDetailService.GetAllById(DetailId), "Detalle de producto obtenido exitosamente");
            return asdas;
        }

        [HttpGet]
        [Route("GetDetailByProductId")]
        public async Task<IActionResult> GetDetailByProductId([FromQuery] int ProductId)
        {
            var asdas = await _apiResponseHandler.HandleResponse(_productDetailService.GetAllByProductId(ProductId), "Detalle producto obtenida exitosamente");
            return asdas;
        }

    }
}
