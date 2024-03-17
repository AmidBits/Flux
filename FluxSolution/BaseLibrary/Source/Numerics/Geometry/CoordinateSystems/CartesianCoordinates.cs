namespace Flux
{
  /// <summary>
  /// <para>This class contains functionality related to cartesian coordinates that does not have an context elsewhere, e.g. conversion to/from linear index (which is simply some integer).</para>
  /// </summary>
  public static partial class CartesianCoordinates
  {
    /// <summary>Comptue the Chebyshev length (using the specified edgeLength) of the cartesian coordinates.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    public static TSelf ChebyshevLength<TSelf>(TSelf edgeLength, params TSelf[] cartesianCoordinates)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      var max = TSelf.Zero;

      for (var i = cartesianCoordinates.Length - 1; i >= 0; i--)
        if (TSelf.Abs(cartesianCoordinates[i]) is var current && current > max)
          max = current;

      return max / edgeLength;
    }

    //=> target.m_v256.Subtract(source.m_v256).Abs().Divide(stepSize).Max3D().GetElement(0) / stepSize;

    /// <summary>Converts cartesian 2D (<paramref name="x"/>, <paramref name="y"/>) coordinates to a linear index of a grid with the <paramref name="width"/> (the length of the x-axis).</summary>
    public static TSelf ConvertCartesian2ToLinearIndex<TSelf>(TSelf x, TSelf y, TSelf width)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => x + (y * width);

    /// <summary>Converts cartesian 3D (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>) coordinates to a linear index of a cube with the <paramref name="width"/> (the length of the x-axis) and <paramref name="height"/> (the length of the y-axis).</summary>
    public static TSelf ConvertCartesian3ToLinearIndex<TSelf>(TSelf x, TSelf y, TSelf z, TSelf width, TSelf height)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => x + (y * width) + (z * width * height);

    /// <summary>Converts a <paramref name="linearIndex"/> of a grid with the <paramref name="width"/> (the length of the x-axis) to cartesian 2D (x, y) coordinates.</summary>
    public static (TSelf x, TSelf y) ConvertLinearIndexToCartesian2<TSelf>(TSelf linearIndex, TSelf width)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
       => (
        linearIndex % width,
        linearIndex / width
      );

    /// <summary>Converts a <paramref name="linearIndex"/> of a cube with the <paramref name="width"/> (the length of the x-axis) and <paramref name="height"/> (the length of the y-axis), to cartesian 3D (x, y, z) coordinates.</summary>
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

    /// <summary>Compute the Manhattan length (using the specified edgeLength) of the cartesian coordinates.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public static TSelf ManhattanLength<TSelf>(TSelf edgeLength, params TSelf[] cartesianCoordinates)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      var sum = TSelf.Zero;

      for (var i = cartesianCoordinates.Length - 1; i >= 0; i--)
        sum += TSelf.Abs(cartesianCoordinates[i]);

      return sum / edgeLength;
    }
  }
}
