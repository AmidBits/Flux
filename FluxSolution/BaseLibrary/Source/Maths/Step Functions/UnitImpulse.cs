namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Returns the discrete-time unit impulse of the specified value, i.e. 0 when not zero, and 1 when zero.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Dirac_delta_function"/>
    public static int UnitImpulse<T>(T value, T zeroReference)
      where T : System.IComparable<T>
      => value.CompareTo(zeroReference) != 0 ? 0 : 1;

    /// <summary>Returns the discrete-time unit impulse of the specified value, i.e. 0 when not zero, and 1 when zero.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Dirac_delta_function"/>
    public static System.Numerics.BigInteger UnitImpulse(System.Numerics.BigInteger value)
      => value != 0 ? 0 : 1;

    /// <summary>Returns the discrete-time unit impulse of the specified value, i.e. 0.0 when not zero, and 1.0 when zero.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Dirac_delta_function"/>
    public static float UnitImpulse(float value)
      => value != 0 ? 0 : 1;
    /// <summary>Returns the discrete-time unit impulse of the specified value, i.e. 0.0 when not zero, and 1.0 when zero.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Dirac_delta_function"/>
    public static double UnitImpulse(double value)
      => value != 0 ? 0 : 1;

    /// <summary>Returns the discrete-time unit impulse of the specified value, i.e. 0 when not zero, and 1 when zero.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Dirac_delta_function"/>
    public static int UnitImpulse(int value)
      => value != 0 ? 0 : 1;
    /// <summary>Returns the discrete-time unit impulse of the specified value, i.e. 0 when not zero, and 1 when zero.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Dirac_delta_function"/>
    public static long UnitImpulse(long value)
      => value != 0 ? 0 : 1;

    /// <summary>Returns the discrete-time unit impulse of the specified value, i.e. 0 when not zero, and 1 when zero.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Dirac_delta_function"/>
    [System.CLSCompliant(false)]
    public static uint UnitImpulse(uint value)
      => value != 0 ? 0U : 1U;
    /// <summary>Returns the discrete-time unit impulse of the specified value, i.e. 0 when not zero, and 1 when zero.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Dirac_delta_function"/>
    [System.CLSCompliant(false)]
    public static ulong UnitImpulse(ulong value)
      => value != 0 ? 0UL : 1UL;
  }
}
