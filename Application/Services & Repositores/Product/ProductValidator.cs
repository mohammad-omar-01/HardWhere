using Application.DTOs.ProductDTO;
using FluentValidation;

namespace HardWhere.Application.Product.Validators;

public class ProductValidator : AbstractValidator<NewProductRequestDTO>
{
    public ProductValidator()
    {
        RuleFor(dto => dto.Price).NotEmpty().WithMessage("Price Must Not Be Empty");

        RuleFor(dto => dto.MainImage).NotEmpty().WithMessage("MainImage Has To be filled");

        RuleFor(dto => dto.GalleryImages.Count)
            .GreaterThanOrEqualTo(2)
            .WithMessage("Images Has to be more than 2");
    }
}
