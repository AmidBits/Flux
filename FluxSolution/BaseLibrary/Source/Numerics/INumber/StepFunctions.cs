#if NET7_0_OR_GREATER
namespace Flux
{
  public static class UnitStepFunction
  {
    /// <summary>Unit step function.</summary>
    public static UnitStepFunction<double> Heaviside
      => new UnitStepFunction<double>(0, 0, 1);
    /// <summary>Unit step function alternate form.</summary>
    public static UnitStepFunction<double> AlternativeForm
      => new UnitStepFunction<double>(0, 1, 1);
    /// <summary>Unit step function using half-maximum convention.</summary>
    public static UnitStepFunction<double> HalfMaximum
      => new UnitStepFunction<double>(0, 0.5, 1);
  }

  public class UnitStepFunction<TSelf>
    : System.IEquatable<UnitStepFunction<TSelf>>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    private TSelf m_lessThan0;
    private TSelf m_equalTo0;
    private TSelf m_greaterThan0;

    public UnitStepFunction(TSelf lessThan0, TSelf equalTo0, TSelf greaterThan0)
    {
      m_lessThan0 = lessThan0;
      m_equalTo0 = equalTo0;
      m_greaterThan0 = greaterThan0;
    }

    public TSelf LessThanZero => m_lessThan0;
    public TSelf EqualToZero => m_equalTo0;
    public TSelf GreaterThanZero => m_greaterThan0;

    public TSelf Apply(TSelf value)
      => (value < TSelf.Zero) ? LessThanZero : (value > TSelf.Zero) ? GreaterThanZero : EqualToZero;

    #region Implemented interfaces
    public bool Equals(UnitStepFunction<TSelf>? other)
      => other is not null && m_lessThan0 == other.m_lessThan0 && m_equalTo0 == other.m_equalTo0 && m_greaterThan0 == other.m_greaterThan0;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is UnitStepFunction<TSelf> o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_lessThan0, m_equalTo0, m_greaterThan0);
    public override string ToString()
      => $"{GetType().Name} {{ < 0 : {m_lessThan0}, 0 : {m_equalTo0}, > 0 : {m_greaterThan0} }}";
    #endregion Object overrides
  }

  public static partial class ExtensionMethods
  {
    /// <summary>PREVIEW! Dirac delta function.
    /// <param name="selectorPositiveW"/>, <paramref name="x"/> = 0
    /// 0, <paramref name="x"/> != 0
    /// </summary>
    /// <param name="selectorPositiveW">Results in a hyperreal number.</param>
    public static TResult DiracDeltaFunction<TSelf, TResult>(this TSelf x, System.Func<TSelf, TResult> selectorPositiveW)
      where TSelf : System.Numerics.INumber<TSelf>
      => x == TSelf.Zero ? selectorPositiveW(x) : default!;

    /// <summary>PREVIEW! Unit step or Heaviside step function.
    /// 1, <paramref name="x"/> > 0
    /// 0, <paramref name="x"/> <= 0
    /// </summary>
    public static TSelf UnitStep<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>
      => x > TSelf.Zero ? TSelf.One : TSelf.Zero;

    /// <summary>PREVIEW! Unit step or Heaviside step function. Alternate form:
    /// 0, <paramref name="x"/> < 0
    /// 1, <paramref name="x"/> >= 0
    /// </summary>
    public static TSelf UnitStepZ<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>
      => x < TSelf.Zero ? TSelf.Zero : TSelf.One;

    /// <summary>PREVIEW! Unit step or Heaviside step function. Using half-maximum convention:
    /// 0.0, <paramref name="x"/> < 0
    /// 0.5, <paramref name="x"/> = 0
    /// 1.0, <paramref name="x"/> > 0
    /// </summary>
    public static TSelf UnitStepHM<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => x < TSelf.Zero ? TSelf.Zero : x > TSelf.Zero ? TSelf.One : TSelf.One.Div2();
  }
}
#endif
