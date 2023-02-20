namespace Flux
{
  public static partial class ExtensionMethodsNumerics
  {
    /// <summary>
    /// <para>Calculate the median of the IList. This list must be pre-sorted.</para>
    /// <see href="http://en.wikipedia.org/wiki/Median"/>
    /// </summary>
    private static TSelf Median<TSelf>(this System.Collections.Generic.IList<TSelf> source)
      where TSelf : System.Numerics.INumberBase<TSelf>
      => System.Math.DivRem(source.Count, 2, out int remainder) is var quotient && remainder == 0 ? (source[quotient - 1] + source[quotient]).Divide(2) : source[quotient];

    /// <summary>
    /// <para>Calculate the median of a sequence. This version buffers the sequence to avoid multiple passes.</para>
    /// <see href="http://en.wikipedia.org/wiki/Median"/>
    /// </summary>
    public static TSelf Median<TSelf>(this System.Collections.Generic.IEnumerable<TSelf> source, out System.Collections.Generic.List<TSelf> orderedList)
      where TSelf : System.Numerics.INumberBase<TSelf>
    {
      orderedList = source.ToList();
      orderedList.Sort();
      return Median(orderedList);
    }
  }
}
