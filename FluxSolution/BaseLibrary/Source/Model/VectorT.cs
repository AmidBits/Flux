//namespace Flux.Model
//{
//  public class Vector1<TX>
//    where TX : System.IComparable<TX>
//  {
//    public TX X;

//    public Vector1(TX x)
//    {
//      X = x;
//    }

//    /// <summary>Compute the one dimensional orthant, i.e. a ray [0, 1], derived from only the X axis.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Orthant"/>
//    /// <seealso cref="https://en.wikipedia.org/wiki/Line_(geometry)#Ray"/>
//    public int ToOrthant1(TX centerAxisX) => unchecked(X.CompareTo(centerAxisX) < 0 ? 1 : 0);
//  }

//  public class Vector2<TX, TY>
//    where TX : System.IComparable<TX>
//    where TY : System.IComparable<TY>
//  {
//    public TX X;
//    public TY Y;

//    public Vector2(TX x, TY y)
//    {
//      X = x;
//      Y = y;
//    }

//    /// <summary>Compute the one dimensional orthant, i.e. a quadrant [0, 3], derived from the X and Y axes.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Orthant"/>
//    /// <seealso cref="https://en.wikipedia.org/wiki/Quadrant_(plane_geometry)"/>
//    public int ToOrthant2(TX centerAxisX, TY centerAxisY) => unchecked(Y.CompareTo(centerAxisY) < 0 ? (X.CompareTo(centerAxisX) < 0 ? 2 : 3) : (X.CompareTo(centerAxisX) < 0 ? 1 : 0));
//  }

//  public class Vector3<TX, TY, TZ>
//    where TX : System.IComparable<TX>
//    where TY : System.IComparable<TY>
//    where TZ : System.IComparable<TZ>
//  {
//    public TX X;
//    public TY Y;
//    public TZ Z;

//    public Vector3(TX x, TY y, TZ z)
//    {
//      X = x;
//      Y = y;
//      Z = z;
//    }

//    /// <summary>Compute the one dimensional orthant, i.e. an octant [0, 7], derived from the X, Y and Z axes.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Orthant"/>
//    /// <seealso cref="https://en.wikipedia.org/wiki/Octant_(solid_geometry)"/>
//    public int ToOrthant3(TX centerAxisX, TY centerAxisY, TZ centerAxisZ) => unchecked((Y.CompareTo(centerAxisY) < 0 ? (X.CompareTo(centerAxisX) < 0 ? 2 : 3) : (X.CompareTo(centerAxisX) < 0 ? 1 : 0)) + (Z.CompareTo(centerAxisZ) < 0 ? 4 : 0));
//  }
//}
