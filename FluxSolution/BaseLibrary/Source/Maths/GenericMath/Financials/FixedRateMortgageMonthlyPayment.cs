namespace Flux
{
  public static partial class Financials
  {
    /// <summary>An amortizing mortgage loan where the interest rate on the note remains the same through the term of the loan, as opposed to loans where the interest rate may adjust or "float".</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Fixed-rate_mortgage"/>
    static public TSelf FixedRateMortgageMonthlyPayment<TSelf>(this TSelf fixedYearlyNominalInterestRate, TSelf numberOfYears, TSelf loanAmount)
      where TSelf : System.Numerics.IPowerFunctions<TSelf>
    {
      var r = fixedYearlyNominalInterestRate / TSelf.CreateChecked(100) / TSelf.CreateChecked(12);

      return r / (TSelf.One - TSelf.Pow(TSelf.One + r, -numberOfYears * TSelf.CreateChecked(12))) * loanAmount;
    }
  }
}
