namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Returns the place value digits (as numbers) of a value, in reverse order.</summary>
    public static System.Span<System.Numerics.BigInteger> GetDigitsReversed(System.Numerics.BigInteger value, int radix)
    {
      if (radix < 2) throw new System.ArgumentOutOfRangeException(nameof(radix));
      if (value < 0) value = -value;

      var list = new System.Collections.Generic.List<System.Numerics.BigInteger>();
      while (value > 0)
      {
        list.Add(value % radix);
        value /= radix;
      }
      return System.Runtime.InteropServices.CollectionsMarshal.AsSpan(list);
    }

    /// <summary>Returns the place value digits (as numbers) of a value, in reverse order.</summary>
    public static System.Span<int> GetDigitsReversed(int value, int radix)
    {
      if (radix < 2) throw new System.ArgumentOutOfRangeException(nameof(radix));
      if (value < 0) value = -value;

      var list = new System.Collections.Generic.List<int>();
      while (value > 0)
      {
        list.Add(value % radix);
        value /= radix;
      }
      return System.Runtime.InteropServices.CollectionsMarshal.AsSpan(list);
    }
    /// <summary>Returns the place value digits (as numbers) of a value, in reverse order.</summary>
    public static System.Span<long> GetDigitsReversed(long value, int radix)
    {
      if (radix < 2) throw new System.ArgumentOutOfRangeException(nameof(radix));
      if (value < 0) value = -value;

      var list = new System.Collections.Generic.List<long>();
      while (value > 0)
      {
        list.Add(value % radix);
        value /= radix;
      }
      return System.Runtime.InteropServices.CollectionsMarshal.AsSpan(list);
    }

    /// <summary>Returns the place value digits (as numbers) of a value, in reverse order.</summary>
    [System.CLSCompliant(false)]
    public static System.Span<uint> GetDigitsReversed(uint value, int radix)
    {
      if (radix < 2) throw new System.ArgumentOutOfRangeException(nameof(radix));

      var list = new System.Collections.Generic.List<uint>();
      while (value > 0)
      {
        list.Add(value % (uint)radix);
        value /= (uint)radix;
      }
      return System.Runtime.InteropServices.CollectionsMarshal.AsSpan(list);
    }
    /// <summary>Returns the place value digits (as numbers) of a value, in reverse order.</summary>
    [System.CLSCompliant(false)]
    public static System.Span<ulong> GetDigitsReversed(ulong value, int radix)
    {
      if (radix < 2) throw new System.ArgumentOutOfRangeException(nameof(radix));

      var list = new System.Collections.Generic.List<ulong>();
      while (value > 0)
      {
        list.Add(value % (ulong)radix);
        value /= (ulong)radix;
      }
      return System.Runtime.InteropServices.CollectionsMarshal.AsSpan(list);
    }
  }
}
