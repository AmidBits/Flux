namespace Flux.NumberSequences
{
  /// <summary></summary>
  /// <see cref="https://en.wikipedia.org/wiki/Centered_polygonal_number"/>
  /// <remarks>This function runs indefinitely, if allowed.</remarks>
  public record class CenteredPolygonalNumber
    : INumericSequence<int>
  {
    public int NumberOfSides { get; set; }

    public CenteredPolygonalNumber(int numberOfSides)
      => NumberOfSides = numberOfSides;

    #region Static methods
    /// <summary></summary>
    /// <remarks>This function runs indefinitely, if allowed.</remarks>
    
    public static System.Collections.Generic.IEnumerable<(int minCenteredNumber, int maxCenteredNumber, int count)> GetLayers(int numberOfSides)
    {
      yield return (1, 1, 1);

      foreach (var v in new CenteredPolygonalNumber(numberOfSides).GetSequence().PartitionTuple2(false, (min, max, index) => (min, max)))
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

    /// <summary></summary>
    /// <see cref="https://en.wikipedia.org/wiki/Centered_polygonal_number"/>
    /// <remarks>This function runs indefinitely, if allowed.</remarks>
    
    public static System.Collections.Generic.IEnumerable<int> GetSequence(int numberOfSides)
    {
      for (var index = 0; ; index++)
        yield return GetNumber(index, numberOfSides);
    }
    #endregion Static methods

    #region Implemented interfaces
    // INumberSequence
    
    public System.Collections.Generic.IEnumerable<int> GetSequence()
      => GetSequence(NumberOfSides);

    
    public System.Collections.Generic.IEnumerator<int> GetEnumerator()
      => GetSequence().GetEnumerator();
    
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();
    #endregion Implemented interfaces
  }
}
