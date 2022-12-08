namespace Flux.Mechanics
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct DroppedTrajectory2D // Projectile dropped from a moving system.
    : ITrajectory2D
  {
    private readonly Quantities.Length m_droppedHeight;
    private readonly Quantities.Acceleration m_gravitationalAcceleration;
    private readonly Quantities.Angle m_initialAngle;
    private readonly Quantities.LinearVelocity m_initialVelocity;

    public DroppedTrajectory2D(Quantities.Length droppedHeight, Quantities.Angle initialAngle, Quantities.LinearVelocity initialVelocity, Quantities.Acceleration gravitationalAcceleration)
    {
      m_droppedHeight = droppedHeight;
      m_initialAngle = initialAngle;
      m_initialVelocity = initialVelocity;
      m_gravitationalAcceleration = gravitationalAcceleration;
    }
    public DroppedTrajectory2D(Quantities.Length droppedHeight, Quantities.Angle initialAngle, Quantities.LinearVelocity initialVelocity)
      : this(droppedHeight, initialAngle, initialVelocity, Quantities.Acceleration.StandardAccelerationOfGravity)
    { }

    // The height when dropped.
    public Quantities.Length DroppedHeight { get => m_droppedHeight; init => m_droppedHeight = value; }
    /// <summary>Gravitational acceleration in meters per second square (M/S²).</summary>
    public Quantities.Acceleration GravitationalAcceleration { get => m_gravitationalAcceleration; init => m_gravitationalAcceleration = value; }
    /// <summary>Initial angle in radians (RAD).</summary>
    public Quantities.Angle InitialAngle { get => m_initialAngle; init => m_initialAngle = value; }
    /// <summary>Initial velocity in meters per second (M/S).</summary>
    public Quantities.LinearVelocity InitialVelocity { get => m_initialVelocity; init => m_initialVelocity = value; }

    public Quantities.Length MaxHeight
      => new(m_droppedHeight.Value);
    public Quantities.Length MaxRange
      => new(m_initialVelocity.Value * MaxTime.Value);
    public Quantities.Time MaxTime
      => new(System.Math.Sqrt(2 * MaxHeight.Value / m_gravitationalAcceleration.Value));

    public Quantities.Length GetHeight(Quantities.Time time)
      => new(m_gravitationalAcceleration.Value * System.Math.Pow(time.Value, 2) / 2);
    public Quantities.LinearVelocity GetVelocity(Quantities.Time time)
      => new(System.Math.Sqrt(System.Math.Pow(m_initialVelocity.Value, 2) + System.Math.Pow(m_gravitationalAcceleration.Value, 2) * time.Value * time.Value));
    public Quantities.LinearVelocity GetVelocityX(Quantities.Time time)
      => new(m_initialVelocity.Value * time.Value);
    public Quantities.LinearVelocity GetVelocityY(Quantities.Time time)
      => new(-m_gravitationalAcceleration.Value * time.Value);
    public Quantities.Length GetX(Quantities.Time time)
      => new(m_initialVelocity.Value * time.Value);
    public Quantities.Length GetY(Quantities.Time time)
      => new(GetHeight(time).Value - m_gravitationalAcceleration.Value * time.Value * time.Value / 2);
  }
}
