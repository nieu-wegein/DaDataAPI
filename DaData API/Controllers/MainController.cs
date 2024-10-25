using AutoMapper;
using DaData_API.DTO;
using DaData_API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DaData_API.Controllers
{
	[ApiController]
	[Route("adress")]
	public class MainController : ControllerBase
	{
		private readonly IWebClient _webClient;
		private readonly IMapper _mapper;


		public MainController(IWebClient webClient, IMapper mapper)
		{
			_webClient = webClient;
			_mapper = mapper;
		}

		[HttpGet("info")]
		public async Task<IActionResult> GetAdressInfoAsync(string adress)
		{
			AdressInfo adressInfo = await _webClient.GetInfoAsync(adress);
			AdressInfoResponse resp = _mapper.Map<AdressInfoResponse>(adressInfo);

			return Ok(resp);
		}
	}
}
