namespace Flux.Geometry
{
  public struct LineTestResult
    : System.IEquatable<LineTestResult>
  {
    public static readonly LineTestResult Empty;

    public LineTestOutcome Outcome { get; }

    public readonly double? X;
    public readonly double? Y;

    public LineTestResult(LineTestOutcome outcome)
    {
      Outcome = outcome;

      X = null;
      Y = null;
    }
    public LineTestResult(LineTestOutcome outcome, double x, double y)
      : this(outcome)
    {
      X = x;
      Y = y;
    }

    #region Overloaded operators
    public static bool operator ==(LineTestResult a, LineTestResult b)
      => a.Equals(b);
    public static bool operator !=(LineTestResult a, LineTestResult b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals(LineTestResult other)
      => Outcome == other.Outcome && X == other.X && Y == other.Y;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is LineTestResult o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(Outcome, X, Y);
    public override string? ToString()
      => $"{GetType().Name} {{ Outcome = {Outcome}, X = {(X.HasValue ? X.Value.ToString() : @"Null")}, Y = {(Y.HasValue ? Y.Value.ToString() : @"Null")} }}";
    #endregion Object overrides
  }
}
