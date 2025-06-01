namespace Flux
{
  public static partial class Types
  {
    /// <summary>
    /// <para>Indicates whether the source type is a .NET signed numeric type.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool IsSignedNumericType(this System.Type source, bool isPrimitive = false)
      => source.IsSystemNullable()
      ? IsSignedNumericType(System.Nullable.GetUnderlyingType(source)!)
      : source is not null
      && !source.IsEnum
      && (!isPrimitive || source.IsPrimitive)
      && (
        System.Type.GetTypeCode(source)
        is System.TypeCode.Decimal
        or System.TypeCode.Double
        or System.TypeCode.Int16
        or System.TypeCode.Int32
        or System.TypeCode.Int64
        or System.TypeCode.SByte
        or System.TypeCode.Single
        || source == typeof(System.Half)
        || source == typeof(System.Int128)
        || source == typeof(System.Runtime.InteropServices.NFloat)
        || source == typeof(System.IntPtr)
        || source == typeof(System.Numerics.BigInteger)
        || source == typeof(System.Numerics.Complex)
        || source.IsAssignableToGenericType(typeof(System.Numerics.ISignedNumber<>))
      );

    /// <summary>
    /// <para>Indicates whether the source type is a .NET unsigned numeric type.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool IsUnsignedNumericType(this System.Type source, bool isPrimitive = false)
      => source.IsSystemNullable()
      ? IsUnsignedNumericType(System.Nullable.GetUnderlyingType(source)!)
      : source is not null
      && !source.IsEnum
      && (!isPrimitive || source.IsPrimitive)
      && (
        System.Type.GetTypeCode(source)
        is System.TypeCode.Byte
        or System.TypeCode.Char
        or System.TypeCode.UInt16
        or System.TypeCode.UInt32
        or System.TypeCode.UInt64
        || source == typeof(System.UInt128)
        || source == typeof(System.UIntPtr)
        || source.IsAssignableToGenericType(typeof(System.Numerics.IUnsignedNumber<>))
      );

    /// <summary>
    /// <para>Indicates whether the source type is a .NET floating-point numeric type.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="isPrimitive"></param>
    /// <returns></returns>
    public static bool IsFloatingPointNumericType(this System.Type source, bool isPrimitive = false)
      => source.IsSystemNullable()
      ? IsFloatingPointNumericType(System.Nullable.GetUnderlyingType(source)!) // Wrapped into System.Nullable<>, so call itself again with the underlying-type.
      : source is not null
      && (!isPrimitive || source.IsPrimitive) // Only verify primitive, if asked for it.
      && (
        System.Type.GetTypeCode(source)
        is System.TypeCode.Decimal
        or System.TypeCode.Double
        or System.TypeCode.Single
        || source == typeof(System.Half)
        || source == typeof(System.Runtime.InteropServices.NFloat)
        || source.IsAssignableToGenericType(typeof(System.Numerics.IFloatingPoint<>))
      );

    /// <summary>
    /// <para>Indicates whether the source type is a .NET integer numeric type.</para>
    /// </summary>
    /// <remarks>This method does not consider <see cref="System.Enum"/> to be an integer numeric type.</remarks>
    /// <param name="source"></param>
    /// <param name="isPrimitive">If true then the integer numeric type also has to be a .NET primitive, otherwise any integer numeric type is accepted.</param>
    /// <returns></returns>
    public static bool IsIntegerNumericType(this System.Type source, bool isPrimitive = false)
      => source.IsSystemNullable()
      ? IsIntegerNumericType(System.Nullable.GetUnderlyingType(source)!) // Wrapped into System.Nullable<>, so call itself again with the underlying-type.
      : source is not null
      && !source.IsEnum // Enum is NOT considered a numeric type, even though it cannot represent anything but a primitive number. Too much ambiguity, so user has to handle it to their needs.
      && (!isPrimitive || source.IsPrimitive) // Only verify primitive, if asked for it.
      && (
        System.Type.GetTypeCode(source)
        is System.TypeCode.Byte
        or System.TypeCode.Char // Even though char is not a number, per se, it does derive from System.Numerics.[numeric]<> interfaces, so we check it here instead of deferring to the slower but inevitable match below by IsAssignableToGenericType(..).
        or System.TypeCode.Int16
        or System.TypeCode.Int32
        or System.TypeCode.Int64
        or System.TypeCode.SByte
        or System.TypeCode.UInt16
        or System.TypeCode.UInt32
        or System.TypeCode.UInt64
        || source == typeof(System.Int128)
        || source == typeof(System.IntPtr)
        || source == typeof(System.Numerics.BigInteger)
        || source == typeof(System.UInt128)
        || source == typeof(System.UIntPtr)
        || source.IsAssignableToGenericType(typeof(System.Numerics.IBinaryInteger<>))
      );
  }
}
