namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns the angle for the source point to the other two specified points.</summary>>
    public static double AngleBetween(this Geometry.Vector2 source, Geometry.Vector2 before, Geometry.Vector2 after)
      => AngleTo(before - source, after - source);

    /// <summary>(2D) Calculate the angle between the source vector and the specified target vector.
    /// When dot eq 0 then the vectors are perpendicular.
    /// When dot gt 0 then the angle is less than 90 degrees (dot=1 can be interpreted as the same direction).
    /// When dot lt 0 then the angle is greater than 90 degrees (dot=-1 can be interpreted as the opposite direction).
    /// </summary>
    public static double AngleTo(this Geometry.Vector2 source, Geometry.Vector2 target)
      => System.Math.Acos(System.Math.Clamp(DotProduct(source.Normalize(), target.Normalize()), -1, 1));

    /// <summary>Compute the Chebyshev distance between the vectors.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    public static double ChebyshevDistanceTo(this Geometry.Vector2 source, Geometry.Vector2 target)
      => ChebyshevLength(target - source);
    /// <summary>Compute the Chebyshev distance between the vectors.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    public static double ChebyshevLength(this Geometry.Vector2 source)
      => System.Math.Max(System.Math.Abs(source.X), System.Math.Abs(source.Y));

    /// <summary>Returns the cross product of the two vectors.</summary>
    /// <remarks>This is equivalent to DotProduct(a, CrossProduct(b)), which is consistent with the notion of a "perpendicular dot product", which this is known as.</remarks>
    public static double CrossProduct(this Geometry.Vector2 source, Geometry.Vector2 target)
      => source.X * target.Y - source.Y * target.X;

    /// <summary>Creates a new vector with the floor(quotient) from each member divided by the value.</summary>
    public static Geometry.Vector2 DivideCeiling(this Geometry.Vector2 source, double scalar)
      => new Geometry.Vector2(System.Math.Ceiling(source.X / scalar), System.Math.Ceiling(source.Y / scalar));
    /// <summary>Creates a new vector with the floor(quotient) from each member divided by the value.</summary>
    public static Geometry.Vector2 DivideFloor(this Geometry.Vector2 source, double scalar)
      => new Geometry.Vector2(System.Math.Floor(source.X / scalar), System.Math.Floor(source.Y / scalar));

    /// <summary>Compute the dot product of the two vectors. The sign of the dot product broadly determine the angle between two arbitrary (no need to normalize) vectors, i.e. positive dot means less than 90 degrees (acute), zero dot means 90 degrees (perpendicular) and negative dot means greater than 90 degrees (obtuse).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Dot_product"/>
    public static double DotProduct(this Geometry.Vector2 source, Geometry.Vector2 target)
      => source.X * target.X + source.Y * target.Y;

    /// <summary>Compute the distance (or magnitude) squared between the two vectors.</summary>
    public static double EuclideanDistanceSquaredTo(this Geometry.Vector2 source, Geometry.Vector2 target)
      => EuclideanLengthSquared(target - source);
    /// <summary>Compute the distance (or magnitude) between the two vectors.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public static double EuclideanDistanceTo(this Geometry.Vector2 source, Geometry.Vector2 target)
      => EuclideanLength(target - source);
    /// <summary>Compute the length (or magnitude) of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public static double EuclideanLength(this Geometry.Vector2 source)
      => System.Math.Sqrt(EuclideanLengthSquared(source));
    /// <summary>Compute the length (or magnitude) squared of the vector. This is much faster than Getlength(), if comparing magnitudes of vectors.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public static double EuclideanLengthSquared(this Geometry.Vector2 source)
      => source.X * source.X + source.Y * source.Y;

    /// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    public static Geometry.Vector2 InterpolateCosineTo(this Geometry.Vector2 source, Geometry.Vector2 target, double mu)
      => mu >= 0 && mu <= 1 && ((1.0 - System.Math.Cos(mu * System.Math.PI)) / 2.0) is double mu2
      ? (source * (1.0 - mu2) + target * mu2)
      : throw new System.ArgumentNullException(nameof(mu));
    /// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    public static Geometry.Vector2 InterpolateCubicTo(this Geometry.Vector2 source, Geometry.Vector2 target, Geometry.Vector2 preSource, Geometry.Vector2 postTarget, double mu)
    {
      var mu2 = mu * mu;

      var a0 = postTarget - target - preSource + source;
      var a1 = preSource - source - a0;
      var a2 = target - preSource;
      var a3 = source;

      return a0 * mu * mu2 + a1 * mu2 + a2 * mu + a3;
    }
    /// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    public static Geometry.Vector2 InterpolateHermiteTo(this Geometry.Vector2 source, Geometry.Vector2 target, Geometry.Vector2 preSource, Geometry.Vector2 postTarget, double mu, double tension, double bias)
    {
      var mu2 = mu * mu;
      var mu3 = mu2 * mu;

      var onePbias = 1 + bias;
      var oneMbias = 1 - bias;

      var oneMtension = 1 - tension;

      var m0 = (source - preSource) * onePbias * oneMtension / 2;
      m0 += (target - source) * oneMbias * oneMtension / 2;
      var m1 = (target - source) * onePbias * oneMtension / 2;
      m1 += (postTarget - target) * oneMbias * oneMtension / 2;

      var a0 = 2 * mu3 - 3 * mu2 + 1;
      var a1 = mu3 - 2 * mu2 + mu;
      var a2 = mu3 - mu2;
      var a3 = -2 * mu3 + 3 * mu2;

      return source * a0 + m0 * a1 + m1 * a2 + target * a3;
    }
    /// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    public static Geometry.Vector2 InterpolateLinearTo(this Geometry.Vector2 source, Geometry.Vector2 target, double mu)
      => source * (1 - mu) + target * mu;

    /// <summary>Creates a new vector by linear interpolation from source to target.</summary>
    /// <param name="mu">[0, 1]</param>
    public static Geometry.Vector2 LerpTo(this Geometry.Vector2 source, Geometry.Vector2 target, in double mu)
      => source + (target - source) * mu;

    /// <summary>Compute the Manhattan distance between the vectors.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public static double ManhattanDistanceTo(this Geometry.Vector2 source, Geometry.Vector2 target)
      => ManhattanLength(target - source);
    /// <summary>Compute the Manhattan distance between the vectors.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public static double ManhattanLength(this Geometry.Vector2 source)
      => System.Math.Abs(source.X) + System.Math.Abs(source.Y);

    /// <summary>Create a new vector with the floor(product) from each member multiplied with the value.</summary>
    public static Geometry.Vector2 MultiplyCeiling(this Geometry.Vector2 source, double scalar)
      => new Geometry.Vector2(System.Math.Ceiling(source.X * scalar), System.Math.Ceiling(source.Y * scalar));
    /// <summary>Create a new vector with the floor(product) from each member multiplied with the value.</summary>
    public static Geometry.Vector2 MultiplyFloor(this Geometry.Vector2 source, double scalar)
      => new Geometry.Vector2(System.Math.Floor(source.X * scalar), System.Math.Floor(source.Y * scalar));

    /// <summary>Creates a new vector by normalized linear interpolation from source to target.</summary>
    /// <param name="mu">[0, 1]</param>
    public static Geometry.Vector2 NlerpTo(this Geometry.Vector2 source, Geometry.Vector2 target, in double mu)
      => LerpTo(source, target, mu).Normalize();

    /// <summary>Create a new vector with the source components normalized.</summary>
    public static Geometry.Vector2 Normalize(this Geometry.Vector2 source)
      => source / source.EuclideanLength();

    /// <summary>Create a new vector, 90 degrees perpendicular to the vector, which is the vector rotated 90 degrees counter-clockwise.</summary>
    public static Geometry.Vector2 PerpendicularCcw(this Geometry.Vector2 source)
      => new Geometry.Vector2(-source.Y, source.X);
    /// <summary>Create a new vector, 90 degrees perpendicular to the vector, which is the vector rotated 90 degrees clockwise.</summary>
    public static Geometry.Vector2 PerpendicularCw(this Geometry.Vector2 source)
      => new Geometry.Vector2(source.Y, -source.X);

    /// <summary>Perpendicular distance to the to the line.</summary>
    public static double PerpendicularDistanceToLine(this Geometry.Vector2 source, Geometry.Line line)
    {
      var a = new Geometry.Vector2(line.X1, line.Y1);
      var b = new Geometry.Vector2(line.X2, line.Y2);

      var ab = b - a;

      return (ab * (source - a)).EuclideanLength() / ab.EuclideanLength();
    }

    /// <summary>Returns the sign indicating whether the point is Left|On|Right of an infinite line (a to b). Through point1 and point2 the result has the meaning: greater than 0 is to the left of the line, equal to 0 is on the line, less than 0 is to the right of the line. (This is also known as an IsLeft function.)</summary>
    public static int SideTest(this Geometry.Vector2 source, Geometry.Vector2 a, Geometry.Vector2 b)
      => System.Math.Sign((source.X - b.X) * (a.Y - b.Y) - (a.X - b.X) * (source.Y - b.Y));

    /// <summary>Creates a new vector by spherical linear interpolation, which travels the torque-minimal path, i.e. it travels along the straightest path the rounded surface of a sphere.</summary>>
    /// <param name="mu">[0, 1]</param>
    public static Geometry.Vector2 SlerpTo(this Geometry.Vector2 source, Geometry.Vector2 target, double mu)
    {
      var dot = System.Math.Clamp(DotProduct(source, target), -1.0f, 1.0f); // Ensure precision doesn't exceed acos limits.
      var theta = System.Math.Acos(dot) * mu; // Angle between start and desired.

      return source * System.Math.Cos(theta) + (target - source * dot).Normalize() * System.Math.Sin(theta);
    }

    /// <summary>Creates a new Point2 from the rounded components in the vector.</summary>
    public static Geometry.Point2 ToPoint2(this Geometry.Vector2 source)
      => new Geometry.Point2(System.Convert.ToInt32(source.X), System.Convert.ToInt32(source.Y));
    /// <summary>Creates a new Point2 from the rounded components in the vector.</summary>
    public static System.Numerics.Vector2 ToVector2(this Geometry.Vector2 source)
      => new System.Numerics.Vector2((float)source.X, (float)source.Y);
  }
}
