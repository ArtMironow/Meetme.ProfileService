using AutoFixture;
using AutoFixture.Xunit2;

namespace Meetme.ProfileService.Tests.Services;

[AttributeUsage(AttributeTargets.Method)]
public class OmitOnRecursionAutoDataAttribute : AutoDataAttribute
{
	public OmitOnRecursionAutoDataAttribute() : base( () =>
	{
		var fixture = new Fixture();

		fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
			.ToList()
			.ForEach(b => fixture.Behaviors.Remove(b));

		fixture.Behaviors.Add(new OmitOnRecursionBehavior());

		return fixture;
	})
	{
	}
}
