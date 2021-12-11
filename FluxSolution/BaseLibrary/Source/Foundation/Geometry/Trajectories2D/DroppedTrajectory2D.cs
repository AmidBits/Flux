namespace Flux.Mechanics
{
  public struct DroppedTrajectory2D // Projectile dropped from a moving system.
    : System.IEquatable<DroppedTrajectory2D>, ITrajectory2D
  {
    private Quantity.Length m_droppedHeight;
    private Quantity.Acceleration m_gravitationalAcceleration;
    private Quantity.Angle m_initialAngle;
    private Quantity.Speed m_initialVelocity;

    public DroppedTrajectory2D(Quantity.Length droppedHeight, Quantity.Angle initialAngle, Quantity.Speed initialVelocity, Quantity.Acceleration gravitationalAcceleration)
    {
      m_droppedHeight = droppedHeight;
      m_initialAngle = initialAngle;
      m_initialVelocity = initialVelocity;
      m_gravitationalAcceleration = gravitationalAcceleration;
    }
    public DroppedTrajectory2D(Quantity.Length droppedHeight, Quantity.Angle initialAngle, Quantity.Speed initialVelocity)
      : this(droppedHeight, initialAngle, initialVelocity, Quantity.Acceleration.StandardAccelerationOfGravity)
    { }

    // The height when dropped.
    public Quantity.Length DroppedHeight { get => m_droppedHeight; set => m_droppedHeight = value; }
    /// <summary>Gravitational acceleration in meters per second square (M/S²).</summary>
    public Quantity.Acceleration GravitationalAcceleration { get => m_gravitationalAcceleration; set => m_gravitationalAcceleration = value; }
    /// <summary>Initial angle in radians (RAD).</summary>
    public Quantity.Angle InitialAngle { get => m_initialAngle; set => m_initialAngle = value; }
    /// <summary>Initial velocity in meters per second (M/S).</summary>
    public Quantity.Speed InitialVelocity { get => m_initialVelocity; set => m_initialVelocity = value; }

    public double MaxHeight
      => m_droppedHeight.StandardUnitValue;
    public double MaxRange
      => m_initialVelocity.StandardUnitValue * MaxTime;
    public double MaxTime
      => System.Math.Sqrt(2 * MaxHeight / m_gravitationalAcceleration.StandardUnitValue);

    public double GetHeight(double time)
      => m_gravitationalAcceleration.StandardUnitValue * System.Math.Pow(time, 2) / 2;
    public double GetVelocity(double time)
      => System.Math.Sqrt(System.Math.Pow(m_initialVelocity.StandardUnitValue, 2) + System.Math.Pow(m_gravitationalAcceleration.StandardUnitValue, 2) * time * time);
    public double GetVelocityX(double time)
      => m_initialVelocity.StandardUnitValue;
    public double GetVelocityY(double time)
      => -m_gravitationalAcceleration.StandardUnitValue * time;
    public double GetX(double time)
      => m_initialVelocity.StandardUnitValue * time;
    public double GetY(double time)
      => GetHeight(time) - m_gravitationalAcceleration.StandardUnitValue * time * time / 2;

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
      => System.HashCode.Combine(m_gravitationalAcceleration.StandardUnitValue, m_initialAngle.StandardUnitValue, m_initialVelocity.StandardUnitValue, m_droppedHeight.StandardUnitValue);
    public override string ToString()
      => $"{GetType().Name} {{ MaxHeight = {MaxHeight:N1} m, MaxRange = {MaxRange:N1} m, MaxTime = {MaxTime:N1} s }}";
    #endregion Object overrides
  }
}
