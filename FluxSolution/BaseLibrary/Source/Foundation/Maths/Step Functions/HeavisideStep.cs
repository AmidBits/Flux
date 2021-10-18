using System.Linq;

namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Returns the unit (or Heaviside) step of the specified value using the half-maximum convention, i.e. 0.0 when less than zero (negative), 1.0 when greater than zero (positive), 0.5 (by default) when zero.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Heaviside_step_function"/>
    public static double Heaviside<T>(T value, T zeroReference)
      where T : System.IComparable<T>
      => value.CompareTo(zeroReference) is var result && result < 0 ? 0 : result > 0 ? 1 : 0.5;

    /// <summary>Returns the unit (or Heaviside) step of the specified value using the half-maximum convention, i.e. 0.0 when less than zero (negative), 1.0 when greater than zero (positive), 0.5 (by default) when zero.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Heaviside_step_function"/>
    public static double Heaviside(System.Numerics.BigInteger value, double whenZero = 0.5)
      => value < 0 ? 0 : value > 0 ? 1 : whenZero;

    /// <summary>Returns the unit (or Heaviside) step of the specified value using the half-maximum convention, i.e. 0.0 when less than zero (negative), 1.0 when greater than zero (positive), 0.5 (by default) when zero.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Heaviside_step_function"/>
    public static float Heaviside(float value, float whenZero = 0.5F)
      => value < 0 ? 0 : value > 0 ? 1 : value == 0 ? whenZero : throw new System.ArithmeticException();
    /// <summary>Returns the unit (or Heaviside) step of the specified value using the half-maximum convention, i.e. 0.0 when less than zero (negative), 1.0 when greater than zero (positive), 0.5 (by default) when zero.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Heaviside_step_function"/>
    public static double Heaviside(double value, double whenZero = 0.5)
      => value < 0 ? 0 : value > 0 ? 1 : value == 0 ? whenZero : throw new System.ArithmeticException();

    /// <summary>Returns the unit (or Heaviside) step of the specified value using the half-maximum convention, i.e. 0.0 when less than zero (negative), 1.0 when greater than zero (positive), 0.5 (by default) when zero.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Heaviside_step_function"/>
    public static double Heaviside(int value, double whenZero = 0.5)
      => value < 0 ? 0 : value > 0 ? 1 : whenZero;
    /// <summary>Returns the unit (or Heaviside) step of the specified value using the half-maximum convention, i.e. 0.0 when less than zero (negative), 1.0 when greater than zero (positive), 0.5 (by default) when zero.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Heaviside_step_function"/>
    public static double Heaviside(long value, double whenZero = 0.5)
      => value < 0 ? 0 : value > 0 ? 1 : whenZero;
  }
}
