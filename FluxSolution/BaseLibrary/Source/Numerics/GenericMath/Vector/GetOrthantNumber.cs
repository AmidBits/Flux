namespace Flux
{
  public static partial class Vector
  {
    /// <summary>Returns the orthant (quadrant) of the 2D vector using the specified center and orthant numbering.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Orthant"/>
    [System.Diagnostics.Contracts.Pure]
    public static int GetOrthantNumber<TSelf>(TSelf x, TSelf y, OrthantNumbering numbering)
      where TSelf : System.Numerics.INumber<TSelf>
      => numbering switch
      {
        OrthantNumbering.Traditional => y >= TSelf.Zero ? (TSelf.Zero >= TSelf.Zero ? 0 : 1) : (x >= TSelf.Zero ? 3 : 2),
        OrthantNumbering.BinaryNegativeAs1 => (x >= TSelf.Zero ? 0 : 1) + (y >= TSelf.Zero ? 0 : 2),
        OrthantNumbering.BinaryPositiveAs1 => (x < TSelf.Zero ? 0 : 1) + (y < TSelf.Zero ? 0 : 2),
        _ => throw new System.ArgumentOutOfRangeException(nameof(numbering))
      };

    /// <summary>Returns the orthant (octant) of the 3D vector using the specified center and orthant numbering.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Orthant"/>
    public static int GetOrthantNumber<TSelf>(TSelf x, TSelf y, TSelf z, OrthantNumbering numbering)
      where TSelf : System.Numerics.INumber<TSelf>
      => numbering switch
      {
        OrthantNumbering.Traditional => z >= TSelf.Zero ? (y >= TSelf.Zero ? (x >= TSelf.Zero ? 0 : 1) : (x >= TSelf.Zero ? 3 : 2)) : (y >= TSelf.Zero ? (x >= TSelf.Zero ? 7 : 6) : (x >= TSelf.Zero ? 4 : 5)),
        OrthantNumbering.BinaryNegativeAs1 => (x >= TSelf.Zero ? 0 : 1) + (y >= TSelf.Zero ? 0 : 2) + (z >= TSelf.Zero ? 0 : 4),
        OrthantNumbering.BinaryPositiveAs1 => (x < TSelf.Zero ? 0 : 1) + (y < TSelf.Zero ? 0 : 2) + (z < TSelf.Zero ? 0 : 4),
        _ => throw new System.ArgumentOutOfRangeException(nameof(numbering))
      };
  }
}
