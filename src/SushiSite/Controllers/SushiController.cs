using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SushiSite.Api;

namespace SushiSite.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class SushiController: ControllerBase
	{
		private SushiTrackerApiClient _client;

		public SushiController(SushiTrackerApiClient client)
		{
			_client = client;
		}
		
		[HttpPost("order")]
		public async Task CreateOrder(int numOfRolls)
		{
			await _client.CreateCashierOrder(numOfRolls);
		}

		[HttpGet("total")]
		public async Task<int> TotalOrderedRolls()
		{
			return await _client.GetTotalOrderedRolls();
		}
	}
}