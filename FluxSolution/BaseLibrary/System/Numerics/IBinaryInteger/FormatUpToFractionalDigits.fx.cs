namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Returns a string format for a dynamic <paramref name="number"/> of fractional digits.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="number"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static string FormatUpToFractionalDigits<TInteger>(this TInteger number)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => number is < 1 or > 339
      ? throw new System.ArgumentOutOfRangeException(nameof(number))
      : "0." + new string('#', 339);
  }
}
