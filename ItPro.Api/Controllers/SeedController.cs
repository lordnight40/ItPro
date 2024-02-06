using ItPro.Core.Repository;
using ItPro.Data;
using ItPro.Data.Entities;
using ItPro.Data.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ItPro.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SeedController : ControllerBase
{
    private readonly DataContext context;

    private string[] names = { "John", "Alex", "Jeam", "Olivia", "Katerina", "Alexandra", "Alexander", "Ivan", "Sergey", "Alexey" };

    public SeedController(DataContext context)
    {
        this.context = context;
    }

    [HttpPost("seed")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Seed()
    {
        try
        {
            var clientsWithOrders = new List<Client>();
            
            for (var i = 1; i <= 100; i++)
            {
                var year = Random.Shared.Next(1970, 2005);
                var month = Random.Shared.Next(1, 12);
                var day = Random.Shared.Next(1, DateTime.DaysInMonth(year, month));

                var client = new Client
                {
                    Name = this.names[Random.Shared.Next(0, this.names.Length - 1)],
                    Surname = i.ToString(),
                    BirthDay = new DateTime(year, month, day),
                    Orders = new List<Order>()
                };

                var ordersCount = Random.Shared.Next(2, 15);
                for (var j = 1; j <= ordersCount; j++)
                {
                    var status = Random.Shared.Next(0, 2) switch
                    {
                        0 => Status.NotInProgress,
                        1 => Status.Canceled,
                        2 => Status.Completed,
                        _ => Status.Canceled
                    };

                    var orderYear = Random.Shared.Next(client.BirthDay.AddYears(10).Year, DateTime.Today.Year);
                    var orderMonth = Random.Shared.Next(1, 12);
                    var orderDay = Random.Shared.Next(1, DateTime.DaysInMonth(orderYear, orderMonth));
                    var orderHour = Random.Shared.Next(0, 24);
                    var orderMinute = Random.Shared.Next(0, 59);

                    // если индекс делится на 3 без остатка, то считаем, что заказ был в день рождения клиента
                    if (j % 5 == 0)
                    {
                        status = Status.Completed;
                        orderMonth = client.BirthDay.Month;
                        orderDay = client.BirthDay.Day;
                    }

                    var order = new Order
                    {
                        Client = client,
                        Amount = Random.Shared.Next(100, 1500),
                        Status = status,
                        CreatedAt = new DateTime(orderYear, orderMonth, orderDay, orderHour, orderMinute, 0)
                    };

                    client.Orders.Add(order);
                    clientsWithOrders.Add(client);
                }
            }
            
            this.context.Clients.AddRange(clientsWithOrders);
            await this.context.SaveChangesAsync(HttpContext.RequestAborted);

            return Ok();
        }
        catch (Exception e)
        {
            return this.Problem(e.Message);
        }
    }
}