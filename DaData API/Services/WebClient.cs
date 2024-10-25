using DaData_API.DTO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using System.Buffers.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime;
using System.Text;

namespace DaData_API.Services
{
	internal class WebClient : IWebClient
	{
		private readonly IHttpClientFactory _clientFactory;
		private readonly IOptions<WebClientSettings> _settings;
		private readonly ILogger<WebClient> _logger;

		public WebClient(IHttpClientFactory clientFactory, IOptions<WebClientSettings> settings, 
			ILogger<WebClient> logger)
		{
			_settings = settings;
			_clientFactory = clientFactory;
			_logger = logger;
		}

		public async Task<AdressInfo> GetInfoAsync(string adress)
		{
			var _httpClient = _clientFactory.CreateClient("Dadata");

			_httpClient.DefaultRequestHeaders.Add("X-Secret", _settings.Value.Secret);
			_httpClient.DefaultRequestHeaders.Add("Authorization", $"Token {_settings.Value.Token}");
			_httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

			using HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, _settings.Value.Url);
			req.Content = new StringContent($"[\"{adress}\"]", Encoding.UTF8,"application/json");

			using var resp = await _httpClient.SendAsync(req);
			var content = await resp.Content.ReadFromJsonAsync<List<AdressInfo>>();

			_logger.LogInformation("Retrieved \"{adress}\" adress information based on \"{string}\" string", content[0].Result, adress);

			return content[0];
		}
	}
}
