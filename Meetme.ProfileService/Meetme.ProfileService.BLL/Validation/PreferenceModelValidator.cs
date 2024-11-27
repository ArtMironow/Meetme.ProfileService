using FluentValidation;
using Meetme.ProfileService.BLL.Models;

namespace Meetme.ProfileService.BLL.Validation;

public class PreferenceModelValidator : AbstractValidator<PreferenceModel>
{
	public PreferenceModelValidator()
	{
		RuleFor(x => x.MinAge).GreaterThanOrEqualTo(16).WithMessage("Min age should be more or equal to 16");
		RuleFor(x => x.MaxAge).LessThanOrEqualTo(60).WithMessage("Max age should be less or equal to 60");
		RuleFor(x => x.GenderPreference).NotEmpty().WithMessage("Gender preference is required");
	}
}
