namespace Meetme.ProfileService.BLL.Exceptions;

public class ProfileServiceException : Exception
{
	public ProfileServiceException()
	{ }

	public ProfileServiceException(string message) : base(message)
	{ }

	public ProfileServiceException(string message, Exception innerException) : base(message, innerException)
	{ }
}
