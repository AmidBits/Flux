namespace Flux
{
  public static partial class QuantitiesExtensionMethods
  {
#pragma warning disable IDE0060 // Remove unused parameter
    public static string GetUnitString(this Quantities.InductanceUnit unit, bool preferUnicode, bool useFullName = false)
#pragma warning restore IDE0060 // Remove unused parameter
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.InductanceUnit.Henry => "H",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum InductanceUnit
    {
      /// <summary>Henry.</summary>
      Henry,
    }

    /// <summary>Electrical inductance unit of Henry.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inductance"/>
    public readonly record struct Inductance
      : System.IComparable, System.IComparable<Inductance>, IUnitQuantifiable<double, InductanceUnit>
    {
      public static readonly Inductance Zero;

      public const InductanceUnit DefaultUnit = InductanceUnit.Henry;

      private readonly double m_value;

      public Inductance(double value, InductanceUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          InductanceUnit.Henry => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators
      public static explicit operator double(Inductance v) => v.m_value;
      public static explicit operator Inductance(double v) => new(v);

      public static bool operator <(Inductance a, Inductance b) => a.CompareTo(b) < 0;
      public static bool operator <=(Inductance a, Inductance b) => a.CompareTo(b) <= 0;
      public static bool operator >(Inductance a, Inductance b) => a.CompareTo(b) > 0;
      public static bool operator >=(Inductance a, Inductance b) => a.CompareTo(b) >= 0;

      public static Inductance operator -(Inductance v) => new(-v.m_value);
      public static Inductance operator +(Inductance a, double b) => new(a.m_value + b);
      public static Inductance operator +(Inductance a, Inductance b) => a + b.m_value;
      public static Inductance operator /(Inductance a, double b) => new(a.m_value / b);
      public static Inductance operator /(Inductance a, Inductance b) => a / b.m_value;
      public static Inductance operator *(Inductance a, double b) => new(a.m_value * b);
      public static Inductance operator *(Inductance a, Inductance b) => a * b.m_value;
      public static Inductance operator %(Inductance a, double b) => new(a.m_value % b);
      public static Inductance operator %(Inductance a, Inductance b) => a % b.m_value;
      public static Inductance operator -(Inductance a, double b) => new(a.m_value - b);
      public static Inductance operator -(Inductance a, Inductance b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is Inductance o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(Inductance other) => m_value.CompareTo(other.m_value);

      // IQuantifiable<>
      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false) => ToUnitString(DefaultUnit, format, preferUnicode, useFullName);
      public double Value { get => m_value; init => m_value = value; }

      // IUnitQuantifiable<>
      public string ToUnitString(InductanceUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";
      public double ToUnitValue(InductanceUnit unit = DefaultUnit)
        => unit switch
        {
          InductanceUnit.Henry => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #endregion Implemented interfaces

      public override string ToString() => ToQuantityString();
    }
  }
}
