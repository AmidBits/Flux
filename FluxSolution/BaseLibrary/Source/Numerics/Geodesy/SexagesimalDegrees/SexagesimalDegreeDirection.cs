namespace Flux
{
  public static partial class SexagesimalDegreesExtensionMethods
  {
    public static CardinalDirection ToCardinalDirection(this SexagesimalDegreeDirection direction, bool isNegative)
      => direction switch
      {
        SexagesimalDegreeDirection.WestEast => isNegative ? CardinalDirection.W : CardinalDirection.E,
        SexagesimalDegreeDirection.NorthSouth => isNegative ? CardinalDirection.S : CardinalDirection.N,
        _ => throw new System.ArgumentOutOfRangeException(nameof(direction))
      };
  }

  public enum SexagesimalDegreeDirection
  {
    None,
    /// <summary>From negative (west) to positive (east).</summary>
    WestEast,
    /// <summary>From negative (south) to positive) (north).</summary>
    NorthSouth
  }
}
