using ItPro.Core.Repository.Queries;

namespace ItPro.Core.Clients;

public sealed class ClientQueryParameters : QueryStringParameters
{
    public string Name { get; set; }
    
    public string Surname { get; set; }
    
    public DateTime? BirthDayGreaterOrEquals { get; set; }
    
    public DateTime? BirthDayLessOrEqual { get; set; }
    
    public DateTime? BirtDayAt { get; set; }
}