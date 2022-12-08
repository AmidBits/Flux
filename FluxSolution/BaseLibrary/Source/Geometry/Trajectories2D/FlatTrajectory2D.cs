namespace Flux.Mechanics
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct FlatTrajectory2D
    : ITrajectory2D
  {
    private readonly Quantities.Acceleration m_gravitationalAcceleration;
    private readonly Quantities.Angle m_initialAngle;
    private readonly Quantities.LinearVelocity m_initialVelocity;

    public FlatTrajectory2D(Quantities.Angle initialAngle, Quantities.LinearVelocity initialVelocity, Quantities.Acceleration gravitationalAcceleration)
    {
      m_initialAngle = initialAngle;
      m_initialVelocity = initialVelocity;
      m_gravitationalAcceleration = gravitationalAcceleration;
    }
    public FlatTrajectory2D(Quantities.Angle initialAngle, Quantities.LinearVelocity initialVelocity)
      : this(initialAngle, initialVelocity, Quantities.Acceleration.StandardAccelerationOfGravity)
    { }

    /// <summary>Gravitational acceleration in meters per second square (M/S²).</summary>
    public Quantities.Acceleration GravitationalAcceleration { get => m_gravitationalAcceleration; init => m_gravitationalAcceleration = value; }
    /// <summary>Initial angle in radians (RAD).</summary>
    public Quantities.Angle InitialAngle { get => m_initialAngle; init => m_initialAngle = value; }
    /// <summary>Initial velocity in meters per second (M/S).</summary>
    public Quantities.LinearVelocity InitialVelocity { get => m_initialVelocity; init => m_initialVelocity = value; }

    public Quantities.Length MaxHeight
      => new(System.Math.Pow(m_initialVelocity.Value, 2) * System.Math.Pow(System.Math.Sin(m_initialAngle.Value), 2) / (2 * m_gravitationalAcceleration.Value));
    public Quantities.Length MaxRange
      => new(m_initialVelocity.Value * MaxTime.Value * System.Math.Cos(m_initialAngle.Value));
    public Quantities.Time MaxTime
      => new(2 * m_initialVelocity.Value * System.Math.Sin(m_initialAngle.Value) / m_gravitationalAcceleration.Value);

    public Quantities.Length GetX(Quantities.Time time)
      => new(m_initialVelocity.Value * System.Math.Cos(m_initialAngle.Value) * time.Value);
    public Quantities.Length GetY(Quantities.Time time)
      => new(m_initialVelocity.Value * System.Math.Sin(m_initialAngle.Value) * time.Value - m_gravitationalAcceleration.Value * time.Value * time.Value / 2);
    public Quantities.LinearVelocity GetVelocityX(Quantities.Time time)
      => new(m_initialVelocity.Value * System.Math.Cos(m_initialAngle.Value) * time.Value);
    public Quantities.LinearVelocity GetVelocityY(Quantities.Time time)
      => new(m_initialVelocity.Value * System.Math.Sin(m_initialAngle.Value) - m_gravitationalAcceleration.Value * time.Value);
    public Quantities.LinearVelocity GetVelocity(Quantities.Time time)
      => new(m_initialVelocity.Value * m_initialVelocity.Value - 2 * m_gravitationalAcceleration.Value * time.Value * m_initialVelocity.Value * System.Math.Sin(m_initialAngle.Value) + System.Math.Pow(m_gravitationalAcceleration.Value, 2) * time.Value * time.Value);
  }
}
