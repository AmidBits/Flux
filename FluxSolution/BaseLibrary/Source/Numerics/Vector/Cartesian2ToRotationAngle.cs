#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class Vector
  {
    /// <summary>PREVIEW! Convert the cartesian 2D coordinate (x, y) where 'right-center' is 'zero' (i.e. positive-x and neutral-y) to a counter-clockwise rotation angle [0, PI*2] (radians). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    public static TSelf Cartesian2ToRotationAngle<TSelf>(TSelf x, TSelf y)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => TSelf.Atan2(y, x) is var atan2 && atan2 < TSelf.Zero ? TSelf.Pi * (TSelf.One + TSelf.One) + atan2 : atan2;
    /// <summary>PREVIEW! Convert the cartesian 2D coordinate (x, y) where 'center-up' is 'zero' (i.e. neutral-x and positive-y) to a clockwise rotation angle [0, PI*2] (radians). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    public static TSelf Cartesian2ToRotationAngleEx<TSelf>(TSelf x, TSelf y)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => TSelf.Pi * (TSelf.One + TSelf.One) - Cartesian2ToRotationAngle(y, -x); // Pass the cartesian vector (x, y) rotated 90 degrees counter-clockwise.
  }
}
#endif
