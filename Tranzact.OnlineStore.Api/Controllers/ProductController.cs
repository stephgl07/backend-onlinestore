using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tranzact.OnlineStore.Application.Handlers;
using Tranzact.OnlineStore.Domain.Models.DTOs;
using Tranzact.OnlineStore.Domain.Services.Product;

namespace Tranzact.OnlineStore.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productsService;
        private readonly IApiResponseHandler _apiResponseHandler;

        public ProductController(IProductService productsService, IApiResponseHandler apiResponseHandler)
        {
            _productsService = productsService;
            _apiResponseHandler = apiResponseHandler;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var asdas = await _apiResponseHandler.HandleResponse(_productsService.GetAll(), "Lista de productos obtenida exitosamente");
            return asdas;
        }
            

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] AddProductDTO newProduct) =>
            await _apiResponseHandler.HandleResponse(_productsService.Create(newProduct), "Producto creado exitosamente");
    }
}
