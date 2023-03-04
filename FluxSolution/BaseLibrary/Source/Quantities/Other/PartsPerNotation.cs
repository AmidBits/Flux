namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this Quantities.PartsPerNotationUnit source, bool preferUnicode, bool useFullName = false)
      => useFullName ? source.ToString() : source switch
      {
        Quantities.PartsPerNotationUnit.PartsPerQuadrillion => "ppq",
        Quantities.PartsPerNotationUnit.PartsPerTrillion => "ppt",
        Quantities.PartsPerNotationUnit.PartsPerBillion => "ppb",
        Quantities.PartsPerNotationUnit.PartsPerMillion => preferUnicode ? "\u33D9" : "ppm",
        Quantities.PartsPerNotationUnit.PerCentMille => "pcm",
        Quantities.PartsPerNotationUnit.PerMyriad => "\u2031",
        Quantities.PartsPerNotationUnit.PerMille => "\u2030",
        Quantities.PartsPerNotationUnit.Percent => "\u0025",
        Quantities.PartsPerNotationUnit.One => "pp1",
        _ => string.Empty,
      };

    /// <summary>Please note that not all units have an equivalent prefix.</summary>
    public static Quantities.MetricMultiplicativePrefix ToMetricMultiplicativePrefix(this Quantities.PartsPerNotationUnit unit)
      => (Quantities.MetricMultiplicativePrefix)(int)unit;
  }

  namespace Quantities
  {
    public enum PartsPerNotationUnit
    {
      /// <summary>This represents a per one (i.e. so many to one).</summary>
      One = 1,
      /// <summary>Percent.</summary>
      Percent = 2,
      /// <summary>Permille.</summary>
      PerMille = 3,
      /// <summary>Permyriad.</summary>
      PerMyriad = 4,
      /// <summary>Per cent mille, abbreviated "pcm".</summary>
      PerCentMille = 5,
      /// <summary>Abbreviated "ppm".</summary>
      PartsPerMillion = 6,
      /// <summary>Abbreviated "ppb".</summary>
      PartsPerBillion = 9,
      /// <summary>Abbreviated "ppt".</summary>
      PartsPerTrillion = 12,
      /// <summary>Abbreviated "ppq".</summary>
      PartsPerQuadrillion = 15,
    }

    /// <summary>Parts per notation. In science and engineering, the parts-per notation is a set of pseudo-units to describe small values of miscellaneous dimensionless quantities, e.g. mole fraction or mass fraction. Since these fractions are quantity-per-quantity measures, they are pure numbers with no associated units of measurement.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Parts-per_notation"/>
    public readonly record struct PartsPerNotation
      : System.IComparable, System.IComparable<PartsPerNotation>, System.IConvertible, IUnitQuantifiable<double, PartsPerNotationUnit>
    {
      public static readonly PartsPerNotation Zero;

      public const PartsPerNotationUnit DefaultUnit = PartsPerNotationUnit.Percent;

      private readonly double m_parts;
      //private readonly PartsPerNotationUnit m_unit;

      /// <summary>Creates a new instance of this type.</summary>
      /// <param name="parts">The parts in parts per notation.</param>
      /// <param name="unit">The notation in parts per notation.</param>
      public PartsPerNotation(double parts, PartsPerNotationUnit unit = DefaultUnit)
      {
        m_parts = unit switch
        {
          PartsPerNotationUnit.One => parts,
          PartsPerNotationUnit.Percent => parts / 1e2,
          PartsPerNotationUnit.PerMille => parts / 1e3,
          PartsPerNotationUnit.PerMyriad => parts / 1e4,
          PartsPerNotationUnit.PerCentMille => parts / 1e5,
          PartsPerNotationUnit.PartsPerMillion => parts / 1e6,
          PartsPerNotationUnit.PartsPerBillion => parts / 1e9,
          PartsPerNotationUnit.PartsPerTrillion => parts / 1e12,
          PartsPerNotationUnit.PartsPerQuadrillion => parts / 1e15,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

        //m_unit = unit;
      }

      #region Static methods
      #endregion Static methods

      #region Overloaded operators
      public static explicit operator double(PartsPerNotation v) => v.Value;

      public static bool operator <(PartsPerNotation a, PartsPerNotation b) => a.CompareTo(b) < 0;
      public static bool operator <=(PartsPerNotation a, PartsPerNotation b) => a.CompareTo(b) <= 0;
      public static bool operator >(PartsPerNotation a, PartsPerNotation b) => a.CompareTo(b) > 0;
      public static bool operator >=(PartsPerNotation a, PartsPerNotation b) => a.CompareTo(b) >= 0;

      public static PartsPerNotation operator -(PartsPerNotation v) => new(-v.m_parts);
      public static PartsPerNotation operator +(PartsPerNotation a, double b) => new(a.m_parts + b);
      public static PartsPerNotation operator +(PartsPerNotation a, PartsPerNotation b) => a + b.m_parts;
      public static PartsPerNotation operator /(PartsPerNotation a, double b) => new(a.m_parts / b);
      public static PartsPerNotation operator /(PartsPerNotation a, PartsPerNotation b) => a / b.m_parts;
      public static PartsPerNotation operator *(PartsPerNotation a, double b) => new(a.m_parts * b);
      public static PartsPerNotation operator *(PartsPerNotation a, PartsPerNotation b) => a * b.m_parts;
      public static PartsPerNotation operator %(PartsPerNotation a, double b) => new(a.m_parts % b);
      public static PartsPerNotation operator %(PartsPerNotation a, PartsPerNotation b) => a % b.m_parts;
      public static PartsPerNotation operator -(PartsPerNotation a, double b) => new(a.m_parts - b);
      public static PartsPerNotation operator -(PartsPerNotation a, PartsPerNotation b) => a - b.m_parts;
      #endregion Overloaded operators

      #region Implemented interfaces
      // IComparable<>
      public int CompareTo(PartsPerNotation other) => m_parts.CompareTo(other.m_parts);
      // IComparable
      public int CompareTo(object? other) => other is not null && other is PartsPerNotation o ? CompareTo(o) : -1;

      #region IConvertible
      public System.TypeCode GetTypeCode() => System.TypeCode.Object;
      public bool ToBoolean(System.IFormatProvider? provider) => Value != 0;
      public byte ToByte(System.IFormatProvider? provider) => System.Convert.ToByte(Value);
      public char ToChar(System.IFormatProvider? provider) => System.Convert.ToChar(Value);
      public System.DateTime ToDateTime(System.IFormatProvider? provider) => System.Convert.ToDateTime(Value);
      public decimal ToDecimal(System.IFormatProvider? provider) => System.Convert.ToDecimal(Value);
      public double ToDouble(System.IFormatProvider? provider) => System.Convert.ToDouble(Value);
      public short ToInt16(System.IFormatProvider? provider) => System.Convert.ToInt16(Value);
      public int ToInt32(System.IFormatProvider? provider) => System.Convert.ToInt32(Value);
      public long ToInt64(System.IFormatProvider? provider) => System.Convert.ToInt64(Value);
      [System.CLSCompliant(false)] public sbyte ToSByte(System.IFormatProvider? provider) => System.Convert.ToSByte(Value);
      public float ToSingle(System.IFormatProvider? provider) => System.Convert.ToSingle(Value);
      public string ToString(System.IFormatProvider? provider) => string.Format(provider, "{0}", Value);
      public object ToType(System.Type conversionType, System.IFormatProvider? provider) => System.Convert.ChangeType(Value, conversionType, provider);
      [System.CLSCompliant(false)] public ushort ToUInt16(System.IFormatProvider? provider) => System.Convert.ToUInt16(Value);
      [System.CLSCompliant(false)] public uint ToUInt32(System.IFormatProvider? provider) => System.Convert.ToUInt32(Value);
      [System.CLSCompliant(false)] public ulong ToUInt64(System.IFormatProvider? provider) => System.Convert.ToUInt64(Value);
      #endregion IConvertible

      // IQuantifiable<>
      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false)
        => ToUnitString(DefaultUnit, format, preferUnicode, useFullName);

      public double Value { get => m_parts; init => m_parts = value; }

      // IUnitQuantifiable<>
      public string ToUnitString(PartsPerNotationUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";

      public double ToUnitValue(PartsPerNotationUnit unit = DefaultUnit)
        => unit switch
        {
          PartsPerNotationUnit.One => m_parts,
          PartsPerNotationUnit.Percent => m_parts * 1e2,
          PartsPerNotationUnit.PerMille => m_parts * 1e3,
          PartsPerNotationUnit.PerMyriad => m_parts * 1e4,
          PartsPerNotationUnit.PerCentMille => m_parts * 1e5,
          PartsPerNotationUnit.PartsPerMillion => m_parts * 1e6,
          PartsPerNotationUnit.PartsPerBillion => m_parts * 1e9,
          PartsPerNotationUnit.PartsPerTrillion => m_parts * 1e12,
          PartsPerNotationUnit.PartsPerQuadrillion => m_parts * 1e15,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };
      #endregion Implemented interfaces

      public override string ToString() => ToQuantityString();
    }
  }
}
