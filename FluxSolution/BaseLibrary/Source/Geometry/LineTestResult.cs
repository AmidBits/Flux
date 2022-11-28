namespace Flux.Geometry
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct LineTestResult
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
  }
}
