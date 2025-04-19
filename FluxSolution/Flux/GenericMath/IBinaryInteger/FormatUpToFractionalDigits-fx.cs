namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>
    /// <para>Returns a string format for a dynamic <paramref name="value"/> of fractional digits.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static string FormatUpToFractionalDigits<TInteger>(this TInteger value)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => value is < 1 or > 339
      ? throw new System.ArgumentOutOfRangeException(nameof(value))
      : "0." + new string('#', 339);
  }
}
