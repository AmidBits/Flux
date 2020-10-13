using System.Linq;

namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>This is basically LERP with the the ability to compute an arbitrary point anywhere on the path from a to b (including before a and after b). The result, when the specified scalar is, <0 is a vector beyond a (backwards), 0 is vector a, 0.5 equals the midpoint vector between a and b, 1 is vector b, and >1 equals a vector beyond b (forward).</summary>>
    public static System.Numerics.Vector2 AlongPathTo(this System.Numerics.Vector2 source, System.Numerics.Vector2 target, float scalar = 0.5f)
      => (source + target) * scalar;

    /// <summary>Returns the angle for the source point to the other two specified points.</summary>>
    public static double AngleBetween(this System.Numerics.Vector2 source, System.Numerics.Vector2 before, System.Numerics.Vector2 after)
      => AngleTo(before - source, after - source);

    /// <summary>(2D) Calculate the angle between the source vector and the specified target vector.
    /// When dot eq 0 then the vectors are perpendicular.
    /// When dot gt 0 then the angle is less than 90 degrees (dot=1 can be interpreted as the same direction).
    /// When dot lt 0 then the angle is greater than 90 degrees (dot=-1 can be interpreted as the opposite direction).
    /// </summary>
    public static double AngleTo(this System.Numerics.Vector2 source, System.Numerics.Vector2 target)
      => System.Math.Acos(System.Math.Clamp(System.Numerics.Vector2.Dot(System.Numerics.Vector2.Normalize(source), System.Numerics.Vector2.Normalize(target)), -1, 1));

    /// <summary>Compute the Chebyshev distance from vector a to vector b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    public static double ChebyshevDistanceTo(this System.Numerics.Vector2 a, System.Numerics.Vector2 b, float edgeLength = 1)
      => System.Math.Max((b.X - a.X) / edgeLength, (b.Y - a.Y) / edgeLength);

    /// <summary>Compute the surface area of the polygon. The resulting area will be negative if clockwise and positive if counterclockwise.</summary>
    public static float ComputeAreaSigned(this System.Collections.Generic.IEnumerable<System.Numerics.Vector2> source)
      => source.PartitionTuple(true, (leading, trailing, index) => (leading.X * trailing.Y - trailing.X * leading.Y) / 2).Sum();
    /// <summary>Compute the surface area of the polygon.</summary>
    public static float ComputeArea(this System.Collections.Generic.IEnumerable<System.Numerics.Vector2> source)
      => System.Math.Abs(ComputeAreaSigned(source));

    /// <summary>Returns the centroid (a.k.a. geometric center, arithmetic mean, barycenter, etc.) point of the polygon. (2D/3D)</summary>
    public static System.Numerics.Vector2 ComputeCentroid(this System.Collections.Generic.IEnumerable<System.Numerics.Vector2> source)
      => source.Aggregate(System.Numerics.Vector2.Zero, (acc, vector, index) => acc + vector, (acc, count) => acc / count);

    /// <summary>Compute the perimeter length of the polygon.</summary>
    public static float ComputePerimeter(this System.Collections.Generic.IEnumerable<System.Numerics.Vector2> source)
      => source.PartitionTuple(true, (leading, trailing, index) => (trailing - leading).Length()).Sum();

    public static double EuclideanDistanceSquaredTo(this System.Numerics.Vector2 a, System.Numerics.Vector2 b)
      => (a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y);
    public static double EuclideanDistanceTo(this System.Numerics.Vector2 a, System.Numerics.Vector2 b)
      => System.Math.Sqrt(EuclideanDistanceSquaredTo(a, b));

    /// <summary>Returns a sequence triplet angles.</summary>
    public static System.Collections.Generic.IEnumerable<double> GetAngles(this System.Collections.Generic.IEnumerable<System.Numerics.Vector2> source)
      => PartitionTuple(source, 2, (leading, midling, trailing, index) => AngleBetween(midling, leading, trailing));
    /// <summary>Returns a sequence triplet angles.</summary>
    public static System.Collections.Generic.IEnumerable<(System.Numerics.Vector2, System.Numerics.Vector2, System.Numerics.Vector2, int index, double angle)> GetAnglesEx(this System.Collections.Generic.IEnumerable<System.Numerics.Vector2> source)
      => PartitionTuple(source, 2, (leading, midling, trailing, index) => (leading, midling, trailing, index, AngleBetween(midling, leading, trailing)));

    /// <summary>Creates a new sequence with the midpoints between all vertices in the sequence.</summary>
    public static System.Collections.Generic.IEnumerable<System.Numerics.Vector2> GetMidpoints(this System.Collections.Generic.IEnumerable<System.Numerics.Vector2> source)
      => PartitionTuple(source, true, (leading, trailing, index) => (trailing + leading) / 2);
    /// <summary>Creates a new sequence of triplets consisting of the leading vector, a newly computed midling vector and the trailing vector.</summary>
    public static System.Collections.Generic.IEnumerable<(System.Numerics.Vector2 leading, System.Numerics.Vector2 midpoint, System.Numerics.Vector2 trailing, int index)> GetMidpointsEx(this System.Collections.Generic.IEnumerable<System.Numerics.Vector2> source)
      => PartitionTuple(source, true, (leading, trailing, index) => (leading, (trailing + leading) / 2, trailing, index));

    /// <summary>Determines the inclusion of a vector in the (2D planar) polygon. This Winding Number method counts the number of times the polygon winds around the point. The point is outside only when this "winding number" is 0, otherwise the point is inside.</summary>
    /// <see cref="http://geomalgorithms.com/a03-_inclusion.html#wn_PnPoly"/>
    public static int InsidePolygon(this System.Collections.Generic.IList<System.Numerics.Vector2> source, System.Numerics.Vector2 vector)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      int wn = 0;

      for (int i = 0; i < source.Count; i++)
      {
        var a = source[i];
        var b = (i == source.Count - 1 ? source[0] : source[i + 1]);

        if (a.Y <= vector.Y)
        {
          if (b.Y > vector.Y && SideTest(vector, a, b) > 0)
          {
            wn++;
          }
        }
        else
        {
          if (b.Y <= vector.Y && SideTest(vector, a, b) < 0)
          {
            wn--;
          }
        }
      }

      return wn;
    }

    /// <summary>Determines whether the polygon is convex. (2D/3D)</summary>
    public static bool IsConvexPolygon(this System.Collections.Generic.IEnumerable<System.Numerics.Vector2> source)
    {
      bool negative = false, positive = false;

      foreach (var angle in GetAngles(source))
      {
        if (angle < 0)
          negative = true;
        else
          positive = true;

        if (negative && positive)
          return false;
      }

      return negative ^ positive;
    }

    /// <summary>Determines whether the polygon is equiangular, i.e. all angles are the same. (2D/3D)</summary>
    public static bool IsEquiangularPolygon(this System.Collections.Generic.IEnumerable<System.Numerics.Vector2> source)
      => source.PartitionTuple(2, (leading, midling, trailing, index) => AngleBetween(midling, leading, trailing)).AllEqual(out _);

    /// <summary>Determines whether the polygon is equiateral, i.e. all sides have the same length.</summary>
    public static bool IsEquilateralPolygon(this System.Collections.Generic.IEnumerable<System.Numerics.Vector2> source)
      => source.PartitionTuple(true, (leading, trailing, index) => (trailing - leading).Length()).AllEqual(out _);

    /// <summary>Compute the Manhattan length (or magnitude) of the vector. Known as the Manhattan distance (i.e. from 0,0,0).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public static double ManhattanDistanceTo(this System.Numerics.Vector2 a, System.Numerics.Vector2 b, float edgeLength = 1)
      => System.Math.Abs(b.X - a.X) / edgeLength + System.Math.Abs(b.Y - a.Y) / edgeLength;

    /// <summary>Returns a point -90 degrees perpendicular to the point, i.e. the point rotated 90 degrees counter clockwise. Only X and Y.</summary>
    public static System.Numerics.Vector2 PerpendicularCcw(this System.Numerics.Vector2 source)
      => new System.Numerics.Vector2(-source.Y, source.X);
    /// <summary>Returns a point 90 degrees perpendicular to the point, i.e. the point rotated 90 degrees clockwise. Only X and Y.</summary>
    public static System.Numerics.Vector2 PerpendicularCw(this System.Numerics.Vector2 source)
      => new System.Numerics.Vector2(source.Y, -source.X);

    /// <summary>Rotate the vector around the specified axis.</summary>
    public static System.Numerics.Vector2 RotateAroundAxis(this System.Numerics.Vector2 source, System.Numerics.Vector3 axis, float angle)
      => System.Numerics.Vector2.Transform(source, System.Numerics.Quaternion.CreateFromAxisAngle(axis, angle));
    /// <summary>Rotate the vector around the world axes.</summary>
    public static System.Numerics.Vector2 RotateAroundWorldAxes(this System.Numerics.Vector2 source, float yaw, float pitch, float roll)
      => System.Numerics.Vector2.Transform(source, System.Numerics.Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll));

    /// <summary>Returns the sign indicating whether the point is Left|On|Right of an infinite line (a to b). Through point1 and point2 the result has the meaning: greater than 0 is to the left of the line, equal to 0 is on the line, less than 0 is to the right of the line. (This is also known as an IsLeft function.)</summary>
    public static int SideTest(this System.Numerics.Vector2 source, System.Numerics.Vector2 a, System.Numerics.Vector2 b)
      => System.Math.Sign((source.X - b.X) * (a.Y - b.Y) - (a.X - b.X) * (source.Y - b.Y));

    /// <summary>Returns a sequence of triangles from the centroid to all midpoints and vertices. Creates a triangle fan from the centroid point. (2D/3D)</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    /// <remarks>Applicable to any shape. (Figure 1 and 8 in link)</remarks>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<System.Numerics.Vector2>> SplitAlongMidpoints(System.Collections.Generic.IEnumerable<System.Numerics.Vector2> source)
    {
      var midpointPolygon = new System.Collections.Generic.List<System.Numerics.Vector2>();

      foreach (var pair in GetMidpointsEx(source).PartitionTuple(true, (leading, trailing, index) => (leading, trailing)))
      {
        midpointPolygon.Add(pair.leading.midpoint);

        yield return new System.Collections.Generic.List<System.Numerics.Vector2>() { pair.leading.trailing, pair.trailing.midpoint, pair.leading.midpoint };
      }

      yield return midpointPolygon;
    }

    /// <summary>Returns a new set of quadrilaterals from the polygon centroid to its midpoints and their corresponding original vertex. Method 5 in link.</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<System.Numerics.Vector2>> SplitCentroidToMidpoints(this System.Collections.Generic.IEnumerable<System.Numerics.Vector2> source)
      => ComputeCentroid(source) is var c ? PartitionTuple(GetMidpoints(source), true, (leading, trailing, index) => new System.Collections.Generic.List<System.Numerics.Vector2>() { c, leading, trailing }) : throw new System.InvalidOperationException();

    /// <summary>Returns a new set of triangles from the polygon centroid to its points. Method 3 and 10 in link.</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<System.Numerics.Vector2>> SplitCentroidToVertices(this System.Collections.Generic.IEnumerable<System.Numerics.Vector2> source)
      => ComputeCentroid(source) is var c ? PartitionTuple(source, true, (leading, trailing, index) => new System.Collections.Generic.List<System.Numerics.Vector2>() { c, leading, trailing }) : throw new System.InvalidOperationException();

    /// <summary>Returns a new set of polygons by splitting the polygon at two points. Method 2 in link when odd number of vertices. method 9 in link when even number of vertices.</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<System.Numerics.Vector2>> SplitInHalf(this System.Collections.Generic.IEnumerable<System.Numerics.Vector2> source)
    {
      var half1 = new System.Collections.Generic.List<System.Numerics.Vector2>();
      var half2 = new System.Collections.Generic.List<System.Numerics.Vector2>();

      foreach (var item in source ?? throw new System.ArgumentNullException(nameof(source)))
      {
        half2.Add(item);

        if (half2.Count > half1.Count)
        {
          half1.Add(half2[0]);
          half2.RemoveAt(0);
        }
      }

      if (half1.Count > half2.Count)
      {
        var midway = AlongPathTo(half1[half1.Count - 1], half2[0]);

        half1.Add(midway);
        half2.Insert(0, midway);
      }
      else if (half1.Count == half2.Count)
      {
        half1.Add(half2[0]);
      }

      half2.Add(half1[0]);

      yield return half1;
      yield return half2;
    }

    public enum TriangulationType
    {
      Sequential,
      SmallestAngle,
      LargestAngle,
      MostSquare,
      LeastSquare,
      Randomized,
    }

    /// <summary>Returns a sequence of triangles from the vertices of the polygon. Triangles with a vertex angle greater or equal to 0 degrees and less than 180 degrees are extracted first. Triangles are returned in the order of smallest to largest angle. (2D/3D)</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    /// <remarks>Applicable to any shape with more than 3 vertices.</remarks>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<System.Numerics.Vector2>> SplitByTriangulation(this System.Collections.Generic.IEnumerable<System.Numerics.Vector2> source, TriangulationType mode)
    {
      var copy = source.ToList();

      (System.Numerics.Vector2 leading, System.Numerics.Vector2 midling, System.Numerics.Vector2 trailing, int index) triplet = default;

      while (copy.Count >= 3)
      {
        switch (mode)
        {
          case TriangulationType.Sequential:
            triplet = copy.PartitionTuple(2, (leading, midling, trailing, i) => (leading, midling, trailing, i)).First();
            break;
          case TriangulationType.Randomized:
            triplet = copy.PartitionTuple(2, (leading, midling, trailing, i) => (leading, midling, trailing, i)).Skip(Random.NumberGenerator.Crypto.Next(copy.Count - 1)).First();
            break;
          case TriangulationType.SmallestAngle:
            var ascendingAngle = GetAnglesEx(copy).Aggregate((a, b) => a.angle < b.angle ? a : b);
            triplet = (ascendingAngle.Item1, ascendingAngle.Item2, ascendingAngle.Item3, ascendingAngle.index);
            break;
          case TriangulationType.LargestAngle:
            var descendingAngle = GetAnglesEx(copy).Aggregate((a, b) => a.angle > b.angle ? a : b);
            triplet = (descendingAngle.Item1, descendingAngle.Item2, descendingAngle.Item3, descendingAngle.index);
            break;
          case TriangulationType.LeastSquare:
            var leastSquare = GetAnglesEx(copy).Aggregate((System.Func<(System.Numerics.Vector2, System.Numerics.Vector2, System.Numerics.Vector2, int index, double angle), (System.Numerics.Vector2, System.Numerics.Vector2, System.Numerics.Vector2, int index, double angle), (System.Numerics.Vector2, System.Numerics.Vector2, System.Numerics.Vector2, int index, double angle)>)((a, b) => System.Math.Abs(a.angle - Maths.PiOver2) > System.Math.Abs(b.angle - Maths.PiOver2) ? a : b));
            triplet = (leastSquare.Item1, leastSquare.Item2, leastSquare.Item3, leastSquare.index);
            break;
          case TriangulationType.MostSquare:
            var mostSquare = GetAnglesEx(copy).Aggregate((System.Func<(System.Numerics.Vector2, System.Numerics.Vector2, System.Numerics.Vector2, int index, double angle), (System.Numerics.Vector2, System.Numerics.Vector2, System.Numerics.Vector2, int index, double angle), (System.Numerics.Vector2, System.Numerics.Vector2, System.Numerics.Vector2, int index, double angle)>)((a, b) => System.Math.Abs(a.angle - Maths.PiOver2) < System.Math.Abs(b.angle - Maths.PiOver2) ? a : b));
            triplet = (mostSquare.Item1, mostSquare.Item2, mostSquare.Item3, mostSquare.index);
            break;
          default:
            throw new System.Exception();
        }

        yield return new System.Collections.Generic.List<System.Numerics.Vector2>() { triplet.midling, triplet.trailing, triplet.leading };

        copy.RemoveAt((triplet.index + 1) % copy.Count);
      }
    }

    /// <summary>Returns a sequence of triangles from the specified polygon index to all other points. Creates a triangle fan from the specified point. (2D/3D)</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    /// <remarks>Applicable to any shape with more than 3 vertices. (Figure 9, in link)</remarks>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<System.Numerics.Vector2>> SplitVertexToVertices(this System.Collections.Generic.IList<System.Numerics.Vector2> source, int index)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var vertex = source[index];

      var startIndex = index + 1;
      var count = startIndex + source.Count - 2;

      for (var i = startIndex; i < count; i++)
      {
        yield return new System.Numerics.Vector2[] { vertex, source[i % source.Count], source[(i + 1) % source.Count] };
      }
    }

    /// <summary>Convert a 2D vector to a point.</summary>
    public static System.Drawing.Point ToPoint(this in System.Numerics.Vector2 source)
      => new System.Drawing.Point((int)source.X, (int)source.Y);
    /// <summary>Convert a 2D vector to a 3D vector.</summary>
    public static System.Numerics.Vector3 ToVector3(this System.Numerics.Vector2 source)
      => new System.Numerics.Vector3(source, 0);
  }
}
