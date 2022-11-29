namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Compute the Chebyshev length of the source vector. To compute the Chebyshev distance between two vectors, ChebyshevLength(target - source).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    public static TSelf ChebyshevLength<TSelf>(this ICartesianCoordinate2<TSelf> source, TSelf edgeLength)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.Max(TSelf.Abs(source.X / edgeLength), TSelf.Abs(source.Y / edgeLength));

    /// <summary>Compute the Euclidean length of the vector.</summary>
    public static TSelf EuclideanLength<TSelf>(this ICartesianCoordinate2<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => TSelf.Sqrt(source.EuclideanLengthSquared());

    /// <summary>Compute the Euclidean length squared of the vector.</summary>
    public static TSelf EuclideanLengthSquared<TSelf>(this ICartesianCoordinate2<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => source.X * source.X + source.Y * source.Y;

    ///// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    //public static CartesianCoordinate2<TSelf> InterpolateLinear<TSelf>(this ICartesianCoordinate2<TSelf> p1, ICartesianCoordinate2<TSelf> p2, TSelf mu, I2NodeInterpolatable<TSelf, TSelf> mode)
    //  where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    //{
    //  mode ??= new Interpolation.LinearInterpolation<TSelf, TSelf>();

    //  return new(mode.Interpolate2Node(p1.X, p2.X, mu), mode.Interpolate2Node(p1.Y, p2.Y, mu));
    //}

    ///// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    //public static CartesianCoordinate2<TSelf> Interpolate<TSelf>(this ICartesianCoordinate2<TSelf> p0, ICartesianCoordinate2<TSelf> p1, ICartesianCoordinate2<TSelf> p2, ICartesianCoordinate2<TSelf> p3, TSelf mu, I4NodeInterpolatable<TSelf, TSelf> mode)
    //  where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    //{
    //  mode ??= new Interpolation.CubicInterpolation<TSelf, TSelf>();

    //  return new(mode.Interpolate4Node(p0.X, p1.X, p2.X, p3.X, mu), mode.Interpolate4Node(p0.Y, p1.Y, p2.Y, p3.Y, mu));
    //}

    /// <summary>Lerp is a linear interpolation between point a (unit interval = 0.0) and point b (unit interval = 1.0).</summary>
    public static CartesianCoordinate2<TSelf> Lerp<TSelf>(this ICartesianCoordinate2<TSelf> source, ICartesianCoordinate2<TSelf> target, TSelf mu)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    {
      var imu = TSelf.One - mu;

      return new(source.X * imu + target.X * mu, source.Y * imu + target.Y * mu);
    }

    /// <summary>Compute the Manhattan length (or magnitude) of the vector. To compute the Manhattan distance between two vectors, ManhattanLength(target - source).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public static TSelf ManhattanLength<TSelf>(this ICartesianCoordinate2<TSelf> source, TSelf edgeLength)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.Abs(source.X / edgeLength) + TSelf.Abs(source.Y / edgeLength);

    public static CartesianCoordinate2<TSelf> Nlerp<TSelf>(this ICartesianCoordinate2<TSelf> source, ICartesianCoordinate2<TSelf> target, TSelf mu)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => Lerp(source, target, mu).Normalized();

    public static CartesianCoordinate2<TSelf> Normalized<TSelf>(this ICartesianCoordinate2<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => source.EuclideanLength() is var m && m != TSelf.Zero ? source.ToCartesianCoordinate2<TSelf>() / m : source.ToCartesianCoordinate2<TSelf>();

    /// <summary>Returns the orthant (quadrant) of the 2D vector using the specified center and orthant numbering.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Orthant"/>
    public static int OrthantNumber<TSelf>(this ICartesianCoordinate2<TSelf> source, ICartesianCoordinate2<TSelf> center, OrthantNumbering numbering)
      where TSelf : System.Numerics.INumber<TSelf>
      => numbering switch
      {
        OrthantNumbering.Traditional => source.Y >= center.Y ? (source.X >= center.X ? 0 : 1) : (source.X >= center.X ? 3 : 2),
        OrthantNumbering.BinaryNegativeAs1 => (source.X >= center.X ? 0 : 1) + (source.Y >= center.Y ? 0 : 2),
        OrthantNumbering.BinaryPositiveAs1 => (source.X < center.X ? 0 : 1) + (source.Y < center.Y ? 0 : 2),
        _ => throw new System.ArgumentOutOfRangeException(nameof(numbering))
      };

    /// <summary>Returns a point -90 degrees perpendicular to the point, i.e. the point rotated 90 degrees counter clockwise. Only X and Y.</summary>
    public static CartesianCoordinate2<TSelf> PerpendicularCcw<TSelf>(this ICartesianCoordinate2<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => new(
        -source.Y,
        source.X
      );

    /// <summary>Returns a point 90 degrees perpendicular to the point, i.e. the point rotated 90 degrees clockwise. Only X and Y.</summary>
    public static CartesianCoordinate2<TSelf> PerpendicularCw<TSelf>(this ICartesianCoordinate2<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => new(
        source.Y,
        -source.X
      );

    /// <summary>Slerp travels the torque-minimal path, which means it travels along the straightest path the rounded surface of a sphere.</summary>>
    public static CartesianCoordinate2<TSelf> Slerp<TSelf>(this ICartesianCoordinate2<TSelf> source, ICartesianCoordinate2<TSelf> target, TSelf mu)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    {
      var dp = TSelf.Clamp(ICartesianCoordinate2<TSelf>.DotProduct(source, target), -TSelf.One, TSelf.One); // Ensure precision doesn't exceed acos limits.
      var theta = TSelf.Acos(dp) * mu; // Angle between start and desired.
      var cos = TSelf.Cos(theta);
      var sin = TSelf.Sin(theta);

      return new(source.X * cos + (target.X - source.X) * dp * sin, source.Y * cos + (target.Y - source.Y) * dp * sin);
    }

    public static TSelf SumOfAbsoluteCoordinates<TSelf>(this ICartesianCoordinate2<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.Abs(source.X) + TSelf.Abs(source.Y);

    /// <summary>Converts the <see cref="ICartesianCoordinate2{TSelf}"/> to a <see cref="CartesianCoordinate2"/>.</summary>
    public static CartesianCoordinate2<TSelf> ToCartesianCoordinate2<TSelf>(this ICartesianCoordinate2<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => source is CartesianCoordinate2<TSelf> cc ? cc : new(source.X, source.Y);

    /// <summary>Converts the <see cref="ICartesianCoordinate2{TSelf}"/> to a <see cref="PolarCoordinate"/>.</summary>
    public static PolarCoordinate<TSelf> ToPolarCoordinate<TSelf>(this ICartesianCoordinate2<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => new(
        TSelf.Sqrt(source.X * source.X + source.Y * source.Y),
        TSelf.Atan2(source.Y, source.X)
      );

    ///// <summary>Convert the cartesian 2D coordinate (x, y) where 'right-center' is 'zero' (i.e. positive-x and neutral-y) to a counter-clockwise rotation angle [0, PI*2] (radians). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</summary>
    ///// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    //public static TSelf ConvertCartesianCoordinate2ToRotationAngle<TSelf>(TSelf x, TSelf y) => TSelf.Atan2(y, x) is var atan2 && atan2 < 0 ? Constants.PiX2 + atan2 : atan2;
    ///// <summary>Convert the cartesian 2D coordinate (x, y) where 'center-up' is 'zero' (i.e. neutral-x and positive-y) to a clockwise rotation angle [0, PI*2] (radians). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</summary>
    ///// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    //public static TSelf ConvertCartesianCoordinate2ToRotationAngleEx<TSelf>(TSelf x, TSelf y) => Constants.PiX2 - ConvertCartesianCoordinate2 < TSelf > ToRotationAngle(y, -x); // Pass the cartesian vector (x, y) rotated 90 degrees counter-clockwise.

    /// <summary>Return the rotation angle using the cartesian 2D coordinate (x, y) where 'right-center' is 'zero' (i.e. positive-x and neutral-y) to a counter-clockwise rotation angle [0, PI*2] (radians). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    public static Angle ToRotationAngle<TSelf>(this ICartesianCoordinate2<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => (Angle)double.CreateChecked(TSelf.Atan2(source.Y, source.X) is var atan2 && atan2 < TSelf.Zero ? TSelf.Tau + atan2 : atan2);

    /// <summary>Convert the cartesian 2D coordinate (x, y) where 'center-up' is 'zero' (i.e. neutral-x and positive-y) to a clockwise rotation angle [0, PI*2] (radians). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    public static Angle ToRotationAngleEx<TSelf>(this ICartesianCoordinate2<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => (Angle)double.CreateChecked(TSelf.Tau - (TSelf.Atan2(source.Y, -source.X) is var atan2 && atan2 < TSelf.Zero ? TSelf.Tau + atan2 : atan2));
  }

  //public static partial class ExtensionMethods
  //{
  //  /// <summary>Convert the cartesian 2D coordinate (x, y) where 'right-center' is 'zero' (i.e. positive-x and neutral-y) to a counter-clockwise rotation angle [0, PI*2] (radians). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</summary>
  //  /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
  //  public static TSelf ToRotationAngle<TSelf>(this ICartesianCoordinate2<TSelf> cartesianCoordinate)
  //    where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
  //    => TSelf.Atan2(cartesianCoordinate.Y, cartesianCoordinate.X) is var atan2 && atan2 < TSelf.Zero ? TSelf.Tau + atan2 : atan2;

  //  /// <summary>Convert the cartesian 2D coordinate (x, y) where 'center-up' is 'zero' (i.e. neutral-x and positive-y) to a clockwise rotation angle [0, PI*2] (radians). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</summary>
  //  /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
  //  public static TSelf ToRotationAngleEx<TSelf>(this ICartesianCoordinate2<TSelf> cartesianCoordinate)
  //    where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
  //    => TSelf.Tau - ToRotationAngle(cartesianCoordinate); // Pass the cartesian vector (x, y) rotated 90 degrees counter-clockwise.

  //  /// <summary>Converts the cartesian 2D coordinates to rounded cartesian 2D coordinates.</summary>
  //  public static (TSelf x, TSelf y) ToCartesianCoordinates<TSelf>(this ICartesianCoordinate2<TSelf> cartesianCoordinate, INumberRoundable<TSelf> rounding)
  //    where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
  //    => (rounding.RoundNumber(cartesianCoordinate.X), rounding.RoundNumber(cartesianCoordinate.Y));

  //  /// <summary>Converts the cartesian 2D coordinates to polar coordinates.</summary>
  //  /// <remarks>All angles in radians.</remarks>
  //  public static (TSelf radius, TSelf azimuth) ToPolarCoordinates<TSelf>(this ICartesianCoordinate2<TSelf> cartesianCoordinate)
  //    where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
  //    => (
  //      TSelf.Sqrt(cartesianCoordinate.X * cartesianCoordinate.X + cartesianCoordinate.Y * cartesianCoordinate.Y),
  //      TSelf.Atan2(cartesianCoordinate.Y, cartesianCoordinate.X)
  //    );
  //}

  /// <summary>Cartesian 2D coordinate.</summary>
  public interface ICartesianCoordinate2<TSelf>
    : System.IFormattable
    where TSelf : System.Numerics.INumber<TSelf>
  {
    TSelf X { get; }
    TSelf Y { get; }

    /// <summary>For 2D vectors, the cross product of two vectors, is equivalent to DotProduct(a, CrossProduct(b)), which is consistent with the notion of a "perpendicular dot product", which this is known as.</summary>
    static TSelf CrossProduct(ICartesianCoordinate2<TSelf> a, ICartesianCoordinate2<TSelf> b)
      => a.X * b.Y - a.Y * b.X;

    /// <summary>Returns the dot product of two 2D vectors.</summary>
    static TSelf DotProduct(ICartesianCoordinate2<TSelf> a, ICartesianCoordinate2<TSelf> b)
      => a.X * b.X + a.Y * b.Y;

    string System.IFormattable.ToString(string? format, System.IFormatProvider? provider)
      => $"{GetType().Name} {{ X = {string.Format($"{{0:{format ?? "N6"}}}", X)}, Y = {string.Format($"{{0:{format ?? "N6"}}}", Y)} }}";
  }
}
