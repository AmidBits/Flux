namespace Flux
{
  public static partial class Em
  {
    public static System.Numerics.BigInteger GetUnitMultiple(this Quantities.DigitalInformationUnit unit)
    {
      return unit switch
      {
        Quantities.DigitalInformationUnit.Byte => 1,
        Quantities.DigitalInformationUnit.kibiByte => 1024,
        Quantities.DigitalInformationUnit.mebiByte => System.Numerics.BigInteger.Pow(1024, 2),
        Quantities.DigitalInformationUnit.gibiByte => System.Numerics.BigInteger.Pow(1024, 3),
        Quantities.DigitalInformationUnit.tebiByte => System.Numerics.BigInteger.Pow(1024, 4),
        Quantities.DigitalInformationUnit.pebiByte => System.Numerics.BigInteger.Pow(1024, 5),
        Quantities.DigitalInformationUnit.exbiByte => System.Numerics.BigInteger.Pow(1024, 6),
        Quantities.DigitalInformationUnit.zebiByte => System.Numerics.BigInteger.Pow(1024, 7),
        Quantities.DigitalInformationUnit.yobiByte => System.Numerics.BigInteger.Pow(1024, 8),
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
    }
  }

  namespace Quantities
  {
    public enum DigitalInformationUnit
    {
      /// <summary>This is the default unit for <see cref="DigitalInformation"/>.</summary>
      Byte,
      kibiByte,
      mebiByte,
      gibiByte,
      tebiByte,
      pebiByte,
      exbiByte,
      zebiByte,
      yobiByte,
    }

    /// <summary>
    /// <para>DigitalStorage, unit of natural number.</para>
    /// <para><seealso cref="https://en.wikipedia.org/wiki/DigitalInformation"/></para>
    /// </summary>
    public readonly record struct DigitalInformation
    : System.IComparable, System.IComparable<DigitalInformation>, System.IFormattable, IUnitValueQuantifiable<System.Numerics.BigInteger, DigitalInformationUnit>
    {
      private readonly System.Numerics.BigInteger m_value;

      public DigitalInformation(System.Numerics.BigInteger value, DigitalInformationUnit unit = DigitalInformationUnit.Byte)
        => m_value = unit switch
        {
          DigitalInformationUnit.Byte => value,
          DigitalInformationUnit.kibiByte => value * unit.GetUnitMultiple(),
          DigitalInformationUnit.mebiByte => value * unit.GetUnitMultiple(),
          DigitalInformationUnit.gibiByte => value * unit.GetUnitMultiple(),
          DigitalInformationUnit.tebiByte => value * unit.GetUnitMultiple(),
          DigitalInformationUnit.pebiByte => value * unit.GetUnitMultiple(),
          DigitalInformationUnit.exbiByte => value * unit.GetUnitMultiple(),
          DigitalInformationUnit.zebiByte => value * unit.GetUnitMultiple(),
          DigitalInformationUnit.yobiByte => value * unit.GetUnitMultiple(),
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Static methods

      #endregion Static methods

      #region Overloaded operators

      public static bool operator <(DigitalInformation a, DigitalInformation b) => a.CompareTo(b) < 0;
      public static bool operator <=(DigitalInformation a, DigitalInformation b) => a.CompareTo(b) <= 0;
      public static bool operator >(DigitalInformation a, DigitalInformation b) => a.CompareTo(b) > 0;
      public static bool operator >=(DigitalInformation a, DigitalInformation b) => a.CompareTo(b) >= 0;

      public static DigitalInformation operator -(DigitalInformation v) => new(-v.m_value);
      public static DigitalInformation operator +(DigitalInformation a, System.Numerics.BigInteger b) => new(a.m_value + b);
      public static DigitalInformation operator +(DigitalInformation a, DigitalInformation b) => a + b.m_value;
      public static DigitalInformation operator /(DigitalInformation a, System.Numerics.BigInteger b) => new(a.m_value / b);
      public static DigitalInformation operator /(DigitalInformation a, DigitalInformation b) => a / b.m_value;
      public static DigitalInformation operator *(DigitalInformation a, System.Numerics.BigInteger b) => new(a.m_value * b);
      public static DigitalInformation operator *(DigitalInformation a, DigitalInformation b) => a * b.m_value;
      public static DigitalInformation operator %(DigitalInformation a, System.Numerics.BigInteger b) => new(a.m_value % b);
      public static DigitalInformation operator %(DigitalInformation a, DigitalInformation b) => a % b.m_value;
      public static DigitalInformation operator -(DigitalInformation a, System.Numerics.BigInteger b) => new(a.m_value - b);
      public static DigitalInformation operator -(DigitalInformation a, DigitalInformation b) => a - b.m_value;

      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable<>
      public int CompareTo(DigitalInformation other) => m_value.CompareTo(other.m_value);

      // IComparable
      public int CompareTo(object? other) => other is not null && other is DigitalInformation o ? CompareTo(o) : -1;

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueSymbolString(DigitalInformationUnit.Byte, format, formatProvider);

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="DigitalInformationUnit.Value"/> property is in <see cref="DigitalInformationUnit.Byte"/>.</para>
      /// </summary>
      public System.Numerics.BigInteger Value => m_value;

      // IUnitQuantifiable<>
      public string GetUnitName(DigitalInformationUnit unit, bool preferPlural)
        => unit.ToString().ConvertUnitNameToPlural(preferPlural && GetUnitValue(unit).IsConsideredPlural());

      public string GetUnitSymbol(DigitalInformationUnit unit, bool preferUnicode)
        => unit switch
        {
          Quantities.DigitalInformationUnit.Byte => "B",
          Quantities.DigitalInformationUnit.kibiByte => "KiB",
          Quantities.DigitalInformationUnit.mebiByte => "MiB",
          Quantities.DigitalInformationUnit.gibiByte => "GiB",
          Quantities.DigitalInformationUnit.tebiByte => "TiB",
          Quantities.DigitalInformationUnit.pebiByte => "PiB",
          Quantities.DigitalInformationUnit.exbiByte => "EiB",
          Quantities.DigitalInformationUnit.zebiByte => "ZiB",
          Quantities.DigitalInformationUnit.yobiByte => "YiB",
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public System.Numerics.BigInteger GetUnitValue(DigitalInformationUnit unit)
        => unit switch
        {
          DigitalInformationUnit.Byte => m_value,
          DigitalInformationUnit.kibiByte => m_value / unit.GetUnitMultiple(),
          DigitalInformationUnit.mebiByte => m_value / unit.GetUnitMultiple(),
          DigitalInformationUnit.gibiByte => m_value / unit.GetUnitMultiple(),
          DigitalInformationUnit.tebiByte => m_value / unit.GetUnitMultiple(),
          DigitalInformationUnit.pebiByte => m_value / unit.GetUnitMultiple(),
          DigitalInformationUnit.exbiByte => m_value / unit.GetUnitMultiple(),
          DigitalInformationUnit.zebiByte => m_value / unit.GetUnitMultiple(),
          DigitalInformationUnit.yobiByte => m_value / unit.GetUnitMultiple(),
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitString(DigitalInformationUnit unit = DigitalInformationUnit.Byte, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
        => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

      public string ToUnitValueNameString(DigitalInformationUnit unit = DigitalInformationUnit.Byte, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = false)
        => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitName(unit, preferPlural);

      public string ToUnitValueSymbolString(DigitalInformationUnit unit = DigitalInformationUnit.Byte, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
        => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitSymbol(unit, preferUnicode);

      #endregion Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
