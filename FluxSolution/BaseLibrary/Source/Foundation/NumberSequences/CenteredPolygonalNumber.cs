namespace Flux.Numerics
{
  public class CenteredPolygonalNumber
    : ASequencedNumbers<int>
  {
    public int NumberOfSides { get; set; }

    public CenteredPolygonalNumber(int numberOfSides)
      => NumberOfSides = numberOfSides;

    // INumberSequence
    /// <summary></summary>
    /// <see cref="https://en.wikipedia.org/wiki/Centered_polygonal_number"/>
    /// <remarks>This function runs indefinitely, if allowed.</remarks>
    public override System.Collections.Generic.IEnumerable<int> GetNumberSequence()
    {
      for (var index = 0; ; index++)
        yield return GetNumber(index, NumberOfSides);
    }

    #region Statics

    /// <summary></summary>
    /// <remarks>This function runs indefinitely, if allowed.</remarks>
    public static System.Collections.Generic.IEnumerable<(int minCenteredNumber, int maxCenteredNumber, int count)> GetLayers(int numberOfSides)
    {
      yield return (1, 1, 1);

      foreach (var v in new CenteredPolygonalNumber(numberOfSides).GetNumberSequence().PartitionTuple2(false, (min, max, index) => (min, max)))
        yield return (v.min + 1, v.max, v.max - v.min);
    }

    /// <summary></summary>
    /// <see cref="https://en.wikipedia.org/wiki/Centered_polygonal_number"/>
    public static int GetNumber(int index, int numberOfSides)
    {
      if (index < 0) throw new System.ArgumentOutOfRangeException(nameof(index));
      if (numberOfSides < 3) throw new System.ArgumentOutOfRangeException(nameof(numberOfSides));

      return (numberOfSides * index * index + numberOfSides * index + 2) / 2;
    }

    #endregion Statics
  }

  //public static partial class Maths
  //{
  //  /// <summary></summary>
  //  /// <remarks>This function runs indefinitely, if allowed.</remarks>
  //  public static System.Collections.Generic.IEnumerable<(int minCenteredNumber, int maxCenteredNumber, int count)> GetCenteredPolygonalLayers(int numberOfSides)
  //  {
  //    yield return (1, 1, 1);

  //    foreach (var v in Flux.Maths.GetCenteredPolygonalSequence(numberOfSides).PartitionTuple2(false, (min, max, index) => (min, max)))
  //      yield return (v.min + 1, v.max, v.max - v.min);
  //  }

  //  /// <summary></summary>
  //  /// <see cref="https://en.wikipedia.org/wiki/Centered_polygonal_number"/>
  //  public static int GetCenteredPolygonalNumber(int index, int numberOfSides)
  //  {
  //    if (index < 0) throw new System.ArgumentOutOfRangeException(nameof(index));
  //    if (numberOfSides < 3) throw new System.ArgumentOutOfRangeException(nameof(numberOfSides));

  //    return (numberOfSides * index * index + numberOfSides * index + 2) / 2;
  //  }

  //  /// <summary></summary>
  //  /// <see cref="https://en.wikipedia.org/wiki/Centered_polygonal_number"/>
  //  /// <remarks>This function runs indefinitely, if allowed.</remarks>
  //  public static System.Collections.Generic.IEnumerable<int> GetCenteredPolygonalSequence(int numberOfSides)
  //  {
  //    for (var index = 0; ; index++)
  //      yield return GetCenteredPolygonalNumber(index, numberOfSides);
  //  }
  //}
}
