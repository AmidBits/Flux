//using System.Runtime.Intrinsics;

namespace Flux
{
  public static partial class Intrinsics
  {
    #region Contains

    /// <summary>Determines the inclusion of a point in the (2D planar) polygon. This Winding Number method counts the number of times the polygon winds around the point. The point is outside only when this "winding number" is 0, otherwise the point is inside.</summary>
    /// <see cref="http://geomalgorithms.com/a03-_inclusion.html#wn_PnPoly"/>
    public static int Contains(this System.Collections.Generic.IList<System.Runtime.Intrinsics.Vector128<double>> polygon, double x, double y)
    {
      System.ArgumentNullException.ThrowIfNull(polygon);

      int wn = 0;

      for (int i = 0; i < polygon.Count; i++)
      {
        var a = polygon[i];
        var b = (i == polygon.Count - 1 ? polygon[0] : polygon[i + 1]);

        if (a[1] <= y)
        {
          if (b[1] > y && Geometry.Shapes.Line.LineGeometry.SideTest(x, y, a[0], a[1], b[0], b[1]) > 0)
            wn++;
        }
        else
        {
          if (b[1] <= y && Geometry.Shapes.Line.LineGeometry.SideTest(x, y, a[0], a[1], b[0], b[1]) < 0)
            wn--;
        }
      }

      return wn;
    }

    /// <summary>Determines the inclusion of a point in the (2D planar) polygon. This Winding Number method counts the number of times the polygon winds around the point. The point is outside only when this "winding number" is 0, otherwise the point is inside.</summary>
    /// <see cref="http://geomalgorithms.com/a03-_inclusion.html#wn_PnPoly"/>
    public static int Contains(this System.Collections.Generic.IList<System.Runtime.Intrinsics.Vector256<double>> polygon, double x, double y)
    {
      System.ArgumentNullException.ThrowIfNull(polygon);

      int wn = 0;

      for (int i = 0; i < polygon.Count; i++)
      {
        var a = polygon[i];
        var b = (i == polygon.Count - 1 ? polygon[0] : polygon[i + 1]);

        if (a[1] <= y)
        {
          if (b[1] > y && Geometry.Shapes.Line.LineGeometry.SideTest(x, y, a[0], a[1], b[0], b[1]) > 0)
            wn++;
        }
        else
        {
          if (b[1] <= y && Geometry.Shapes.Line.LineGeometry.SideTest(x, y, a[0], a[1], b[0], b[1]) < 0)
            wn--;
        }
      }

      return wn;
    }

    #endregion // Contains

    #region GetAngles

    /// <summary>Returns a sequence of triplets, their angles and ordinal indices. (2D/3D)</summary>
    public static System.Collections.Generic.IList<double> GetAngles(System.Collections.Generic.IEnumerable<System.Runtime.Intrinsics.Vector128<double>> source)
    {
      var list = source.PartitionTuple3(2, (leading, midling, trailing, index) => midling.AngleBetween(leading, trailing)).ToList();

      list.Insert(0, list.Last());
      list.RemoveAt(list.Count - 1);

      return list;
    }

    /// <summary>Returns a sequence of triplets, their angles and ordinal indices. (2D/3D)</summary>
    public static System.Collections.Generic.IList<double> GetAngles(System.Collections.Generic.IEnumerable<System.Runtime.Intrinsics.Vector256<double>> source)
    {
      var list = source.PartitionTuple3(2, (leading, midling, trailing, index) => midling.AngleBetween(leading, trailing)).ToList();

      list.Insert(0, list.Last());
      list.RemoveAt(list.Count - 1);

      return list;
    }

    #endregion // GetAngles

    #region TryGetBoundingBox

    public static bool TryGetBoundingBox(System.Collections.Generic.IEnumerable<System.Runtime.Intrinsics.Vector128<double>> source, out System.Runtime.Intrinsics.Vector128<double> min, out System.Runtime.Intrinsics.Vector128<double> max)
    {
      try
      {
        (min, max) = source.Aggregate((System.Runtime.Intrinsics.Vector128.Create(double.MaxValue), System.Runtime.Intrinsics.Vector128.Create(double.MinValue)), (acc, current) => (System.Runtime.Intrinsics.Vector128.Min(acc.Item1, current), System.Runtime.Intrinsics.Vector128.Max(acc.Item2, current)));

        return true;
      }
      catch
      {
        min = default;
        max = default;

        return false;
      }
    }

    public static bool TryGetBoundingBox(System.Collections.Generic.IEnumerable<System.Runtime.Intrinsics.Vector256<double>> source, out System.Runtime.Intrinsics.Vector256<double> min, out System.Runtime.Intrinsics.Vector256<double> max)
    {
      try
      {
        (min, max) = source.Aggregate((System.Runtime.Intrinsics.Vector256.Create(double.MaxValue), System.Runtime.Intrinsics.Vector256.Create(double.MinValue)), (acc, current) => (System.Runtime.Intrinsics.Vector256.Min(acc.Item1, current), System.Runtime.Intrinsics.Vector256.Max(acc.Item2, current)));

        return true;
      }
      catch
      {
        min = default;
        max = default;

        return false;
      }
    }

    #endregion // TryGetBoundingBox

    #region GetMidpoints

    /// <summary>
    /// <para>Returns all midpoints (halfway) points of the polygon.</para>
    /// </summary>
    public static System.Collections.Generic.IEnumerable<System.Runtime.Intrinsics.Vector128<double>> GetMidpoints(System.Collections.Generic.IEnumerable<System.Runtime.Intrinsics.Vector128<double>> source)
      => source.PartitionTuple2(true, (leading, trailing, index) => (leading, trailing)).Select(edge => (edge.trailing + edge.leading) / System.Runtime.Intrinsics.Vector128.Create(2d));

    /// <summary>Returns all midpoints (halfway) points of the polygon. (2D/3D)</summary>
    public static System.Collections.Generic.IEnumerable<(System.Runtime.Intrinsics.Vector256<double> midpoint, (System.Runtime.Intrinsics.Vector256<double>, System.Runtime.Intrinsics.Vector256<double>) pair)> GetMidpoints(System.Collections.Generic.IEnumerable<System.Runtime.Intrinsics.Vector256<double>> source)
      => source.PartitionTuple2(true, (leading, trailing, index) => ((trailing + leading) / System.Runtime.Intrinsics.Vector256.Create(2d), (leading, trailing)));

    #endregion // GetMidpoints

    #region ToString..

    public static string ToStringXY(this System.Collections.Generic.IEnumerable<System.Runtime.Intrinsics.Vector128<double>> source, string? format, IFormatProvider? formatProvider)
      => string.Join(@", ", source.Select(v => v.ToStringXY(format, formatProvider)));

    public static string ToStringXY(this System.Collections.Generic.IEnumerable<System.Runtime.Intrinsics.Vector256<double>> source, string? format, IFormatProvider? formatProvider)
      => string.Join(@", ", source.Select(v => v.ToStringXY(format, formatProvider)));

    public static string ToStringXYZ(this System.Collections.Generic.IEnumerable<System.Runtime.Intrinsics.Vector256<double>> source, string? format, IFormatProvider? formatProvider)
      => string.Join(@", ", source.Select(v => v.ToStringXYZ(format, formatProvider)));

    public static string ToStringXYZW(this System.Collections.Generic.IEnumerable<System.Runtime.Intrinsics.Vector256<double>> source, string? format, IFormatProvider? formatProvider)
      => string.Join(@", ", source.Select(v => v.ToStringXYZW(format, formatProvider)));

    #endregion ToString..
  }
}
