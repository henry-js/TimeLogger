namespace TimeLogger.Domain.Models;

public class Client
{
    public int Id { get; set; }
    public string Name { get; set; }
    public float HourlyRate { get; set; }
    public string Email { get; set; }
    public bool PreBill { get; set; }
    public bool HasCutOff { get; set; }
    public int CutOff { get; set; }
    public float MinimumHours { get; set; }
    public float BillingIncrement { get; set; }
    public int RoundUpAfterXMinutes { get; set; }
    public virtual List<Payment> Payments { get; set; }

}
