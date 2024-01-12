namespace Flux
{
  public static partial class Maths
  {
    public static TValue StepFunction<TValue>(this TValue x, TValue referenceValue, TValue lessThan, TValue equalTo, TValue greaterThan)
      where TValue : System.IComparable<TValue>
      => x.CompareTo(referenceValue) < 0 ? lessThan : x.CompareTo(referenceValue) > 0 ? greaterThan : equalTo;

#if NET7_0_OR_GREATER

    /// <summary>The rectangular function.</summary>
    /// <remarks>Basis = +0.5, where LT = +1, EQ = +0.5 and GT = 0.</remarks>
    /// <see href="https://en.wikipedia.org/wiki/Rectangular_function"/>
    public static TSelf Rectangular<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => StepFunction(x, TSelf.One / TSelf.CreateChecked(2), TSelf.One, TSelf.One / TSelf.CreateChecked(2), TSelf.Zero);

    /// <summary>The sign step function.</summary>
    /// <remarks>Basis = 0, where LT = -1, EQ = 0 and GT = +1.</remarks>
    /// <see href="https://en.wikipedia.org/wiki/Step_function"/>
    /// <seealso href="https://en.wikipedia.org/wiki/Sign_function"/>
    public static TSelf Signum<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => StepFunction(x, TSelf.Zero, TSelf.NegativeOne, TSelf.Zero, TSelf.One);

    /// <summary>The same as the signum step function, but restricted to unit values.</summary>
    /// <remarks>Basis = 0, where LT = -1 and GTE = +1.</remarks>
    /// <see href="https://en.wikipedia.org/wiki/Step_function"/>
    /// <seealso href="https://en.wikipedia.org/wiki/Sign_(mathematics)"/>
    public static TSelf SignumEx<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => StepFunction(x, TSelf.Zero, TSelf.NegativeOne, TSelf.One, TSelf.One);

    /// <summary>The unit step function (as per Wikipedia), a.k.a. Heaviside step function.</summary>
    /// <remarks>Basis = 0, where LTE = 0 and GT = +1.</remarks>
    /// <see href="https://en.wikipedia.org/wiki/Heaviside_step_function"/>
    public static TSelf UnitStep<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => StepFunction(x, TSelf.Zero, TSelf.Zero, TSelf.Zero, TSelf.One);

    /// <summary>The Wikipedia discrete, alternative form, of the unit step function.</summary>
    /// <remarks>Basis = 0, where LT = 0 and GTE = +1.</remarks>
    /// <see href="https://en.wikipedia.org/wiki/Heaviside_step_function#Discrete_form"/>
    public static TSelf UnitStepAlternativeForm<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => StepFunction(x, TSelf.Zero, TSelf.Zero, TSelf.One, TSelf.One);

    /// <summary>The Wikipedia discrete form, half maximum convention, of the unit step.</summary>
    /// <remarks>Basis = 0, where LT = 0, EQ = +0.5 and GT = +1.</remarks>
    /// <see href="https://en.wikipedia.org/wiki/Heaviside_step_function#Discrete_form"/>
    public static TSelf UnitStepHalfMaximumConvention<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => StepFunction(x, TSelf.Zero, TSelf.Zero, TSelf.One / TSelf.CreateChecked(2), TSelf.One);

#endif
  }
}
