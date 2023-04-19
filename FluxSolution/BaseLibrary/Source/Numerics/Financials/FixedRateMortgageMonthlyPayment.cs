namespace Flux
{
  public static partial class Financials
  {
    /// <summary>An amortizing mortgage loan where the interest rate on the note remains the same through the term of the loan, as opposed to loans where the interest rate may adjust or "float".</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Fixed-rate_mortgage"/>
    static public double FixedRateMortgageMonthlyPayment(double loanAmount, double fixedYearlyNominalInterestRate, int numberOfYears)
    {
      var r = fixedYearlyNominalInterestRate / 100 / 12;

      return r / (1 - System.Math.Pow(1 + r, -numberOfYears * 12)) * loanAmount;
    }
  }
}
