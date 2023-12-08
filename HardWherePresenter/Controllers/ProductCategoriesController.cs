using Application.DTOs;
using Application.Repositories;
using Application.Services.Categoery;
using Domain.Payment;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HardWherePresenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoriesController : ControllerBase
    {
        private readonly ICategoeryService _categoeryService;

        public ProductCategoriesController(ICategoeryService categoeryService)
        {
            this._categoeryService = categoeryService;
        }

        [HttpGet]
        public async Task<ActionResult<CategoeryDTO>> Get()
        {
            var list = await _categoeryService.GetCategories();
            var json = JsonConvert.SerializeObject(list);
            return Content(json, "application/json", Encoding.UTF8);
        }
    }
}
