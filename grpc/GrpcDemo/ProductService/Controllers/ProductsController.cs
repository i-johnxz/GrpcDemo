using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductService.Api.Commands;
using ProductService.Api.Queries;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductsController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await mediator.Send(new FindAllProductsQuery());
            return new JsonResult(result);
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetByCode([FromRoute] string code)
        {
            var result = await mediator.Send(new FindProductByCodeQuery {ProductCode = code});
            return new JsonResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostDraft([FromBody] CreateProductDraftCommand request)
        {
            var result = await mediator.Send(request);
            return new JsonResult(result);
        }

        [HttpPost("/activate")]
        public async Task<IActionResult> Activate([FromBody] ActivateProductCommand request)
        {
            var result = await mediator.Send(request);
            return new JsonResult(result);
        }

        [HttpPost("/discontinue")]
        public async Task<IActionResult> Discontinue([FromBody] DiscontinueProductCommand request)
        {
            var result = await mediator.Send(request);
            return new JsonResult(result);
        }

    }
}