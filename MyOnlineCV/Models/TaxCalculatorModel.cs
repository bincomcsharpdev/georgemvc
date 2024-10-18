public class TaxCalculatorModel
{
    public decimal Income { get; set; }

    public decimal CalculateTax()
    {

        // Simple tax bracket logic for demonstration
        if (Income <= 300000) return Income * 0.07m; // 7% for low-income
        else if (Income <= 600000) return Income * 0.11m; // 11% for middle-income
        else return Income * 0.24m; // 24% for high-income
    }
}

