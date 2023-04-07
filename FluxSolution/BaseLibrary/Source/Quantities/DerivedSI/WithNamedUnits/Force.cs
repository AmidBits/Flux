namespace Flux
{
  public static partial class QuantitiesExtensionMethods
  {
#pragma warning disable IDE0060 // Remove unused parameter
    public static string GetUnitString(this Quantities.ForceUnit unit, bool preferUnicode, bool useFullName = false)
#pragma warning restore IDE0060 // Remove unused parameter
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.ForceUnit.Newton => "N",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum ForceUnit
    {
      /// <summary>Newton.</summary>
      Newton,
    }

    /// <summary>Force, unit of newton. This is an SI derived quantity.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Force"/>
    public readonly record struct Force
      : System.IComparable, System.IComparable<Force>, IUnitQuantifiable<double, ForceUnit>
    {
      public const ForceUnit DefaultUnit = ForceUnit.Newton;

      private readonly double m_value;

      public Force(double value, ForceUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          ForceUnit.Newton => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators
      public static explicit operator double(Force v) => v.m_value;
      public static explicit operator Force(double v) => new(v);

      public static bool operator <(Force a, Force b) => a.CompareTo(b) < 0;
      public static bool operator <=(Force a, Force b) => a.CompareTo(b) <= 0;
      public static bool operator >(Force a, Force b) => a.CompareTo(b) > 0;
      public static bool operator >=(Force a, Force b) => a.CompareTo(b) >= 0;

      public static Force operator -(Force v) => new(-v.m_value);
      public static Force operator +(Force a, double b) => new(a.m_value + b);
      public static Force operator +(Force a, Force b) => a + b.m_value;
      public static Force operator /(Force a, double b) => new(a.m_value / b);
      public static Force operator /(Force a, Force b) => a / b.m_value;
      public static Force operator *(Force a, double b) => new(a.m_value * b);
      public static Force operator *(Force a, Force b) => a * b.m_value;
      public static Force operator %(Force a, double b) => new(a.m_value % b);
      public static Force operator %(Force a, Force b) => a % b.m_value;
      public static Force operator -(Force a, double b) => new(a.m_value - b);
      public static Force operator -(Force a, Force b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is Force o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(Force other) => m_value.CompareTo(other.m_value);

      // IQuantifiable<>
      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false) => ToUnitString(DefaultUnit, format, preferUnicode, useFullName);
      public double Value { get => m_value; init => m_value = value; }

      // IUnitQuantifiable<>
      public string ToUnitString(ForceUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";
      public double ToUnitValue(ForceUnit unit = DefaultUnit)
        => unit switch
        {
          ForceUnit.Newton => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #endregion Implemented interfaces

      public override string ToString() => ToQuantityString();
    }
  }
}
