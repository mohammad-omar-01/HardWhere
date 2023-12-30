using Application.Services.ProductServiceNS;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using Application.DTOs.Product;
using HardWhere.Application.Product.Validators;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HardWherePresenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ProductValidator productValidator;
        private readonly string UploadFilePath = "../Uplaods";

        public ProductController(IProductService productService, ProductValidator validatior)
        {
            this._productService = productService;
            this.productValidator = validatior;
        }

        [HttpGet("Categoery/{CategoeryId}")]
        public async Task<ActionResult<List<SimpleProductDTO>>> Get([FromRoute] int CategoeryId)
        {
            var response = await _productService.GetsimpleProductsInCategoryAsync(CategoeryId);
            var json = JsonConvert.SerializeObject(
                response,
                Formatting.Indented,
                new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }
            );
            return Content(json, "application/json", Encoding.UTF8);
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<SimpleProductDTO>> GetProduct([FromRoute] int productId)
        {
            var response = await _productService.GetSimpleProductById(productId);
            var json = JsonConvert.SerializeObject(
                response,
                Formatting.Indented,
                new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }
            );

            return Ok(json);
        }

        [HttpGet("image")]
        public IActionResult Get()
        {
            Byte[] b = System.IO.File.ReadAllBytes(
                @"C:\\Users\\mhamm\\Pictures\\Screenshots\\f.png"
            ); // You can use your own method over here.
            return File(b, "image/jpeg");   
        }

        [HttpGet("slug/{slugName}")]
        public async Task<ActionResult<SimpleProductDTO>> GetProductBySlug(
            [FromRoute] string slugName
        )
        {
            var response = await _productService.GetSimpleProductBySlugName(slugName);
            var json = JsonConvert.SerializeObject(
                response,
                Formatting.Indented,
                new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }
            );

            return Ok(json);
        }

        private byte[] GetFileBytes(string fileName)
        {
            var filePath = Path.Combine(UploadFilePath, fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return new byte[0];
            }

            // Read the file into a byte array
            return System.IO.File.ReadAllBytes(filePath);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromForm] NewProductRequestDTO product)
        {
            var validationResult = productValidator.Validate(product);
            if (validationResult.IsValid)
            {
                var response = await _productService.AddNewProduct(product);
                if (response != null)
                {
                    return Created("Product Has been successfully Added", response);
                }
                return BadRequest();
            }
            else
            {
                return BadRequest(validationResult.Errors);
            }
        }
    }
}
