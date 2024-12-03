using FluentValidation;
using Meetme.ProfileService.API.ViewModels.PhotoViewModels;

namespace Meetme.ProfileService.API.Validation.PhotoViewModelValidators;

public class PhotoViewModelValidator : AbstractValidator<PhotoViewModel>
{
	public PhotoViewModelValidator()
	{
		RuleFor(x => x.ProfileId).NotEmpty().WithMessage(PhotoViewModelValidationMessages.ProfileIdRequired);
	}
}
