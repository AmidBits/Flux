namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Convert the cartesian 2D coordinate (x, y) where 'right-center' is 'zero' (i.e. positive-x and neutral-y) to a counter-clockwise rotation angle [0, PI*2] (radians).</summary>
    public static double CartesianToRotationAngle(double x, double y)
      => System.Math.Atan2(y, x) is var atan2 && atan2 < 0 ? PiX2 + atan2 : atan2;
    /// <summary>Convert the cartesian 2D coordinate (x, y) where 'center-up' is 'zero' (i.e. neutral-x and positive-y) to a clockwise rotation angle [0, PI*2] (radians).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    public static double CartesianToRotationAngleEx(double x, double y)
      => PiX2 - CartesianToRotationAngle(y, -x);

    /// <summary>Convert the angle specified in degrees to gradians (grads).</summary>
    public static double DegreeToGradian(double degrees)
      => degrees * (10.0 / 9.0);
    /// <summary>Convert the angle specified in degrees to radians.</summary>
    public static double DegreeToRadian(double degrees)
      => degrees * (System.Math.PI / 180);

    /// <summary>Convert the angle specified in gradians (grads) to degrees.</summary>
    public static double GradianToDegree(double gradians)
      => gradians * (9.0 / 10.0);
    /// <summary>Convert the angle specified in gradians (grads) to radians.</summary>
    public static double GradianToRadian(double gradians)
      => gradians * (System.Math.PI / 200);

    /// <summary>Convert the angle specified in radians to degrees.</summary>
    public static double RadianToDegree(double radians)
      => radians * (180 / System.Math.PI);
    /// <summary>Convert the angle specified in radians to gradians (grads).</summary>
    public static double RadianToGradian(double radians)
      => radians * (200 / System.Math.PI);

    /// <summary>Convert the specified counter-clockwise rotation angle [0, PI*2] (radians) where 'zero' is 'right-center' (i.e. positive-x and neutral-y) to a cartesian 2D coordinate (x, y).</summary>
    public static (double x, double y) RotationAngleToCartesian(double radians)
      => (System.Math.Cos(radians), System.Math.Sin(radians));
    /// <summary>Convert the specified counter-clockwise rotation angle [0, PI*2] (radians) where 'zero' is 'center-up' (i.e. neutral-x and positive-y) to a cartesian 2D coordinate (x, y).</summary>
    public static (double x, double y) RotationAngleToCartesianEx(double radians)
      => RotationAngleToCartesian(PiX2 - (radians >= PiX2 ? radians % PiX2 : radians) + PiOver2);
  }
}
