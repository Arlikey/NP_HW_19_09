using Microsoft.AspNetCore.SignalR;
using NP_HW_19_09;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
var app = builder.Build();

app.MapHub<ExchangeRateHub>("/exchangeHub");

StartExchangeUpdates(app.Services);

app.Run();

static void StartExchangeUpdates(IServiceProvider services)
{
    var random = new Random();
    var hub = services.GetService<IHubContext<ExchangeRateHub>>();

    new Thread(() =>
    {
        while (true)
        {
            var USDtoHRN = Math.Round(random.NextDouble() * (37 * 1.05 - 37 * 0.95) + 37*0.95, 4);
            var EURtoUSD = Math.Round(random.NextDouble() * (1.1 * 1.05 - 1.1 * 0.95) + 1.1 * 0.95, 4);

            hub.Clients.All.SendAsync("ReceiveCurrencyUpdate", "USD/HRN", USDtoHRN);
            hub.Clients.All.SendAsync("ReceiveCurrencyUpdate", "EUR/USD", EURtoUSD);

            Thread.Sleep(5000);
        }

    }).Start();
}