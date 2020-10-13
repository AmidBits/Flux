namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>This is basically LERP with the the ability to compute an arbitrary point anywhere on the path from a to b (including before a and after b). The result, when the specified scalar is, <0 is a vector beyond a (backwards), 0 is vector a, 0.5 equals the midpoint vector between a and b, 1 is vector b, and >1 equals a vector beyond b (forward).</summary>>
    public static System.Drawing.Point AlongPathTo2(this System.Drawing.Point source, System.Drawing.Point target, float scalar = 0.5f)
      => new System.Drawing.Point((int)((source.X + target.X) * scalar), (int)((source.Y + target.Y) * scalar));

    /// <summary>(2D) Calculate the angle between the source vector and the specified target vector.
    /// When dot eq 0 then the vectors are perpendicular.
    /// When dot gt 0 then the angle is less than 90 degrees (dot=1 can be interpreted as the same direction).
    /// When dot lt 0 then the angle is greater than 90 degrees (dot=-1 can be interpreted as the opposite direction).
    /// </summary>
    public static double AngleTo(this System.Drawing.Point source, System.Drawing.Point target)
      => AngleTo(source.ToVector2(), target.ToVector2());  // Delegate to the Vector2 implementation.

    /// <summary>Compute the Chebyshev distance between point a and point b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    public static int ChebyshevDistanceTo(this in System.Drawing.Point a, in System.Drawing.Point b)
      => System.Math.Max(b.X - a.X, b.Y - a.Y);

    /// <summary>Compute the Euclidean distance squared between point a and point b.</summary>
    public static int EuclideanDistanceSquaredTo(this in System.Drawing.Point a, in System.Drawing.Point b)
      => (a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y);
    /// <summary>Compute the Euclidean distance between point a and point b.</summary>
    public static double EuclideanDistanceTo(this in System.Drawing.Point a, in System.Drawing.Point b)
      => System.Math.Sqrt(EuclideanDistanceSquaredTo(a, b));

    /// <summary>Compute the Manhattan distance between point a and point b.<summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public static int ManhattanDistanceTo(this in System.Drawing.Point a, in System.Drawing.Point b)
      => System.Math.Abs(b.X - a.X) + System.Math.Abs(b.Y - a.Y);

    /// <summary>Returns a point -90 degrees perpendicular to the point, i.e. the point rotated 90 degrees counter clockwise. Only X and Y.</summary>
    public static System.Drawing.Point PerpendicularCcw(this System.Drawing.Point source)
      => new System.Drawing.Point(-source.Y, source.X);
    /// <summary>Returns a point 90 degrees perpendicular to the point, i.e. the point rotated 90 degrees clockwise. Only X and Y.</summary>
    public static System.Drawing.Point PerpendicularCw(this System.Drawing.Point source)
      => new System.Drawing.Point(source.Y, -source.X);

    /// <summary>Convert a point to a 2D vector.</summary>
    public static System.Numerics.Vector2 ToVector2(this in System.Drawing.Point source)
      => new System.Numerics.Vector2(source.X, source.Y);
    /// <summary>Convert a point to a 3D vector.</summary>
    public static System.Numerics.Vector3 ToVector3(this in System.Drawing.Point source)
      => new System.Numerics.Vector3(source.X, source.Y, 0);

  }
}
