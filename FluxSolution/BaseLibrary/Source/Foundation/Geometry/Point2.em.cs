namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static Geometry.Vector2 ToVector(this Geometry.Point2 source)
      => new Geometry.Vector2(source.X, source.Y);
    public static System.Numerics.Vector2 ToVector2(this Geometry.Point2 source)
      => new System.Numerics.Vector2(source.X, source.Y);
    public static System.Numerics.Vector3 ToVector3(this Geometry.Point2 source)
      => new System.Numerics.Vector3(source.X, source.Y, 0);

    //    /// <summary>Returns the angle for the source point to the other two specified points.</summary>>
    //    public static double AngleBetween(this Geometry.Point2 source, Geometry.Point2 before, Geometry.Point2 after)
    //      => AngleTo(before - source, after - source);

    //    /// <summary>(2D) Calculate the angle between the source vector and the specified target vector.
    //    /// When dot eq 0 then the vectors are perpendicular.
    //    /// When dot gt 0 then the angle is less than 90 degrees (dot=1 can be interpreted as the same direction).
    //    /// When dot lt 0 then the angle is greater than 90 degrees (dot=-1 can be interpreted as the opposite direction).
    //    /// </summary>
    //    public static double AngleTo(this Geometry.Point2 source, Geometry.Point2 target)
    //      => System.Math.Acos(System.Math.Clamp(Geometry.Point2.DotProduct(source.Normalize(), target.Normalize()), -1, 1));

    //    /// <summary>Compute the Chebyshev distance from vector a to vector b.</summary>
    //    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    //    public static double ChebyshevDistanceTo(this Geometry.Point2 source, Geometry.Point2 target, double edgeLength = 1)
    //      => ChebyshevLength(target - source, edgeLength);
    //    /// <summary>Compute the Chebyshev distance from vector a to vector b.</summary>
    //    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    //    public static double ChebyshevLength(this Geometry.Point2 source, double edgeLength = 1)
    //      => System.Math.Max(System.Math.Abs(source.X / edgeLength), System.Math.Abs(source.Y / edgeLength));

    //    /// <summary>Returns the cross product of the two vectors.</summary>
    //    /// <remarks>This is equivalent to DotProduct(a, CrossProduct(b)), which is consistent with the notion of a "perpendicular dot product", which this is known as.</remarks>
    //    public static double CrossProduct(this Geometry.Point2 p1, Geometry.Point2 p2)
    //      => p1.X * p2.Y - p1.Y * p2.X;

    //    /// <summary>Creates a new vector with the floor(quotient) from each member divided by the value.</summary>
    //    public static Geometry.Point2 DivideCeiling(this Geometry.Point2 source, double scalar)
    //      => new Geometry.Point2(System.Convert.ToInt32(System.Math.Ceiling(source.X / scalar)), System.Convert.ToInt32(System.Math.Ceiling(source.Y / scalar)));
    //    /// <summary>Creates a new vector with the floor(quotient) from each member divided by the value.</summary>
    //    public static Geometry.Point2 DivideFloor(this Geometry.Point2 source, double scalar)
    //      => new Geometry.Point2(System.Convert.ToInt32(System.Math.Floor(source.X / scalar)), System.Convert.ToInt32(System.Math.Floor(source.Y / scalar)));

    //    /// <summary>Returns the dot product of the two vectors. The sign of the dot product broadly determine the angle between two arbitrary (no need to normalize) vectors, i.e. positive dot means less than 90 degrees (acute), zero dot means 90 degrees (perpendicular) and negative dot means greater than 90 degrees (obtuse).</summary>
    //    public static double DotProduct(this Geometry.Point2 p1, Geometry.Point2 p2)
    //      => p1.X * p2.X + p1.Y * p2.Y;

    //    /// <summary>Compute the distance (or magnitude) squared between the two vectors.</summary>
    //    public static double EuclideanDistanceSquaredTo(this Geometry.Point2 source, Geometry.Point2 target)
    //      => EuclideanLengthSquared(target - source);
    //    /// <summary>Compute the distance (or magnitude) between the two vectors.</summary>
    //    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    //    public static double EuclideanDistanceTo(this Geometry.Point2 source, Geometry.Point2 target)
    //      => EuclideanLength(target - source);
    //    /// <summary>Returns the length of the vector.</summary>
    //    public static double EuclideanLength(this Geometry.Point2 source)
    //      => System.Math.Sqrt(EuclideanLengthSquared(source));
    //    /// <summary>Returns the length (squared) of the vector.</summary>
    //    public static double EuclideanLengthSquared(this Geometry.Point2 source)
    //      => source.X * source.X + source.Y * source.Y;

    //    /// <summary>Returns a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    //    public static Geometry.Point2 InterpolateCosine(this Geometry.Point2 y1, Geometry.Point2 y2, double mu)
    //      => mu >= 0 && mu <= 1 && ((1.0 - System.Math.Cos(mu * System.Math.PI)) / 2.0) is double mu2
    //      ? (y1 * (1.0 - mu2) + y2 * mu2)
    //      : throw new System.ArgumentNullException(nameof(mu));
    //    /// <summary>Returns a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    //    public static Geometry.Point2 InterpolateCubic(this Geometry.Point2 y0, Geometry.Point2 y1, Geometry.Point2 y2, Geometry.Point2 y3, double mu)
    //    {
    //      var mu2 = mu * mu;

    //      var a0 = y3 - y2 - y0 + y1;
    //      var a1 = y0 - y1 - a0;
    //      var a2 = y2 - y0;
    //      var a3 = y1;

    //      return a0 * mu * mu2 + a1 * mu2 + a2 * mu + a3;
    //    }
    //    /// <summary>Returns a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    //    public static Geometry.Point2 InterpolateHermite(this Geometry.Point2 y0, Geometry.Point2 y1, Geometry.Point2 y2, Geometry.Point2 y3, double mu, double tension, double bias)
    //    {
    //      var mu2 = mu * mu;
    //      var mu3 = mu2 * mu;

    //      var onePbias = 1 + bias;
    //      var oneMbias = 1 - bias;

    //      var oneMtension = 1 - tension;

    //      var m0 = (y1 - y0) * onePbias * oneMtension / 2;
    //      m0 += (y2 - y1) * oneMbias * oneMtension / 2;
    //      var m1 = (y2 - y1) * onePbias * oneMtension / 2;
    //      m1 += (y3 - y2) * oneMbias * oneMtension / 2;

    //      var a0 = 2 * mu3 - 3 * mu2 + 1;
    //      var a1 = mu3 - 2 * mu2 + mu;
    //      var a2 = mu3 - mu2;
    //      var a3 = -2 * mu3 + 3 * mu2;

    //      return y1 * a0 + m0 * a1 + m1 * a2 + y2 * a3;
    //    }
    //    /// <summary>Returns a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    //    public static Geometry.Point2 InterpolateLinear(this Geometry.Point2 y1, Geometry.Point2 y2, double mu)
    //      => y1 * (1 - mu) + y2 * mu;

    //    public static Geometry.Point2 LerpTo(this Geometry.Point2 source, Geometry.Point2 target, in double mu = 0.5)
    //      => source + (target - source) * mu;

    //    /// <summary>Compute the Manhattan length (or magnitude) of the vector. Known as the Manhattan distance (i.e. from 0,0,0).</summary>
    //    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    //    public static double ManhattanDistanceTo(this Geometry.Point2 a, Geometry.Point2 b, float edgeLength = 1)
    //      => System.Math.Abs(b.X - a.X) / edgeLength + System.Math.Abs(b.Y - a.Y) / edgeLength;

    //    /// <summary>Create a new vector with the floor(product) from each member multiplied with the value.</summary>
    //    public static Geometry.Point2 MultiplyCeiling(this Geometry.Point2 source, double scalar)
    //      => new Geometry.Point2(System.Convert.ToInt32(System.Math.Ceiling(source.X * scalar)), System.Convert.ToInt32(System.Math.Ceiling(source.Y * scalar)));
    //    /// <summary>Create a new vector with the floor(product) from each member multiplied with the value.</summary>
    //    public static Geometry.Point2 MultiplyFloor(this Geometry.Point2 source, double scalar)
    //      => new Geometry.Point2(System.Convert.ToInt32(System.Math.Floor(source.X * scalar)), System.Convert.ToInt32(System.Math.Floor(source.Y * scalar)));

    //    public static Geometry.Point2 NlerpTo(this Geometry.Point2 source, Geometry.Point2 target, in double mu = 0.5)
    //      => source.NlerpTo(target, mu).Normalize();

    //    /// <summary>Create a new vector with the source components normalized.</summary>
    //    public static Geometry.Point2 Normalize(this Geometry.Point2 source)
    //      => source / source.EuclideanLength();

    //    /// <summary>Returns a point -90 degrees perpendicular to the point, i.e. the point rotated 90 degrees counter clockwise. Only X and Y.</summary>
    //    public static Geometry.Point2 PerpendicularCcw(this Geometry.Point2 source)
    //      => new Geometry.Point2(-source.Y, source.X);
    //    /// <summary>Returns a point 90 degrees perpendicular to the point, i.e. the point rotated 90 degrees clockwise. Only X and Y.</summary>
    //    public static Geometry.Point2 PerpendicularCw(this Geometry.Point2 source)
    //      => new Geometry.Point2(source.Y, -source.X);

    //    /// <summary>Perpendicular distance to the to the line.</summary>
    //    public static double PerpendicularDistance(this Geometry.Point2 source, Geometry.Point2 a, Geometry.Point2 b)
    //    {
    //      var ab = b - a;

    //      return (ab * (source - a)).EuclideanLength() / ab.EuclideanLength();
    //    }

    //    /// <summary>Find the perpendicular distance from a point in a 2D plane to a line equation (ax+by+c=0).</summary>
    //    /// <see cref="https://www.geeksforgeeks.org/perpendicular-distance-between-a-point-and-a-line-in-2-d/"/>
    //    /// <param name="a">Represents a of the line equation (ax+by+c=0).</param>
    //    /// <param name="b">Represents b of the line equation (ax+by+c=0).</param>
    //    /// <param name="c">Represents c of the line equation (ax+by+c=0).</param>
    //    /// <param name="source">A given point.</param>
    //    public static double PerpendicularDistance(this Geometry.Point2 source, double a, double b, double c)
    //      => System.Math.Abs(a * source.X + b * source.Y + c) / System.Math.Sqrt(a * a + b * b);

    //    /// <summary>Find foot of perpendicular from a point in 2D a plane to a line equation (ax+by+c=0).</summary>
    //    /// <see cref="https://www.geeksforgeeks.org/find-foot-of-perpendicular-from-a-point-in-2-d-plane-to-a-line/"/>
    //    /// <param name="a">Represents a of the line equation (ax+by+c=0).</param>
    //    /// <param name="b">Represents b of the line equation (ax+by+c=0).</param>
    //    /// <param name="c">Represents c of the line equation (ax+by+c=0).</param>
    //    /// <param name="source">A given point.</param>
    //    //public static Geometry.Point2 PerpendicularFoot(this Geometry.Point2 source, double a, double b, double c)
    //    //  => -1 * (a * source.X + b * source.Y + c) / (a * a + b * b) * new Geometry.Point2(a + source.X, b + source.Y);

    //    /// <summary>Creates four vectors, each of which represents the center axis for each of the quadrants for the vector and the specified sizes of X and Y.</summary>
    //    /// <see cref="https://en.wikipedia.org/wiki/Quadrant_(plane_geometry)"/>
    //    public static System.Collections.Generic.IEnumerable<Geometry.Point2> QuadrantCenterVectors(this Geometry.Point2 source, Geometry.Size2 subQuadrant)
    //    {
    //      yield return new Geometry.Point2(source.X + subQuadrant.Width, source.Y + subQuadrant.Height);
    //      yield return new Geometry.Point2(source.X - subQuadrant.Width, source.Y + subQuadrant.Height);
    //      yield return new Geometry.Point2(source.X - subQuadrant.Width, source.Y - subQuadrant.Height);
    //      yield return new Geometry.Point2(source.X + subQuadrant.Width, source.Y - subQuadrant.Height);
    //    }
    //    /// <summary>Convert the 2D vector to a quadrant based on the specified center vector.</summary>
    //    /// <returns>The quadrant identifer in the range 0-3, i.e. one of the four quadrants.</returns>
    //    /// <see cref="https://en.wikipedia.org/wiki/Quadrant_(plane_geometry)"/>
    //    public static int QuadrantNumber(this Geometry.Point2 source, Geometry.Point2 center)
    //      => (source.X >= center.X ? 1 : 0) + (source.Y >= center.Y ? 2 : 0);

    //    /// <summary>Create a new vector with the remainder from the vector divided by the other.</summary>
    //    public static Geometry.Point2 Remainder(this Geometry.Point2 p1, Geometry.Point2 p2)
    //      => new Geometry.Point2(p1.X % p2.X, p1.Y % p2.Y);
    //    /// <summary>Create a new vector with the floor(remainder) from each member divided by the value.</summary>
    //    //public static Geometry.Point2 Remainder(this Geometry.Point2 p, double value)
    //    //  => new Geometry.Point2(p.Y % value, p.Y % value);

    //    ///// <summary>Rotate the vector around the specified axis.</summary>
    //    //public static Geometry.Point2 RotateAroundAxis(this Geometry.Point2 source, System.Numerics.Vector3 axis, float angle)
    //    //  => Geometry.Point2.Transform(source, System.Numerics.Quaternion.CreateFromAxisAngle(axis, angle));
    //    ///// <summary>Rotate the vector around the world axes.</summary>
    //    //public static Geometry.Point2 RotateAroundWorldAxes(this Geometry.Point2 source, float yaw, float pitch, float roll)
    //    //  => Geometry.Point2.Transform(source, System.Numerics.Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll));

    //    /// <summary>Returns the sign indicating whether the point is Left|On|Right of an infinite line (a to b). Through point1 and point2 the result has the meaning: greater than 0 is to the left of the line, equal to 0 is on the line, less than 0 is to the right of the line. (This is also known as an IsLeft function.)</summary>
    //    public static int SideTest(this Geometry.Point2 source, Geometry.Point2 a, Geometry.Point2 b)
    //      => System.Math.Sign((source.X - b.X) * (a.Y - b.Y) - (a.X - b.X) * (source.Y - b.Y));

    //    /// <summary>Slerp travels the torque-minimal path, which means it travels along the straightest path the rounded surface of a sphere.</summary>>
    //    public static Geometry.Point2 SlerpTo(this Geometry.Point2 source, Geometry.Point2 target, double percent = 0.5)
    //    {
    //      var dot = System.Math.Clamp(Geometry.Point2.DotProduct(source, target), -1.0f, 1.0f); // Ensure precision doesn't exceed acos limits.
    //      var theta = System.Math.Acos(dot) * percent; // Angle between start and desired.
    //      var relative = (target - source * dot).Normalize();
    //      return source * System.Math.Cos(theta) + relative * System.Math.Sin(theta);
    //    }

    //    ///// <summary>Convert a 2D vector to a 3D vector.</summary>
    //    //public static System.Numerics.Vector3 ToVector3(this Geometry.Point2 source)
    //    //  => new System.Numerics.Vector3(source, 0);
  }
}
