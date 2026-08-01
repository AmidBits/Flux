namespace Flux
{
  public static class BinaryIntegerConstants
  {
    /// <summary>
    /// <para>The smallest prime number.</para>
    /// </summary>
    public const int MinPrimeNumber = 2;

    extension<TInteger>(TInteger)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      /// <summary>
      /// <para>The smallest prime number (2) representable by any generic integer type.</para>
      /// </summary>
      public static TInteger MinPrimeNumber => TInteger.CreateChecked(2);

      /// <summary>
      /// <para>The largest prime number representable by the generic integer type <typeparamref name="TInteger"/>. Only fixed size types are supported.</para>
      /// </summary>
      /// <remarks>Supports built-in integral types via TypeCode and provides explicit values for System.Int128 and System.UInt128. Throws System.NotImplementedException for unsupported or unhandled numeric types.</remarks>
      public static TInteger MaxPrimeNumber
        => System.Type.GetTypeCode(typeof(TInteger)) switch
        {
          System.TypeCode.Byte => TInteger.CreateChecked((byte)251),
          System.TypeCode.Int16 => TInteger.CreateChecked((short)32749),
          System.TypeCode.Int32 => TInteger.CreateChecked(2147483647),
          System.TypeCode.Int64 => TInteger.CreateChecked(9223372036854775807),
          System.TypeCode.SByte => TInteger.CreateChecked((sbyte)127),
          System.TypeCode.UInt16 or System.TypeCode.Char => TInteger.CreateChecked((ushort)65521),
          System.TypeCode.UInt32 => TInteger.CreateChecked(4294967291u),
          System.TypeCode.UInt64 => TInteger.CreateChecked(18446744073709551557ul),
          _ => typeof(TInteger) switch
          {
            var t when t == typeof(System.Int128) => TInteger.CreateChecked(new System.Int128(0x7FFFFFFFFFFFFFFFul, 0xFFFFFFFFFFFFFFFFul)),
            var t when t == typeof(System.UInt128) => TInteger.CreateChecked(new System.UInt128(0xFFFFFFFFFFFFFFFFul, 0xFFFFFFFFFFFFFF53ul)),
            _ => throw new System.NotImplementedException($"{nameof(TInteger)} as {typeof(TInteger)}"),
          },
        };

      /// <summary>
      /// <para>Main classification number system:</para>
      /// <para>Signed integers: "DOUBLE-STRUCK CAPITAL Z" = U+2124 = '&#x2124;'</para>
      /// <para>Unsigned integers (natural numbers): "DOUBLE-STRUCK CAPITAL N" = U+2115 = '&#x2115;'</para>
      /// </summary>
      public static char NumberClassificationSymbol
        => typeof(TInteger).ImplementsISignedNumber() ? '\u2124' // "DOUBLE-STRUCK CAPITAL Z"
        : typeof(TInteger).ImplementsIUnsignedNumber() ? '\u2115' // "DOUBLE-STRUCK CAPITAL N"
        : throw new System.NotImplementedException();
    }
  }
}
