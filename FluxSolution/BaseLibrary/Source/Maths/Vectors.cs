namespace Flux
{
  public static partial class Maths
  {
    /// <summary>(2D/3D) Calculate the angle between the source vector and the specified target vector. 
    /// When dot eq 0 then the vectors are perpendicular.
    /// When dot gt 0 then the angle is less than 90 degrees (dot=1 can be interpreted as the same direction).
    /// When dot lt 0 then the angle is greater than 90 degrees (dot=-1 can be interpreted as the opposite direction).
    /// </summary>
    public static double AngleBetween(in System.Numerics.Vector3 a, in System.Numerics.Vector3 b) => System.Math.Acos(System.Numerics.Vector3.Dot(System.Numerics.Vector3.Normalize(a), System.Numerics.Vector3.Normalize(b)));
    /// <summary>(2D/3D) Calculate the angle between the source vector and the specified target vector. 
    /// When dot eq 0 then the vectors are perpendicular.
    /// When dot gt 0 then the angle is less than 90 degrees (dot=1 can be interpreted as the same direction).
    /// When dot lt 0 then the angle is greater than 90 degrees (dot=-1 can be interpreted as the opposite direction).
    /// </summary>
    public static double AngleBetween(in System.Numerics.Vector2 a, in System.Numerics.Vector2 b) => System.Math.Acos(System.Numerics.Vector2.Dot(System.Numerics.Vector2.Normalize(a), System.Numerics.Vector2.Normalize(b)));
    /// <summary>(2D/3D) Calculate the angle between the source vector and the specified target vector. 
    /// When dot eq 0 then the vectors are perpendicular.
    /// When dot gt 0 then the angle is less than 90 degrees (dot=1 can be interpreted as the same direction).
    /// When dot lt 0 then the angle is greater than 90 degrees (dot=-1 can be interpreted as the opposite direction).
    /// </summary>
    public static double AngleBetween(in (double x, double y, double z) a, in (double x, double y, double z) b) => System.Math.Acos(DotProduct(NormalizeVector(a), NormalizeVector(b)));
    /// <summary>(2D) Calculate the angle between the source vector and the specified target vector.
    /// When dot eq 0 then the vectors are perpendicular.
    /// When dot gt 0 then the angle is less than 90 degrees (dot=1 can be interpreted as the same direction).
    /// When dot lt 0 then the angle is greater than 90 degrees (dot=-1 can be interpreted as the opposite direction).
    /// </summary>
    public static double AngleBetween(in double ax, in double ay, in double az, in double bx, in double by, in double bz) => System.Math.Acos(DotProduct(NormalizeVector(ax, ay, az), NormalizeVector(bx, by, bz)));

    ///// <summary>Compute the Chebyshev distance from vector a to vector b.</summary>
    ///// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    //public static double ChebyshevDistanceTo(this in System.Numerics.Vector3 a, in System.Numerics.Vector3 b, float edgeLength = 1) => System.Math.Max(System.Math.Max((b.X - a.X) / edgeLength, (b.Y - a.Y) / edgeLength), (b.Z - a.Z) / edgeLength);
    ///// <summary>Compute the Chebyshev distance from vector a to vector b.</summary>
    ///// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    //public static double ChebyshevDistanceTo(this in System.Numerics.Vector2 a, in System.Numerics.Vector2 b, float edgeLength = 1) => System.Math.Max((b.X - a.X) / edgeLength, (b.Y - a.Y) / edgeLength);

    /// <summary>Compute the Chebyshev distance from vector a to vector b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    public static double ChebyshevDistance(in (double x, double y, double z) a, in (double x, double y, double z) b)
      => Maths.Max(b.x - a.x, b.y - a.y, b.z - a.z);
    /// <summary>Compute the Chebyshev distance from vector a to vector b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    public static double ChebyshevDistance(in (double x, double y, double z) a, in (double x, double y, double z) b, double edgeLength = 1)
      => Maths.Max((b.x - a.x) / edgeLength, (b.y - a.y) / edgeLength, (b.z - a.z) / edgeLength);
    /// <summary>Compute the Chebyshev distance from vector a to vector b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    public static double ChebyshevDistance(in double ax, in double ay, in double az, in double bx, in double by, in double bz) => System.Math.Max(System.Math.Max(bx - ax, by - ay), bz - az);
    /// <summary>Compute the Chebyshev distance from vector a to vector b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    public static double ChebyshevDistance(in double ax, in double ay, in double az, in double bx, in double by, in double bz, double edgeLength = 1) => System.Math.Max(System.Math.Max((bx - ax) / edgeLength, (by - ay) / edgeLength), (bz - az) / edgeLength);

    /// <summary>Create a new vector by computing the cross product, i.e. cross(a, b), of the vector (a) and vector b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Cross_product"/>
    public static (double x, double y, double z) CrossProduct(in (double x, double y, double z) a, in (double x, double y, double z) b) => (a.y * b.z - a.z * b.y, a.z * b.x - a.x * b.z, a.x * b.y - a.y * b.x);
    /// <summary>Create a new vector by computing the cross product, i.e. cross(a, b), of the vector (a) and vector b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Cross_product"/>
    public static (double x, double y, double z) CrossProduct(in double ax, in double ay, in double az, in double bx, in double by, in double bz) => (ay * bz - az * by, az * bx - ax * bz, ax * by - ay * bx);

    /// <summary>Compute the dot product, i.e. dot(a, b), of the vector (a) and vector b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Dot_product"/>
    public static double DotProduct(in (double x, double y, double z) a, in (double x, double y, double z) b) => a.x * b.x + a.y * b.y + a.z * b.z;
    /// <summary>Compute the dot product, i.e. dot(a, b), of the vector (a) and vector b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Dot_product"/>
    public static double DotProduct(in double ax, in double ay, in double az, in double bx, in double by, in double bz) => ax * bx + ay * by + az * bz;

    /// <summary>Compute the euclidean length (or magnitude) of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public static double EuclideanDistance(in (double x, double y, double z) a, in (double x, double y, double z) b) => EuclideanLength(b.x - a.x, b.y - a.y, b.z - a.z);
    /// <summary>Compute the euclidean length (or magnitude) of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public static double EuclideanDistance(in double ax, in double ay, in double az, in double bx, in double by, in double bz) => EuclideanLength(bx - ax, by - ay, bz - az);

    /// <summary>Compute the euclidean length (or magnitude) of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public static double EuclideanLength(in (double x, double y, double z) v) => System.Math.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z);
    /// <summary>Compute the euclidean length (or magnitude) of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public static double EuclideanLength(in double x, in double y, in double z) => System.Math.Sqrt(x * x + y * y + z * z);

    ///// <summary>Compute the Manhattan length (or magnitude) of the vector. Known as the Manhattan distance (i.e. from 0,0,0).</summary>
    ///// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    //public static double ManhattanDistanceTo(this in System.Numerics.Vector2 a, in System.Numerics.Vector2 b, float edgeLength = 1) => System.Math.Abs((b.X - a.X) / edgeLength) + System.Math.Abs((b.Y - a.Y) / edgeLength);
    ///// <summary>Compute the Manhattan length (or magnitude) of the vector. Known as the Manhattan distance (i.e. from 0,0,0).</summary>
    ///// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    //public static double ManhattanDistanceTo(this in System.Numerics.Vector3 a, in System.Numerics.Vector3 b, float edgeLength = 1) => System.Math.Abs((b.X - a.X) / edgeLength) + System.Math.Abs((b.Y - a.Y) / edgeLength) + System.Math.Abs((b.Z - a.Z) / edgeLength);

    /// <summary>Compute the Manhattan length (or magnitude) of the vector. Known as the Manhattan distance (i.e. from 0,0,0).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public static double ManhattanDistance(in (double x, double y, double z) a, in (double x, double y, double z) b) => System.Math.Abs(b.x - a.x) + System.Math.Abs(b.y - a.y) + System.Math.Abs(b.z - a.z);
    /// <summary>Compute the Manhattan length (or magnitude) of the vector. Known as the Manhattan distance (i.e. from 0,0,0).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public static double ManhattanDistance(in (double x, double y, double z) a, in (double x, double y, double z) b, double edgeLength = 1) => System.Math.Abs((b.x - a.x) / edgeLength) + System.Math.Abs((b.y - a.y) / edgeLength) + System.Math.Abs((b.z - a.z) / edgeLength);
    /// <summary>Compute the Manhattan length (or magnitude) of the vector. Known as the Manhattan distance (i.e. from 0,0,0).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public static double ManhattanDistance(in double ax, in double ay, in double az, in double bx, in double by, in double bz) => System.Math.Abs(bx - ax) + System.Math.Abs(by - ay) + System.Math.Abs(bz - az);
    /// <summary>Compute the Manhattan length (or magnitude) of the vector. Known as the Manhattan distance (i.e. from 0,0,0).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public static double ManhattanDistance(in double ax, in double ay, in double az, in double bx, in double by, in double bz, double edgeLength = 1) => System.Math.Abs((bx - ax) / edgeLength) + System.Math.Abs((by - ay) / edgeLength) + System.Math.Abs((bz - az) / edgeLength);

    /// <summary>Create a new vector by normalizing the vector.</summary>
    public static (double x, double y, double z) NormalizeVector(in (double x, double y, double z) v) => EuclideanLength(v) is var norm && norm != 0 ? (v.x / norm, v.y / norm, v.z / norm) : v;
    /// <summary>Create a new vector by normalizing the vector.</summary>
    public static (double x, double y, double z) NormalizeVector(in double x, in double y, in double z) => EuclideanLength(x, y, z) is var norm && norm != 0 ? (x / norm, y / norm, z / norm) : (x, y, z);

    /// <summary>Returns a point -90 degrees perpendicular to the point, i.e. the point rotated 90 degrees counter clockwise. Only X and Y.</summary>
    public static System.Numerics.Vector2 PerpendicularCcw(this in System.Numerics.Vector2 v) => new System.Numerics.Vector2(-v.Y, v.X);
    /// <summary>Returns a point -90 degrees perpendicular to the point, i.e. the point rotated 90 degrees counter clockwise. Only X and Y.</summary>
    public static (double x, double y) PerpendicularCcw(in (double x, double y) v) => (-v.y, v.x);
    /// <summary>Returns a point -90 degrees perpendicular to the point, i.e. the point rotated 90 degrees counter clockwise. Only X and Y.</summary>
    public static (double x, double y) PerpendicularCcw(in double x, in double y) => (-y, x);

    /// <summary>Returns a point 90 degrees perpendicular to the point, i.e. the point rotated 90 degrees clockwise. Only X and Y.</summary>
    public static System.Numerics.Vector2 PerpendicularCw(this in System.Numerics.Vector2 v) => new System.Numerics.Vector2(v.Y, -v.X);
    /// <summary>Returns a point 90 degrees perpendicular to the point, i.e. the point rotated 90 degrees clockwise. Only X and Y.</summary>
    public static (double x, double y) PerpendicularCw(in double x, in double y) => (y, -x);
    /// <summary>Returns a point 90 degrees perpendicular to the point, i.e. the point rotated 90 degrees clockwise. Only X and Y.</summary>
    public static (double x, double y) PerpendicularCw(in (double x, double y) v) => (v.y, -v.x);

    public static double PerpendicularDistance(System.Numerics.Vector2 p1, System.Numerics.Vector2 p2, System.Numerics.Vector2 point)
    {
      var ab = p2 - p1;

      return (ab * (point - p1)).Length() / ab.Length();
    }

    /// <summary>Find foot of perpendicular from a point in 2 D plane to a line.</summary>
    /// <see cref="https://www.geeksforgeeks.org/perpendicular-distance-between-a-point-and-a-line-in-2-d/"/>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <param name="point"></param>
    public static double PerpendicularDistance(System.Numerics.Vector2 point, float a, float b, float c) => System.Math.Abs(a * point.X + b * point.Y + c) / System.Math.Sqrt(a * a + b * b);

    /// <summary>Find foot of perpendicular from a point in 2 D plane to a line.</summary>
    /// <see cref="https://www.geeksforgeeks.org/find-foot-of-perpendicular-from-a-point-in-2-d-plane-to-a-line/"/>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <param name="point"></param>
    public static System.Numerics.Vector2 PerpendicularIntersect(float a, float b, float c, System.Numerics.Vector2 point) => -1 * (a * point.X + b * point.Y + c) / (a * a + b * b) * new System.Numerics.Vector2(a + point.X, b + point.Y);

    /// <summary>Compute the scalar triple product, i.e. dot(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Triple_product#Scalar_triple_product"/>
    public static double ScalarTripleProduct(this in System.Numerics.Vector3 a, in System.Numerics.Vector3 b, in System.Numerics.Vector3 c) => System.Numerics.Vector3.Dot(a, System.Numerics.Vector3.Cross(b, c));
    /// <summary>Compute the scalar triple product, i.e. dot(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Triple_product#Scalar_triple_product"/>
    public static double ScalarTripleProduct(in (double x, double y, double z) a, in (double x, double y, double z) b, in (double x, double y, double z) c) => DotProduct(a, CrossProduct(b, c));
    /// <summary>Compute the scalar triple product, i.e. dot(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Triple_product#Scalar_triple_product"/>
    public static double ScalarTripleProduct(in double ax, in double ay, in double az, in double bx, in double by, in double bz, in double cx, in double cy, in double cz) => DotProduct((ax, ay, az), CrossProduct(bx, by, bz, cx, cy, cz));

    /// <summary>Create a new vector by computing the vector triple product, i.e. cross(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Triple_product#Vector_triple_product"/>
    public static System.Numerics.Vector3 VectorTripleProduct(this in System.Numerics.Vector3 a, in System.Numerics.Vector3 b, in System.Numerics.Vector3 c) => System.Numerics.Vector3.Cross(a, System.Numerics.Vector3.Cross(b, c));
    /// <summary>Create a new vector by computing the vector triple product, i.e. cross(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Triple_product#Vector_triple_product"/>
    public static (double x, double y, double z) VectorTripleProduct(in (double x, double y, double z) a, in (double x, double y, double z) b, in (double x, double y, double z) c) => CrossProduct(a, CrossProduct(b, c));
    /// <summary>Create a new vector by computing the vector triple product, i.e. cross(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Triple_product#Vector_triple_product"/>
    public static (double x, double y, double z) VectorTripleProduct(in double ax, in double ay, in double az, in double bx, in double by, in double bz, in double cx, in double cy, in double cz) => CrossProduct((ax, ay, az), CrossProduct(bx, by, bz, cx, cy, cz));
  }
}
