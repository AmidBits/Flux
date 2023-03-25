namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this Quantities.CatalyticActivityUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.CatalyticActivityUnit.Katal => "kat",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum CatalyticActivityUnit
    {
      /// <summary>Katal = (mol/s).</summary>
      Katal,
    }

    /// <summary>Catalytic activity unit of Katal.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Catalysis"/>
    public readonly record struct CatalyticActivity
      : System.IComparable, System.IComparable<CatalyticActivity>, IUnitQuantifiable<double, CatalyticActivityUnit>
    {
      public static readonly CatalyticActivity Zero;

      public const CatalyticActivityUnit DefaultUnit = CatalyticActivityUnit.Katal;

      private readonly double m_value;

      public CatalyticActivity(double value, CatalyticActivityUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          CatalyticActivityUnit.Katal => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators
      public static explicit operator double(CatalyticActivity v) => v.m_value;
      public static explicit operator CatalyticActivity(double v) => new(v);

      public static bool operator <(CatalyticActivity a, CatalyticActivity b) => a.CompareTo(b) < 0;
      public static bool operator <=(CatalyticActivity a, CatalyticActivity b) => a.CompareTo(b) <= 0;
      public static bool operator >(CatalyticActivity a, CatalyticActivity b) => a.CompareTo(b) > 0;
      public static bool operator >=(CatalyticActivity a, CatalyticActivity b) => a.CompareTo(b) >= 0;

      public static CatalyticActivity operator -(CatalyticActivity v) => new(-v.m_value);
      public static CatalyticActivity operator +(CatalyticActivity a, double b) => new(a.m_value + b);
      public static CatalyticActivity operator +(CatalyticActivity a, CatalyticActivity b) => a + b.m_value;
      public static CatalyticActivity operator /(CatalyticActivity a, double b) => new(a.m_value / b);
      public static CatalyticActivity operator /(CatalyticActivity a, CatalyticActivity b) => a / b.m_value;
      public static CatalyticActivity operator *(CatalyticActivity a, double b) => new(a.m_value * b);
      public static CatalyticActivity operator *(CatalyticActivity a, CatalyticActivity b) => a * b.m_value;
      public static CatalyticActivity operator %(CatalyticActivity a, double b) => new(a.m_value % b);
      public static CatalyticActivity operator %(CatalyticActivity a, CatalyticActivity b) => a % b.m_value;
      public static CatalyticActivity operator -(CatalyticActivity a, double b) => new(a.m_value - b);
      public static CatalyticActivity operator -(CatalyticActivity a, CatalyticActivity b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is CatalyticActivity o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(CatalyticActivity other) => m_value.CompareTo(other.m_value);

      // IQuantifiable<>
      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false) => ToUnitString(DefaultUnit, format, preferUnicode, useFullName);
      public double Value { get => m_value; init => m_value = value; }

      // IUnitQuantifiable<>
      public string ToUnitString(CatalyticActivityUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";
      public double ToUnitValue(CatalyticActivityUnit unit = DefaultUnit)
        => unit switch
        {
          CatalyticActivityUnit.Katal => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #endregion Implemented interfaces

      public override string ToString() => ToQuantityString();
    }
  }
}
