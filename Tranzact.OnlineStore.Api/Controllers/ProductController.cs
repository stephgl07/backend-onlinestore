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
        [Route("GetAllContent")]
        public async Task<IActionResult> GetAllContent()
        {
            var asdas = await _apiResponseHandler.HandleResponse(_productsService.GetAllContent(), "Lista de productos obtenida exitosamente");
            return asdas;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var asdas = await _apiResponseHandler.HandleResponse(_productsService.GetAll(), "Lista de productos obtenida exitosamente");
            return asdas;
        }

        // Required in Document
        [HttpGet]
        [Route("GetAllById")]
        public async Task<IActionResult> GetAllById([FromQuery] int ProductId)
        {
            var asdas = await _apiResponseHandler.HandleResponse(_productsService.GetAllById(ProductId), "Lista de productos obtenida exitosamente");
            return asdas;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] AddEditProductDTO newProduct) =>
            await _apiResponseHandler.HandleResponse(_productsService.Create(newProduct), "Producto creado exitosamente");

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] AddEditProductDTO newProduct) =>
            await _apiResponseHandler.HandleResponse(_productsService.Update(newProduct), "Producto actualizado exitosamente");
    }
}
