using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SushiTrackerApiContracts;

namespace SushiTrackerApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class OrdersController : ControllerBase
	{
		const int MinimalRollsCount = 6;
		const int FullRollPrice = 39;
		const int DiscountRollPrice = 9;

		private static int _totalOrderedRolls = 0;

		private readonly ILogger<OrdersController> _logger;

		public OrdersController(ILogger<OrdersController> logger)
		{
			_logger = logger;
		}

		[HttpGet]
		public IActionResult Get()
		{
			return Ok();
		}

		[HttpPost]
		public IActionResult Create([FromBody] CreateOrderRequest createOrderRequest)
		{
			if (createOrderRequest.RollsCount < MinimalRollsCount)
			{
				return BadRequest($"Rolls count is low. Minimal rolls count is {MinimalRollsCount}");
			}

			Interlocked.Add(ref _totalOrderedRolls, createOrderRequest.RollsCount);

			return new JsonResult(new CreateOrderResponse
			{
				OrderPrice = createOrderRequest.IsMobileApp
					? createOrderRequest.RollsCount * DiscountRollPrice
					: createOrderRequest.RollsCount * FullRollPrice
			});
		}

		[HttpGet("total_rolls")]
		public int TotalOrderedRolls()
		{
			return _totalOrderedRolls;
		}
	}
}