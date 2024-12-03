using FluentValidation;
using Meetme.ProfileService.API.ViewModels.ProfileViewModels;

namespace Meetme.ProfileService.API.Validation.ProfileViewModelValidators;

public class CreateProfileViewModelValidator : AbstractValidator<CreateProfileViewModel>
{
	public CreateProfileViewModelValidator()
	{
		RuleFor(x => x.Name)
			.MaximumLength(ProfileViewModelValidationKeys.NameMaxLength)
			.NotEmpty()
			.WithMessage(ProfileViewModelValidationMessages.NameRequired);

		RuleFor(x => x.Age).Must(IsAgeInRange).WithMessage(ProfileViewModelValidationMessages.AgeIsNotInRange);
		RuleFor(x => x.Bio).MaximumLength(ProfileViewModelValidationKeys.BioMaxLength);
		RuleFor(x => x.Gender).NotEmpty().WithMessage(ProfileViewModelValidationMessages.GenderRequired);
	}

	private static bool IsAgeInRange(int age)
	{
		return age >= ProfileViewModelValidationKeys.MinAge && age <= ProfileViewModelValidationKeys.MaxAge;
	}
}
