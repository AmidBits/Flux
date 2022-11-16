namespace Flux
{
  public static partial class Vector
  {
    /// <summary>Returns the Manhattan length (magnitude) for the 2D vector.</summary>
    public static TSelf ManhattanLength<TSelf>(TSelf x, TSelf y, TSelf edgeLength)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.Abs(x / edgeLength) + TSelf.Abs(y / edgeLength);

    /// <summary>Returns the Manhattan length (magnitude) for the 3D vector.</summary>
    public static TSelf ManhattanLength<TSelf>(TSelf x, TSelf y, TSelf z, TSelf edgeLength)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.Abs(x / edgeLength) + TSelf.Abs(y / edgeLength) + TSelf.Abs(z / edgeLength);
  }
}
