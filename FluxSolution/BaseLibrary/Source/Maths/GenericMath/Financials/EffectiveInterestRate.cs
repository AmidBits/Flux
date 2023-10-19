namespace Flux
{
  public static partial class Financials
  {
    /// <summary>The interest rate on a loan or financial product restated from the nominal interest rate as an interest rate with annual compound interest payable in arrears.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Effective_interest_rate"/>
    static public TSelf EffectiveInterestRate<TSelf>(this TSelf nominalRate, TSelf compoundingPeriods)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IPowerFunctions<TSelf>
      => TSelf.Pow(TSelf.One + nominalRate / compoundingPeriods, compoundingPeriods) - TSelf.One;
  }
}
