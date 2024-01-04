namespace Flux.Mechanics
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct UphillTrajectory2D
    : ITrajectory2D
  {
    private readonly double m_gravitationalAcceleration;
    private readonly double m_initialAngle;
    private readonly double m_initialVelocity;
    private readonly double m_verticalDifference;

    public UphillTrajectory2D(double verticalDifference, double initialAngle, double initialVelocity, double gravitationalAcceleration)
    {
      m_verticalDifference = verticalDifference;
      m_initialAngle = initialAngle;
      m_initialVelocity = initialVelocity;
      m_gravitationalAcceleration = gravitationalAcceleration;
    }
    public UphillTrajectory2D(double verticalDifference, double initialAngle, double initialVelocity) : this(verticalDifference, initialAngle, initialVelocity, 9.80665) { }

    public double GravitationalAcceleration { get => m_gravitationalAcceleration; init => m_gravitationalAcceleration = value; }
    public double InitialAngle { get => m_initialAngle; init => m_initialAngle = value; }
    public double InitialVelocity { get => m_initialVelocity; init => m_initialVelocity = value; }
    /// <summary>The difference of vertical level in meters (M).</summary>
    public double VerticalDifference { get => m_verticalDifference; init => m_verticalDifference = value; }

    public double MaxHeight => System.Math.Pow(m_initialVelocity, 2) * System.Math.Pow(System.Math.Sin(m_initialAngle), 2) / (2 * m_gravitationalAcceleration);
    public double MaxRange => m_initialVelocity * System.Math.Cos(m_initialAngle) * MaxTime;
    public double MaxTime => m_initialVelocity * System.Math.Sin(m_initialAngle) / m_gravitationalAcceleration + System.Math.Sqrt(2 * (MaxHeight - m_verticalDifference) / m_gravitationalAcceleration);

    public double GetVelocity(double time) => m_initialVelocity * m_initialVelocity - 2 * m_gravitationalAcceleration * time * m_initialVelocity * System.Math.Sin(m_initialAngle) + System.Math.Pow(m_gravitationalAcceleration, 2) * time * time;
    public double GetVelocityX(double time) => m_initialVelocity * System.Math.Cos(m_initialAngle) * time;
    public double GetVelocityY(double time) => m_initialVelocity * System.Math.Sin(m_initialAngle) - m_gravitationalAcceleration * time;
    public double GetX(double time) => m_initialVelocity * System.Math.Cos(m_initialAngle) * time;
    public double GetY(double time) => m_initialVelocity * System.Math.Sin(m_initialAngle) * time - m_gravitationalAcceleration * time * time / 2;
  }
}
