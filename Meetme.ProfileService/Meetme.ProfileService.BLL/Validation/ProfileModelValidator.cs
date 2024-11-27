using FluentValidation;
using Meetme.ProfileService.BLL.Models;

namespace Meetme.ProfileService.BLL.Validation;

public class ProfileModelValidator : AbstractValidator<ProfileModel>
{
	public ProfileModelValidator()
	{
		RuleFor(x => x.Name).MaximumLength(100).NotEmpty().WithMessage("Name is required");
		RuleFor(x => x.Age).Must(BeAValidAge).WithMessage("Age should be >= 16 and <= 60");
		RuleFor(x => x.Bio).MaximumLength(500);
		RuleFor(x => x.Gender).NotEmpty().WithMessage("Gender is required");
	}

	private static bool BeAValidAge(int age)
	{
		if (age >= 16 && age <= 60)
		{ 
			return true;
		}

		return false;
	}
}
