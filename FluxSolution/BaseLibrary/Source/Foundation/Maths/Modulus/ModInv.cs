
//namespace Flux
//{
//  public static partial class Maths
//  {
//    /// <summary>Modular multiplicative inverse.</summary>
//    /// <returns>-1, if no inverse.</returns>
//    public static System.Numerics.BigInteger ModInv(System.Numerics.BigInteger a, System.Numerics.BigInteger b)
//    {
//      if (b < 0)
//        b = -b;
//      if (a < 0)
//        a = b - (-a % b);

//      var t = System.Numerics.BigInteger.Zero;
//      var nt = System.Numerics.BigInteger.One;
//      var r = b;
//      var nr = a % b;

//      while (nr != 0)
//      {
//        var q = r / nr;

//        var tmp = nt; nt = t - q * nt; t = tmp;
//        tmp = nr; nr = r - q * nr; r = tmp;
//      }

//      if (r > 1)
//        return -1;  // No inverse.
//      if (t < 0)
//        t += b;

//      return t;
//    }

//    ///// <summary>Modular multiplicative inverse.</summary>
//    ///// <returns>-1, if no inverse.</returns>
//    //public static int ModInv(int a, int b)
//    //{
//    //  if (b < 0)
//    //    b = -b;
//    //  if (a < 0)
//    //    a = b - (-a % b);

//    //  var t = 0;
//    //  var nt = 1;
//    //  var r = b;
//    //  var nr = a % b;

//    //  while (nr != 0)
//    //  {
//    //    var q = r / nr;

//    //    var tmp = nt; nt = t - q * nt; t = tmp;
//    //    tmp = nr; nr = r - q * nr; r = tmp;
//    //  }

//    //  if (r > 1)
//    //    return -1;  // No inverse.
//    //  if (t < 0)
//    //    t += b;

//    //  return t;
//    //}
//    ///// <summary>Modular multiplicative inverse.</summary>
//    ///// <returns>-1, if no inverse.</returns>
//    //public static long ModInv(long a, long b)
//    //{
//    //  if (b < 0)
//    //    b = -b;
//    //  if (a < 0)
//    //    a = b - (-a % b);

//    //  var t = 0L;
//    //  var nt = 1L;
//    //  var r = b;
//    //  var nr = a % b;

//    //  while (nr != 0)
//    //  {
//    //    var q = r / nr;

//    //    var tmp = nt; nt = t - q * nt; t = tmp;
//    //    tmp = nr; nr = r - q * nr; r = tmp;
//    //  }

//    //  if (r > 1)
//    //    return -1;  // No inverse.
//    //  if (t < 0)
//    //    t += b;

//    //  return t;
//    //}
//  }
//}
