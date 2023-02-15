namespace Flux.Data
{
  public readonly record struct TsqlNullability
  {
    public const string CsNotNull = @"NOT NULL";
    public const string CsNull = @"NULL";

    public static TsqlNullability NotNull => new(false);
    public static TsqlNullability Null => new(true);

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

    #region Object overrides
    public override string ToString() => IsNullable ? CsNull : CsNotNull;
    #endregion Object overrides
  }
}