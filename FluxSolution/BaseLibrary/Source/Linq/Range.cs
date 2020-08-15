namespace Flux
{
  public static partial class LinqEx
  {
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> Range(System.Numerics.BigInteger start, System.Numerics.BigInteger count, System.Numerics.BigInteger step)
    {
      for (var value = start; count > 0; count--, value += step)
        yield return value;
    }
    public static System.Collections.Generic.IEnumerable<decimal> Range(decimal start, decimal count, decimal step)
    {
      for (var value = start; count > 0; count--, value += step)
        yield return value;
    }
    public static System.Collections.Generic.IEnumerable<double> Range(double start, double count, double step)
    {
      for (var value = start; count > 0; count--, value += step)
        yield return value;
    }
    public static System.Collections.Generic.IEnumerable<int> Range(int start, int count, int step)
    {
      for (var value = start; count > 0; count--, value += step)
        yield return value;
    }
    public static System.Collections.Generic.IEnumerable<long> Range(long start, long count, long step)
    {
      for (var value = start; count > 0; count--, value += step)
        yield return value;
    }
    public static System.Collections.Generic.IEnumerable<float> Range(float start, float count, float step)
    {
      for (var value = start; count > 0; count--, value += step)
        yield return value;
    }
    [System.CLSCompliant(false)]
    public static System.Collections.Generic.IEnumerable<uint> Range(uint start, uint count, uint step)
    {
      for (var value = start; count > 0; count--, value += step)
        yield return value;
    }
    [System.CLSCompliant(false)]
    public static System.Collections.Generic.IEnumerable<ulong> Range(ulong start, ulong count, ulong step)
    {
      for (var value = start; count > 0; count--, value += step)
        yield return value;
    }
  }
}
