namespace Flux
{
  #region ExtensionMethods
  public static partial class Em
  {
    public static float AbsoluteSum(this System.Numerics.Vector3 source)
      => System.Math.Abs(source.X) + System.Math.Abs(source.Y) + System.Math.Abs(source.Z);

    /// <summary>(3D) Calculate the angle between the source vector and the specified target vector.
    /// When dot eq 0 then the vectors are perpendicular.
    /// When dot gt 0 then the angle is less than 90 degrees (dot=1 can be interpreted as the same direction).
    /// When dot lt 0 then the angle is greater than 90 degrees (dot=-1 can be interpreted as the opposite direction).
    /// </summary>
    public static float AngleTo(this System.Numerics.Vector3 a, System.Numerics.Vector3 b)
      => (float)System.Math.Acos(System.Math.Clamp(System.Numerics.Vector3.Dot(a, b) / (a.EuclideanLength() * b.EuclideanLength()), -1f, 1f));

    /// <summary>Compute the Chebyshev length of the source vector. To compute the Chebyshev distance between two vectors, ChebyshevLength(target - source).</summary>
    /// <see href="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    public static float ChebyshevLength(this System.Numerics.Vector3 source, float edgeLength)
      => System.Math.Max(
        System.Math.Abs(source.X / edgeLength),
        System.Math.Max(
          System.Math.Abs(source.Y / edgeLength),
          System.Math.Abs(source.Z / edgeLength)
        )
      );

    /// <summary>Returns the dot product of two non-normalized 3D vectors.</summary>
    /// <remarks>This method saves a square root computation by doing a two-in-one.</remarks>
    /// <see href="https://gamedev.stackexchange.com/a/89832/129646"/>
    public static float DotProductEx(this System.Numerics.Vector3 a, System.Numerics.Vector3 b)
      => (float)(System.Numerics.Vector3.Dot(a, b) / System.Math.Sqrt(a.EuclideanLengthSquared() * b.EuclideanLengthSquared()));

    /// <summary>Compute the Euclidean length of the vector.</summary>
    public static float EuclideanLength(this System.Numerics.Vector3 source)
      => (float)System.Math.Sqrt(source.EuclideanLengthSquared());

    /// <summary>Compute the Euclidean length squared of the vector.</summary>
    public static float EuclideanLengthSquared(this System.Numerics.Vector3 source)
      => source.X * source.X + source.Y * source.Y + source.Z * source.Z;

    /// <summary>Lerp is a linear interpolation between point a (unit interval = 0.0) and point b (unit interval = 1.0).</summary>
    public static System.Numerics.Vector3 Lerp(this System.Numerics.Vector3 source, System.Numerics.Vector3 target, float mu)
    {
      var imu = 1 - mu;

      return new(source.X * imu + target.X * mu, source.Y * imu + target.Y * mu, source.Z * imu + target.Z * mu);
    }

    /// <summary>Compute the Manhattan length (or magnitude) of the vector. To compute the Manhattan distance between two vectors, ManhattanLength(target - source).</summary>
    /// <see href="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public static float ManhattanLength(this System.Numerics.Vector3 source, float edgeLength)
      => System.Math.Abs(source.X / edgeLength) + System.Math.Abs(source.Y / edgeLength) + System.Math.Abs(source.Z / edgeLength);

    /// <summary>Lerp is a normalized linear interpolation between point a (unit interval = 0.0) and point b (unit interval = 1.0).</summary>
    public static System.Numerics.Vector3 Nlerp(this System.Numerics.Vector3 source, System.Numerics.Vector3 target, float mu)
      => source.Lerp(target, mu).Normalized();

    /// <summary>Creates a new normalized <see cref="System.Numerics.Vector3"/> from a <see cref="System.Numerics.Vector3"/>.</summary>
    public static System.Numerics.Vector3 Normalized(this System.Numerics.Vector3 source)
      => (float)source.EuclideanLength() is var m && m != 0d ? new System.Numerics.Vector3(source.X, source.Y, source.Z) / m : source;

    /// <summary>Returns the orthant (quadrant) of the 2D vector using the specified center and orthant numbering.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Orthant"/>
    public static int OrthantNumber(this System.Numerics.Vector3 source, System.Numerics.Vector3 center, OrthantNumbering numbering)
      => numbering switch
      {
        OrthantNumbering.Traditional => source.Z >= center.Z ? (source.Y >= center.Y ? (source.X >= center.X ? 0 : 1) : (source.X >= center.X ? 3 : 2)) : (source.Y >= center.Y ? (source.X >= center.X ? 7 : 6) : (source.X >= center.X ? 4 : 5)),
        OrthantNumbering.BinaryNegativeAs1 => (source.X >= center.X ? 0 : 1) + (source.Y >= center.Y ? 0 : 2) + (source.Z >= center.Z ? 0 : 4),
        OrthantNumbering.BinaryPositiveAs1 => (source.X < center.X ? 0 : 1) + (source.Y < center.Y ? 0 : 2) + (source.Z < center.Z ? 0 : 4),
        _ => throw new System.ArgumentOutOfRangeException(nameof(numbering))
      };

    /// <summary>Always works if the input is non-zero. Does not require the input to be normalized, and does not normalize the output.</summary>
    /// <see cref="http://lolengine.net/blog/2013/09/21/picking-orthogonal-vector-combing-coconuts"/>
    public static System.Numerics.Vector3 Orthogonal(this System.Numerics.Vector3 source)
      => System.Math.Abs(source.X) > System.Math.Abs(source.Z)
      ? new(
          -source.Y,
          source.X,
          0
        )
      : new(
          0,
          -source.X,
          source.Y
        );

    /// <summary>Slerp travels the torque-minimal path, which means it travels along the straightest path the rounded surface of a sphere.</summary>>
    public static System.Numerics.Vector3 Slerp(this System.Numerics.Vector3 source, System.Numerics.Vector3 target, float mu)
    {
      var dp = System.Math.Clamp(System.Numerics.Vector3.Dot(source, target), -1, 1); // Ensure precision doesn't exceed acos limits.
      var theta = System.Math.Acos(dp) * mu; // Angle between start and desired.
      var (sin, cos) = System.Math.SinCos(theta);

      return new((float)(source.X * cos + (target.X - source.X) * dp * sin), (float)(source.Y * cos + (target.Y - source.Y) * dp * sin), (float)(source.Z * cos + (target.Z - source.Z) * dp * sin));
    }

    /// <summary>Creates a new <see cref="System.Numerics.Vector2"/> from a <see cref="System.Numerics.Vector3"/> using the X and Y.</summary>
    public static System.Numerics.Vector2 ToVector2XY(this System.Numerics.Vector3 source)
      => new(source.X, source.Y);

    /// <summary>Creates a new <see cref="System.Numerics.Vector3"/> from a <see cref="System.Numerics.Vector3"/> using a <see cref="RoundingMode"/>.</summary>
    public static System.Numerics.Vector3 ToVector3(this System.Numerics.Vector3 source, RoundingMode mode, out System.Numerics.Vector3 result)
      => result = new((float)((double)(source.X)).Round(mode), (float)((double)(source.Y)).Round(mode), (float)((double)(source.Z)).Round(mode));

    /// <summary>Returns a quaternion from two vectors.
    /// <para><see href="http://lolengine.net/blog/2014/02/24/quaternion-from-two-vectors-final"/></para>
    /// <para><see href="http://lolengine.net/blog/2013/09/18/beautiful-maths-quaternion-from-vectors"/></para>
    /// </summary>
    public static System.Numerics.Quaternion ToQuaternion(this System.Numerics.Vector3 source, System.Numerics.Vector3 target)
    {
      var norm_u_norm_v = System.Math.Sqrt(System.Numerics.Vector3.Dot(source, source) * System.Numerics.Vector3.Dot(target, target));
      var real_part = (float)norm_u_norm_v + System.Numerics.Vector3.Dot(source, target);

      System.Numerics.Vector3 w;

      if (real_part < Maths.Epsilon1E7 * norm_u_norm_v)
      {
        real_part = 0;

        // If u and v are exactly opposite, rotate 180 degrees around an arbitrary orthogonal axis. Axis normalisation can happen later, when we normalise the quaternion.
        w = System.Math.Abs(source.X) > System.Math.Abs(source.Z) ? new System.Numerics.Vector3(-source.Y, source.X, 0) : new System.Numerics.Vector3(0, -source.Z, source.Y);
      }
      else
      {
        w = System.Numerics.Vector3.Cross(source, target);
      }

      return System.Numerics.Quaternion.Normalize(new System.Numerics.Quaternion(w.X, w.Y, w.Z, real_part));
    }
  }
  #endregion ExtensionMethods
}
