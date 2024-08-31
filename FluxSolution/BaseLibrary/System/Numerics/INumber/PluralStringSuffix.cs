namespace Flux
{
  public static partial class Fx
  {
    //public static string AppendPluralStringSuffix(this string number, bool isPlural) => isPlural ? number + 's'.ToString() : number;

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="number"></param>
    /// <returns></returns>
    public static bool IsPluralString<TNumber>(this TNumber number)
      where TNumber : System.Numerics.INumber<TNumber>
      => number != TNumber.One;

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="number"></param>
    /// <returns></returns>
    public static string PluralStringSuffix<TNumber>(this TNumber number)
      where TNumber : System.Numerics.INumber<TNumber>
      => number.IsPluralString() ? 's'.ToString() : string.Empty;
  }
}
