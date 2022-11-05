namespace Flux.Mechanics
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct DroppedTrajectory2D // Projectile dropped from a moving system.
    : ITrajectory2D
  {
    private readonly Length m_droppedHeight;
    private readonly Acceleration m_gravitationalAcceleration;
    private readonly Angle m_initialAngle;
    private readonly LinearVelocity m_initialVelocity;

    public DroppedTrajectory2D(Length droppedHeight, Angle initialAngle, LinearVelocity initialVelocity, Acceleration gravitationalAcceleration)
    {
      m_droppedHeight = droppedHeight;
      m_initialAngle = initialAngle;
      m_initialVelocity = initialVelocity;
      m_gravitationalAcceleration = gravitationalAcceleration;
    }
    public DroppedTrajectory2D(Length droppedHeight, Angle initialAngle, LinearVelocity initialVelocity)
      : this(droppedHeight, initialAngle, initialVelocity, Acceleration.StandardAccelerationOfGravity)
    { }

    // The height when dropped.
    public Length DroppedHeight { get => m_droppedHeight; init => m_droppedHeight = value; }
    /// <summary>Gravitational acceleration in meters per second square (M/S²).</summary>
    public Acceleration GravitationalAcceleration { get => m_gravitationalAcceleration; init => m_gravitationalAcceleration = value; }
    /// <summary>Initial angle in radians (RAD).</summary>
    public Angle InitialAngle { get => m_initialAngle; init => m_initialAngle = value; }
    /// <summary>Initial velocity in meters per second (M/S).</summary>
    public LinearVelocity InitialVelocity { get => m_initialVelocity; init => m_initialVelocity = value; }

    public Length MaxHeight
      => new(m_droppedHeight.Value);
    public Length MaxRange
      => new(m_initialVelocity.Value * MaxTime.Value);
    public Time MaxTime
      => new(System.Math.Sqrt(2 * MaxHeight.Value / m_gravitationalAcceleration.Value));

    public Length GetHeight(Time time)
      => new(m_gravitationalAcceleration.Value * System.Math.Pow(time.Value, 2) / 2);
    public LinearVelocity GetVelocity(Time time)
      => new(System.Math.Sqrt(System.Math.Pow(m_initialVelocity.Value, 2) + System.Math.Pow(m_gravitationalAcceleration.Value, 2) * time.Value * time.Value));
    public LinearVelocity GetVelocityX(Time time)
      => new(m_initialVelocity.Value * time.Value);
    public LinearVelocity GetVelocityY(Time time)
      => new(-m_gravitationalAcceleration.Value * time.Value);
    public Length GetX(Time time)
      => new(m_initialVelocity.Value * time.Value);
    public Length GetY(Time time)
      => new(GetHeight(time).Value - m_gravitationalAcceleration.Value * time.Value * time.Value / 2);
  }
}
