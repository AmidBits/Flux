//using System.Runtime.Intrinsics;

//namespace Flux
//{
//  public static partial class Intrinsics
//  {
//    #region Contains

//    /// <summary>Determines the inclusion of a point in the (2D planar) polygon. This Winding Number method counts the number of times the polygon winds around the point. The point is outside only when this "winding number" is 0, otherwise the point is inside.</summary>
//    /// <see cref="http://geomalgorithms.com/a03-_inclusion.html#wn_PnPoly"/>
//    public static int Contains(this System.Collections.Generic.IList<System.Runtime.Intrinsics.Vector128<double>> polygon, double x, double y)
//    {
//      System.ArgumentNullException.ThrowIfNull(polygon);

//      int wn = 0;

//      for (int i = 0; i < polygon.Count; i++)
//      {
//        var a = polygon[i];
//        var b = (i == polygon.Count - 1 ? polygon[0] : polygon[i + 1]);

//        if (a[1] <= y)
//        {
//          if (b[1] > y && Geometry.LineGeometry.SideTest(x, y, a[0], a[1], b[0], b[1]) > 0)
//            wn++;
//        }
//        else
//        {
//          if (b[1] <= y && Geometry.LineGeometry.SideTest(x, y, a[0], a[1], b[0], b[1]) < 0)
//            wn--;
//        }
//      }

//      return wn;
//    }

//    /// <summary>Determines the inclusion of a point in the (2D planar) polygon. This Winding Number method counts the number of times the polygon winds around the point. The point is outside only when this "winding number" is 0, otherwise the point is inside.</summary>
//    /// <see cref="http://geomalgorithms.com/a03-_inclusion.html#wn_PnPoly"/>
//    public static int Contains(this System.Collections.Generic.IList<System.Runtime.Intrinsics.Vector256<double>> polygon, double x, double y)
//    {
//      System.ArgumentNullException.ThrowIfNull(polygon);

//      int wn = 0;

//      for (int i = 0; i < polygon.Count; i++)
//      {
//        var a = polygon[i];
//        var b = (i == polygon.Count - 1 ? polygon[0] : polygon[i + 1]);

//        if (a[1] <= y)
//        {
//          if (b[1] > y && Geometry.LineGeometry.SideTest(x, y, a[0], a[1], b[0], b[1]) > 0)
//            wn++;
//        }
//        else
//        {
//          if (b[1] <= y && Geometry.LineGeometry.SideTest(x, y, a[0], a[1], b[0], b[1]) < 0)
//            wn--;
//        }
//      }

//      return wn;
//    }

//    #endregion // Contains
//  }
//}
