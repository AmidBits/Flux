namespace Flux
{
  public static partial class XtendNumerics
  {
    /// <summary>This is basically LERP with the the ability to compute an arbitrary point anywhere on the path from a to b (including before a and after b). The result, when the specified scalar is, <0 is a vector beyond a (backwards), 0 is vector a, 0.5 equals the midpoint vector between a and b, 1 is vector b, and >1 equals a vector beyond b (forward).</summary>>
    public static System.Numerics.Vector3 AlongPathTo(this System.Numerics.Vector3 source, System.Numerics.Vector3 target, float scalar = 0.5f)
      => (source + target) * scalar;

    /// <summary>Returns the angle for the source point to the other two specified points.</summary>>
    public static double AngleBetween(this System.Numerics.Vector3 source, System.Numerics.Vector3 before, System.Numerics.Vector3 after)
      => (before - source).AngleTo(after - source);

    /// <summary>Calculate the angle between the source vector and the specified target vector. (2D/3D)
    /// when dot eq 0 then the vectors are perpendicular
    /// when dot gt 0 then the angle is less than 90 degrees (dot=1 can be interpreted as the same direction)
    /// when dot lt 0 then the angle is greater than 90 degrees (dot=-1 can be interpreted as the opposite direction)
    /// </summary>
    public static double AngleTo(this System.Numerics.Vector3 source, System.Numerics.Vector3 target)
      => System.Numerics.Vector3.Cross(source, target) is var cross ? System.Math.Atan2(System.Numerics.Vector3.Dot(System.Numerics.Vector3.Normalize(cross), cross), System.Numerics.Vector3.Dot(source, target)) : throw new System.Exception();

    public static double AngleToAxisX(this System.Numerics.Vector3 source)
      => System.Math.Atan2(System.Math.Sqrt(source.Y * source.Y + source.Z * source.Z), source.X);
    public static double AngleToAxisY(this System.Numerics.Vector3 source)
      => System.Math.Atan2(System.Math.Sqrt(source.Z * source.Z + source.X * source.X), source.Y);
    public static double AngleToAxisZ(this System.Numerics.Vector3 source)
      => System.Math.Atan2(System.Math.Sqrt(source.X * source.X + source.Y * source.Y), source.Z);

    /// <summary>Compute the Chebyshev distance from vector a to vector b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    public static double ChebyshevDistanceTo(this in System.Numerics.Vector3 a, in System.Numerics.Vector3 b, float edgeLength = 1)
      => System.Math.Max(System.Math.Max((b.X - a.X) / edgeLength, (b.Y - a.Y) / edgeLength), (b.Z - a.Z) / edgeLength);

    public static double EuclideanDistanceSquaredTo(this in System.Numerics.Vector3 a, in System.Numerics.Vector3 b)
      => (a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y) + (a.Z - b.Z) * (a.Z - b.Z);
    public static double EuclideanDistanceTo(this in System.Numerics.Vector3 a, in System.Numerics.Vector3 b)
      => System.Math.Sqrt(EuclideanDistanceSquaredTo(a, b));

    /// <summary>Compute the Manhattan length (or magnitude) of the vector. Known as the Manhattan distance (i.e. from 0,0,0).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public static double ManhattanDistanceTo(this in System.Numerics.Vector3 a, in System.Numerics.Vector3 b, float edgeLength = 1)
      => System.Math.Abs(b.X - a.X) / edgeLength + System.Math.Abs(b.Y - a.Y) / edgeLength + System.Math.Abs(b.Z - a.Z) / edgeLength;

    /// <summary>Always works if the input is non-zero. Does not require the input to be normalised, and does not normalise the output.</summary>
    /// <see cref="http://lolengine.net/blog/2013/09/21/picking-orthogonal-vector-combing-coconuts"/>
    public static System.Numerics.Vector3 Orthogonal(this System.Numerics.Vector3 source)
      => System.Math.Abs(source.X) > System.Math.Abs(source.Z) ? new System.Numerics.Vector3(-source.Y, source.X, 0) : new System.Numerics.Vector3(0, -source.Z, source.Y);

    /// <summary>Rotate the vector around the specified axis.</summary>
    public static System.Numerics.Vector3 RotateAroundAxis(this System.Numerics.Vector3 source, System.Numerics.Vector3 axis, float angle)
      => System.Numerics.Vector3.Transform(source, System.Numerics.Quaternion.CreateFromAxisAngle(axis, angle));
    /// <summary>Rotate the vector around the world axes.</summary>
    public static System.Numerics.Vector3 RotateAroundWorldAxes(this System.Numerics.Vector3 source, float yaw, float pitch, float roll)
      => System.Numerics.Vector3.Transform(source, System.Numerics.Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll));

    public static System.Numerics.Vector2 ToVector2(this System.Numerics.Vector3 source)
      => new System.Numerics.Vector2(source.X, source.Y);

    /// <summary>Determines the inclusion of a point in the 3D planar polygon. This Winding Number method counts the number of times the polygon winds around the point. The point is outside only when this "winding number" is 0, otherwise the point is inside. (2D/3D)</summary>
    //public static int InsidePolygon(this System.Numerics.Vector3 source, System.Collections.Generic.IList<System.Numerics.Vector3> polygon)
    //{
    //  var normal = polygon.SurfaceNormal();

    //  var x = System.Math.Abs(normal.X);
    //  var y = System.Math.Abs(normal.Y);
    //  var z = System.Math.Abs(normal.Z);

    //  if (z > x && z > y)
    //  {
    //    return source.InsidePolygonXY(polygon);
    //  }

    //  if (x > y)
    //  {
    //    return source.InsidePolygonXY(polygon.Select(v => new System.Numerics.Vector3() { X = v.Y, Y = v.Z }).ToList());
    //  }
    //  else
    //  {
    //    return source.InsidePolygonXY(polygon.Select(v => new System.Numerics.Vector3() { X = v.X, Y = v.Z }).ToList());
    //  }
    //}
    /// <summary>Determines the inclusion of a point in the (2D planar) polygon. This Winding Number method counts the number of times the polygon winds around the point. The point is outside only when this "winding number" is 0, otherwise the point is inside.</summary>
    /// <see cref="http://geomalgorithms.com/a03-_inclusion.html#wn_PnPoly"/>
    //public static int InsidePolygonXY(this System.Numerics.Vector3 source, System.Collections.Generic.IList<System.Numerics.Vector3> polygon)
    //{
    //  int wn = 0;

    //  for (int i = 0; i < polygon.Count; i++)
    //  {
    //    var a = polygon[i];
    //    var b = (i == polygon.Count - 1 ? polygon[0] : polygon[i + 1]);

    //    if (a.Y <= source.Y)
    //    {
    //      if (b.Y > source.Y && source.SideTestXY(a, b) > 0)
    //      {
    //        wn++;
    //      }
    //    }
    //    else
    //    {
    //      if (b.Y <= source.Y && source.SideTestXY(a, b) < 0)
    //      {
    //        wn--;
    //      }
    //    }
    //  }

    //  return wn;
    //}
  }
}
