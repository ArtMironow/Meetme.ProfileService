using FluentValidation;
using Meetme.ProfileService.BLL.Models.ProfileModels;

namespace Meetme.ProfileService.BLL.Validation.ProfileModelValidators;

public class ProfileModelValidator : AbstractValidator<ProfileModel>
{
	public ProfileModelValidator()
	{
		RuleFor(x => x.Name).MaximumLength(ProfileModelValidationKeys.NameMaxLength).NotEmpty().WithMessage(ProfileModelValidationMessages.NameRequired);
		RuleFor(x => x.Age).Must(IsAgeInRange).WithMessage(ProfileModelValidationMessages.AgeIsNotInRange);
		RuleFor(x => x.Bio).MaximumLength(ProfileModelValidationKeys.BioMaxLength);
		RuleFor(x => x.Gender).NotEmpty().WithMessage(ProfileModelValidationMessages.GenderRequired);
	}

	private static bool IsAgeInRange(int age)
	{
		if (age >= ProfileModelValidationKeys.MinAge && age <= ProfileModelValidationKeys.MaxAge)
		{
			return true;
		}

		return false;
	}
}
