namespace Flux
{
  public static partial class Maths
  {
#if NET7_0_OR_GREATER

    public static int UnitSign<TValue>(this TValue number)
      where TValue : System.Numerics.INumber<TValue>
      => TValue.IsNegative(number) ? -1 : +1;

#else

    public static int UnitSign<TValue>(this System.Numerics.BigInteger number) => number < 0 ? -1 : +1;

    public static int UnitSign<TValue>(this decimal number) => number < 0 ? -1 : +1;

    public static int UnitSign<TValue>(this double number) => number < 0 ? -1 : +1;
    public static int UnitSign<TValue>(this float number) => number < 0 ? -1 : +1;

    public static int UnitSign<TValue>(this int number) => number < 0 ? -1 : +1;
    public static int UnitSign<TValue>(this long number) => number < 0 ? -1 : +1;

#endif
  }
}
