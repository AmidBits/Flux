namespace Flux
{
  public static partial class Vector
  {
    /// <summary>Convert the cartesian 2D coordinate (x, y) where 'right-center' is 'zero' (i.e. positive-x and neutral-y) to a counter-clockwise rotation angle [0, PI*2] (radians). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    public static (TSelf x, TSelf y) RotationAngleToCartesian2<TSelf>(TSelf radAngle)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => (TSelf.Cos(radAngle), TSelf.Sin(radAngle));

    /// <summary>Convert the cartesian 2D coordinate (x, y) where 'center-up' is 'zero' (i.e. neutral-x and positive-y) to a clockwise rotation angle [0, PI*2] (radians). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    public static (TSelf x, TSelf y) RotationAngleToCartesian2Ex<TSelf>(TSelf radAngle)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => RotationAngleToCartesian2(TSelf.Tau - (radAngle % TSelf.Tau is var rad && rad < TSelf.Zero ? rad + TSelf.Tau : rad) + TSelf.Pi.Div2());
  }
}
