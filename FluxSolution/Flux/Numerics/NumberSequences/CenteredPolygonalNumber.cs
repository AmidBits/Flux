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
    public static System.Collections.Generic.IEnumerable<(TSelf minCenteredNumber, TSelf maxCenteredNumber, TSelf count)> GetLayers<TSelf>(TSelf numberOfSides)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      yield return (TSelf.One, TSelf.One, TSelf.One);

      foreach (var v in GetSequence(numberOfSides).PartitionTuple2(false, (min, max, index) => (min, max)))
        yield return (v.min + TSelf.One, v.max, v.max - v.min);
    }

    /// <summary></summary>
    /// <see href="https://en.wikipedia.org/wiki/Centered_polygonal_number"/>
    public static TSelf GetNumber<TSelf>(TSelf index, TSelf numberOfSides)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (index < TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(index));
      if (numberOfSides < TSelf.CreateChecked(3)) throw new System.ArgumentOutOfRangeException(nameof(numberOfSides));

      var two = TSelf.CreateChecked(2);

      return (numberOfSides * index * index + numberOfSides * index + two) / two;
    }

    /// <summary></summary>
    /// <see href="https://en.wikipedia.org/wiki/Centered_polygonal_number"/>
    /// <remarks>This function runs indefinitely, if allowed.</remarks>
    public static System.Collections.Generic.IEnumerable<TSelf> GetSequence<TSelf>(TSelf numberOfSides)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      for (var index = TSelf.Zero; ; index++)
        yield return GetNumber(index, numberOfSides);
    }
  }
}
