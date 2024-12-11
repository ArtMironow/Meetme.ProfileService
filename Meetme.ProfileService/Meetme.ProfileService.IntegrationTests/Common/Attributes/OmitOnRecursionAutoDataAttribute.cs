using AutoFixture;
using AutoFixture.Xunit2;
using Meetme.ProfileService.API.Validation.ProfileViewModelValidators;
using Meetme.ProfileService.API.ViewModels.PreferenceViewModels;
using Meetme.ProfileService.API.ViewModels.ProfileViewModels;
using Meetme.ProfileService.IntegrationTests.Common.Keys;

namespace Meetme.ProfileService.IntegrationTests.Common.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class OmitOnRecursionAutoDataAttribute : AutoDataAttribute
{
	public OmitOnRecursionAutoDataAttribute() : base(() =>
	{
		var fixture = new Fixture();

		fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
			.ToList()
			.ForEach(b => fixture.Behaviors.Remove(b));

		fixture.Behaviors.Add(new OmitOnRecursionBehavior());

		fixture.Customize<CreateProfileViewModel>(
			composer => composer
			.With(x => x.Age, new Random().Next(ProfileViewModelValidationKeys.MinAge, ProfileViewModelValidationKeys.MaxAge + 1))
			.With(x => x.Gender, DAL.Entities.Enums.Gender.Female)
			);

		fixture.Customize<CreatePreferenceViewModel>(
			composer => composer
			.With(x => x.MinAge, ViewModelsCustomizationKeys.PreferenceMinPartnerAge)
			.With(x => x.MaxAge, ViewModelsCustomizationKeys.PreferenceMaxPartnerAge)
			.With(x => x.GenderPreference, DAL.Entities.Enums.Gender.Female)
			);

		fixture.Customize<UpdateProfileViewModel>(
			composer => composer
			.With(x => x.Age, new Random().Next(ProfileViewModelValidationKeys.MinAge, ProfileViewModelValidationKeys.MaxAge + 1))
			.With(x => x.Gender, DAL.Entities.Enums.Gender.Female)
			);

		fixture.Customize<UpdatePreferenceViewModel>(
			composer => composer
			.With(x => x.MinAge, ViewModelsCustomizationKeys.PreferenceMinPartnerAge)
			.With(x => x.MaxAge, ViewModelsCustomizationKeys.PreferenceMaxPartnerAge)
			.With(x => x.GenderPreference, DAL.Entities.Enums.Gender.Female)
			);

		return fixture;
	})
	{
	}
}