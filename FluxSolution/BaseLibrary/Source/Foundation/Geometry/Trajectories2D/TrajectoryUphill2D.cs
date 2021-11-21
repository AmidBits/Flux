namespace Flux.Model.Trajectories
{
  public struct TrajectoryUphill2D
    : System.IEquatable<TrajectoryUphill2D>, ITrajectory2D
  {
    private Quantity.Acceleration m_gravitationalAcceleration;
    private Quantity.Angle m_initialAngle;
    private Quantity.Speed m_initialVelocity;
    private Quantity.Length m_verticalDifference;

    public TrajectoryUphill2D(Quantity.Length verticalDifference, Quantity.Angle initialAngle, Quantity.Speed initialVelocity, Quantity.Acceleration gravitationalAcceleration)
    {
      m_verticalDifference = verticalDifference;
      m_initialAngle = initialAngle;
      m_initialVelocity = initialVelocity;
      m_gravitationalAcceleration = gravitationalAcceleration;
    }
    public TrajectoryUphill2D(Quantity.Length verticalDifference, Quantity.Angle initialAngle, Quantity.Speed initialVelocity)
      : this(verticalDifference, initialAngle, initialVelocity, Quantity.Acceleration.StandardAccelerationOfGravity)
    { }

    /// <summary>Gravitational acceleration in meters per second square (M/S²).</summary>
    public Quantity.Acceleration GravitationalAcceleration { get => m_gravitationalAcceleration; set => m_gravitationalAcceleration = value; }
    /// <summary>Initial angle in radians (RAD).</summary>
    public Quantity.Angle InitialAngle { get => m_initialAngle; set => m_initialAngle = value; }
    /// <summary>Initial velocity in meters per second (M/S).</summary>
    public Quantity.Speed InitialVelocity { get => m_initialVelocity; set => m_initialVelocity = value; }
    /// <summary>The difference of vertical level in meters (M).</summary>
    public Quantity.Length VerticalDifference { get => m_verticalDifference; set => m_verticalDifference = value; }

    public double MaxHeight
      => System.Math.Pow(m_initialVelocity.Value, 2) * System.Math.Pow(System.Math.Sin(m_initialAngle.Value), 2) / (2 * m_gravitationalAcceleration.Value);
    public double MaxRange
      => m_initialVelocity.Value * System.Math.Cos(m_initialAngle.Value) * MaxTime;
    public double MaxTime
      => m_initialVelocity.Value * System.Math.Sin(m_initialAngle.Value) / m_gravitationalAcceleration.Value + System.Math.Sqrt(2 * (MaxHeight - m_verticalDifference.Value) / m_gravitationalAcceleration.Value);

    public double GetVelocity(double time)
      => m_initialVelocity.Value * m_initialVelocity.Value - 2 * m_gravitationalAcceleration.Value * time * m_initialVelocity.Value * System.Math.Sin(m_initialAngle.Value) + System.Math.Pow(m_gravitationalAcceleration.Value, 2) * time * time;
    public double GetVelocityX(double time)
      => m_initialVelocity.Value * System.Math.Cos(m_initialAngle.Value);
    public double GetVelocityY(double time)
      => m_initialVelocity.Value * System.Math.Sin(m_initialAngle.Value) - m_gravitationalAcceleration.Value * time;
    public double GetX(double time)
      => m_initialVelocity.Value * System.Math.Cos(m_initialAngle.Value) * time;
    public double GetY(double time)
      => m_initialVelocity.Value * System.Math.Sin(m_initialAngle.Value) * time - m_gravitationalAcceleration.Value * time * time / 2;

    #region Overloaded operators
    public static bool operator ==(TrajectoryUphill2D h1, TrajectoryUphill2D h2)
      => h1.Equals(h2);
    public static bool operator !=(TrajectoryUphill2D h1, TrajectoryUphill2D h2)
      => !h1.Equals(h2);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals(TrajectoryUphill2D other)
      => m_gravitationalAcceleration == other.m_gravitationalAcceleration && m_initialAngle == other.m_initialAngle && m_initialVelocity == other.m_initialVelocity && m_verticalDifference == other.m_verticalDifference;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is TrajectoryUphill2D o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_gravitationalAcceleration.Value, m_initialAngle.Value, m_initialVelocity.Value, m_verticalDifference.Value);
    public override string ToString()
      => $"{GetType().Name} {{ MaxHeight = {MaxHeight:N1} m, MaxRange = {MaxRange:N1} m, MaxTime = {MaxTime:N1} s }}";
    #endregion Object overrides
  }
}
