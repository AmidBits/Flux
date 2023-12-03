namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.EnplethyUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.EnplethyUnit.Mole => preferUnicode ? "\u33D6" : "mol",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum EnplethyUnit
    {
      Mole,
    }

    /// <summary>Enplethy, or amount of substance. SI unit of mole. This is a base quantity.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Amount_of_substance"/>
    public readonly record struct Enplethy
      : System.IComparable, System.IComparable<Enplethy>, System.IFormattable, IUnitQuantifiable<double, EnplethyUnit>
    {
      public const EnplethyUnit DefaultUnit = EnplethyUnit.Mole;

      public static readonly Enplethy AvagadroConstant = new(1 / 6.02214076e23);

      private readonly double m_value;

      public Enplethy(double value, EnplethyUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          EnplethyUnit.Mole => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Static methods
      #endregion Static methods

      #region Overloaded operators
      public static explicit operator double(Enplethy v) => v.m_value;
      public static explicit operator Enplethy(double v) => new(v);

      public static bool operator <(Enplethy a, Enplethy b) => a.CompareTo(b) < 0;
      public static bool operator <=(Enplethy a, Enplethy b) => a.CompareTo(b) <= 0;
      public static bool operator >(Enplethy a, Enplethy b) => a.CompareTo(b) > 0;
      public static bool operator >=(Enplethy a, Enplethy b) => a.CompareTo(b) >= 0;

      public static Enplethy operator -(Enplethy v) => new(-v.Value);
      public static Enplethy operator +(Enplethy a, double b) => new(a.m_value + b);
      public static Enplethy operator +(Enplethy a, Enplethy b) => a + b.m_value;
      public static Enplethy operator /(Enplethy a, double b) => new(a.m_value / b);
      public static Enplethy operator /(Enplethy a, Enplethy b) => a / b.m_value;
      public static Enplethy operator *(Enplethy a, double b) => new(a.m_value * b);
      public static Enplethy operator *(Enplethy a, Enplethy b) => a * b.m_value;
      public static Enplethy operator %(Enplethy a, double b) => new(a.m_value % b);
      public static Enplethy operator %(Enplethy a, Enplethy b) => a % b.m_value;
      public static Enplethy operator -(Enplethy a, double b) => new(a.m_value - b);
      public static Enplethy operator -(Enplethy a, Enplethy b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is Enplethy o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(Enplethy other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // IQuantifiable<>
      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false) => ToUnitString(DefaultUnit, format, preferUnicode, useFullName);
      public double Value { get => m_value; init => m_value = value; }

      // IUnitQuantifiable<>
      public string ToUnitString(EnplethyUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";
      public double ToUnitValue(EnplethyUnit unit = DefaultUnit)
        => unit switch
        {
          EnplethyUnit.Mole => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #endregion Implemented interfaces

      public override string ToString() => ToQuantityString();
    }
  }
}
