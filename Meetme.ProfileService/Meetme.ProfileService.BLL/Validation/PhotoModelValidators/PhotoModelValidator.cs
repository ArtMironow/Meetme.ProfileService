using FluentValidation;
using Meetme.ProfileService.BLL.Models.PhotoModels;

namespace Meetme.ProfileService.BLL.Validation.PhotoModelValidators;

public class PhotoModelValidator : AbstractValidator<PhotoModel>
{
	public PhotoModelValidator()
	{
		RuleFor(x => x.ProfileId).NotEmpty().WithMessage(PhotoModelValidationMessages.ProfileIdRequired);
	}
}
