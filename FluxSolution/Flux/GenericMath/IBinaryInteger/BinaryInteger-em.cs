namespace Flux
{
  public static class GenericMath // (or BinaryInteger)
  {
    extension<TInteger>(TInteger value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      public TInteger BinomialCoefficient<TNumber>(TInteger k)
      {
        //if (TNumber.IsNegative(k) || k > n) return TNumber.Zero; // (k < 0 || k > n) = 0
        //else if (TNumber.IsZero(k) || k == n) return TNumber.One; // (k == 0 || k == n) = 1
  
        if (k > TNumber.Zero && k < n) // 0 < k < n
          checked
          {
            k = TNumber.Min(k, n - k); // Optimize for the loop below.
  
            var c = TNumber.One;
  
            for (var i = TNumber.One; i <= k; i++)
              c = c * (n - k + i) / i;
            //c = c * n-- / i;
  
            return c;
          }
        else if (k.Equals(TNumber.Zero) || k.Equals(n))
          return TNumber.One; // 1 if (k == 0 || k == n)
  
        return TNumber.Zero; // 0 if (k < 0 || k > n)
      }
    }
  }
}
