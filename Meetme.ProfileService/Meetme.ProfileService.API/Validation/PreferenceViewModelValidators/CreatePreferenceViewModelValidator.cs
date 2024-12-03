using FluentValidation;
using Meetme.ProfileService.API.ViewModels.PreferenceViewModels;

namespace Meetme.ProfileService.API.Validation.PreferenceViewModelValidators;

public class CreatePreferenceViewModelValidator : AbstractValidator<CreatePreferenceViewModel>
{
	public CreatePreferenceViewModelValidator()
	{
		RuleFor(x => x.MinAge)
			.GreaterThanOrEqualTo(PreferenceViewModelValidationKeys.MinPartnerAge)
			.WithMessage(PreferenceViewModelValidationMessages.IncorrectMinAge);

		RuleFor(x => x.MaxAge)
			.LessThanOrEqualTo(PreferenceViewModelValidationKeys.MaxPartnerAge)
			.WithMessage(PreferenceViewModelValidationMessages.IncorrectMaxAge);

		RuleFor(x => x.GenderPreference).NotEmpty().WithMessage(PreferenceViewModelValidationMessages.GenderPreferenceRequired);
	}
}
