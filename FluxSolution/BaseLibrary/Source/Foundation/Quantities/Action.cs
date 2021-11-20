namespace Flux.Quantity
{
  public enum ActionUnit
  {
    JouleSecond,
  }

  /// <summary>Action. Unit of Joule second.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Energy"/>
  public struct Action
    : System.IComparable<Action>, System.IEquatable<Action>, IValuedUnit<double>
  {
    private readonly double m_value;

    public Action(double value, ActionUnit unit = ActionUnit.JouleSecond)
      => m_value = unit switch
      {
        ActionUnit.JouleSecond => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double Value
      => m_value;

    public double ToUnitValue(ActionUnit unit = ActionUnit.JouleSecond)
      => unit switch
      {
        ActionUnit.JouleSecond => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Overloaded operators
    public static explicit operator double(Action v)
      => v.m_value;
    public static explicit operator Action(double v)
      => new(v);

    public static bool operator <(Action a, Action b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Action a, Action b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Action a, Action b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Action a, Action b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Action a, Action b)
      => a.Equals(b);
    public static bool operator !=(Action a, Action b)
      => !a.Equals(b);

    public static Action operator -(Action v)
      => new(-v.m_value);
    public static Action operator +(Action a, double b)
      => new(a.m_value + b);
    public static Action operator +(Action a, Action b)
      => a + b.m_value;
    public static Action operator /(Action a, double b)
      => new(a.m_value / b);
    public static Action operator /(Action a, Action b)
      => a / b.m_value;
    public static Action operator *(Action a, double b)
      => new(a.m_value * b);
    public static Action operator *(Action a, Action b)
      => a * b.m_value;
    public static Action operator %(Action a, double b)
      => new(a.m_value % b);
    public static Action operator %(Action a, Action b)
      => a % b.m_value;
    public static Action operator -(Action a, double b)
      => new(a.m_value - b);
    public static Action operator -(Action a, Action b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Action other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(Action other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Action o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {m_value} J s }}";
    #endregion Object overrides
  }
}
