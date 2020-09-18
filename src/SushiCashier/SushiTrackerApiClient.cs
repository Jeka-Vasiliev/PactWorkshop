using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SushiTrackerApiContracts;

namespace SushiCashier
{
    public class SushiTrackerApiClient
    {
        private readonly string _sushiTrackerUri;

        public SushiTrackerApiClient(string sushiTrackerUri)
        {
            _sushiTrackerUri = sushiTrackerUri;
        }
        
        public async Task<HttpResponseMessage> CreateCashierOrder(int rollsCount)
        {
            using var client = new HttpClient { BaseAddress = new Uri(_sushiTrackerUri)};
            
            try
            {
                var createOrderJson = JsonConvert.SerializeObject(new CreateOrderRequest()
                {
                    RollsCount = rollsCount,
                    IsMobileApp = false
                });
                var createOrderData = new StringContent(createOrderJson, Encoding.UTF8, "application/json");
                    
                var response = await client.PostAsync($"/api/orders", createOrderData);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("There was a problem connecting to Provider API.", ex);
            }
        }

        public async Task<int> GetTotalOrderedRolls()
        {
            using var client = new HttpClient { BaseAddress = new Uri(_sushiTrackerUri)};
            
            try
            {
                   
                var response = await client.GetAsync($"/api/orders/total_rolls");
                return int.Parse(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                throw new Exception("There was a problem connecting to Provider API.", ex);
            }
        }
    }
}