namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static bool IsPerfectIRoot(this System.Numerics.BigInteger number, int nth, System.Numerics.BigInteger root)
      => number == System.Numerics.BigInteger.Pow(root, nth);

    /// <summary>Returns the the largest integer less than or equal to the square root of the specified number.</summary>
    public static System.Numerics.BigInteger IRoot(this System.Numerics.BigInteger number, int nth)
    {
      if (number < 0) throw new System.ArgumentOutOfRangeException(nameof(number));
      if (nth <= 0) throw new System.ArgumentOutOfRangeException(nameof(nth));

      var nthm1 = nth - 1;
      var c = System.Numerics.BigInteger.One;
      var d = (nthm1 + number) / nth;
      var e = (nthm1 * d + number / System.Numerics.BigInteger.Pow(d, nthm1)) / nth;

      while (c != d && c != e)
      {
        c = d;
        d = e;
        e = (nthm1 * e + number / System.Numerics.BigInteger.Pow(e, nthm1)) / nth;
      }

      return d < e ? d : e;
    }

    public static bool TryIRoot(this System.Numerics.BigInteger number, int nth, out System.Numerics.BigInteger root, out bool isPerfect)
    {
      try
      {
        root = IRoot(number, nth);
        isPerfect = number == System.Numerics.BigInteger.Pow(root, nth);
        return true;
      }
      catch
      {
        root = 0;
        isPerfect = false;
        return false;
      }
    }
  }
}
