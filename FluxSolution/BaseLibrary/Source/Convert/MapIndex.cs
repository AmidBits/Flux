namespace Flux
{
  public static partial class Convert
  {
#if NET7_0_OR_GREATER

    /// <summary>Converts the <see cref="CartesianCoordinate2{TSelf}"/> to a 'mapped' unique index.</summary>
    /// <remarks>A 2D cartesian coordinate can be uniquely indexed using a grid <paramref name="width"/>. The unique index can also be converted back to a 2D cartesian coordinate with the same grid width value.</remarks>
    public static TSelf Cartesian2ToMapIndex<TSelf>(TSelf x, TSelf y, TSelf width)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => x + (y * width);

    /// <summary>Creates a new 'mapped' unique index from a <see cref="ICartesianCoordinate3{TSelf}"/> using a grid <paramref name="width"/> by <paramref name="height"/>.</summary>
    /// <remarks>A 3D cartesian coordinate can be uniquely indexed using a <paramref name="width"/> and <paramref name="height"/>. The unique index can also be converted back to a 3D cartesian coordinate with the same grid width and height values.</remarks>
    public static TSelf Cartesian3ToMapIndex<TSelf>(TSelf x, TSelf y, TSelf z, TSelf width, TSelf height)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => x + (y * width) + (z * width * height);

    /// <summary>Convert a 'mapped' unique index to a <see cref="CartesianCoordinate2{TSelf}"/>.</summary>
    /// <remarks>An index can be uniquely mapped to 2D cartesian coordinates using a grid <paramref name="width"/>. The 2D cartesian coordinates can also be converted back to a unique index with the same grid width value.</remarks>
    public static (TSelf x, TSelf y) MapIndexToCartesian2<TSelf>(TSelf index, TSelf width)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => (
        index % width,
        index / width
      );

    /// <summary>Convert a 'mapped' unique index to a <see cref="CartesianCoordinate3{TSelf}"/>.</summary>
    /// <remarks>An index can be uniquely mapped to 3D cartesian coordinates using a <paramref name="width"/> and <paramref name="height"/>. The 3D cartesian coordinates can also be converted back to a unique index with the same grid width and height values.</remarks>
    public static (TSelf x, TSelf y, TSelf z) MapIndexToCartesian3<TSelf>(TSelf index, TSelf width, TSelf height)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var xy = width * height;
      var irxy = index % xy;

      return (
        irxy % width,
        irxy / width,
        index / xy
      );
    }

#else

    /// <summary>Converts the <see cref="CartesianCoordinate2{TSelf}"/> to a 'mapped' unique index.</summary>
    /// <remarks>A 2D cartesian coordinate can be uniquely indexed using a grid <paramref name="width"/>. The unique index can also be converted back to a 2D cartesian coordinate with the same grid width value.</remarks>
    public static System.Numerics.BigInteger Cartesian2ToMapIndex(System.Numerics.BigInteger x, System.Numerics.BigInteger y, System.Numerics.BigInteger width)
      => x + (y * width);

    /// <summary>Creates a new 'mapped' unique index from a <see cref="ICartesianCoordinate3{TSelf}"/> using a grid <paramref name="width"/> by <paramref name="height"/>.</summary>
    /// <remarks>A 3D cartesian coordinate can be uniquely indexed using a <paramref name="width"/> and <paramref name="height"/>. The unique index can also be converted back to a 3D cartesian coordinate with the same grid width and height values.</remarks>
    public static System.Numerics.BigInteger Cartesian3ToMapIndex(System.Numerics.BigInteger x, System.Numerics.BigInteger y, System.Numerics.BigInteger z, System.Numerics.BigInteger width, System.Numerics.BigInteger height)
      => x + (y * width) + (z * width * height);

    /// <summary>Convert a 'mapped' unique index to a <see cref="CartesianCoordinate2{TSelf}"/>.</summary>
    /// <remarks>An index can be uniquely mapped to 2D cartesian coordinates using a grid <paramref name="width"/>. The 2D cartesian coordinates can also be converted back to a unique index with the same grid width value.</remarks>
    public static (System.Numerics.BigInteger x, System.Numerics.BigInteger y) MapIndexToCartesian2(System.Numerics.BigInteger index, System.Numerics.BigInteger width)
      => (
        index % width,
        index / width
      );

    /// <summary>Convert a 'mapped' unique index to a <see cref="CartesianCoordinate3{TSelf}"/>.</summary>
    /// <remarks>An index can be uniquely mapped to 3D cartesian coordinates using a <paramref name="width"/> and <paramref name="height"/>. The 3D cartesian coordinates can also be converted back to a unique index with the same grid width and height values.</remarks>
    public static (System.Numerics.BigInteger x, System.Numerics.BigInteger y, System.Numerics.BigInteger z) MapIndexToCartesian3(System.Numerics.BigInteger index, System.Numerics.BigInteger width, System.Numerics.BigInteger height)
    {
      var xy = width * height;
      var irxy = index % xy;

      return (
        irxy % width,
        irxy / width,
        index / xy
      );
    }

#endif
  }
}
