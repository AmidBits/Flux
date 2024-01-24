#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class CartesianConversion
  {
    #region LinearIndex (UniqueIndex, etc.)

    /// <summary>Converts cartesian 2D (<paramref name="x"/>, <paramref name="y"/>) coordinates to a linear index of a grid with the <paramref name="width"/>.</summary>
    public static TSelf Cartesian2ToLinearIndex<TSelf>(TSelf x, TSelf y, TSelf width)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => x + (y * width);

    /// <summary>Converts cartesian 3D (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>) coordinates to a linear index of a cube with the <paramref name="width"/> and <paramref name="height"/>.</summary>
    public static TSelf Cartesian3ToLinearIndex<TSelf>(TSelf x, TSelf y, TSelf z, TSelf width, TSelf height)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => x + (y * width) + (z * width * height);

    /// <summary>Converts a <paramref name="linearIndex"/> of a grid with the <paramref name="width"/> to cartesian 2D (x, y) coordinates.</summary>
    public static (TSelf x, TSelf y) LinearIndexToCartesian2<TSelf>(TSelf linearIndex, TSelf width)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => (
        linearIndex % width,
        linearIndex / width
      );

    /// <summary>Converts a <paramref name="linearIndex"/> of a cube with the <paramref name="width"/> and <paramref name="height"/>, to cartesian 3D (x, y, z) coordinates.</summary>
    public static (TSelf x, TSelf y, TSelf z) LinearIndexToCartesian3<TSelf>(TSelf linearIndex, TSelf width, TSelf height)
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

    #endregion // LinearIndex (UniqueIndex, etc.)

    #region RotationAngle

    /// <summary>Convert the cartesian 2D coordinate (x, y) where 'right-center' is 'zero' (i.e. positive-x and neutral-y) to a counter-clockwise rotation angle [0, PI*2] (i.e. radians). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    public static double Cartesian2ToRotationAngle(double x, double y) => System.Math.Atan2(y, x) is var atan2 && atan2 < 0 ? System.Math.Tau + atan2 : atan2;

    /// <summary>Convert the cartesian 2D coordinate (x, y) where 'center-up' is 'zero' (i.e. neutral-x and positive-y) to a clockwise rotation angle [0, PI*2] (i.e. radians). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    public static double Cartesian2ToRotationAngleEx(double x, double y) => System.Math.Tau - Cartesian2ToRotationAngle(y, -x);

    /// <summary>Convert the specified counter-clockwise rotation angle [0, PI*2] (i.e. radians) where 'zero' is 'right-center' (i.e. positive-x and neutral-y) to a cartesian 2D coordinate (x, y). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    public static (double x, double y) RotationAngleToCartesian2(double radAngle)
      => (System.Math.Cos(radAngle), System.Math.Sin(radAngle));

    /// <summary>Convert the specified clockwise rotation angle [0, PI*2] (i.e. radians) where 'zero' is 'center-up' (i.e. neutral-x and positive-y) to a cartesian 2D coordinate (x, y). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    public static (double x, double y) RotationAngleToCartesian2Ex(double radAngle)
      => RotationAngleToCartesian2(System.Math.Tau - (radAngle % System.Math.Tau is var rad && rad < 0 ? rad + System.Math.Tau : rad) + System.Math.PI / 2);
    //=> (-System.Math.Sin(radAngle), System.Math.Cos(radAngle));

    #endregion // RotationAngle
  }
}
#endif
