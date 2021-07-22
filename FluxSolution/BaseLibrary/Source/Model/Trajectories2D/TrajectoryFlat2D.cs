namespace Flux.Model.Trajectories
{
  public struct TrajectoryFlat2D
    : System.IEquatable<TrajectoryFlat2D>, ITrajectory2D
  {
    private Quantity.Acceleration m_gravitationalAcceleration;
    private Quantity.Angle m_initialAngle;
    private Quantity.Speed m_initialVelocity;

    public TrajectoryFlat2D(Quantity.Angle initialAngle, Quantity.Speed initialVelocity, Quantity.Acceleration gravitationalAcceleration)
    {
      m_initialAngle = initialAngle;
      m_initialVelocity = initialVelocity;
      m_gravitationalAcceleration = gravitationalAcceleration;
    }
    public TrajectoryFlat2D(Quantity.Angle initialAngle, Quantity.Speed initialVelocity)
      : this(initialAngle, initialVelocity, Quantity.Acceleration.StandardAccelerationOfGravity)
    { }

    /// <summary>Gravitational acceleration in meters per second square (M/S²).</summary>
    public Quantity.Acceleration GravitationalAcceleration { get => m_gravitationalAcceleration; set => m_gravitationalAcceleration = value; }
    /// <summary>Initial angle in radians (RAD).</summary>
    public Quantity.Angle InitialAngle { get => m_initialAngle; set => m_initialAngle = value; }
    /// <summary>Initial velocity in meters per second (M/S).</summary>
    public Quantity.Speed InitialVelocity { get => m_initialVelocity; set => m_initialVelocity = value; }

    public double MaxHeight
      => System.Math.Pow(m_initialVelocity.Value, 2) * System.Math.Pow(System.Math.Sin(m_initialAngle.Radian), 2) / (2 * m_gravitationalAcceleration.Value);
    public double MaxRange
      => m_initialVelocity.Value * MaxTime * System.Math.Cos(m_initialAngle.Radian);
    public double MaxTime
      => 2 * m_initialVelocity.Value * System.Math.Sin(m_initialAngle.Radian) / m_gravitationalAcceleration.Value;

    public double GetX(double time)
      => m_initialVelocity.Value * System.Math.Cos(m_initialAngle.Radian) * time;
    public double GetY(double time)
      => m_initialVelocity.Value * System.Math.Sin(m_initialAngle.Radian) * time - m_gravitationalAcceleration.Value * time * time / 2;
    public double GetVelocityX(double time)
      => m_initialVelocity.Value * System.Math.Cos(m_initialAngle.Radian);
    public double GetVelocityY(double time)
      => m_initialVelocity.Value * System.Math.Sin(m_initialAngle.Radian) - m_gravitationalAcceleration.Value * time;
    public double GetVelocity(double time)
      => m_initialVelocity.Value * m_initialVelocity.Value - 2 * m_gravitationalAcceleration.Value * time * m_initialVelocity.Value * System.Math.Sin(m_initialAngle.Radian) + System.Math.Pow(m_gravitationalAcceleration.Value, 2) * time * time;

    #region Overloaded operators
    public static bool operator ==(TrajectoryFlat2D h1, TrajectoryFlat2D h2)
      => h1.Equals(h2);
    public static bool operator !=(TrajectoryFlat2D h1, TrajectoryFlat2D h2)
      => !h1.Equals(h2);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals(TrajectoryFlat2D other)
      => m_gravitationalAcceleration == other.m_gravitationalAcceleration && m_initialAngle == other.m_initialAngle && m_initialVelocity == other.m_initialVelocity;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is TrajectoryFlat2D o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_gravitationalAcceleration.Value, m_initialAngle.Radian, m_initialVelocity.Value);
    public override string ToString()
      => $"<{GetType().Name}: H={MaxHeight:N1} m, R={MaxRange:N1} m, T={MaxTime:N1} s>";
    #endregion Object overrides
  }
}
