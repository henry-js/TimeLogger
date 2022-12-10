namespace TimeLogger.Domain.Models;

public class Work
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public virtual Client Client { get; set; }
    public double Hours { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DateEntered { get; set; }
    public int Paid { get; set; }
    public int? PaymentId { get; set; }
    public virtual Payment Payment { get; set; }
}
