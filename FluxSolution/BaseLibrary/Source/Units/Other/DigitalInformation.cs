namespace Flux
{
  public static partial class Em
  {
    public static System.Numerics.BigInteger GetUnitMultiple(this Units.DigitalInformationUnit unit)
    {
      return unit switch
      {
        Units.DigitalInformationUnit.Byte => 1,
        Units.DigitalInformationUnit.kibiByte => 1024,
        Units.DigitalInformationUnit.mebiByte => System.Numerics.BigInteger.Pow(1024, 2),
        Units.DigitalInformationUnit.gibiByte => System.Numerics.BigInteger.Pow(1024, 3),
        Units.DigitalInformationUnit.tebiByte => System.Numerics.BigInteger.Pow(1024, 4),
        Units.DigitalInformationUnit.pebiByte => System.Numerics.BigInteger.Pow(1024, 5),
        Units.DigitalInformationUnit.exbiByte => System.Numerics.BigInteger.Pow(1024, 6),
        Units.DigitalInformationUnit.zebiByte => System.Numerics.BigInteger.Pow(1024, 7),
        Units.DigitalInformationUnit.yobiByte => System.Numerics.BigInteger.Pow(1024, 8),
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
    }

    public static string GetUnitString(this Units.DigitalInformationUnit unit, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.DigitalInformationUnit.Byte => "B",
        Units.DigitalInformationUnit.kibiByte => "KiB",
        Units.DigitalInformationUnit.mebiByte => "MiB",
        Units.DigitalInformationUnit.gibiByte => "GiB",
        Units.DigitalInformationUnit.tebiByte => "TiB",
        Units.DigitalInformationUnit.pebiByte => "PiB",
        Units.DigitalInformationUnit.exbiByte => "EiB",
        Units.DigitalInformationUnit.zebiByte => "ZiB",
        Units.DigitalInformationUnit.yobiByte => "YiB",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
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
    : System.IComparable, System.IComparable<DigitalInformation>, System.IFormattable, IValueQuantifiable<System.Numerics.BigInteger>
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
        => ToUnitValueString(DigitalInformationUnit.Byte, format, formatProvider);

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="DigitalInformationUnit.Value"/> property is in <see cref="DigitalInformationUnit.Byte"/>.</para>
      /// </summary>
      public System.Numerics.BigInteger Value => m_value;

      // IUnitQuantifiable<>
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

      public string ToUnitValueString(DigitalInformationUnit unit = DigitalInformationUnit.Byte, string? format = null, System.IFormatProvider? formatProvider = null, bool preferUnicode = false, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool useFullName = false)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(format, formatProvider));
        sb.Append(unitSpacing.ToSpacingString());
        sb.Append(unit.GetUnitString(useFullName));
        return sb.ToString();
      }

      #endregion Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
