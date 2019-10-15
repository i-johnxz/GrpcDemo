using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using MediatR;
using ProductService.Grpc.Protos;


namespace ProductService.Grpc.Services
{
    public class ProductService : Protos.ProductService.ProductServiceBase
    {
        private readonly IMediator mediator;

        public ProductService(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public override async Task<GetAllResponse> GetAll(FindAllProductsQuery request, ServerCallContext context)
        {
            var productResult = await mediator.Send(new Api.Queries.FindAllProductsQuery());

            var result = new GetAllResponse();

            foreach (var productDto in productResult)
            {
                var product = new ProductDto()
                {
                    Code = productDto.Code,
                    Name = productDto.Name,
                    Image = productDto.Image,
                    Description = productDto.Description,
                    MaxNumberOfInsured = productDto.MaxNumberOfInsured,
                };

                foreach (var coverDto in productDto.Covers)
                {
                    product.Covers.Add(new CoverDto()
                    {
                        Code = coverDto.Code,
                        Name = coverDto.Name,
                        Description = coverDto.Description,
                        Optional = coverDto.Optional,
                        SumInsured = (float?) coverDto.SumInsured ?? 0F
                    });
                }

                foreach (var questionDto in productDto.Questions)
                {
                    Enum.TryParse(questionDto.QuestionType.ToString(), out QuestionDto.Types.QuestionType questionType);

                    product.Questions.Add(new QuestionDto()
                    {
                        Index = questionDto.Index,
                        QuestionCode = questionDto.QuestionCode,
                        QuestionType = questionType,
                        Text = questionDto.Text
                    });
                }

                result.Products.Add(product);
            }



            return result;

        }

        public override async Task<ProductDto> GetByCode(FindProductByCodeQuery request, ServerCallContext context)
        {
            var result = await mediator.Send(new Api.Queries.FindProductByCodeQuery()
            {
                ProductCode = request.ProductCode
            });
            return new ProductDto()
            {
                Code = result.Code,
                Description = result.Description,
                Image = result.Image,
                MaxNumberOfInsured = result.MaxNumberOfInsured,
                Name = result.Name
            };
        }

        public override async Task<CreateProductDraftResult> PostDraft(CreateProductDraftCommand request, ServerCallContext context)
        {
            var result = await mediator.Send(new Api.Commands.CreateProductDraftCommand()
            {
                ProductDraft = new Api.Commands.Dtos.ProductDraftDto()
                {
                    Code = request.ProductDraft.Code,
                    Description = request.ProductDraft.Description,
                    Image = request.ProductDraft.Image,
                    MaxNumberOfInsured = request.ProductDraft.MaxNumberOfInsured,
                    Name = request.ProductDraft.Name
                }
            });
            return new CreateProductDraftResult()
            {
                ProductId = result.ProductId.ToString()
            };
        }

        public override async Task<ActivateProductResult> Activate(ActivateProductCommand request, ServerCallContext context)
        {
            var result = await mediator.Send(new Api.Commands.ActivateProductCommand()
            {
                ProductId = Guid.Parse(request.ProductId)
            });
            return new ActivateProductResult()
            {
                ProductId = result.ProductId.ToString()
            };
        }

        public override async Task<DiscontinueProductResult> Discontinue(DiscontinueProductCommand request, ServerCallContext context)
        {
            var result = await mediator.Send(new Api.Commands.DiscontinueProductCommand()
            {
                ProductId = Guid.Parse(request.ProductId)
            });
            return new DiscontinueProductResult()
            {
                ProductId = result.ProductId.ToString()
            };
        }
    }
}
