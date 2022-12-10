namespace TimeLogger.Domain.Models;

public class Payment
{
    public int Id { get; set; }
    public float Hours { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public int ClientId { get; set; }
    public virtual Client Client { get; set; }
}
