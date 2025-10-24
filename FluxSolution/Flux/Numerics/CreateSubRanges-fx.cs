namespace Flux
{
  public static partial class IBinaryIntegers
  {
    /// <summary>
    /// <para>Creates new ranges of <paramref name="subLength"/> chunks (except for the last which may be shorter). This function splits a sequence into specified lengths (except for the last which may be shorter).</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="totalLength"></param>
    /// <param name="subLength"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<System.Range> CreateSubRanges<TInteger>(this TInteger totalLength, TInteger subLength)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      for (var index = TInteger.Zero; index < totalLength; index += subLength)
        yield return XtensionRange.FromOffsetAndLength(int.CreateChecked(index), int.CreateChecked(TInteger.Min(subLength, totalLength - index)));
    }
  }
}
