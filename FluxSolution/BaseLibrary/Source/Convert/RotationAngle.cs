namespace Flux
{
  public static partial class Convert
  {
    /// <summary>Return the rotation angle using the cartesian 2D coordinate (x, y) where 'right-center' is 'zero' (i.e. positive-x and neutral-y) to a counter-clockwise rotation angle [0, PI*2] (radians). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    public static TSelf Cartesian2ToRotationAngle<TSelf>(TSelf x, TSelf y)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
       => TSelf.Atan2(y, x) is var atan2 && atan2 < TSelf.Zero ? TSelf.Tau + atan2 : atan2;

    /// <summary>Convert the cartesian 2D coordinate (x, y) where 'center-up' is 'zero' (i.e. neutral-x and positive-y) to a clockwise rotation angle [0, PI*2] (radians). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    public static TSelf Cartesian2ToRotationAngleEx<TSelf>(TSelf x, TSelf y)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => TSelf.Tau - Cartesian2ToRotationAngle(y, -x);

    /// <summary>Convert the specified counter-clockwise rotation angle [0, PI*2] (radians) where 'zero' is 'right-center' (i.e. positive-x and neutral-y) to a cartesian 2D coordinate (x, y). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>

    public static (TSelf x, TSelf y) RotationAngleToCartesian2<TSelf>(TSelf angle)
      where TSelf : System.Numerics.ITrigonometricFunctions<TSelf>
      => (TSelf.Cos(angle), TSelf.Sin(angle));

    /// <summary>Convert the specified clockwise rotation angle [0, PI*2] (radians) where 'zero' is 'center-up' (i.e. neutral-x and positive-y) to a cartesian 2D coordinate (x, y). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>

    public static (TSelf x, TSelf y) RotationAngleToCartesian2Ex<TSelf>(TSelf angle)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => RotationAngleToCartesian2(TSelf.Tau - (angle % TSelf.Tau is var rad && rad < TSelf.Zero ? rad + TSelf.Tau : rad) + TSelf.Pi.Divide(2));
  }
}
