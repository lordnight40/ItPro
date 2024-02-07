namespace ItPro.Data.Entities;

public sealed class BirthDaysReceiptStatistics
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Surname { get; set; }
    
    public DateTime BirthDay { get; set; }
    
    public decimal Sum { get; set; }
}