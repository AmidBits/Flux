namespace Flux
{
  /// <summary>The Hex coordinate system used is the Cube coordinate, and can be specified using </summary>
  /// <see href="https://www.redblobgames.com/grids/hexagons/"/>
  public interface IHexCoordinate<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    /// <summary>The first component or coordinate.</summary>
    TSelf Q { get; }
    /// <summary>The second component or coordinate.</summary>
    TSelf R { get; }
    /// <summary>The third component or coordinate, that can be calculated from Q and R with the formula (-Q - R).</summary>
    TSelf S { get; }

    /// <summary>Returns whether the coordinate make up a valid cube hex, i.e. it satisfies the required cube constraint.</summary>
    public static bool IsCubeCoordinate(TSelf q, TSelf r, TSelf s) => TSelf.IsZero(q + r + s);

    /// <summary>Returns the length of the coordinate.</summary>
    public static TSelf CubeLength(TSelf q, TSelf r, TSelf s) => (TSelf.Abs(q) + TSelf.Abs(r) + TSelf.Abs(s)).Div2();
  }
}
