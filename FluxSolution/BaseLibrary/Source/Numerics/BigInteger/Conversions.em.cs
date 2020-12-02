namespace Flux
{
  public static partial class BigIntegerEm
  {
    /// <summary>Convert the source to a BigInteger.</summary>
    public static System.Numerics.BigInteger ToBigInteger(this byte source)
      => new System.Numerics.BigInteger(source);
    /// <summary>Convert the source to a BigInteger.</summary>
    public static System.Numerics.BigInteger ToBigInteger(this decimal source)
      => new System.Numerics.BigInteger(source);
    /// <summary>Convert the source to a BigInteger.</summary>
    public static System.Numerics.BigInteger ToBigInteger(this double source)
      => new System.Numerics.BigInteger(source);
    /// <summary>Convert the source to a BigInteger.</summary>
    public static System.Numerics.BigInteger ToBigInteger(this short source)
      => new System.Numerics.BigInteger(source);
    /// <summary>Convert the source to a BigInteger.</summary>
    public static System.Numerics.BigInteger ToBigInteger(this int source)
      => new System.Numerics.BigInteger(source);
    /// <summary>Convert the source to a BigInteger.</summary>
    public static System.Numerics.BigInteger ToBigInteger(this long source)
      => new System.Numerics.BigInteger(source);
    /// <summary>Convert the source to a BigInteger.</summary>
    [System.CLSCompliant(false)]
    public static System.Numerics.BigInteger ToBigInteger(this sbyte source)
      => new System.Numerics.BigInteger(source);
    /// <summary>Convert the source to a BigInteger.</summary>
    public static System.Numerics.BigInteger ToBigInteger(this float source)
      => new System.Numerics.BigInteger(source);
    /// <summary>Convert the source to a BigInteger.</summary>
    [System.CLSCompliant(false)]
    public static System.Numerics.BigInteger ToBigInteger(this ushort source)
      => new System.Numerics.BigInteger(source);
    /// <summary>Convert the source to a BigInteger.</summary>
    [System.CLSCompliant(false)]
    public static System.Numerics.BigInteger ToBigInteger(this uint source)
      => new System.Numerics.BigInteger(source);
    /// <summary>Convert the source to a BigInteger.</summary>
    [System.CLSCompliant(false)]
    public static System.Numerics.BigInteger ToBigInteger(this ulong source)
      => new System.Numerics.BigInteger(source);

    /// <summary>Conversion from BigInteger to built-in type.</summary>
    /// <remarks>Does not generate any exceptions. If the source is lower than the lowest the the lowest possible is returned. IF the source is larger than the largest possible, then the largest is returned.</remarks>
    public static byte ToByte(this System.Numerics.BigInteger source)
      => source < byte.MinValue ? byte.MinValue : source > byte.MaxValue ? byte.MaxValue : (byte)source;
    /// <summary>Conversion from BigInteger to built-in type.</summary>
    /// <remarks>Does not generate any exceptions. If the source is lower than the lowest the the lowest possible is returned. IF the source is larger than the largest possible, then the largest is returned.</remarks>
    public static decimal ToDecimal(this System.Numerics.BigInteger source)
      => source < (System.Numerics.BigInteger)decimal.MinValue ? decimal.MinValue : source > (System.Numerics.BigInteger)decimal.MaxValue ? decimal.MaxValue : (decimal)source;
    /// <summary>Conversion from BigInteger to built-in type.</summary>
    /// <remarks>Does not generate any exceptions. If the source is lower than the lowest the the lowest possible is returned. IF the source is larger than the largest possible, then the largest is returned.</remarks>
    public static double ToDouble(this System.Numerics.BigInteger source)
      => source < (System.Numerics.BigInteger)double.MinValue ? double.MinValue : source > (System.Numerics.BigInteger)double.MaxValue ? double.MaxValue : (double)source;
    /// <summary>Conversion from BigInteger to built-in type.</summary>
    /// <remarks>Does not generate any exceptions. If the source is lower than the lowest the the lowest possible is returned. IF the source is larger than the largest possible, then the largest is returned.</remarks>
    public static short ToInt16(this System.Numerics.BigInteger source)
      => source < short.MinValue ? short.MinValue : source > short.MaxValue ? short.MaxValue : (short)source;
    /// <summary>Conversion from BigInteger to built-in type.</summary>
    /// <remarks>Does not generate any exceptions. If the source is lower than the lowest the the lowest possible is returned. IF the source is larger than the largest possible, then the largest is returned.</remarks>
    public static int ToInt32(this System.Numerics.BigInteger source)
      => source < int.MinValue ? int.MinValue : source > int.MaxValue ? int.MaxValue : (int)source;
    /// <summary>Conversion from BigInteger to built-in type.</summary>
    /// <remarks>Does not generate any exceptions. If the source is lower than the lowest the the lowest possible is returned. IF the source is larger than the largest possible, then the largest is returned.</remarks>
    public static long ToInt64(this System.Numerics.BigInteger source)
      => source < long.MinValue ? long.MinValue : source > long.MaxValue ? long.MaxValue : (long)source;
    /// <summary>Conversion from BigInteger to built-in type.</summary>
    /// <remarks>Does not generate any exceptions. If the source is lower than the lowest the the lowest possible is returned. IF the source is larger than the largest possible, then the largest is returned.</remarks>
    [System.CLSCompliant(false)]
    public static sbyte ToSByte(this System.Numerics.BigInteger source)
      => source < sbyte.MinValue ? sbyte.MinValue : source > sbyte.MaxValue ? sbyte.MaxValue : (sbyte)source;
    /// <summary>Conversion from BigInteger to built-in type.</summary>
    /// <remarks>Does not generate any exceptions. If the source is lower than the lowest the the lowest possible is returned. IF the source is larger than the largest possible, then the largest is returned.</remarks>
    public static float ToSingle(this System.Numerics.BigInteger source)
      => source < (System.Numerics.BigInteger)float.MinValue ? float.MinValue : source > (System.Numerics.BigInteger)float.MaxValue ? float.MaxValue : (float)source;
    /// <summary>Conversion from BigInteger to built-in type.</summary>
    /// <remarks>Does not generate any exceptions. If the source is lower than the lowest the the lowest possible is returned. IF the source is larger than the largest possible, then the largest is returned.</remarks>
    [System.CLSCompliant(false)]
    public static ushort ToUInt16(this System.Numerics.BigInteger source)
      => source < ushort.MinValue ? ushort.MinValue : source > ushort.MaxValue ? ushort.MaxValue : (ushort)source;
    /// <summary>Conversion from BigInteger to built-in type.</summary>
    /// <remarks>Does not generate any exceptions. If the source is lower than the lowest the the lowest possible is returned. IF the source is larger than the largest possible, then the largest is returned.</remarks>
    [System.CLSCompliant(false)]
    public static uint ToUInt32(this System.Numerics.BigInteger source)
      => source < uint.MinValue ? uint.MinValue : source > uint.MaxValue ? uint.MaxValue : (uint)source;
    /// <summary>Conversion from BigInteger to built-in type.</summary>
    /// <remarks>Does not generate any exceptions. If the source is lower than the lowest the the lowest possible is returned. IF the source is larger than the largest possible, then the largest is returned.</remarks>
    [System.CLSCompliant(false)]
    public static ulong ToUInt64(this System.Numerics.BigInteger source)
      => source < ulong.MinValue ? ulong.MinValue : source > ulong.MaxValue ? ulong.MaxValue : (ulong)source;
  }
}
