namespace Flux.Media.Geometry
{
  public struct LineTestResult
    : System.IEquatable<LineTestResult>
  {
    public static readonly LineTestResult Empty;
    public bool IsEmpty => Equals(Empty);

    public LineTestOutcome Outcome { get; set; }

    public Flux.Maui.Graphics.Point? Point { get; set; }

    public LineTestResult(LineTestOutcome outcome)
    {
      Outcome = outcome;

      Point = null;
    }
    public LineTestResult(LineTestOutcome outcome, double x, double y)
      : this(outcome)
      => Point = new Flux.Maui.Graphics.Point(x, y);

    // Operators
    public static bool operator ==(LineTestResult a, LineTestResult b)
      => a.Equals(b);
    public static bool operator !=(LineTestResult a, LineTestResult b)
      => !a.Equals(b);
    // IEquatable
    public bool Equals(LineTestResult other)
      => Outcome == other.Outcome && Point!.HasValue == other.Point!.HasValue && Point!.Value == other.Point!.Value;
    // Object (overrides)
    public override bool Equals(object? obj)
      => obj is LineTestResult o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(Outcome, Point);
    public override string? ToString()
      => $"<{Outcome}{(Point!.HasValue ? $", {Point!.Value.ToString()}" : string.Empty)}>";
  }
}
