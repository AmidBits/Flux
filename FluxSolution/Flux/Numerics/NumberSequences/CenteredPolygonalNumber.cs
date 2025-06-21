namespace Flux.Numerics
{
  public static partial class NumberSequence
  {
    /// <summary>
    /// <para>Creates a new sequence of </para>
    /// <para><see href="https://en.wikipedia.org/wiki/Centered_polygonal_number"/></para>
    /// </summary>
    /// <remarks>This function runs indefinitely, if allowed.</remarks>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="numberOfSides"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<(TSelf minCenteredNumber, TSelf maxCenteredNumber, TSelf count)> GetCenteredPolygonalLayers<TSelf>(TSelf numberOfSides)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      yield return (TSelf.One, TSelf.One, TSelf.One);

      foreach (var v in GetCenteredPolygonalNumberSequence(numberOfSides).PartitionTuple2(false, (min, max, index) => (min, max)))
        yield return (v.min + TSelf.One, v.max, v.max - v.min);
    }

    /// <summary></summary>
    /// <see href="https://en.wikipedia.org/wiki/Centered_polygonal_number"/>
    public static TSelf GetCenteredPolygonalNumber<TSelf>(TSelf index, TSelf numberOfSides)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      System.ArgumentOutOfRangeException.ThrowIfNegative(index);
      System.ArgumentOutOfRangeException.ThrowIfLessThan(numberOfSides, TSelf.CreateChecked(3));

      var two = TSelf.CreateChecked(2);

      return (numberOfSides * index * index + numberOfSides * index + two) / two;
    }

    /// <summary></summary>
    /// <see href="https://en.wikipedia.org/wiki/Centered_polygonal_number"/>
    /// <remarks>This function runs indefinitely, if allowed.</remarks>
    public static System.Collections.Generic.IEnumerable<TSelf> GetCenteredPolygonalNumberSequence<TSelf>(TSelf numberOfSides)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      for (var index = TSelf.Zero; ; index++)
        yield return GetCenteredPolygonalNumber(index, numberOfSides);
    }
  }
}
