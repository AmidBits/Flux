
namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Modular multiplication.</summary>
    public static System.Numerics.BigInteger MulMod(System.Numerics.BigInteger a, System.Numerics.BigInteger b, System.Numerics.BigInteger m)
    {
      if (a < 0) throw new System.ArgumentOutOfRangeException(nameof(a));
      if (b < 0) throw new System.ArgumentOutOfRangeException(nameof(b));
      if (m < 0) throw new System.ArgumentOutOfRangeException(nameof(m));

      if (a >= m)
        a %= m;
      if (b >= m)
        b %= m;

      var x = a;
      var c = x * b / m;
      var r = (a * b - c * m) % m;

      return r < 0 ? r + m : r;
    }

    ///// <summary>Modular multiplication.</summary>
    //public static int MulMod(int a, int b, int m)
    //{
    //  if (a < 0) throw new System.ArgumentOutOfRangeException(nameof(a));
    //  if (b < 0) throw new System.ArgumentOutOfRangeException(nameof(b));
    //  if (m < 0) throw new System.ArgumentOutOfRangeException(nameof(m));

    //  if (a >= m)
    //    a %= m;
    //  if (b >= m)
    //    b %= m;

    //  var x = a;
    //  var c = x * b / m;
    //  var r = (a * b - c * m) % m;

    //  return r < 0 ? r + m : r;
    //}
    /// <summary>Modular multiplication.</summary>
    //public static long MulMod(long a, long b, long m)
    //{
    //  if (a < 0) throw new System.ArgumentOutOfRangeException(nameof(a));
    //  if (b < 0) throw new System.ArgumentOutOfRangeException(nameof(b));
    //  if (m < 0) throw new System.ArgumentOutOfRangeException(nameof(m));

    //  if (a >= m)
    //    a %= m;
    //  if (b >= m)
    //    b %= m;

    //  var x = a;
    //  var c = x * b / m;
    //  var r = (a * b - c * m) % m;

    //  return r < 0 ? r + m : r;
    //}
  }
}
