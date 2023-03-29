namespace Flux
{
  public static partial class QuantitiesExtensionMethods
  {
    public static string GetUnitString(this Quantities.ImpulseUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.ImpulseUnit.NewtonSecond => preferUnicode ? "N\u22C5s" : "N·s",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum ImpulseUnit
    {
      NewtonSecond,
    }

    /// <summary>Impulse, unit of Newton second.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Impulse"/>
    public readonly record struct Impulse
      : System.IComparable, System.IComparable<Impulse>, System.IFormattable, IUnitQuantifiable<double, ImpulseUnit>
    {
      public static readonly Impulse Zero;

      public const ImpulseUnit DefaultUnit = ImpulseUnit.NewtonSecond;

      private readonly double m_value;

      public Impulse(double value, ImpulseUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          ImpulseUnit.NewtonSecond => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Static methods

      public static Impulse From(Force force, Time time)
        => new(force.Value / time.Value);
      #endregion Static methods

      #region Overloaded operators
      public static explicit operator double(Impulse v) => v.m_value;
      public static explicit operator Impulse(double v) => new(v);

      public static bool operator <(Impulse a, Impulse b) => a.CompareTo(b) < 0;
      public static bool operator <=(Impulse a, Impulse b) => a.CompareTo(b) <= 0;
      public static bool operator >(Impulse a, Impulse b) => a.CompareTo(b) > 0;
      public static bool operator >=(Impulse a, Impulse b) => a.CompareTo(b) >= 0;

      public static Impulse operator -(Impulse v) => new(-v.m_value);
      public static Impulse operator +(Impulse a, double b) => new(a.m_value + b);
      public static Impulse operator +(Impulse a, Impulse b) => a + b.m_value;
      public static Impulse operator /(Impulse a, double b) => new(a.m_value / b);
      public static Impulse operator /(Impulse a, Impulse b) => a / b.m_value;
      public static Impulse operator *(Impulse a, double b) => new(a.m_value * b);
      public static Impulse operator *(Impulse a, Impulse b) => a * b.m_value;
      public static Impulse operator %(Impulse a, double b) => new(a.m_value % b);
      public static Impulse operator %(Impulse a, Impulse b) => a % b.m_value;
      public static Impulse operator -(Impulse a, double b) => new(a.m_value - b);
      public static Impulse operator -(Impulse a, Impulse b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is Impulse o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(Impulse other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // IQuantifiable<>
      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false) => ToUnitString(DefaultUnit, format, preferUnicode, useFullName);
      public double Value { get => m_value; init => m_value = value; }

      // IUnitQuantifiable<>
      public string ToUnitString(ImpulseUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";
      public double ToUnitValue(ImpulseUnit unit = DefaultUnit)
        => unit switch
        {
          ImpulseUnit.NewtonSecond => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #endregion Implemented interfaces

      public override string ToString() => ToQuantityString();
    }
  }
}
