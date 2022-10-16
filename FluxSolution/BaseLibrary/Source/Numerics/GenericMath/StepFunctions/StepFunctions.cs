#if NET7_0_OR_GREATER
namespace Flux
{
  public interface IStepFunction<TSelf, TValue>
    where TSelf : System.Numerics.INumber<TSelf>
    where TValue : System.Numerics.INumber<TValue>
  {
    TSelf Value { get; }

    TValue LessThan { get; }
    TValue EqualTo { get; }
    TValue GreaterThan { get; }

    TValue Evaluate(TSelf x);
  }

  public record class StepFunction<TSelf, TValue>
    : IStepFunction<TSelf, TValue>
    where TSelf : System.Numerics.IFloatingPoint<TSelf>
    where TValue : System.Numerics.INumber<TValue>
  {
    /// <summary>The rectangular function.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Rectangular_function"/>
    public static IStepFunction<TSelf, TValue> Rectangular => new StepFunction<TSelf, TValue>(TSelf.One.Div2(), TValue.One, TValue.One.Div2(), TValue.Zero);

    /// <summary>The unit step function (as per Wikipedia).</summary>
    /// <see href="https://en.wikipedia.org/wiki/Heaviside_step_function"/>
    public static IStepFunction<TSelf, TValue> UnitStep => new StepFunction<TSelf, TValue>(TSelf.Zero, TValue.Zero, TValue.Zero, TValue.One);
    /// <summary>The Wikipedia discrete, alternative form, of the unit step function.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Heaviside_step_function#Discrete_form"/>
    public static IStepFunction<TSelf, TValue> AlternativeFormUnitStep => new StepFunction<TSelf, TValue>(TSelf.Zero, TValue.Zero, TValue.One, TValue.One);
    /// <summary>The Wikipedia discrete form, half maximum convention, of the unit step.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Heaviside_step_function#Discrete_form"/>
    public static IStepFunction<TSelf, TValue> HalfMaximumUnitStep => new StepFunction<TSelf, TValue>(TSelf.Zero, TValue.Zero, TValue.One.Div2(), TValue.One);

    /// <summary>The sign() step function.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Step_function"/>
    /// <seealso href="https://en.wikipedia.org/wiki/Sign_function"/>
    public static StepFunction<TSelf, TValue> Sign => new StepFunction<TSelf, TValue>(TSelf.Zero, -TValue.One, TValue.Zero, TValue.One);

    private TSelf m_value;
    private TValue m_lessThan;
    private TValue m_equalTo;
    private TValue m_greaterThan;

    public StepFunction(TSelf value, TValue lessThan, TValue equalTo, TValue greaterThan)
    {
      m_value = value;
      m_lessThan = lessThan;
      m_equalTo = equalTo;
      m_greaterThan = greaterThan;
    }

    public TSelf Value => m_value;
    public TValue LessThan => m_lessThan;
    public TValue EqualTo => m_equalTo;
    public TValue GreaterThan => m_greaterThan;

    public TValue Evaluate(TSelf x)
      => (x < m_value) ? m_lessThan : (x > m_value) ? m_greaterThan : m_equalTo;
  }
}
#endif
