namespace Flux
{
  public static partial class Maths
  {
    /// <summary></summary>
    /// <remarks>This function runs indefinitely, if allowed.</remarks>
    public static System.Collections.Generic.IEnumerable<(int minCenteredNumber, int maxCenteredNumber, int count)> GetCenteredPolygonalLayers(int numberOfSides)
    {
      yield return (1, 1, 1);

      foreach (var v in Flux.Maths.GetCenteredPolygonalSequence(numberOfSides).PartitionTuple(false, (min, max, index) => (min, max)))
      {
        yield return (v.min + 1, v.max, v.max - v.min);
      }
    }

    /// <summary></summary>
    /// <see cref="https://en.wikipedia.org/wiki/Centered_polygonal_number"/>
    public static int GetCenteredPolygonalNumber(int index, int numberOfSides)
      => index >= 0 ? (numberOfSides * index * index + numberOfSides * index + 2) / 2 : throw new System.ArgumentOutOfRangeException(nameof(index));

    /// <summary></summary>
    /// <see cref="https://en.wikipedia.org/wiki/Centered_polygonal_number"/>
    /// <remarks>This function runs indefinitely, if allowed.</remarks>
    public static System.Collections.Generic.IEnumerable<int> GetCenteredPolygonalSequence(int numberOfSides)
    {
      for (var index = 0; ; index++)
      {
        yield return GetCenteredPolygonalNumber(index, numberOfSides);
      }
    }
  }
}
