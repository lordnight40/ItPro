using ItPro.Core.Repository;
using ItPro.Data;
using ItPro.Data.Entities;

namespace ItPro.Core.Clients;

public sealed class ClientRepository : BaseRepository<Client>
{
    public ClientRepository(DataContext context) : base(context)
    {
    }
}