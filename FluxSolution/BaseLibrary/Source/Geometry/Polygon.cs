namespace Flux.Geometry
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly struct Polygon
    : System.IEquatable<Polygon>
  //, I2DSurfaceArea, /*I2DSurfaceCentroid,*/ I2DSurfacePerimeter
  {
    public static readonly Polygon Empty;

    public System.Collections.Generic.IReadOnlyList<System.Numerics.Vector3> Vertices { get; }

    public Polygon(System.Collections.Generic.IEnumerable<System.Numerics.Vector3> vertices)
      => Vertices = new System.Collections.Generic.List<System.Numerics.Vector3>(vertices);
    public Polygon(params System.Numerics.Vector3[] vertices)
      : this(vertices.AsEnumerable())
    {
    }

    public static System.Collections.Generic.IEnumerable<string> GetOneHundredPolygonNames()
    {
      string[] NameOfOnes = new string[] { string.Empty, @"hena", @"di", @"tri", @"tetra", @"penta", @"hexa", @"hepta", @"octa", @"ennea" };
      string[] NameOfTens = new string[] { string.Empty, @"deca", @"icosi", @"triaconta", @"tetraconta", @"pentaconta", @"hexaconta", @"heptaconta", @"octaconta", @"enneaconta" };

      for (var index = 1; index < 100; index++)
      {
        var name = index switch
        {
          1 => @"mono",
          2 => @"di",
          9 => @"nona",
          20 => @"icosa",
          var tens when tens >= 10 && tens <= 90 && tens % 10 == 0 => $"{NameOfTens[tens / 10]}",
          11 => @"hendeca",
          12 => @"dodeca",
          var tens when tens >= 10 && tens <= 19 && tens % 10 is var tensRemainder => $"{NameOfOnes[tensRemainder]}deca",
          100 => @"hecto",
          _ => $"{NameOfTens[index / 10]}{NameOfOnes[index % 10]}",
        };

        yield return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name + @"gon");
      }
    }

    ///// <summary>The surface area of the polygon. The resulting area will be negative if clockwise and positive if counterclockwise.</summary>
    //public double GetSurfaceAreaSigned()
    //  => Vertices.ComputeAreaSigned();
    ///// <summary>The surface area of the polygon.</summary>
    //public double Get2DSurfaceArea()
    //  => Vertices.ComputeArea();

    ///// <summary>The centroid (a.k.a. geometric center, arithmetic mean, barycenter, etc.) point of the polygon. (2D/3D)</summary>
    //public System.Numerics.Vector3 GetSurfaceCentroid()
    //  => Vertices.ComputeCentroid();
    ////public (double x, double y) GetSurfaceCentroid()
    ////  => Vertices.ComputeCentroid() is var c ? (c.X, c.Y) : throw new System.ArithmeticException();

    ///// <summary>Compute the surface normal of the polygon, which is simply the cross product of three vertices (as in a subtriangle of the polygon).</summary>
    ////  Modified from http://www.fullonsoftware.co.uk/snippets/content/Math_-_Calculating_Face_Normals.pdf
    //public System.Numerics.Vector3 GetSurfaceNormal()
    //  => Vertices.ComputeNormal();

    ///// <summary>Compute the perimeter length of the polygon.</summary>
    //public double Get2DSurfacePerimeter()
    //  => Vertices.ComputePerimeter();

    #region Static methods
    /// <summary>Returns all vertices interlaced with all midpoints (halfway) of the polygon.</summary>
    public static System.Collections.Generic.IEnumerable<System.Numerics.Vector2> AddMidpoints(System.Collections.Generic.IEnumerable<System.Numerics.Vector2> source)
      => source.PartitionTuple2(true, (leading, trailing, index) => (leading, trailing)).SelectMany(edge => new System.Numerics.Vector2[] { edge.leading, (edge.trailing + edge.leading) / 2 });
    /// <summary>Returns all vertices interlaced with all midpoints (halfway) of the polygon. (2D/3D)</summary>
    public static System.Collections.Generic.IEnumerable<System.Numerics.Vector3> AddMidpoints(System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
      => source.PartitionTuple2(true, (leading, trailing, index) => (leading, trailing)).SelectMany(pair => new System.Numerics.Vector3[] { pair.leading, (pair.leading + pair.trailing) / 2 });

    public static bool TryGetAxisAlignedBoundingBox(System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source, out System.Numerics.Vector3 min, out System.Numerics.Vector3 max)
    {

      using var e = source.GetEnumerator();

      if (e.MoveNext())
      {
        min = e.Current;
        max = min;

        while (e.MoveNext())
        {
          min = System.Numerics.Vector3.Min(min, e.Current);
          max = System.Numerics.Vector3.Max(max, e.Current);
        }

        return true;
      }

      min = System.Numerics.Vector3.Zero;
      max = System.Numerics.Vector3.Zero;

      return false;
    }

    /// <summary>Determines the inclusion of a point in the (2D planar) polygon. This Winding Number method counts the number of times the polygon winds around the point. The point is outside only when this "winding number" is 0, otherwise the point is inside.</summary>
    /// <see cref="http://geomalgorithms.com/a03-_inclusion.html#wn_PnPoly"/>
    public static int ContainsPoint(System.Collections.Generic.IList<System.Numerics.Vector2> polygon, System.Numerics.Vector2 point)
    {
      if (polygon is null) throw new System.ArgumentNullException(nameof(polygon));

      int wn = 0;

      for (int i = 0; i < polygon.Count; i++)
      {
        var a = polygon[i];
        var b = (i == polygon.Count - 1 ? polygon[0] : polygon[i + 1]);

        if (a.Y <= point.Y)
        {
          if (b.Y > point.Y && LineSegment.SideTest(point, a, b) > 0)
          {
            wn++;
          }
        }
        else
        {
          if (b.Y <= point.Y && LineSegment.SideTest(point, a, b) < 0)
          {
            wn--;
          }
        }
      }

      return wn;
    }

    /// <summary>Returns a sequence of triplets, their angles and ordinal indices. (2D/3D)</summary>
    //public static System.Collections.Generic.IEnumerable<(System.Numerics.Vector3, System.Numerics.Vector3, System.Numerics.Vector3, int index, float angle)> GetAngles(System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
    //  => source.PartitionTuple3(2, (leading, midling, trailing, index) => (leading, midling, trailing, index, midling.AngleBetween(leading, trailing)));

    /// <summary>Returns all midpoints (halfway) points of the polygon.</summary>
    //public static System.Collections.Generic.IEnumerable<System.Numerics.Vector2> GetMidpoints(System.Collections.Generic.IEnumerable<System.Numerics.Vector2> source)
    //  => source.PartitionTuple2(true, (leading, trailing, index) => (leading, trailing)).Select(edge => (edge.trailing + edge.leading) / 2);
    /// <summary>Returns all midpoints (halfway) points of the polygon. (2D/3D)</summary>
    //public static System.Collections.Generic.IEnumerable<(System.Numerics.Vector3 midpoint, (System.Numerics.Vector3, System.Numerics.Vector3) pair)> GetMidpoints(System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
    //  => source.PartitionTuple2(true, (leading, trailing, index) => ((trailing + leading) / 2, (leading, trailing)));

    /// <summary>Determines whether the polygon is convex. (2D/3D)</summary>
    //public static bool IsConvex(System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
    //{
    //  bool negative = false, positive = false;

    //  foreach (var angle in source.PartitionTuple3(2, (leading, midling, trailing, index) => midling.AngleBetween(leading, trailing)))
    //  {
    //    if (angle < 0) negative = true;
    //    else positive = true;

    //    if (negative && positive) return false;
    //  }

    //  return negative ^ positive;
    //}
    //public static bool IsConvex2(System.Collections.Generic.IList<System.Numerics.Vector3> polygon)
    //{
    //  bool negative = false, positive = false;

    //  for (int i = 0; i < polygon.Count; i++)
    //  {
    //    var c = polygon[i];
    //    var a = polygon[(i == 0 ? polygon.Count - 1 : i - 1)] - c;
    //    var b = polygon[(i == polygon.Count - 1 ? 0 : i + 1)] - c;

    //    if (System.Math.Acos(System.Numerics.Vector3.Dot(System.Numerics.Vector3.Normalize(a), System.Numerics.Vector3.Normalize(b))) is double angle && angle < 0)
    //    {
    //      negative = true;
    //    }
    //    else
    //    {
    //      positive = true;
    //    }

    //    if (negative && positive)
    //    {
    //      return false;
    //    }
    //  }

    //  return (negative ^ positive);
    //}

    /// <summary>Determines whether the polygon is equiangular, i.e. all angles are the same. (2D/3D)</summary>
    //public static bool IsEquiangular(System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
    //{
    //  var angle = double.NaN;

    //  foreach (var triplet in source.PartitionTuple3(2, (leading, midling, trailing, index) => (leading, midling, trailing)))
    //  {
    //    if (double.IsNaN(angle)) angle = triplet.midling.AngleBetween(triplet.leading, triplet.trailing);
    //    else if (angle != triplet.midling.AngleBetween(triplet.leading, triplet.trailing)) return false;
    //  }

    //  return true;
    //}

    /// <summary>Determines whether the polygon is equiateral, i.e. all sides have the same length. (2D/3D)</summary>
    //public static bool IsEquilateral(System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
    //{
    //  var length = double.NaN;

    //  foreach (var pair in source.PartitionTuple2(true, (leading, trailing, index) => (leading, trailing)))
    //  {
    //    if (double.IsNaN(length)) length = (pair.trailing - pair.leading).Length();
    //    else if (length != (pair.trailing - pair.leading).Length()) return false;
    //  }

    //  return true;
    //}

    //#region Potential base interface and class for splitter framework.
    //interface ISplitter
    //{
    //  System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<System.Numerics.Vector3>>
    //     Split(System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source);
    //}

    //public enum SplitTargetEnum
    //{
    //  Midpoints,
    //  Vertices
    //}

    //public abstract class CSplitter : ISplitter
    //{
    //  public abstract System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<System.Numerics.Vector3>> Split(System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source);
    //}

    //public class CentroidSplitter : CSplitter
    //{
    //  public SplitTargetEnum Target { get; set; }

    //  public CentroidSplitter(SplitTargetEnum target)
    //  {
    //    Target = target;
    //  }

    //  public override System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<System.Numerics.Vector3>> Split(System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
    //  {
    //    throw new System.NotImplementedException();
    //  }
    //}

    //public class VertexSplitter : CSplitter
    //{
    //  public SplitTargetEnum Target { get; set; }

    //  public VertexSplitter(SplitTargetEnum target)
    //  {
    //    Target = target;
    //  }

    //  public override System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<System.Numerics.Vector3>> Split(System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
    //  {
    //    throw new System.NotImplementedException();
    //  }
    //}
    //#endregion

    /// <summary>Returns a sequence of triangles from the centroid to all midpoints and vertices. Creates a triangle fan from the centroid point. (2D/3D)</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    /// <remarks>Applicable to any shape. (Figure 1 and 8 in link)</remarks>
    //    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<System.Numerics.Vector3>> SplitAlongMidpoints(System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
    //      => source.SplitAlongMidpoints();
    //{
    //  var midpointPolygon = new System.Collections.Generic.List<System.Numerics.Vector3>();

    //  foreach (var pair in GetMidpoints(source).PartitionTuple2(true, (leading, trailing, index) => (leading, trailing)))
    //  {
    //    midpointPolygon.Add(pair.leading.midpoint);

    //    yield return new System.Collections.Generic.List<System.Numerics.Vector3>() { pair.leading.pair.Item2, pair.trailing.midpoint, pair.leading.midpoint };
    //  }

    //  yield return midpointPolygon;
    //}

    /// <summary>Returns a sequence of triangles from the vertices of the polygon. Triangles with a vertex angle greater or equal to 0 degrees and less than 180 degrees are extracted first. Triangles are returned in the order of smallest to largest angle. (2D/3D)</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    /// <remarks>Applicable to any shape with more than 3 vertices.</remarks>
    //    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<System.Numerics.Vector3>> SplitByTriangulation(System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source, TriangulationType mode)
    //      => source.SplitByTriangulation(mode);
    //{
    //  var rng = new System.Random();

    //  var copy = source.ToList();

    //  (System.Numerics.Vector3 leading, System.Numerics.Vector3 midling, System.Numerics.Vector3 trailing, int index) triplet = default;

    //  while (copy.Count >= 3)
    //  {
    //    switch (mode)
    //    {
    //      case TriangulationType.Sequential:
    //        triplet = copy.PartitionTuple3(2, (leading, midling, trailing, i) => (leading, midling, trailing, i)).First();
    //        break;
    //      case TriangulationType.Randomized:
    //        triplet = copy.PartitionTuple3(2, (leading, midling, trailing, i) => (leading, midling, trailing, i)).Skip(rng.Next(copy.Count - 1)).First();
    //        break;
    //      case TriangulationType.SmallestAngle:
    //        var ascendingAngle = GetAngles(copy).Aggregate((a, b) => a.angle < b.angle ? a : b);
    //        triplet = (ascendingAngle.Item1, ascendingAngle.Item2, ascendingAngle.Item3, ascendingAngle.index);
    //        break;
    //      case TriangulationType.LargestAngle:
    //        var descendingAngle = GetAngles(copy).Aggregate((a, b) => a.angle > b.angle ? a : b);
    //        triplet = (descendingAngle.Item1, descendingAngle.Item2, descendingAngle.Item3, descendingAngle.index);
    //        break;
    //      case TriangulationType.LeastSquare:
    //        var leastSquare = GetAngles(copy).Aggregate((System.Func<(System.Numerics.Vector3, System.Numerics.Vector3, System.Numerics.Vector3, int index, double angle), (System.Numerics.Vector3, System.Numerics.Vector3, System.Numerics.Vector3, int index, double angle), (System.Numerics.Vector3, System.Numerics.Vector3, System.Numerics.Vector3, int index, double angle)>)((a, b) => System.Math.Abs(a.angle - Maths.PiOver2) > System.Math.Abs(b.angle - Maths.PiOver2) ? a : b));
    //        triplet = (leastSquare.Item1, leastSquare.Item2, leastSquare.Item3, leastSquare.index);
    //        break;
    //      case TriangulationType.MostSquare:
    //        var mostSquare = GetAngles(copy).Aggregate((System.Func<(System.Numerics.Vector3, System.Numerics.Vector3, System.Numerics.Vector3, int index, double angle), (System.Numerics.Vector3, System.Numerics.Vector3, System.Numerics.Vector3, int index, double angle), (System.Numerics.Vector3, System.Numerics.Vector3, System.Numerics.Vector3, int index, double angle)>)((a, b) => System.Math.Abs(a.angle - Maths.PiOver2) < System.Math.Abs(b.angle - Maths.PiOver2) ? a : b));
    //        triplet = (mostSquare.Item1, mostSquare.Item2, mostSquare.Item3, mostSquare.index);
    //        break;
    //      default:
    //        throw new System.Exception();
    //    }

    //    yield return new System.Collections.Generic.List<System.Numerics.Vector3>() { triplet.midling, triplet.trailing, triplet.leading };

    //    copy.RemoveAt((triplet.index + 1) % copy.Count);
    //  }
    //}

    /// <summary>Returns a sequence of triangles from the centroid to all midpoints and vertices. Creates a triangle fan from the centroid point. (2D/3D)</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    /// <remarks>Applicable to any shape. (Figure 5 in link)</remarks>
    //    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<System.Numerics.Vector3>> SplitCentroidToMidpoints(System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
    //      => source.SplitCentroidToMidpoints();
    //=> source.ComputeCentroid() is System.Numerics.Vector3 sc ? GetMidpoints(source).PartitionTuple2(true, (leading, trailing, index) => new System.Collections.Generic.List<System.Numerics.Vector3>() { sc, leading.midpoint, leading.pair.Item2, trailing.midpoint }) : throw new System.InvalidOperationException();
    /// <summary>Returns a sequence of triangles from the centroid to all vertices. Creates a triangle fan from the centroid point. (2D/3D)</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    /// <remarks>Applicable to any shape. (Figure 3 and 10 in link)</remarks>
    //    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<System.Numerics.Vector3>> SplitCentroidToVertices(System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
    //      => source.SplitCentroidToVertices();
    //=> source.ComputeCentroid() is System.Numerics.Vector3 sc ? source.PartitionTuple2(true, (leading, trailing, index) => new System.Collections.Generic.List<System.Numerics.Vector3>() { sc, leading, trailing }) : throw new System.InvalidOperationException();

    /// <summary>Returns two polygons by splitting the polygon at two points. (2D/3D)</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    /// <remarks>Applicable to any shape. (Figure 2 if odd count vertices and figure 9 if even count vertices, in link)</remarks>
    //    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<System.Numerics.Vector3>> SplitInHalf(System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
    //      => source.SplitInHalf();
    //{
    //  var half1 = new System.Collections.Generic.List<System.Numerics.Vector3>();
    //  var half2 = new System.Collections.Generic.List<System.Numerics.Vector3>();

    //  foreach (var item in source ?? throw new System.ArgumentNullException(nameof(source)))
    //  {
    //    if (half1.Count > half2.Count)
    //    {
    //      half2.Add(half1[0]);

    //      half1.RemoveAt(0);
    //    }

    //    half1.Add(item);
    //  }

    //  if (half2.Count < 3)
    //  {
    //    var midpoint = Line.IntermediaryPoint(half1[half1.Count - 1], half2[0]);

    //    half1.Add(midpoint);
    //    half2.Insert(0, midpoint);
    //  }

    //  half1.Add(half2[0]);
    //  half2.Add(half1[0]);

    //  yield return half1;
    //  yield return half2;
    //}

    /// <summary>Returns a sequence of triangles from the specified polygon index to all midpoints, splitting all triangles at their midpoint along the polygon perimeter. Creates a triangle fan from the specified point. (2D/3D)</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    /// <remarks>Applicable to any shape. (Figure 2, in link)</remarks>
    //    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<System.Numerics.Vector3>> SplitVertexToMidpoints(System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source, int index)
    //      => source.SplitVertexToMidpoints(index);
    //{
    //  var angles = GetAngles(source).ToList();

    //  if (index < 0 || index > angles.Count - 1)
    //  {
    //    index = (index == -1) ? angles.Aggregate((a, b) => (a.angle > b.angle) ? a : b).index : throw new System.ArgumentOutOfRangeException(nameof(index));
    //  }

    //  var vertex = angles[index].Item2;

    //  var startIndex = index + 1;
    //  var count = startIndex + angles.Count - 2;

    //  for (var i = startIndex; i < count; i++)
    //  {
    //    var triplet = angles[i % angles.Count];
    //    var midpoint23 = Line.IntermediaryPoint(triplet.Item2, triplet.Item3);

    //    yield return new System.Collections.Generic.List<System.Numerics.Vector3>() { vertex, triplet.Item2, midpoint23 };
    //    yield return new System.Collections.Generic.List<System.Numerics.Vector3>() { vertex, midpoint23, triplet.Item3 };
    //  }
    //}
    /// <summary>Returns a sequence of triangles from the specified polygon index to all other points. Creates a triangle fan from the specified point. (2D/3D)</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    /// <remarks>Applicable to any shape with more than 3 vertices. (Figure 9, in link)</remarks>
    //    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<System.Numerics.Vector3>> SplitVertexToVertices(System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source, int index)
    //      => source.SplitVertexToVertices(index);
    //{
    //  var angles = GetAngles(source).ToList();

    //  if (index < 0 || index > angles.Count - 1)
    //  {
    //    index = (index == -1) ? (angles.Aggregate((a, b) => (a.angle > b.angle) ? a : b).index - 1 + angles.Count) % angles.Count : throw new System.ArgumentOutOfRangeException(nameof(index));
    //  }

    //  var vertex = angles[index].Item2;

    //  var startIndex = index + 1;
    //  var count = startIndex + angles.Count - 2;

    //  for (var i = startIndex; i < count; i++)
    //  {
    //    yield return new System.Collections.Generic.List<System.Numerics.Vector3>() { vertex, angles[i % angles.Count].Item2, angles[(i + 1) % angles.Count].Item2 };
    //  }
    //}

    ///// <summary>Determines the inclusion of a point in the (2D planar) polygon. This Winding Number method counts the number of times the polygon winds around the point. The point is outside only when this "winding number" is 0, otherwise the point is inside.</summary>
    ///// <see cref="http://geomalgorithms.com/a03-_inclusion.html#wn_PnPoly"/>
    //public static int InclusionTest(System.Collections.Generic.IList<System.Numerics.Vector2> polygon, System.Numerics.Vector2 point)
    //{
    //  int wn = 0;

    //  System.Numerics.Vector2 a, b;

    //  for (int i = 0; i < polygon.Count; i++)
    //  {
    //    a = polygon[i];
    //    b = (i == polygon.Count - 1 ? polygon[0] : polygon[i + 1]);

    //    if (a.Y <= point.Y)
    //    {
    //      if (b.Y > point.Y)
    //        if (point.SideTest(a, b) > 0)
    //          wn++;
    //    }
    //    else
    //    {
    //      if (b.Y <= point.Y)
    //        if (point.SideTest(a, b) < 0)
    //          wn--;
    //    }
    //  }

    //  return wn;
    //}
    ///// <summary>Determines whether the specified polygons A and B intersect.</summary>
    //public static bool IntersectingPolygon(System.Collections.Generic.IList<System.Numerics.Vector2> polygonA, System.Collections.Generic.IList<System.Numerics.Vector2> polygonB)
    //{
    //  if (Geometry.Line.IntersectionTest(polygonA[polygonA.Count - 1], polygonA[0], polygonB[polygonB.Count - 1], polygonB[0]).Outcome == Geometry.Line.TestOutcome.LinesIntersecting)
    //    return true;
    //  for (int i = 1; i < polygonA.Count; i++)
    //  {
    //    if (Geometry.Line.IntersectionTest(polygonA[i - 1], polygonA[i], polygonB[polygonB.Count - 1], polygonB[0]).Outcome == Geometry.Line.TestOutcome.LinesIntersecting)
    //      return true;
    //    for (int p = 1; p < polygonB.Count; p++)
    //    {
    //      if (Geometry.Line.IntersectionTest(polygonA[i - 1], polygonA[i], polygonB[p - 1], polygonB[p]).Outcome == Geometry.Line.TestOutcome.LinesIntersecting)
    //        return true;
    //    }
    //  }
    //  return false;
    //  //return t.Any(point => point.InsidePolygon(polygon)) || polygon.Any(point => point.InsidePolygon(t));
    //}
    ///// <summary>Returns all midpoints (halfway) points of the polygon. Including original points if the entire set (twice the number of points).</summary>
    //public static System.Collections.Generic.IEnumerable<System.Numerics.Vector2> MidwayPoints(System.Collections.Generic.IEnumerable<System.Numerics.Vector2> polygon, bool includeOriginalPoints = false)
    //{
    //  using (var enumerator = polygon.GetEnumerator())
    //  {
    //    if (enumerator.MoveNext())
    //    {
    //      var first = enumerator.Current;

    //      var previous = first;

    //      while (enumerator.MoveNext())
    //      {
    //        if (includeOriginalPoints)
    //          yield return previous;

    //        yield return (previous + enumerator.Current) * 0.5f;

    //        previous = enumerator.Current;
    //      }

    //      if (includeOriginalPoints)
    //        yield return previous;

    //      yield return (previous + first) * 0.5f;
    //    }
    //  }
    //}

    ///// <summary>Returns a new polygon as System.Collections.Generic.IEnumerable from the polygon using optional indices. No indices results in a copy of the polygon.</summary>
    //public static System.Collections.Generic.IEnumerable<System.Numerics.Vector2> SubPolygon(System.Collections.Generic.IEnumerable<System.Numerics.Vector2> polygon, params int[] indices)
    //{
    //  foreach (var index in indices)
    //    yield return polygon.ElementAt(index);
    //}
    ///// <summary>Returns a new set of triangular polygons from the specified polygon.</summary>
    ////public static void Triangulate(System.Collections.Generic.IList<System.Numerics.Vector2> polygon)
    //public static System.Collections.Generic.IEnumerable<System.Numerics.Vector2[]> Triangulate(System.Collections.Generic.IList<System.Numerics.Vector2> polygon)
    //{
    //  System.Collections.Generic.IList<System.Numerics.Vector2> copy = polygon.ToList();

    //  System.Numerics.Vector2[] triangle = new System.Numerics.Vector2[3];

    //  System.Random random = new System.Random();

    //  while (copy.Count > 3)
    //  {
    //    for (int i = 0; i < copy.Count; i++)
    //    {
    //      triangle[0] = copy[i]; // c
    //      triangle[1] = copy[(i == copy.Count - 1 ? 0 : i + 1)]; // b
    //      triangle[2] = copy[(i == 0 ? copy.Count - 1 : i - 1)]; // a

    //      var angle = System.Math.Acos(System.Numerics.Vector2.Dot(System.Numerics.Vector2.Normalize(triangle[2] - triangle[0]), System.Numerics.Vector2.Normalize(triangle[1] - triangle[0])));

    //      if (angle > 0 && angle < System.Math.PI)
    //      {
    //        yield return triangle;

    //        copy.RemoveAt(i);

    //        break;
    //      }
    //    }
    //  }

    //  yield return copy.ToArray();
    //}

    //#region Triangle Geometry
    ///// <summary>Returns the area of a triangle.</summary>
    //public static double TriangleArea(System.Numerics.Vector2 v0, System.Numerics.Vector2 v1, System.Numerics.Vector2 v2)
    //{
    //  return (v1.X - v0.X) * (v2.Y - v0.Y) - (v2.X - v0.X) * (v1.Y - v0.Y);
    //}
    ///// <summary>Returns the area of a quadrilateral.</summary>
    //public static double QuadrilateralArea(System.Numerics.Vector2 v0, System.Numerics.Vector2 v1, System.Numerics.Vector2 v2, System.Numerics.Vector2 v3)
    //{
    //  return ((v2.X - v0.X) * (v3.Y - v1.Y) - (v3.X - v1.X) * (v2.Y - v0.Y)) / 2;
    //}
    ///// <summary>Returns the area of a triangle.</summary>
    //public static double Area(System.Numerics.Vector2 a, System.Numerics.Vector2 b, System.Numerics.Vector2 c) => (c.X - a.X) * (b.Y - a.Y) - (b.X - a.X) * (c.Y - a.Y);
    //public static int AreaSign(System.Numerics.Vector2 a, System.Numerics.Vector2 b, System.Numerics.Vector2 c) => Area(a, b, c) is var area && area > 0.5 ? 1 : area < -0.5 ? -1 : 0;

    //public static bool Between(System.Numerics.Vector2 a, System.Numerics.Vector2 b, System.Numerics.Vector2 c)
    //{
    //  //System.Numerics.Vector2 ba, ca;

    //  if (!IsCollinear(a, b, c))
    //    return false;

    //  if (a.X != b.X)
    //    return ((a.X <= b.X) && (b.X <= c.X)) || ((a.X >= b.X) && (b.X >= c.X));
    //  else
    //    return ((a.Y <= b.Y) && (b.Y <= c.Y)) || ((a.Y >= b.Y) && (b.Y >= c.Y));
    //}

    //public static bool Diagonally(System.Numerics.Vector2 a, System.Numerics.Vector2 b, System.Numerics.Vector2 c)
    //{
    //  throw new System.NotImplementedException();
    //}

    //public static bool IsLeft(System.Numerics.Vector2 a, System.Numerics.Vector2 b, System.Numerics.Vector2 c) => AreaSign(a, b, c) > 0;
    //public static bool IsLeftOn(System.Numerics.Vector2 a, System.Numerics.Vector2 b, System.Numerics.Vector2 c) => AreaSign(a, b, c) >= 0;
    //public static bool IsCollinear(System.Numerics.Vector2 a, System.Numerics.Vector2 b, System.Numerics.Vector2 c) => AreaSign(a, b, c) == 0;

    //public static bool Intersect(System.Numerics.Vector2 a, System.Numerics.Vector2 b, System.Numerics.Vector2 c, System.Numerics.Vector2 d)
    //{
    //  if (IntersectProp(a, b, c, d))
    //    return true;
    //  else if (Between(a, b, c) || Between(a, b, d) || Between(c, d, a) || Between(c, d, b))
    //    return true;
    //  else
    //    return false;
    //}

    //public static bool IntersectProp(System.Numerics.Vector2 a, System.Numerics.Vector2 b, System.Numerics.Vector2 c, System.Numerics.Vector2 d)
    //{
    //  if (IsCollinear(a, b, c) || IsCollinear(a, b, d) || IsCollinear(c, d, a) || IsCollinear(c, d, b))
    //    return false;

    //  return Xor(IsLeft(a, b, c), IsLeft(a, b, d)) && Xor(IsLeft(c, d, a), IsLeft(c, d, b));
    //}

    //public static bool Xor(bool x, bool y)
    //{
    //  return !x ^ !y;
    //}
    //#endregion

    //  public interface ISplitter
    //{

    //}

    //public class TriangulationSplitter : CSplitter
    //{
    //  public enum TriangulatingAlgorithmEnum
    //  {
    //    AngleAscending = 0,
    //    AngleDescending = 1,
    //    MostSquare = 2,
    //    Randomized
    //  }

    //  public TriangulatingAlgorithmEnum Algorithm { get; set; } = TriangulatingAlgorithmEnum.Randomized;

    //  public Flux.Random.NumberGenerator RandomNumberGenerator { get; set; } = new Flux.Random.NumberGenerator();

    //  public override System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<System.Numerics.Vector3>>
    //    Split(System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
    //  {
    //    var copy = source.ToList();

    //    (System.Numerics.Vector3, System.Numerics.Vector3, System.Numerics.Vector3, int Index) triplet = default;

    //    while (copy.Count >= 3)
    //    {
    //      switch (Algorithm)
    //      {
    //        case TriangulatingAlgorithmEnum.AngleAscending:
    //          var ascendingAngle = GetAngles(copy).Aggregate((a, b) => a.angle < b.angle ? a : b);
    //          triplet = (ascendingAngle.Item1, ascendingAngle.Item2, ascendingAngle.Item3, ascendingAngle.index);
    //          break;
    //        case TriangulatingAlgorithmEnum.AngleDescending:
    //          var descendingAngle = GetAngles(copy).Aggregate((a, b) => a.angle > b.angle ? a : b);
    //          triplet = (descendingAngle.Item1, descendingAngle.Item2, descendingAngle.Item3, descendingAngle.index);
    //          break;
    //        case TriangulatingAlgorithmEnum.MostSquare:
    //          var anglets = GetAngles(copy).Aggregate((a, b) => System.Math.Abs(a.angle - Math.Pi.Over2) < System.Math.Abs(b.angle - Math.Pi.Over2) ? a : b);
    //          triplet = (anglets.Item1, anglets.Item2, anglets.Item3, anglets.index);
    //          break;
    //        case TriangulatingAlgorithmEnum.Randomized:
    //          triplet = copy.PartitionTriplets(IEnumerable.PartitionTripletsEnum.WrapToSecond).Select((t, i) => (t.leading, t.midling, t.trailing, i)).RandomElement();
    //          break;
    //        default:
    //          break;
    //      }

    //      yield return new System.Collections.Generic.List<System.Numerics.Vector3>() { triplet.Item2, triplet.Item3, triplet.Item1 };

    //      copy.RemoveAt((triplet.Index + 1) % copy.Count);
    //    }
    //  }
    //}

    // Operators
    #endregion Static methods

    #region Overloaded operators
    public static bool operator ==(Polygon a, Polygon b)
      => a.Equals(b);
    public static bool operator !=(Polygon a, Polygon b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals(Polygon other)
    {
      if (Vertices.Count != other.Vertices.Count)
        return false;

      for (var index = Vertices.Count; index >= 0; index--)
        if (Vertices[index] != other.Vertices[index])
          return false;

      return true;
    }
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Polygon o && Equals(o);
    public override int GetHashCode()
    {
      var hc = new System.HashCode();
      for (var index = Vertices.Count - 1; index >= 0; index--)
        hc.Add(Vertices[index]);
      return hc.ToHashCode();
    }
    public override string? ToString()
      => $"{GetType().Name} {{ {string.Join(@", ", Vertices)} }}";
    #endregion Object overrides
  }
}