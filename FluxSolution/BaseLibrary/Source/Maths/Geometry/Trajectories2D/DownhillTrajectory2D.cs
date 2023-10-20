namespace Flux.Mechanics
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct DownhillTrajectory2D
    : ITrajectory2D
  {
    private readonly double m_gravitationalAcceleration;
    private readonly double m_initialAngle;
    private readonly double m_initialVelocity;
    private readonly double m_verticalDifference;

    public DownhillTrajectory2D(double verticalDifference, double initialAngle, double initialVelocity, double gravitationalAcceleration)
    {
      m_verticalDifference = verticalDifference;
      m_initialAngle = initialAngle;
      m_initialVelocity = initialVelocity;
      m_gravitationalAcceleration = gravitationalAcceleration;
    }
    public DownhillTrajectory2D(double verticalDifference, double initialAngle, double initialVelocity)
      : this(verticalDifference, initialAngle, initialVelocity, 9.80665)
    { }

    /// <summary>Gravitational acceleration in meters per second square (M/S²).</summary>
    public double GravitationalAcceleration { get => m_gravitationalAcceleration; init => m_gravitationalAcceleration = value; }
    /// <summary>Initial angle in radians (RAD).</summary>
    public double InitialAngle { get => m_initialAngle; init => m_initialAngle = value; }
    /// <summary>Initial velocity in meters per second (M/S).</summary>
    public double InitialVelocity { get => m_initialVelocity; init => m_initialVelocity = value; }
    /// <summary>The difference of vertical level in meters (M).</summary>
    public double VerticalDifference { get => m_verticalDifference; init => m_verticalDifference = value; }

    public double MaxHeight
      => m_verticalDifference + System.Math.Pow(m_initialVelocity, 2) * System.Math.Pow(System.Math.Sin(m_initialAngle), 2) / (2 * m_gravitationalAcceleration);
    public double MaxRange
      => m_initialVelocity * System.Math.Cos(m_initialAngle) * MaxTime;
    public double MaxTime
      => m_initialVelocity * System.Math.Sin(m_initialAngle) / m_gravitationalAcceleration + System.Math.Sqrt(2 * MaxHeight / m_gravitationalAcceleration);

    public double GetVelocity(double time)
      => m_initialVelocity * m_initialVelocity - 2 * m_gravitationalAcceleration * time * m_initialVelocity * System.Math.Sin(m_initialAngle) + System.Math.Pow(m_gravitationalAcceleration, 2) * time * time;
    public double GetVelocityX(double time)
      => m_initialVelocity * System.Math.Cos(m_initialAngle) * time;
    public double GetVelocityY(double time)
      => m_initialVelocity * System.Math.Sin(m_initialAngle) - m_gravitationalAcceleration * time;
    public double GetX(double time)
      => m_initialVelocity * System.Math.Cos(m_initialAngle) * time;
    public double GetY(double time)
      => m_initialVelocity * System.Math.Sin(m_initialAngle) * time - m_gravitationalAcceleration * time * time / 2;
  }
}
