namespace Flux
{
  /// <summary>This static class was created to not bloat every single object with extension methods for this functionality.</summary>
  public static partial class Object
  {
    /// <summary>Returns whether the source type is a .NET signed number.</summary>
    public static bool IsNumberSigned<T>(this T source)
      where T : struct,
      System.IComparable,
      System.IComparable<T>,
      //System.IConvertible,
      System.IEquatable<T>,
      System.IFormattable
      => source
      is System.Decimal
      or System.Double
      or System.Half
      or System.Int128
      or System.Int16
      or System.Int32
      or System.Int64
      or System.IntPtr
      or System.Numerics.BigInteger
      or System.Numerics.Complex
      or System.Runtime.InteropServices.NFloat
      or System.SByte
      or System.Single;

    /// <summary>Returns whether the source type is a .NET unsigned number.</summary>
    public static bool IsNumberUnsigned<T>(this T source)
      where T : struct,
      System.IComparable,
      System.IComparable<T>,
      //System.IConvertible,
      System.IEquatable<T>,
      System.IFormattable
      => source
      is System.Byte
      or System.Char
      or System.UInt128
      or System.UInt16
      or System.UInt32
      or System.UInt64
      or System.UIntPtr;

    /// <summary>Returns whether the source type is a .NET floating-point number-base.</summary>
    public static bool IsNumericFloatingPoint<T>(T source)
      where T : struct,
      System.IComparable,
      System.IComparable<T>,
      //System.IConvertible,
      System.IEquatable<T>,
      System.IFormattable
      => source
      is System.Decimal
      or System.Double
      or System.Half
      or System.Runtime.InteropServices.NFloat
      or System.Single;

    /// <summary>Returns whether the source type is a .NET integer number-base.</summary>
    public static bool IsNumericInteger<T>(T source)
      where T : struct,
      System.IComparable,
      System.IComparable<T>,
      //System.IConvertible,
      System.IEquatable<T>,
      System.IFormattable
      => source
      is System.Byte
      or System.Char
      or System.Int128
      or System.Int16
      or System.Int32
      or System.Int64
      or System.IntPtr
      or System.Numerics.BigInteger
      or System.SByte
      or System.UInt128
      or System.UInt16
      or System.UInt32
      or System.UInt64
      or System.UIntPtr;
  }
}
