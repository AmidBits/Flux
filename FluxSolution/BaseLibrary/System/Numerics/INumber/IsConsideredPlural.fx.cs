namespace Flux
{
  public static partial class Fx
  {
    //public static string AppendPluralStringSuffix(this string number, bool isPlural) => isPlural ? number + 's'.ToString() : number;

    /// <summary>
    /// <para>Determines whether the <paramref name="number"/> is considered plural in terms of writing.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="number"></param>
    /// <returns></returns>
    public static bool IsConsideredPlural<TNumber>(this TNumber number)
      where TNumber : System.Numerics.INumber<TNumber>
      => number != TNumber.One;

    ///// <summary>
    ///// <para></para>
    ///// </summary>
    ///// <typeparam name="TNumber"></typeparam>
    ///// <param name="number"></param>
    ///// <returns></returns>
    //public static string PluralStringSuffix<TNumber>(this TNumber number)
    //  where TNumber : System.Numerics.INumber<TNumber>
    //  => number.IsConsideredPlural() ? 's'.ToString() : string.Empty;

    ///// <summary>
    ///// <para></para>
    ///// </summary>
    ///// <typeparam name="TNumber"></typeparam>
    ///// <param name="number"></param>
    ///// <param name="singular"></param>
    ///// <returns></returns>
    //public static string ToPluralString<TNumber>(this TNumber number, string singular)
    //  where TNumber : System.Numerics.INumber<TNumber>
    //{
    //  if (number.IsConsideredPlural())
    //  {
    //    if (singular.EndsWith("x", true, null))
    //      return singular + "es";
    //    else if (singular.EndsWith("y", true, null))
    //      return singular[..^2] + "ies";
    //    else
    //      return singular + 's'.ToString();
    //  }
    //  else
    //    return singular;
    //}
  }
}
