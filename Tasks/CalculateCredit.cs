public class CalculateCredit
{   
    public static void Run(double initial_deposit, int years, double interest_rate)
    {
        interest_rate /= 100;
        double resultSavings = initial_deposit;
        for (var i=1; i <= years; i++)
        {
            resultSavings = resultSavings * (1 + interest_rate);
            Console.WriteLine("Год " + i + ": " + resultSavings.ToString("F2") + " руб.");
        }
    }
}
