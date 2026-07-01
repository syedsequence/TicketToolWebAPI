namespace Ticketing.Application.Response
{
	public class APIWarning
	{
		public string WarningMessage { get; set; }

		public APIWarning(string warning)
		{
			WarningMessage = warning;

		}
	}
}
