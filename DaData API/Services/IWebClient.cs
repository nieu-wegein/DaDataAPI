using DaData_API.DTO;

namespace DaData_API.Services
{
	public interface IWebClient
	{
		Task<AdressInfo> GetInfoAsync(string adress);
	}
}
