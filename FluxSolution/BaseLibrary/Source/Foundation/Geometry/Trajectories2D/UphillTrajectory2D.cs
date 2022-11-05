namespace Flux.Mechanics
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct UphillTrajectory2D
    : ITrajectory2D
  {
    private readonly Acceleration m_gravitationalAcceleration;
    private readonly Angle m_initialAngle;
    private readonly LinearVelocity m_initialVelocity;
    private readonly Length m_verticalDifference;

    public UphillTrajectory2D(Length verticalDifference, Angle initialAngle, LinearVelocity initialVelocity, Acceleration gravitationalAcceleration)
    {
      m_verticalDifference = verticalDifference;
      m_initialAngle = initialAngle;
      m_initialVelocity = initialVelocity;
      m_gravitationalAcceleration = gravitationalAcceleration;
    }
    public UphillTrajectory2D(Length verticalDifference, Angle initialAngle, LinearVelocity initialVelocity)
      : this(verticalDifference, initialAngle, initialVelocity, Acceleration.StandardAccelerationOfGravity)
    { }

    /// <summary>Gravitational acceleration in meters per second square (M/S²).</summary>
    public Acceleration GravitationalAcceleration { get => m_gravitationalAcceleration; init => m_gravitationalAcceleration = value; }
    /// <summary>Initial angle in radians (RAD).</summary>
    public Angle InitialAngle { get => m_initialAngle; init => m_initialAngle = value; }
    /// <summary>Initial velocity in meters per second (M/S).</summary>
    public LinearVelocity InitialVelocity { get => m_initialVelocity; init => m_initialVelocity = value; }
    /// <summary>The difference of vertical level in meters (M).</summary>
    public Length VerticalDifference { get => m_verticalDifference; init => m_verticalDifference = value; }

    public Length MaxHeight
      => new(System.Math.Pow(m_initialVelocity.Value, 2) * System.Math.Pow(System.Math.Sin(m_initialAngle.Value), 2) / (2 * m_gravitationalAcceleration.Value));
    public Length MaxRange
      => new(m_initialVelocity.Value * System.Math.Cos(m_initialAngle.Value) * MaxTime.Value);
    public Time MaxTime
      => new(m_initialVelocity.Value * System.Math.Sin(m_initialAngle.Value) / m_gravitationalAcceleration.Value + System.Math.Sqrt(2 * (MaxHeight.Value - m_verticalDifference.Value) / m_gravitationalAcceleration.Value));

    public LinearVelocity GetVelocity(Time time)
      => new(m_initialVelocity.Value * m_initialVelocity.Value - 2 * m_gravitationalAcceleration.Value * time.Value * m_initialVelocity.Value * System.Math.Sin(m_initialAngle.Value) + System.Math.Pow(m_gravitationalAcceleration.Value, 2) * time.Value * time.Value);
    public LinearVelocity GetVelocityX(Time time)
      => new(m_initialVelocity.Value * System.Math.Cos(m_initialAngle.Value) * time.Value);
    public LinearVelocity GetVelocityY(Time time)
      => new(m_initialVelocity.Value * System.Math.Sin(m_initialAngle.Value) - m_gravitationalAcceleration.Value * time.Value);
    public Length GetX(Time time)
      => new(m_initialVelocity.Value * System.Math.Cos(m_initialAngle.Value) * time.Value);
    public Length GetY(Time time)
      => new(m_initialVelocity.Value * System.Math.Sin(m_initialAngle.Value) * time.Value - m_gravitationalAcceleration.Value * time.Value * time.Value / 2);
  }
}
