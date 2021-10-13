namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitSymbol(this Quantity.PartsPerNotationUnit unit)
    {
      switch (unit)
      {
        case Quantity.PartsPerNotationUnit.Hundred:
          return Quantity.PartsPerNotation.PercentSymbol.ToString();
        case Quantity.PartsPerNotationUnit.Thousand:
          return Quantity.PartsPerNotation.PermilleSymbol.ToString();
        case Quantity.PartsPerNotationUnit.TenThousand:
          return Quantity.PartsPerNotation.PermyriadSymbol.ToString();
        case Quantity.PartsPerNotationUnit.HundredThousand:
          return @" pcm";
        case Quantity.PartsPerNotationUnit.Million:
          return @" ppm";
        case Quantity.PartsPerNotationUnit.Billion:
          return @" ppb";
        case Quantity.PartsPerNotationUnit.Trillion:
          return @" ppt";
        case Quantity.PartsPerNotationUnit.Quadrillion:
          return @" ppq";
        default:
          return string.Empty;
      }
    }
  }

  namespace Quantity
  {
    /// <summary>Parts per notation. In science and engineering, the parts-per notation is a set of pseudo-units to describe small values of miscellaneous dimensionless quantities, e.g. mole fraction or mass fraction. Since these fractions are quantity-per-quantity measures, they are pure numbers with no associated units of measurement.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Parts-per_notation"/>
    public struct PartsPerNotation
      : System.IComparable<PartsPerNotation>, System.IEquatable<PartsPerNotation>, IValuedUnit
    {
      public const char PercentSymbol = '\u0025';
      public const char PermilleSymbol = '\u2030';
      public const char PermyriadSymbol = '\u2031';

      private readonly double m_value;
      private readonly PartsPerNotationUnit m_unit;

      public PartsPerNotation(double value, PartsPerNotationUnit unit = PartsPerNotationUnit.Hundred)
      {
        switch (unit)
        {
          case PartsPerNotationUnit.Hundred:
            m_value = value / 1e2;
            break;
          case PartsPerNotationUnit.Thousand:
            m_value = value / 1e3;
            break;
          case PartsPerNotationUnit.TenThousand:
            m_value = value / 1e4;
            break;
          case PartsPerNotationUnit.HundredThousand:
            m_value = value / 1e5;
            break;
          case PartsPerNotationUnit.Million:
            m_value = value / 1e6;
            break;
          case PartsPerNotationUnit.Billion:
            m_value = value / 1e9;
            break;
          case PartsPerNotationUnit.Trillion:
            m_value = value / 1e12;
            break;
          case PartsPerNotationUnit.Quadrillion:
            m_value = value / 1e15;
            break;
          default:
            throw new System.ArgumentOutOfRangeException(nameof(unit));
        }

        m_unit = unit;
      }

      public double Value
        => m_value;

      public double ToUnitValue(PartsPerNotationUnit unit = PartsPerNotationUnit.Hundred)
      {
        switch (unit)
        {
          case PartsPerNotationUnit.Hundred:
            return m_value * 1e2;
          case PartsPerNotationUnit.Thousand:
            return m_value * 1e3;
          case PartsPerNotationUnit.TenThousand:
            return m_value * 1e4;
          case PartsPerNotationUnit.HundredThousand:
            return m_value * 1e5;
          case PartsPerNotationUnit.Million:
            return m_value * 1e6;
          case PartsPerNotationUnit.Billion:
            return m_value * 1e9;
          case PartsPerNotationUnit.Trillion:
            return m_value * 1e12;
          case PartsPerNotationUnit.Quadrillion:
            return m_value * 1e15;
          default:
            throw new System.ArgumentOutOfRangeException(nameof(unit));
        }
      }

      #region Static methods
      #endregion Static methods

      #region Overloaded operators
      public static explicit operator double(PartsPerNotation v)
        => v.Value;

      public static bool operator <(PartsPerNotation a, PartsPerNotation b)
        => a.CompareTo(b) < 0;
      public static bool operator <=(PartsPerNotation a, PartsPerNotation b)
        => a.CompareTo(b) <= 0;
      public static bool operator >(PartsPerNotation a, PartsPerNotation b)
        => a.CompareTo(b) > 0;
      public static bool operator >=(PartsPerNotation a, PartsPerNotation b)
        => a.CompareTo(b) >= 0;

      public static bool operator ==(PartsPerNotation a, PartsPerNotation b)
        => a.Equals(b);
      public static bool operator !=(PartsPerNotation a, PartsPerNotation b)
        => !a.Equals(b);

      public static PartsPerNotation operator -(PartsPerNotation v)
        => new PartsPerNotation(-v.m_value);
      public static PartsPerNotation operator +(PartsPerNotation a, double b)
        => new PartsPerNotation(a.m_value + b);
      public static PartsPerNotation operator +(PartsPerNotation a, PartsPerNotation b)
        => a + b.m_value;
      public static PartsPerNotation operator /(PartsPerNotation a, double b)
        => new PartsPerNotation(a.m_value / b);
      public static PartsPerNotation operator /(PartsPerNotation a, PartsPerNotation b)
        => a / b.m_value;
      public static PartsPerNotation operator *(PartsPerNotation a, double b)
        => new PartsPerNotation(a.m_value * b);
      public static PartsPerNotation operator *(PartsPerNotation a, PartsPerNotation b)
        => a * b.m_value;
      public static PartsPerNotation operator %(PartsPerNotation a, double b)
        => new PartsPerNotation(a.m_value % b);
      public static PartsPerNotation operator %(PartsPerNotation a, PartsPerNotation b)
        => a % b.m_value;
      public static PartsPerNotation operator -(PartsPerNotation a, double b)
        => new PartsPerNotation(a.m_value - b);
      public static PartsPerNotation operator -(PartsPerNotation a, PartsPerNotation b)
        => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces
      // IComparable
      public int CompareTo(PartsPerNotation other)
        => m_value.CompareTo(other.m_value);

      // IEquatable
      public bool Equals(PartsPerNotation other)
        => m_value == other.m_value;
      #endregion Implemented interfaces

      #region Object overrides
      public override bool Equals(object? obj)
        => obj is PartsPerNotation o && Equals(o);
      public override int GetHashCode()
        => System.HashCode.Combine(m_value);
      public override string ToString()
        => $"<{GetType().Name}: {ToUnitValue(m_unit)}{m_unit.GetUnitSymbol()}>";
      #endregion Object overrides
    }
  }
}
