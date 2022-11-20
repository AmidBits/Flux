namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns the sign of <paramref name="x"/>.</summary>
    public static TSelf Signum<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>
      => StepFunction<TSelf, TSelf>.Sign.Evaluate(x);

    /// <summary>Returns the sign of <paramref name="x"/>.</summary>
    public static TSelf SignumNoZero<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>
      => StepFunction<TSelf, TSelf>.SignNoZero.Evaluate(x);
  }

  public record class StepFunction<TSelf, TResult>
    : IStepFunction<TSelf, TResult>
    where TSelf : System.Numerics.INumber<TSelf>
    where TResult : System.Numerics.INumber<TResult>
  {
    #region Static instances

    /// <summary>The rectangular function.</summary>
    /// <remarks>Basis is +0.5, where < makes +1.0, = makes +0.5 and > makes 0.0.</remarks>
    /// <see href="https://en.wikipedia.org/wiki/Rectangular_function"/>
    public static IStepFunction<TSelf, TResult> Rectangular => new StepFunction<TSelf, TResult>(TSelf.One.Div2(), TResult.One, TResult.One.Div2(), TResult.Zero);

    /// <summary>The unit step function (as per Wikipedia).</summary>
    /// <remarks>Zero basis, where <= makes 0.0 and > makes +1.0.</remarks>
    /// <see href="https://en.wikipedia.org/wiki/Heaviside_step_function"/>
    public static IStepFunction<TSelf, TResult> Unit => new StepFunction<TSelf, TResult>(TSelf.Zero, TResult.Zero, TResult.Zero, TResult.One);

    /// <summary>The Wikipedia discrete, alternative form, of the unit step function.</summary>
    /// <remarks>Zero basis, where < makes 0.0 and >= makes +1.0.</remarks>
    /// <see href="https://en.wikipedia.org/wiki/Heaviside_step_function#Discrete_form"/>
    public static IStepFunction<TSelf, TResult> AlternativeFormUnit => new StepFunction<TSelf, TResult>(TSelf.Zero, TResult.Zero, TResult.One, TResult.One);

    /// <summary>The Wikipedia discrete form, half maximum convention, of the unit step.</summary>
    /// <remarks>Zero basis, where < is 0.0, = is +0.5 and > is +1.0.</remarks>
    /// <see href="https://en.wikipedia.org/wiki/Heaviside_step_function#Discrete_form"/>
    public static IStepFunction<TSelf, TResult> HalfMaximumUnit => new StepFunction<TSelf, TResult>(TSelf.Zero, TResult.Zero, TResult.One.Div2(), TResult.One);

    /// <summary>The sign step function.</summary>
    /// <remarks>Zero basis, where < is -1, = is 0 and > is +1.</remarks>
    /// <see href="https://en.wikipedia.org/wiki/Step_function"/>
    /// <seealso href="https://en.wikipedia.org/wiki/Sign_function"/>
    public static IStepFunction<TSelf, TResult> Sign => new StepFunction<TSelf, TResult>(TSelf.Zero, -TResult.One, TResult.Zero, TResult.One);

    /// <summary>The same as the signum step function, but restricted to unit values, -1 (less than zero) or 1 (greater or equal to zero).</summary>
    /// <remarks>Zero basis, where < is -1, >= is +1.</remarks>
    /// <see href="https://en.wikipedia.org/wiki/Step_function"/>
    /// <seealso href="https://en.wikipedia.org/wiki/Sign_(mathematics)"/>
    public static IStepFunction<TSelf, TResult> SignNoZero => new StepFunction<TSelf, TResult>(TSelf.Zero, -TResult.One, TResult.One, TResult.One);

    #endregion Static instances

    private TSelf m_referenceValue;
    private TResult m_lessThan;
    private TResult m_equalTo;
    private TResult m_greaterThan;

    public StepFunction(TSelf referenceValue, TResult lessThan, TResult equalTo, TResult greaterThan)
    {
      m_referenceValue = referenceValue;
      m_lessThan = lessThan;
      m_equalTo = equalTo;
      m_greaterThan = greaterThan;
    }

    public TSelf ReferenceValue => m_referenceValue;
    public TResult LessThan => m_lessThan;
    public TResult EqualTo => m_equalTo;
    public TResult GreaterThan => m_greaterThan;

    public TResult Evaluate(TSelf x)
      => (x < m_referenceValue) ? m_lessThan : (x > m_referenceValue) ? m_greaterThan : m_equalTo;
  }
}
