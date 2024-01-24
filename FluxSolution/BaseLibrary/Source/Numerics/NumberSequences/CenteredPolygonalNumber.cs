#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class NumberSequence
  {
    /// <summary></summary>
    /// <remarks>This function runs indefinitely, if allowed.</remarks>
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
#endif

//namespace Flux.NumberSequences
//{
//  /// <summary></summary>
//  /// <see href="https://en.wikipedia.org/wiki/Centered_polygonal_number"/>
//  /// <remarks>This function runs indefinitely, if allowed.</remarks>
//  public record class CenteredPolygonalNumber
//    : INumericSequence<int>
//  {
//    public int NumberOfSides { get; set; }

//    public CenteredPolygonalNumber(int numberOfSides)
//      => NumberOfSides = numberOfSides;

//    #region Static methods

//#if NET7_0_OR_GREATER
//    /// <summary></summary>
//    /// <remarks>This function runs indefinitely, if allowed.</remarks>
//    public static System.Collections.Generic.IEnumerable<(TSelf minCenteredNumber, TSelf maxCenteredNumber, TSelf count)> GetLayers<TSelf>(TSelf numberOfSides)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//    {
//      yield return (TSelf.One, TSelf.One, TSelf.One);

//      foreach (var v in GetSequence(numberOfSides).PartitionTuple2(false, (min, max, index) => (min, max)))
//        yield return (v.min + TSelf.One, v.max, v.max - v.min);
//    }

//    /// <summary></summary>
//    /// <see href="https://en.wikipedia.org/wiki/Centered_polygonal_number"/>
//    public static TSelf GetNumber<TSelf>(TSelf index, TSelf numberOfSides)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//    {
//      if (index < TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(index));
//      if (numberOfSides < TSelf.CreateChecked(3)) throw new System.ArgumentOutOfRangeException(nameof(numberOfSides));

//      var two = TSelf.CreateChecked(2);

//      return (numberOfSides * index * index + numberOfSides * index + two) / two;
//    }

//    /// <summary></summary>
//    /// <see href="https://en.wikipedia.org/wiki/Centered_polygonal_number"/>
//    /// <remarks>This function runs indefinitely, if allowed.</remarks>
//    public static System.Collections.Generic.IEnumerable<TSelf> GetSequence<TSelf>(TSelf numberOfSides)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//    {
//      for (var index = TSelf.Zero; ; index++)
//        yield return GetNumber(index, numberOfSides);
//    }
//#endif

//    #endregion Static methods

//    #region Implemented interfaces
//    // INumberSequence

//    public System.Collections.Generic.IEnumerable<int> GetSequence()
//      => GetSequence(NumberOfSides);


//    public System.Collections.Generic.IEnumerator<int> GetEnumerator()
//      => GetSequence().GetEnumerator();

//    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
//      => GetEnumerator();
//    #endregion Implemented interfaces
//  }
//}
