namespace Flux
{
  /// <summary>This static class was created to not bloat every single object with extension methods for this functionality.</summary>
  public static partial class Object
  {
    /// <summary>Returns whether the source type is a primitive numeric data type (e.g. <see cref="System.Int32"/> or <see cref="System.Int64"/>) and whether to consider <see cref="System.Numerics.BigInteger"/>.</summary>
    public static bool IsNumeric(object source)
      => IsNumericComplex(source) || IsNumericFloatingPoint(source) || IsNumericInteger(source);

    /// <summary>Returns whether the source type is a primitive numeric data type (e.g. <see cref="System.Int32"/> or <see cref="System.Int64"/>) and whether to consider <see cref="System.Numerics.BigInteger"/>.</summary>
    public static bool IsNumericComplex(object source)
      => source is System.Numerics.Complex;

    /// <summary>Returns whether the source type is a primitive floating point numeric data type (i.e. <see cref="System.Decimal"/>, <see cref="System.Double"/> or <see cref="System.Single"/>).</summary>
    public static bool IsNumericFloatingPoint(object source)
      => source is System.Decimal || source is System.Double || source is System.Half || source is System.Single;

    /// <summary>Returns whether the source type is a primitive integer numeric data type (e.g. <see cref="System.Int32"/> or <see cref="System.Int64"/>) and whether to consider <see cref="System.Numerics.BigInteger"/>.</summary>
    public static bool IsNumericInteger(object source)
      => IsNumericIntegerSigned(source) || IsNumericIntegerUnsigned(source);

    /// <summary>Returns whether the source type is a primitive signed integer numeric data type (e.g. <see cref="System.Int32"/> or <see cref="System.Int64"/>) and whether to consider <see cref="System.Numerics.BigInteger"/>.</summary>
    public static bool IsNumericIntegerSigned(object source)
      => source is System.Numerics.BigInteger || source is System.SByte || source is System.UInt16 || source is System.UInt32 || source is System.UInt64; // || source is System.UInt128;
    /// <summary>Returns whether the source type is a primitive unsigned integer numeric data type (e.g. <see cref="System.Int32"/> or <see cref="System.Int64"/>) and whether to consider <see cref="System.Numerics.BigInteger"/>.</summary>
    public static bool IsNumericIntegerUnsigned(object source)
      => source is System.Byte || source is System.UInt16 || source is System.UInt32 || source is System.UInt64; // || source is System.UInt128;
  }
}
