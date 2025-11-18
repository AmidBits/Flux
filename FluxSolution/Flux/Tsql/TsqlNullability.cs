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
      => new System.Text.StringBuilder(expression).NormalizeConsecutive(1, char.IsWhiteSpace).ToString() is var text
      && (text.Equals(CsNotNull, System.StringComparison.InvariantCultureIgnoreCase))
      ? NotNull
      : (text.Equals(CsNull, System.StringComparison.InvariantCultureIgnoreCase))
      ? Null
      : throw new System.ArgumentOutOfRangeException(nameof(expression));

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

    public override string ToString() => IsNullable ? CsNull : CsNotNull;
  }
}