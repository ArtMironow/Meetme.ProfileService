using FluentValidation;
using Meetme.ProfileService.BLL.Models.PreferenceModels;

namespace Meetme.ProfileService.BLL.Validation.PreferenceModelValidators;

public class PreferenceModelValidator : AbstractValidator<PreferenceModel>
{
	public PreferenceModelValidator()
	{
		RuleFor(x => x.MinAge).GreaterThanOrEqualTo(PreferenceModelValidationKeys.MinPartnerAge).WithMessage(PreferenceModelValidationMessages.IncorrectMinAge);
		RuleFor(x => x.MaxAge).LessThanOrEqualTo(PreferenceModelValidationKeys.MaxPartnerAge).WithMessage(PreferenceModelValidationMessages.IncorrectMaxAge);
		RuleFor(x => x.GenderPreference).NotEmpty().WithMessage(PreferenceModelValidationMessages.GenderPreferenceRequired);
	}
}
