namespace Flux.Geometry
{
  public struct LineSlope
    : System.IEquatable<LineSlope>
  {
    public static readonly LineSlope Zero;

    private readonly double m_slope;

    public LineSlope(double slope)
      => m_slope = slope;
    public LineSlope(double x1, double y1, double x2, double y2)
      => m_slope = (y2 - y1) / (x2 - x1);

    #region Static methods
    #endregion Static methods

    #region Overloaded operators
    public static bool operator ==(LineSlope a, LineSlope b)
      => a.Equals(b);
    public static bool operator !=(LineSlope a, LineSlope b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals(LineSlope other)
      => m_slope == other.m_slope;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is LineSlope o && Equals(o);
    public override int GetHashCode()
      => m_slope.GetHashCode();
    public override string? ToString()
      => $"{GetType().Name} {{ Slope = {m_slope} }}";
    #endregion Object overrides
  }
}
