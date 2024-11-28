using FluentValidation;
using Meetme.ProfileService.BLL.Models.ProfileModels;
using Meetme.ProfileService.BLL.Validation.Common;

namespace Meetme.ProfileService.BLL.Validation.ProfileModelValidators;

public class ProfileModelValidator : AbstractValidator<ProfileModel>
{
	public ProfileModelValidator()
	{
		RuleFor(x => x.Name).MaximumLength(100).NotEmpty().WithMessage(ProfileModelValidationMessages.NameRequired);
		RuleFor(x => x.Age).Must(IsAgeInRange).WithMessage(ProfileModelValidationMessages.AgeIsNotInRange);
		RuleFor(x => x.Bio).MaximumLength(500);
		RuleFor(x => x.Gender).NotEmpty().WithMessage(ProfileModelValidationMessages.GenderRequired);
	}

	private static bool IsAgeInRange(int age)
	{
		if (age >= ValidationKeys.MinAge && age <= ValidationKeys.MaxAge)
		{
			return true;
		}

		return false;
	}
}
