namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Returns the discrete unit step of the specified value, i.e. 0 when less than zero (negative), and 1 when greater than or equal to zero.</summary>
    /// <seealso cref="https://en.wikipedia.org/wiki/Heaviside_step_function"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Dirac_delta_function"/>
    public static int UnitStep<T>(T value, T zeroReference)
      where T : System.IComparable<T>
      => value.CompareTo(zeroReference) < 0 ? 0 : 1;

    /// <summary>Returns the discrete unit step of the specified value, i.e. 0 when less than zero (negative), and 1 when greater than or equal to zero.</summary>
    /// <seealso cref="https://en.wikipedia.org/wiki/Heaviside_step_function"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Dirac_delta_function"/>
    public static System.Numerics.BigInteger UnitStep(System.Numerics.BigInteger value)
      => value < System.Numerics.BigInteger.Zero ? System.Numerics.BigInteger.Zero : System.Numerics.BigInteger.One;

    /// <summary>Returns the discrete unit step of the specified value, i.e. 0 when less than zero (negative), and 1 when greater than or equal to zero.</summary>
    /// <seealso cref="https://en.wikipedia.org/wiki/Heaviside_step_function"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Dirac_delta_function"/>
    public static float UnitStep(float value)
      => value < 0 ? 0 : 1;
    /// <summary>Returns the discrete unit step of the specified value, i.e. 0 when less than zero (negative), and 1 when greater than or equal to zero.</summary>
    /// <seealso cref="https://en.wikipedia.org/wiki/Heaviside_step_function"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Dirac_delta_function"/>
    public static double UnitStep(double value)
      => value < 0 ? 0 : 1;

    /// <summary>Returns the discrete unit step of the specified value, i.e. 0 when less than zero (negative), and 1 when greater than or equal to zero.</summary>
    /// <seealso cref="https://en.wikipedia.org/wiki/Heaviside_step_function"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Dirac_delta_function"/>
    public static int UnitStep(int value)
      => value < 0 ? 0 : 1;
    /// <summary>Returns the discrete unit step of the specified value, i.e. 0 when less than zero (negative), and 1 when greater than or equal to zero.</summary>
    /// <seealso cref="https://en.wikipedia.org/wiki/Heaviside_step_function"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Dirac_delta_function"/>
    public static long UnitStep(long value)
      => value < 0L ? 0L : 1L;

    /// <summary>Returns the discrete unit step of the specified value, i.e. 0 when less than zero (negative), and 1 when greater than or equal to zero.</summary>
    /// <seealso cref="https://en.wikipedia.org/wiki/Heaviside_step_function"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Dirac_delta_function"/>
    [System.CLSCompliant(false)]
    public static uint UnitStep(uint value)
      => value < 0U ? 0U : 1U;
    /// <summary>Returns the discrete unit step of the specified value, i.e. 0 when less than zero (negative), and 1 when greater than or equal to zero.</summary>
    /// <seealso cref="https://en.wikipedia.org/wiki/Heaviside_step_function"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Dirac_delta_function"/>
    [System.CLSCompliant(false)]
    public static ulong UnitStep(ulong value)
      => value < 0UL ? 0UL : 1UL;
  }
}
