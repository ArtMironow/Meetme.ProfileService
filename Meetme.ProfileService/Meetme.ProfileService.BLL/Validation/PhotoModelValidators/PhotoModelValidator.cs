using FluentValidation;
using Meetme.ProfileService.BLL.Models;

namespace Meetme.ProfileService.BLL.Validation.PhotoModelValidators;

public class PhotoModelValidator : AbstractValidator<PhotoModel>
{
	public PhotoModelValidator()
	{
		RuleFor(x => x.PhotoUrl).NotEmpty();
	}
}
