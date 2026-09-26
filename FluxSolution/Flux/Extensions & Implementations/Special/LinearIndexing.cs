namespace Flux
{
  public static class LinearIndexing
  {
    extension<TInteger>(TInteger)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      /// <summary>
      /// <para>Converts cartesian-coordinates (<paramref name="x"/>, <paramref name="y"/>) to a linear index of a grid with the specified <paramref name="width"/> (the length of the x-axis).</para>
      /// </summary>
      public static TInteger Cartesian2ToLinearIndex(TInteger x, TInteger y, TInteger width)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(width);

        return checked(x + (y * width));
      }

      /// <summary>
      /// <para>Converts cartesian-coordinates (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>) to a linear index of a cube with the specified <paramref name="width"/> (the length of the x-axis) and <paramref name="height"/> (the length of the y-axis).</para>
      /// </summary>
      public static TInteger Cartesian3ToLinearIndex(TInteger x, TInteger y, TInteger z, TInteger width, TInteger height)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(width);
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(height);

        return checked(x + (y * width) + (z * width * height));
      }

      /// <summary>
      /// <para>Converts a <paramref name="linearIndex"/> of a grid with the specified <paramref name="width"/> (the length of the x-axis) to cartesian-coordinates (x, y).</para>
      /// </summary>
      /// <param name="linearIndex"></param>
      /// <param name="width"></param>
      /// <returns>A 2D cartesian-coordinate.</returns>
      public static (TInteger X, TInteger Y) LinearIndexToCartesian2(TInteger linearIndex, TInteger width)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(width);

        return (
          linearIndex % width,
          linearIndex / width
        );
      }

      /// <summary>
      /// <para>Converts a <paramref name="linearIndex"/> of a cube with the <paramref name="width"/> (the length of the x-axis) and <paramref name="height"/> (the length of the y-axis), to cartesian 3D (x, y, z) coordinates.</para>
      /// </summary>
      /// <param name="linearIndex"></param>
      /// <param name="width"></param>
      /// <param name="height"></param>
      /// <returns>A 3D cartesian-coordinate.</returns>
      public static (TInteger X, TInteger Y, TInteger Z) LinearIndexToCartesian3(TInteger linearIndex, TInteger width, TInteger height)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(width);
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(height);

        var xy = checked(width * height);
        var irxy = linearIndex % xy;

        return (
          irxy % width,
          irxy / width,
          linearIndex / xy
        );
      }
    }
  }
}
