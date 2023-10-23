namespace Flux
{
  #region ExtensionMethods
  public static partial class GeometryExtensionMethods
  {
    public static float AbsoluteSum(this System.Numerics.Vector2 source)
      => float.Abs(source.X) + float.Abs(source.Y);

    /// <summary>(3D) Calculate the angle between the source vector and the specified target vector.
    /// When dot eq 0 then the vectors are perpendicular.
    /// When dot gt 0 then the angle is less than 90 degrees (dot=1 can be interpreted as the same direction).
    /// When dot lt 0 then the angle is greater than 90 degrees (dot=-1 can be interpreted as the opposite direction).
    /// </summary>
    public static float AngleTo(this System.Numerics.Vector2 a, System.Numerics.Vector2 b)
      => float.Acos(float.Clamp(System.Numerics.Vector2.Dot(a, b) / (a.EuclideanLength() * b.EuclideanLength()), -1, 1));

    /// <summary>Compute the Chebyshev length of the source vector. To compute the Chebyshev distance between two vectors, ChebyshevLength(target - source).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    public static float ChebyshevLength(this System.Numerics.Vector2 source, float edgeLength)
      => float.Max(float.Abs(source.X / edgeLength), float.Abs(source.Y / edgeLength));

    /// <summary>Returns the dot product of two non-normalized 3D vectors.</summary>
    /// <remarks>This method saves a square root computation by doing a two-in-one.</remarks>
    /// <see href="https://gamedev.stackexchange.com/a/89832/129646"/>
    public static float DotProductEx(this System.Numerics.Vector2 a, System.Numerics.Vector2 b)
      => System.Numerics.Vector2.Dot(a, b) / float.Sqrt(a.EuclideanLengthSquared() * b.EuclideanLengthSquared());

    /// <summary>Compute the Euclidean length of the vector.</summary>
    public static float EuclideanLength(this System.Numerics.Vector2 source)
      => float.Sqrt(source.EuclideanLengthSquared());

    /// <summary>Compute the Euclidean length squared of the vector.</summary>
    public static float EuclideanLengthSquared(this System.Numerics.Vector2 source)
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
    public static System.Numerics.Vector2 Lerp(this System.Numerics.Vector2 source, System.Numerics.Vector2 target, float mu)
    {
      var imu = 1 - mu;

      return new(source.X * imu + target.X * mu, source.Y * imu + target.Y * mu);
    }

    /// <summary>Compute the Manhattan length (or magnitude) of the vector. To compute the Manhattan distance between two vectors, ManhattanLength(target - source).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public static float ManhattanLength(this System.Numerics.Vector2 source, float edgeLength)
      => float.Abs(source.X / edgeLength) + float.Abs(source.Y / edgeLength);

    /// <summary>Lerp is a normalized linear interpolation between point a (unit interval = 0.0) and point b (unit interval = 1.0).</summary>
    public static System.Numerics.Vector2 Nlerp(this System.Numerics.Vector2 source, System.Numerics.Vector2 target, float mu)
      => source.Lerp(target, mu).Normalized();

    /// <summary>Creates a new normalized <see cref="CartesianCoordinate2{TSelf}"/> from a <see cref="ICartesianCoordinate2{TSelf}"/>.</summary>
    public static System.Numerics.Vector2 Normalized(this System.Numerics.Vector2 source)
      => source.EuclideanLength() is var m && m != 0 ? new System.Numerics.Vector2(source.X, source.Y) / m : source;

    /// <summary>Returns the orthant (quadrant) of the 2D vector using the specified center and orthant numbering.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Orthant"/>
    public static int OrthantNumber(this System.Numerics.Vector2 source, System.Numerics.Vector2 center, OrthantNumbering numbering)
      => numbering switch
      {
        OrthantNumbering.Traditional => source.Y >= center.Y ? (source.X >= center.X ? 0 : 1) : (source.X >= center.X ? 3 : 2),
        OrthantNumbering.BinaryNegativeAs1 => (source.X >= center.X ? 0 : 1) + (source.Y >= center.Y ? 0 : 2),
        OrthantNumbering.BinaryPositiveAs1 => (source.X < center.X ? 0 : 1) + (source.Y < center.Y ? 0 : 2),
        _ => throw new System.ArgumentOutOfRangeException(nameof(numbering))
      };

    /// <summary>Returns a point -90 degrees perpendicular to the point, i.e. the point rotated 90 degrees counter clockwise. Only X and Y.</summary>
    public static System.Numerics.Vector2 PerpendicularCcw(this System.Numerics.Vector2 source)
      => new(
        -source.Y,
        source.X
      );

    /// <summary>Returns a point 90 degrees perpendicular to the point, i.e. the point rotated 90 degrees clockwise. Only X and Y.</summary>
    public static System.Numerics.Vector2 PerpendicularCw(this System.Numerics.Vector2 source)
      => new(
        source.Y,
        -source.X
      );

    /// <summary>Slerp travels the torque-minimal path, which means it travels along the straightest path the rounded surface of a sphere.</summary>>
    public static System.Numerics.Vector2 Slerp(this System.Numerics.Vector2 source, System.Numerics.Vector2 target, float mu)
    {
      var dp = float.Clamp(System.Numerics.Vector2.Dot(source, target), -1, 1); // Ensure precision doesn't exceed acos limits.
      var theta = float.Acos(dp) * mu; // Angle between start and desired.
      var (sin, cos) = float.SinCos(theta);

      return new(source.X * cos + (target.X - source.X) * dp * sin, source.Y * cos + (target.Y - source.Y) * dp * sin);
    }

    /// <summary>
    /// <para>A slope or gradient of a line is a number that describes both the direction and the steepness of the line (in this case from <paramref name="a"/> to <paramref name="b"/>).</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Slope"/></para>
    /// </summary>
    /// <param name="a">The cartesian source point.</param>
    /// <param name="b">The cartesian target point.</param>
    /// <returns>The slopes for both rise-over-run and run-over-rise.</returns>
    public static (float mx, float my) Slope(System.Numerics.Vector2 a, System.Numerics.Vector2 b)
    {
      var dx = b.X - a.X;
      var dy = b.Y - a.Y;

      return (dx == 0) || (dy == 0) ? (0, 0) : (dx / dy, dy / dx);
    }

    /// <summary>Creates a new <see cref="CartesianCoordinate2{TSelf}"/> from a <see cref="Maths.ICartesianCoordinate2{TResult}"/>.</summary>
    public static System.Numerics.Vector2 ToVector2<TSelf, TResult>(this System.Numerics.Vector2 source, RoundingMode mode, out System.Numerics.Vector2 result)
      => result = new(source.X.Round(mode), source.Y.Round(mode));
  }
  #endregion ExtensionMethods
}
