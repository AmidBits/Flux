namespace Flux.Geometry
{
#if NET5_0
  public struct LineTestResult
    : System.IEquatable<LineTestResult>
#elif NET6_0_OR_GREATER
  public record struct LineTestResult
#endif
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
#if NET5_0
    public static bool operator ==(LineTestResult a, LineTestResult b)
      => a.Equals(b);
    public static bool operator !=(LineTestResult a, LineTestResult b)
      => !a.Equals(b);
#endif
    #endregion Overloaded operators

    #region Implemented interfaces
#if NET5_0
    // IEquatable
    public bool Equals(LineTestResult other)
      => Outcome == other.Outcome && X == other.X && Y == other.Y;
#endif
    #endregion Implemented interfaces

    #region Object overrides
#if NET5_0
    public override bool Equals(object? obj)
      => obj is LineTestResult o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(Outcome, X, Y);
#endif
    public override string? ToString()
      => $"{GetType().Name} {{ Outcome = {Outcome}, X = {(X.HasValue ? X.Value.ToString() : @"Null")}, Y = {(Y.HasValue ? Y.Value.ToString() : @"Null")} }}";
    #endregion Object overrides
  }
}
