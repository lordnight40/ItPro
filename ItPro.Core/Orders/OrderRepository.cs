using ItPro.Core.Repository;
using ItPro.Data;
using ItPro.Data.Entities;

namespace ItPro.Core.Orders;

public sealed class OrderRepository : BaseRepository<Order>
{
    public OrderRepository(DataContext context) : base(context)
    {
    }
}