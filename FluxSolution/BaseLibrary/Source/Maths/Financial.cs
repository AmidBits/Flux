#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class Financial
  {
    /// <summary>Compound interest is the addition of interest to the principal sum of a loan or deposit, or in other words, interest on interest.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Compound_interest"/>
    public static TSelf CompoundInterestRate<TSelf>(this TSelf nominalInterestRate, TSelf compoundingPeriodsPerYear, TSelf numberOfYears)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IPowerFunctions<TSelf>
      => TSelf.Pow(TSelf.One + nominalInterestRate / compoundingPeriodsPerYear, compoundingPeriodsPerYear * numberOfYears);

    /// <summary>The interest rate on a loan or financial product restated from the nominal interest rate as an interest rate with annual compound interest payable in arrears.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Effective_interest_rate"/>
    public static TSelf EffectiveInterestRate<TSelf>(this TSelf nominalRate, TSelf compoundingPeriods)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IPowerFunctions<TSelf>
      => TSelf.Pow(TSelf.One + nominalRate / compoundingPeriods, compoundingPeriods) - TSelf.One;

    /// <summary>An amortizing mortgage loan where the interest rate on the note remains the same through the term of the loan, as opposed to loans where the interest rate may adjust or "float".</summary>
    /// <see href="https://en.wikipedia.org/wiki/Fixed-rate_mortgage"/>
    public static TSelf FixedRateMortgageMonthlyPayment<TSelf>(this TSelf fixedYearlyNominalInterestRate, TSelf numberOfYears, TSelf loanAmount)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IPowerFunctions<TSelf>
    {
      var r = fixedYearlyNominalInterestRate / TSelf.CreateChecked(100) / TSelf.CreateChecked(12);

      return r / (TSelf.One - TSelf.Pow(TSelf.One + r, -numberOfYears * TSelf.CreateChecked(12))) * loanAmount;
    }
  }
}
#endif
