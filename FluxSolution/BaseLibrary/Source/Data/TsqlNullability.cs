namespace Flux.Data
{
  public struct TsqlNullability
    : System.IEquatable<TsqlNullability>
  {
    public const string CsNotNull = @"NOT NULL";
    public const string CsNull = @"NULL";

    public bool IsNullable { get; private set; }

    public TsqlNullability(bool isNullable)
      => IsNullable = isNullable;

    public static TsqlNullability Parse(string expression)
      => (expression.ToStringBuilder().NormalizeAll(' ', char.IsWhiteSpace).ToUpperCase(System.Globalization.CultureInfo.InvariantCulture).ToString()) switch
      {
        CsNotNull => new TsqlNullability() { IsNullable = false },
        CsNull => new TsqlNullability() { IsNullable = true },
        _ => throw new System.ArgumentOutOfRangeException(nameof(expression)),
      };
    public static bool TryParse(string expression, out TsqlNullability result)
    {
      try
      {
        result = Parse(expression);
        return true;
      }
#pragma warning disable CA1031 // Do not catch general exception types.
      catch
#pragma warning restore CA1031 // Do not catch general exception types.
      { }

      result = default;
      return false;
    }

    // System.IEquatable<SqlDefinitionNullability>
    public bool Equals(TsqlNullability other)
      => IsNullable == other.IsNullable;
    // System.Object Overrides
    public override bool Equals(object? obj)
      => obj is TsqlNullability ? this.Equals((TsqlNullability)obj) : false;
    public override int GetHashCode()
      => IsNullable.GetHashCode();
    public override string ToString()
      => IsNullable ? CsNull : CsNotNull;
    // Operators
    public static bool operator ==(TsqlNullability left, TsqlNullability right)
      => left.Equals(right);
    public static bool operator !=(TsqlNullability left, TsqlNullability right)
      => !left.Equals(right);
  }
}