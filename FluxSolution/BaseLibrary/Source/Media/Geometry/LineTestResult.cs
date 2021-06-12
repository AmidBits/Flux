namespace Flux.Media.Geometry
{
  public struct LineTestResult
    : System.IEquatable<LineTestResult>
  {
    public static readonly LineTestResult Empty;
    public bool IsEmpty => Equals(Empty);

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

    // Operators
    public static bool operator ==(LineTestResult a, LineTestResult b)
      => a.Equals(b);
    public static bool operator !=(LineTestResult a, LineTestResult b)
      => !a.Equals(b);
    // IEquatable
    public bool Equals(LineTestResult other)
      => Outcome == other.Outcome && X == other.X && Y == other.Y;
    // Object (overrides)
    public override bool Equals(object? obj)
      => obj is LineTestResult o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(Outcome, X, Y);
    public override string? ToString()
      => $"<{GetType().Name}: {Outcome}{(X.HasValue ? $", {X.Value}" : string.Empty)}{(Y.HasValue ? $", {Y.Value}" : string.Empty)}>";
  }
}
