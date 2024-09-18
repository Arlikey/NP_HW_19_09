using Microsoft.AspNetCore.SignalR;

namespace NP_HW_19_09
{
    public class ExchangeRateHub : Hub
    {
        public async Task SendExchangeRateAsync(string currency, decimal rate)
        {
            await Clients.All.SendAsync("ReceiveCurrencyUpdate", currency, rate);
        }
    }
}
