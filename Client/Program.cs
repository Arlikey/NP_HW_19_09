using Microsoft.AspNetCore.SignalR.Client;

class Program
{
    static async Task Main(string[] args)
    {
        var connection = new HubConnectionBuilder().WithUrl("https://localhost:7196/exchangeHub").Build();

        connection.On<string, decimal>("ReceiveCurrencyUpdate", (currencyPair, rate) =>
        {
            Console.WriteLine($"Rate for {currencyPair} : {rate}");
        });

        await connection.StartAsync();
        Console.WriteLine("Connected to server.");
        Console.ReadLine();
    }
}