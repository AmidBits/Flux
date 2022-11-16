namespace Flux
{
  public static partial class Vector
  {
    /// <summary>Returns a point -90 degrees perpendicular to the point, i.e. the point rotated 90 degrees counter clockwise. Only X and Y.</summary>
    public static (TSelf x, TSelf y) PerpendicularCcw2D<TSelf>(TSelf x, TSelf y)
      where TSelf : System.Numerics.INumber<TSelf>
      => (-y, x);
    /// <summary>Returns a point 90 degrees perpendicular to the point, i.e. the point rotated 90 degrees clockwise. Only X and Y.</summary>
    public static (TSelf x, TSelf y) PerpendicularCw2D<TSelf>(TSelf x, TSelf y)
      where TSelf : System.Numerics.INumber<TSelf>
      => (y, -x);
  }
}
