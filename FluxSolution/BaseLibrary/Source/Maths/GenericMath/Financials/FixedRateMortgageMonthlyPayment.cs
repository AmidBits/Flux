namespace Flux
{
  public static partial class Financials
  {
    /// <summary>An amortizing mortgage loan where the interest rate on the note remains the same through the term of the loan, as opposed to loans where the interest rate may adjust or "float".</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Fixed-rate_mortgage"/>
#if NET7_0_OR_GREATER
    public static TSelf FixedRateMortgageMonthlyPayment<TSelf>(this TSelf fixedYearlyNominalInterestRate, TSelf numberOfYears, TSelf loanAmount)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IPowerFunctions<TSelf>
    {
      var r = fixedYearlyNominalInterestRate / TSelf.CreateChecked(100) / TSelf.CreateChecked(12);

      return r / (TSelf.One - TSelf.Pow(TSelf.One + r, -numberOfYears * TSelf.CreateChecked(12))) * loanAmount;
    }
#else
    public static double FixedRateMortgageMonthlyPayment(this double fixedYearlyNominalInterestRate, double numberOfYears, double loanAmount)
    {
      var r = fixedYearlyNominalInterestRate / 100 / 12;

      return r / (1 - System.Math.Pow(1 + r, -numberOfYears * 12)) * loanAmount;
    }
#endif
  }
}
