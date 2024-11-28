using FluentValidation;
using Meetme.ProfileService.BLL.Models.PreferenceModels;
using Meetme.ProfileService.BLL.Validation.Common;

namespace Meetme.ProfileService.BLL.Validation.PreferenceModelValidators;

public class PreferenceModelValidator : AbstractValidator<PreferenceModel>
{
	public PreferenceModelValidator()
	{
		RuleFor(x => x.MinAge).GreaterThanOrEqualTo(ValidationKeys.MinAge).WithMessage(PreferenceModelValidationMessages.IncorrectMinAge);
		RuleFor(x => x.MaxAge).LessThanOrEqualTo(ValidationKeys.MaxAge).WithMessage(PreferenceModelValidationMessages.IncorrectMaxAge);
		RuleFor(x => x.GenderPreference).NotEmpty().WithMessage(PreferenceModelValidationMessages.GenderPreferenceRequired);
	}
}
