namespace TimeLogger.Domain.Models;

public class Defaults
{
    public int Id { get; set; }
    public float HourlyRate { get; set; }
    public bool PreBill { get; set; }
    public bool HasCutOff { get; set; }
    public int CutOff { get; set; }
    public float MinimumHours { get; set; }
    public float BillingIncrement { get; set; }
    public int RoundUpAfterXMinutes { get; set; }
}
