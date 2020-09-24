namespace Flux
{
  public static partial class Angle
  {
    public const double MultiplierDegreeToRadian = System.Math.PI / 180d;
    public const double MultiplierDegreeToGradian = 10d / 9d;
    public const double MultiplierGradianToDegree = 9d / 10d;
    public const double MultiplierGradianToRadian = System.Math.PI / 200d;
    public const double MultiplierRadianToDegree = 180d / System.Math.PI;
    public const double MultiplierRadianToGradian = 200d / System.Math.PI;

    /// <summary>Convert the cartesian 2D coordinate (x, y) where 'right-center' is 'zero' (i.e. positive-x and neutral-y) to a counter-clockwise rotation angle [0, PI*2] (radians). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    public static double CartesianToRotationAngle(double x, double y)
      => System.Math.Atan2(y, x) is var atan2 && atan2 < 0 ? Maths.PiX2 + atan2 : atan2;
    /// <summary>Convert the cartesian 2D coordinate (x, y) where 'right-center' is 'zero' (i.e. positive-x and neutral-y) to a clockwise rotation angle [0, PI*2] (radians). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    public static double CartesianToRotationAngle(this System.Numerics.Vector2 vector)
      => CartesianToRotationAngle(vector.X, vector.Y);

    /// <summary>Convert the cartesian 2D coordinate (x, y) where 'center-up' is 'zero' (i.e. neutral-x and positive-y) to a clockwise rotation angle [0, PI*2] (radians). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    public static double CartesianToRotationAngleEx(double x, double y)
      => Maths.PiX2 - CartesianToRotationAngle(y, -x);
    /// <summary>Convert the cartesian 2D coordinate (x, y) where 'center-up' is 'zero' (i.e. neutral-x and positive-y) to a clockwise rotation angle [0, PI*2] (radians). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    public static double CartesianToRotationAngleEx(this System.Numerics.Vector2 vector)
      => CartesianToRotationAngleEx(vector.X, vector.Y);

    /// <summary>Convert the angle specified in degrees to gradians (grads).</summary>
    public static double DegreeToGradian(double degrees)
      => degrees * MultiplierDegreeToGradian;
    /// <summary>Convert the angle specified in degrees to radians.</summary>
    public static double DegreeToRadian(double degrees)
      => degrees * MultiplierDegreeToRadian;

    /// <summary>Convert the angle specified in gradians (grads) to degrees.</summary>
    public static double GradianToDegree(double gradians)
      => gradians * MultiplierGradianToDegree;
    /// <summary>Convert the angle specified in gradians (grads) to radians.</summary>
    public static double GradianToRadian(double gradians)
      => gradians * MultiplierGradianToRadian;

    /// <summary>Convert the angle specified in radians to degrees.</summary>
    public static double RadianToDegree(double radians)
      => radians * MultiplierRadianToDegree;
    /// <summary>Convert the angle specified in radians to gradians (grads).</summary>
    public static double RadianToGradian(double radians)
      => radians * MultiplierRadianToGradian;

    /// <summary>Convert the specified counter-clockwise rotation angle [0, PI*2] (radians) where 'zero' is 'right-center' (i.e. positive-x and neutral-y) to a cartesian 2D coordinate (x, y). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</summary>
    public static System.Numerics.Vector2 RotationAngleToCartesian(double radians, out double x, out double y)
      => new System.Numerics.Vector2((float)(x = System.Math.Cos(radians)), (float)(y = System.Math.Sin(radians)));
    /// <summary>Convert the specified clockwise rotation angle [0, PI*2] (radians) where 'zero' is 'center-up' (i.e. neutral-x and positive-y) to a cartesian 2D coordinate (x, y). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</summary>
    public static System.Numerics.Vector2 RotationAngleToCartesianEx(double radians, out double x, out double y)
      => RotationAngleToCartesian(Maths.PiX2 - (radians >= Maths.PiX2 ? radians % Maths.PiX2 : radians) + Maths.PiOver2, out x, out y);
  }
}
