namespace Flux
{
  public static partial class Fx
  {
    //public static string AppendPluralStringSuffix(this string source, bool isPlural) => isPlural ? source + 's'.ToString() : source;

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool IsPluralString<TSelf>(this TSelf source)
      where TSelf : System.Numerics.INumber<TSelf>
      => source != TSelf.One;

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static string PluralStringSuffix<TSelf>(this TSelf source)
      where TSelf : System.Numerics.INumber<TSelf>
      => source.IsPluralString() ? 's'.ToString() : string.Empty;
  }
}
