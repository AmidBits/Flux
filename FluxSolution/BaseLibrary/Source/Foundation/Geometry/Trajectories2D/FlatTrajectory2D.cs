namespace Flux.Mechanics
{
  public struct FlatTrajectory2D
    : System.IEquatable<FlatTrajectory2D>, ITrajectory2D
  {
    private Quantity.Acceleration m_gravitationalAcceleration;
    private Quantity.Angle m_initialAngle;
    private Quantity.Speed m_initialVelocity;

    public FlatTrajectory2D(Quantity.Angle initialAngle, Quantity.Speed initialVelocity, Quantity.Acceleration gravitationalAcceleration)
    {
      m_initialAngle = initialAngle;
      m_initialVelocity = initialVelocity;
      m_gravitationalAcceleration = gravitationalAcceleration;
    }
    public FlatTrajectory2D(Quantity.Angle initialAngle, Quantity.Speed initialVelocity)
      : this(initialAngle, initialVelocity, Quantity.Acceleration.StandardAccelerationOfGravity)
    { }

    /// <summary>Gravitational acceleration in meters per second square (M/S²).</summary>
    public Quantity.Acceleration GravitationalAcceleration { get => m_gravitationalAcceleration; set => m_gravitationalAcceleration = value; }
    /// <summary>Initial angle in radians (RAD).</summary>
    public Quantity.Angle InitialAngle { get => m_initialAngle; set => m_initialAngle = value; }
    /// <summary>Initial velocity in meters per second (M/S).</summary>
    public Quantity.Speed InitialVelocity { get => m_initialVelocity; set => m_initialVelocity = value; }

    public double MaxHeight
      => System.Math.Pow(m_initialVelocity.StandardUnitValue, 2) * System.Math.Pow(System.Math.Sin(m_initialAngle.StandardUnitValue), 2) / (2 * m_gravitationalAcceleration.StandardUnitValue);
    public double MaxRange
      => m_initialVelocity.StandardUnitValue * MaxTime * System.Math.Cos(m_initialAngle.StandardUnitValue);
    public double MaxTime
      => 2 * m_initialVelocity.StandardUnitValue * System.Math.Sin(m_initialAngle.StandardUnitValue) / m_gravitationalAcceleration.StandardUnitValue;

    public double GetX(double time)
      => m_initialVelocity.StandardUnitValue * System.Math.Cos(m_initialAngle.StandardUnitValue) * time;
    public double GetY(double time)
      => m_initialVelocity.StandardUnitValue * System.Math.Sin(m_initialAngle.StandardUnitValue) * time - m_gravitationalAcceleration.StandardUnitValue * time * time / 2;
    public double GetVelocityX(double time)
      => m_initialVelocity.StandardUnitValue * System.Math.Cos(m_initialAngle.StandardUnitValue);
    public double GetVelocityY(double time)
      => m_initialVelocity.StandardUnitValue * System.Math.Sin(m_initialAngle.StandardUnitValue) - m_gravitationalAcceleration.StandardUnitValue * time;
    public double GetVelocity(double time)
      => m_initialVelocity.StandardUnitValue * m_initialVelocity.StandardUnitValue - 2 * m_gravitationalAcceleration.StandardUnitValue * time * m_initialVelocity.StandardUnitValue * System.Math.Sin(m_initialAngle.StandardUnitValue) + System.Math.Pow(m_gravitationalAcceleration.StandardUnitValue, 2) * time * time;

    #region Overloaded operators
    public static bool operator ==(FlatTrajectory2D h1, FlatTrajectory2D h2)
      => h1.Equals(h2);
    public static bool operator !=(FlatTrajectory2D h1, FlatTrajectory2D h2)
      => !h1.Equals(h2);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals(FlatTrajectory2D other)
      => m_gravitationalAcceleration == other.m_gravitationalAcceleration && m_initialAngle == other.m_initialAngle && m_initialVelocity == other.m_initialVelocity;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is FlatTrajectory2D o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_gravitationalAcceleration.StandardUnitValue, m_initialAngle.StandardUnitValue, m_initialVelocity.StandardUnitValue);
    public override string ToString()
      => $"{GetType().Name} {{ MaxHeight = {MaxHeight:N1} m, MaxRange = {MaxRange:N1} m, MaxTime = {MaxTime:N1} s }}";
    #endregion Object overrides
  }
}
