namespace Flux
{
  public static partial class GenericMath
  {
    public static TValue StepFunction<TValue>(this TValue x, TValue referenceValue, TValue lessThan, TValue equalTo, TValue greaterThan)
      where TValue : System.IComparable<TValue>
      => x.CompareTo(referenceValue) < 0 ? lessThan : x.CompareTo(referenceValue) > 0 ? greaterThan : equalTo;

#if NET7_0_OR_GREATER

    /// <summary>The rectangular function.</summary>
    /// <remarks>Basis is +0.5, where < makes +1.0, = makes +0.5 and > makes 0.0.</remarks>
    /// <see href="https://en.wikipedia.org/wiki/Rectangular_function"/>
    public static TSelf Rectangular<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => StepFunction(x, TSelf.One.Divide(2), TSelf.One, TSelf.One.Divide(2), TSelf.Zero);

    /// <summary>The sign step function.</summary>
    /// <remarks>Zero basis, where < is -1, = is 0 and > is +1.</remarks>
    /// <see href="https://en.wikipedia.org/wiki/Step_function"/>
    /// <seealso href="https://en.wikipedia.org/wiki/Sign_function"/>
    public static TSelf Signum<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => StepFunction(x, TSelf.Zero, TSelf.NegativeOne, TSelf.Zero, TSelf.One);

    /// <summary>The same as the signum step function, but restricted to unit values, -1 (less than zero) or 1 (greater or equal to zero).</summary>
    /// <remarks>Zero basis, where < is -1, >= is +1.</remarks>
    /// <see href="https://en.wikipedia.org/wiki/Step_function"/>
    /// <seealso href="https://en.wikipedia.org/wiki/Sign_(mathematics)"/>
    public static TSelf SignumEx<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => StepFunction(x, TSelf.Zero, TSelf.NegativeOne, TSelf.One, TSelf.One);

    /// <summary>The unit step function (as per Wikipedia), a.k.a. Heaviside step function.</summary>
    /// <remarks>Zero basis, where <= makes 0.0 and > makes +1.0.</remarks>
    /// <see href="https://en.wikipedia.org/wiki/Heaviside_step_function"/>
    public static TSelf UnitStep<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => StepFunction(x, TSelf.Zero, TSelf.Zero, TSelf.Zero, TSelf.One);

    /// <summary>The Wikipedia discrete, alternative form, of the unit step function.</summary>
    /// <remarks>Zero basis, where < makes 0.0 and >= makes +1.0.</remarks>
    /// <see href="https://en.wikipedia.org/wiki/Heaviside_step_function#Discrete_form"/>
    public static TSelf UnitStepAlternativeForm<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => StepFunction(x, TSelf.Zero, TSelf.Zero, TSelf.One, TSelf.One);

    /// <summary>The Wikipedia discrete form, half maximum convention, of the unit step.</summary>
    /// <remarks>Zero basis, where < is 0.0, = is +0.5 and > is +1.0.</remarks>
    /// <see href="https://en.wikipedia.org/wiki/Heaviside_step_function#Discrete_form"/>
    public static TSelf UnitStepHalfMaximumConvention<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => StepFunction(x, TSelf.Zero, TSelf.Zero, TSelf.One.Divide(2), TSelf.One);

#endif
  }
}
