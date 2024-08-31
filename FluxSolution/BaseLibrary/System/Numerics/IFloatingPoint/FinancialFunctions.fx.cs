namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Compound interest is the addition of interest to the principal sum of a loan or deposit, or in other words, interest on interest.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Compound_interest"/>
    public static TValue CompoundInterestRate<TValue>(this TValue nominalInterestRate, TValue compoundingPeriodsPerYear, TValue numberOfYears)
      where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.IPowerFunctions<TValue>
      => TValue.Pow(TValue.One + nominalInterestRate / compoundingPeriodsPerYear, compoundingPeriodsPerYear * numberOfYears);

    /// <summary>The interest rate on a loan or financial product restated from the nominal interest rate as an interest rate with annual compound interest payable in arrears.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Effective_interest_rate"/>
    public static TValue EffectiveInterestRate<TValue>(this TValue nominalRate, TValue compoundingPeriods)
      where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.IPowerFunctions<TValue>
      => TValue.Pow(TValue.One + nominalRate / compoundingPeriods, compoundingPeriods) - TValue.One;

    /// <summary>An amortizing mortgage loan where the interest rate on the note remains the same through the term of the loan, as opposed to loans where the interest rate may adjust or "float".</summary>
    /// <see href="https://en.wikipedia.org/wiki/Fixed-rate_mortgage"/>
    public static TValue FixedRateMortgageMonthlyPayment<TValue>(this TValue fixedYearlyNominalInterestRate, TValue numberOfYears, TValue loanAmount)
      where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.IPowerFunctions<TValue>
    {
      //var r = fixedYearlyNominalInterestRate / TValue.CreateChecked(100) / TValue.CreateChecked(12);
      var r = fixedYearlyNominalInterestRate / TValue.CreateChecked(12); // Was divide by 100 just to make it a decimal percent?

      return r / (TValue.One - TValue.Pow(TValue.One + r, -numberOfYears * TValue.CreateChecked(12))) * loanAmount;
    }
  }
}
