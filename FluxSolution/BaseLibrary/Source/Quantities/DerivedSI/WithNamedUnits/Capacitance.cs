namespace Flux
{
  public static partial class QuantitiesExtensionMethods
  {
    public static string GetUnitString(this Quantities.CapacitanceUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.CapacitanceUnit.Farad => "F",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum CapacitanceUnit
    {
      /// <summary>Farad.</summary>
      Farad,
    }

    /// <summary>Electrical capacitance unit of Farad.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Capacitance"/>
    public readonly record struct Capacitance
      : System.IComparable, System.IComparable<Capacitance>, IUnitQuantifiable<double, CapacitanceUnit>
    {
      public static readonly Capacitance Zero;

      public const CapacitanceUnit DefaultUnit = CapacitanceUnit.Farad;

      private readonly double m_value;

      public Capacitance(double value, CapacitanceUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          CapacitanceUnit.Farad => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators
      public static explicit operator double(Capacitance v) => v.m_value;
      public static explicit operator Capacitance(double v) => new(v);

      public static bool operator <(Capacitance a, Capacitance b) => a.CompareTo(b) < 0;
      public static bool operator <=(Capacitance a, Capacitance b) => a.CompareTo(b) <= 0;
      public static bool operator >(Capacitance a, Capacitance b) => a.CompareTo(b) > 0;
      public static bool operator >=(Capacitance a, Capacitance b) => a.CompareTo(b) >= 0;

      public static Capacitance operator -(Capacitance v) => new(-v.m_value);
      public static Capacitance operator +(Capacitance a, double b) => new(a.m_value + b);
      public static Capacitance operator +(Capacitance a, Capacitance b) => a + b.m_value;
      public static Capacitance operator /(Capacitance a, double b) => new(a.m_value / b);
      public static Capacitance operator /(Capacitance a, Capacitance b) => a / b.m_value;
      public static Capacitance operator *(Capacitance a, double b) => new(a.m_value * b);
      public static Capacitance operator *(Capacitance a, Capacitance b) => a * b.m_value;
      public static Capacitance operator %(Capacitance a, double b) => new(a.m_value % b);
      public static Capacitance operator %(Capacitance a, Capacitance b) => a % b.m_value;
      public static Capacitance operator -(Capacitance a, double b) => new(a.m_value - b);
      public static Capacitance operator -(Capacitance a, Capacitance b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is Capacitance o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(Capacitance other) => m_value.CompareTo(other.m_value);

      // IQuantifiable<>
      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false) => ToUnitString(DefaultUnit, format, preferUnicode, useFullName);
      public double Value { get => m_value; init => m_value = value; }

      // IUnitQuantifiable<>
      public string ToUnitString(CapacitanceUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";
      public double ToUnitValue(CapacitanceUnit unit = DefaultUnit)
        => unit switch
        {
          CapacitanceUnit.Farad => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #endregion Implemented interfaces

      public override string ToString() => ToQuantityString();
    }
  }
}
