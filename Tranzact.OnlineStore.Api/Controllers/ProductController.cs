﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tranzact.OnlineStore.Application.Handlers;
using Tranzact.OnlineStore.Domain.Models.DTOs;
using Tranzact.OnlineStore.Domain.Services.Product;

namespace Tranzact.OnlineStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productsService;
        private readonly IApiResponseHandler _apiResponseHandler;

        public ProductController(IProductService productsService, IApiResponseHandler apiResponseHandler)
        {
            _productsService = productsService;
            _apiResponseHandler = apiResponseHandler;
        }

        [NonAction]
        [HttpGet]
        [Route("GetAllContent")]
        public async Task<IActionResult> GetAllContent() => 
            await _apiResponseHandler.HandleResponse(_productsService.GetAllContent(), "Lista de productos obtenida exitosamente");

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll() => 
            await _apiResponseHandler.HandleResponse(_productsService.GetAll(), "Lista de productos obtenida exitosamente");

        [HttpGet]
        [Route("GetAllById")]
        public async Task<IActionResult> GetAllById([FromQuery] int ProductId) => 
            await _apiResponseHandler.HandleResponse(_productsService.GetAllById(ProductId), "Producto obtenido exitosamente");

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
