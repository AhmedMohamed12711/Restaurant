using FluentValidation;
using Restaurants.Application.Restaurants.DTOs;

namespace Restaurants.Application.Restaurants.Validators;

public class CreateRestaurantDtoValidator:AbstractValidator<CreateRestaurantDto>
{
    private readonly List<string> validCategories=["Itelian","Indian","Egyption","American","japanise"];
    public CreateRestaurantDtoValidator()
    {
        RuleFor(dto => dto.Name).Length(3, 100);

        RuleFor(dto=>dto.Description).NotEmpty().WithMessage("Description is Reguired.");

        RuleFor(dto => dto.Category).
            Must(category=>validCategories.Contains(category))
            .WithMessage("category\", \"Invalid Category .Please Choose from  a valid category");
        //    Custom((value, context) =>
        //{
        //    var isValidCategory = validCategories.Contains(value);
        //    if (!isValidCategory)
        //    {
        //        context.AddFailure("category", "Invalid Category .Please Choose from  a valid category");
        //    }
        //});
        RuleFor(dto => dto.ContactEmail).EmailAddress().WithMessage("Please provide a valid email address");
        RuleFor(dto => dto.PostalCode).Matches(@"^\d{2}-\d{3}$").WithMessage("Please Provide a valid Postal Code  (XX-XXX).");
    }
}
