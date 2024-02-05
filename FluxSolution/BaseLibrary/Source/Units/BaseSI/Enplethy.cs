namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.EnplethyUnit unit, QuantifiableValueStringOptions options)
      => options.UseFullName ? unit.ToString() : unit switch
      {
        Units.EnplethyUnit.Mole => options.PreferUnicode ? "\u33D6" : "mol",
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
    /// <see href="https://en.wikipedia.org/wiki/Amount_of_substance"/>
    public readonly record struct Enplethy
      : System.IComparable, System.IComparable<Enplethy>, System.IFormattable, IUnitValueQuantifiable<double, EnplethyUnit>
    {
      /// <summary>The exact number of elementary entities in one mole.</summary>
      public static readonly double AvagadroNumber = 6.02214076e23;

      /// <summary>The dimension of the Avagadro constant is the reciprocal of amount of substance.</summary>
      public static readonly Enplethy AvagadroConstant = new(1 / AvagadroNumber);

      private readonly double m_value;

      public Enplethy(double value, EnplethyUnit unit = EnplethyUnit.Mole)
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
      public string ToValueString(QuantifiableValueStringOptions options) => ToUnitValueString(EnplethyUnit.Mole, options);

      /// <summary>
      /// <para>The unit of the <see cref="Enplethy.Value"/> property is in <see cref="EnplethyUnit.Mole"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(EnplethyUnit unit)
        => unit switch
        {
          EnplethyUnit.Mole => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(EnplethyUnit unit, QuantifiableValueStringOptions options)
        => $"{string.Format($"{{0{(options.Format is null ? string.Empty : $":{options.Format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(options)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString(QuantifiableValueStringOptions.Default);
    }
  }
}
