namespace Flux.Data
{
  public struct TsqlNullability
    : System.IEquatable<TsqlNullability>
  {
    public const string CsNotNull = @"NOT NULL";
    public const string CsNull = @"NULL";

    public static TsqlNullability NotNull
      => new(false);
    public static TsqlNullability Null
      => new(true);

    public readonly bool IsNullable;

    private TsqlNullability(bool isNullable)
      => IsNullable = isNullable;

    #region Static methods
    public static TsqlNullability FromBoolean(bool value)
      => value ? Null : NotNull;
    public static TsqlNullability Parse(string expression)
    {
      var text = expression.ToStringBuilder().NormalizeAll(' ', char.IsWhiteSpace).ToString();

      if (text.Equals(CsNotNull, System.StringComparison.InvariantCultureIgnoreCase))
        return NotNull;
      else if (text.Equals(CsNull, System.StringComparison.InvariantCultureIgnoreCase))
        return Null;
      else
        throw new System.ArgumentOutOfRangeException(nameof(expression));
    }
    public static bool TryParse(string expression, out TsqlNullability result)
    {
      try
      {
        result = Parse(expression);
        return true;
      }
      catch { }

      result = default;
      return false;
    }
    #endregion Static methods

    #region Overloaded operators
    public static bool operator ==(TsqlNullability left, TsqlNullability right)
      => left.Equals(right);
    public static bool operator !=(TsqlNullability left, TsqlNullability right)
      => !left.Equals(right);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals(TsqlNullability other)
      => IsNullable == other.IsNullable;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is TsqlNullability o && Equals(o);
    public override int GetHashCode()
      => IsNullable.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ IsNullable = {IsNullable} ({ToUnitString()}) }}";
    #endregion Object overrides

    public string ToUnitString()
      => IsNullable ? CsNull : CsNotNull;
  }
}