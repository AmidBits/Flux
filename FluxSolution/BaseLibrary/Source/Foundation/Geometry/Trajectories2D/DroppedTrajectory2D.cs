namespace Flux.Mechanics
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public struct DroppedTrajectory2D // Projectile dropped from a moving system.
    : System.IEquatable<DroppedTrajectory2D>, ITrajectory2D
  {
    private Length m_droppedHeight;
    private Acceleration m_gravitationalAcceleration;
    private Angle m_initialAngle;
    private LinearVelocity m_initialVelocity;

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
    public Length DroppedHeight { get => m_droppedHeight; set => m_droppedHeight = value; }
    /// <summary>Gravitational acceleration in meters per second square (M/S²).</summary>
    public Acceleration GravitationalAcceleration { get => m_gravitationalAcceleration; set => m_gravitationalAcceleration = value; }
    /// <summary>Initial angle in radians (RAD).</summary>
    public Angle InitialAngle { get => m_initialAngle; set => m_initialAngle = value; }
    /// <summary>Initial velocity in meters per second (M/S).</summary>
    public LinearVelocity InitialVelocity { get => m_initialVelocity; set => m_initialVelocity = value; }

    public double MaxHeight
      => m_droppedHeight.Value;
    public double MaxRange
      => m_initialVelocity.Value * MaxTime;
    public double MaxTime
      => System.Math.Sqrt(2 * MaxHeight / m_gravitationalAcceleration.Value);

    public double GetHeight(double time)
      => m_gravitationalAcceleration.Value * System.Math.Pow(time, 2) / 2;
    public double GetVelocity(double time)
      => System.Math.Sqrt(System.Math.Pow(m_initialVelocity.Value, 2) + System.Math.Pow(m_gravitationalAcceleration.Value, 2) * time * time);
    public double GetVelocityX(double time)
      => m_initialVelocity.Value;
    public double GetVelocityY(double time)
      => -m_gravitationalAcceleration.Value * time;
    public double GetX(double time)
      => m_initialVelocity.Value * time;
    public double GetY(double time)
      => GetHeight(time) - m_gravitationalAcceleration.Value * time * time / 2;

    #region Overloaded operators
    public static bool operator ==(DroppedTrajectory2D h1, DroppedTrajectory2D h2)
      => h1.Equals(h2);
    public static bool operator !=(DroppedTrajectory2D h1, DroppedTrajectory2D h2)
      => !h1.Equals(h2);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals(DroppedTrajectory2D other)
      => m_gravitationalAcceleration == other.m_gravitationalAcceleration && m_initialAngle == other.m_initialAngle && m_initialVelocity == other.m_initialVelocity && m_droppedHeight == other.m_droppedHeight;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is DroppedTrajectory2D o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_gravitationalAcceleration.Value, m_initialAngle.Value, m_initialVelocity.Value, m_droppedHeight.Value);
    public override string ToString()
      => $"{GetType().Name} {{ MaxHeight = {MaxHeight:N1} m, MaxRange = {MaxRange:N1} m, MaxTime = {MaxTime:N1} s }}";
    #endregion Object overrides
  }
}
