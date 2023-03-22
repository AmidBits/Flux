namespace Flux
{
  public static partial class Financials
  {
    /// <summary>An amortizing mortgage loan where the interest rate on the note remains the same through the term of the loan, as opposed to loans where the interest rate may adjust or "float".</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Fixed-rate_mortgage"/>
    static public TSelf FixedRateMortgageMonthlyPayment<TSelf>(TSelf loanAmount, TSelf fixedYearlyNominalInterestRate, TSelf numberOfYears)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    {
      var r = fixedYearlyNominalInterestRate.Divide(100).Divide(12);

      return r / (TSelf.One - TSelf.Pow(TSelf.One + r, -numberOfYears.Multiply(12))) * loanAmount;
    }
  }
}
