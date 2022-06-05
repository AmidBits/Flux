namespace Flux.Geometry
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly struct LineTestResult
    : System.IEquatable<LineTestResult>
  {
    public static readonly LineTestResult Empty;

    public LineTestOutcome Outcome { get; }

    private readonly double? m_x;
    private readonly double? m_y;

    public LineTestResult(LineTestOutcome outcome)
    {
      Outcome = outcome;

      m_x = null;
      m_y = null;
    }
    public LineTestResult(LineTestOutcome outcome, double x, double y)
      : this(outcome)
    {
      m_x = x;
      m_y = y;
    }

    /// <summary>The normal vector X of the Plane.</summary>
    public double? X { get => m_x; init => m_x = value; }
    /// <summary>The normal vector Y of the Plane.</summary>
    public double? Y { get => m_y; init => m_y = value; }

    #region Overloaded operators
    public static bool operator ==(LineTestResult a, LineTestResult b)
      => a.Equals(b);
    public static bool operator !=(LineTestResult a, LineTestResult b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals(LineTestResult other)
      => Outcome == other.Outcome && m_x == other.m_x && m_y == other.m_y;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is LineTestResult o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(Outcome, m_x, m_y);
    public override string? ToString()
      => $"{GetType().Name} {{ Outcome = {Outcome}, X = {(m_x.HasValue ? m_x.Value.ToString() : @"Null")}, Y = {(m_y.HasValue ? m_y.Value.ToString() : @"Null")} }}";
    #endregion Object overrides
  }
}
