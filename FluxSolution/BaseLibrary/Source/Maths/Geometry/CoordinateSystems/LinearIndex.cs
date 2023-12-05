namespace Flux
{
  public static partial class LinearIndex
  {
    /// <summary>Converts cartesian 2D (<paramref name="x"/>, <paramref name="y"/>) coordinates to a linear index of a grid with the <paramref name="width"/>.</summary>
    public static TSelf ConvertCartesian2ToLinearIndex<TSelf>(TSelf x, TSelf y, TSelf width)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => x + (y * width);

    /// <summary>Converts cartesian 3D (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>) coordinates to a linear index of a cube with the <paramref name="width"/> and <paramref name="height"/>.</summary>
    public static TSelf ConvertCartesian3ToLinearIndex<TSelf>(TSelf x, TSelf y, TSelf z, TSelf width, TSelf height)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => x + (y * width) + (z * width * height);

    /// <summary>Converts a <paramref name="linearIndex"/> of a grid with the <paramref name="width"/> to cartesian 2D (x, y) coordinates.</summary>
    public static (TSelf x, TSelf y) ConvertLinearIndexToCartesian2<TSelf>(TSelf linearIndex, TSelf width)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => (
        linearIndex % width,
        linearIndex / width
      );

    /// <summary>Converts a <paramref name="linearIndex"/> of a cube with the <paramref name="width"/> and <paramref name="height"/>, to cartesian 3D (x, y, z) coordinates.</summary>
    public static (TSelf x, TSelf y, TSelf z) ConvertLinearIndexToCartesian3<TSelf>(TSelf linearIndex, TSelf width, TSelf height)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var xy = width * height;
      var irxy = linearIndex % xy;

      return (
        irxy % width,
        irxy / width,
        linearIndex / xy
      );
    }
  }
}
