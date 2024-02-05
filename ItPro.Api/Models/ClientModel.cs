namespace ItPro.Api.Models;

public sealed class ClientModel
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Surname { get; set; }
    
    public DateTime BirthDay { get; set; }
}