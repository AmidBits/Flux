namespace Flux
{
  public static partial class UniqueIndex
  {
    /// <summary>Converts a 2D cartesian (<paramref name="x"/>, <paramref name="y"/>) coordinate, to a uniquely mapped index of a grid with the <paramref name="width"/>.</summary>
    public static System.Numerics.BigInteger ConvertCartesian2ToUniqueIndex(System.Numerics.BigInteger x, System.Numerics.BigInteger y, System.Numerics.BigInteger width)
      => x + (y * width);

    /// <summary>Converts a 3D cartesian (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>) coordinate, to a uniquely mapped index of a grid with the <paramref name="width"/> and <paramref name="height"/>.</summary>
    public static System.Numerics.BigInteger Cartesian3ToUniqueIndex(System.Numerics.BigInteger x, System.Numerics.BigInteger y, System.Numerics.BigInteger z, System.Numerics.BigInteger width, System.Numerics.BigInteger height)
      => x + (y * width) + (z * width * height);

    /// <summary>Converts a uniquely mapped <paramref name="index"/> of a grid with the <paramref name="width"/>, to a 2D cartesian (x, y) coordinate.</summary>
    public static (System.Numerics.BigInteger x, System.Numerics.BigInteger y) ConvertUniqueIndexToCartesian2(System.Numerics.BigInteger index, System.Numerics.BigInteger width)
      => (
        index % width,
        index / width
      );

    /// <summary>Converts a uniquely mapped <paramref name="index"/> of a cube with the <paramref name="width"/> and <paramref name="height"/>, to a 3D cartesian (x, y, z) coordinate.</summary>
    public static (System.Numerics.BigInteger x, System.Numerics.BigInteger y, System.Numerics.BigInteger z) ConvertUniqueIndexToCartesian3(System.Numerics.BigInteger index, System.Numerics.BigInteger width, System.Numerics.BigInteger height)
    {
      var xy = width * height;
      var irxy = index % xy;

      return (
        irxy % width,
        irxy / width,
        index / xy
      );
    }
  }
}
