namespace Flux.Mechanics
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct DroppedTrajectory2D // Projectile dropped from a moving system.
    : ITrajectory2D
  {
    private readonly Units.Length m_droppedHeight;
    private readonly Units.Acceleration m_gravitationalAcceleration;
    private readonly Units.Angle m_initialAngle;
    private readonly Units.LinearVelocity m_initialVelocity;

    public DroppedTrajectory2D(Units.Length droppedHeight, Units.Angle initialAngle, Units.LinearVelocity initialVelocity, Units.Acceleration gravitationalAcceleration)
    {
      m_droppedHeight = droppedHeight;
      m_initialAngle = initialAngle;
      m_initialVelocity = initialVelocity;
      m_gravitationalAcceleration = gravitationalAcceleration;
    }
    public DroppedTrajectory2D(Units.Length droppedHeight, Units.Angle initialAngle, Units.LinearVelocity initialVelocity)
      : this(droppedHeight, initialAngle, initialVelocity, Units.Acceleration.StandardAccelerationOfGravity)
    { }

    // The height when dropped.
    public Units.Length DroppedHeight { get => m_droppedHeight; init => m_droppedHeight = value; }
    /// <summary>Gravitational acceleration in meters per second square (M/S²).</summary>
    public Units.Acceleration GravitationalAcceleration { get => m_gravitationalAcceleration; init => m_gravitationalAcceleration = value; }
    /// <summary>Initial angle in radians (RAD).</summary>
    public Units.Angle InitialAngle { get => m_initialAngle; init => m_initialAngle = value; }
    /// <summary>Initial velocity in meters per second (M/S).</summary>
    public Units.LinearVelocity InitialVelocity { get => m_initialVelocity; init => m_initialVelocity = value; }

    public Units.Length MaxHeight
      => new(m_droppedHeight.Value);
    public Units.Length MaxRange
      => new(m_initialVelocity.Value * MaxTime.Value);
    public Units.Time MaxTime
      => new(System.Math.Sqrt(2 * MaxHeight.Value / m_gravitationalAcceleration.Value));

    public Units.Length GetHeight(Units.Time time)
      => new(m_gravitationalAcceleration.Value * System.Math.Pow(time.Value, 2) / 2);
    public Units.LinearVelocity GetVelocity(Units.Time time)
      => new(System.Math.Sqrt(System.Math.Pow(m_initialVelocity.Value, 2) + System.Math.Pow(m_gravitationalAcceleration.Value, 2) * time.Value * time.Value));
    public Units.LinearVelocity GetVelocityX(Units.Time time)
      => new(m_initialVelocity.Value * time.Value);
    public Units.LinearVelocity GetVelocityY(Units.Time time)
      => new(-m_gravitationalAcceleration.Value * time.Value);
    public Units.Length GetX(Units.Time time)
      => new(m_initialVelocity.Value * time.Value);
    public Units.Length GetY(Units.Time time)
      => new(GetHeight(time).Value - m_gravitationalAcceleration.Value * time.Value * time.Value / 2);
  }
}
