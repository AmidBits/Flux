namespace Flux.Mechanics
{
  public struct FlatTrajectory2D
    : System.IEquatable<FlatTrajectory2D>, ITrajectory2D
  {
    private Acceleration m_gravitationalAcceleration;
    private Angle m_initialAngle;
    private Speed m_initialVelocity;

    public FlatTrajectory2D(Angle initialAngle, Speed initialVelocity, Acceleration gravitationalAcceleration)
    {
      m_initialAngle = initialAngle;
      m_initialVelocity = initialVelocity;
      m_gravitationalAcceleration = gravitationalAcceleration;
    }
    public FlatTrajectory2D(Angle initialAngle, Speed initialVelocity)
      : this(initialAngle, initialVelocity, Acceleration.StandardAccelerationOfGravity)
    { }

    /// <summary>Gravitational acceleration in meters per second square (M/S�).</summary>
    public Acceleration GravitationalAcceleration { get => m_gravitationalAcceleration; set => m_gravitationalAcceleration = value; }
    /// <summary>Initial angle in radians (RAD).</summary>
    public Angle InitialAngle { get => m_initialAngle; set => m_initialAngle = value; }
    /// <summary>Initial velocity in meters per second (M/S).</summary>
    public Speed InitialVelocity { get => m_initialVelocity; set => m_initialVelocity = value; }

    public double MaxHeight
      => System.Math.Pow(m_initialVelocity.GeneralUnitValue, 2) * System.Math.Pow(System.Math.Sin(m_initialAngle.GeneralUnitValue), 2) / (2 * m_gravitationalAcceleration.GeneralUnitValue);
    public double MaxRange
      => m_initialVelocity.GeneralUnitValue * MaxTime * System.Math.Cos(m_initialAngle.GeneralUnitValue);
    public double MaxTime
      => 2 * m_initialVelocity.GeneralUnitValue * System.Math.Sin(m_initialAngle.GeneralUnitValue) / m_gravitationalAcceleration.GeneralUnitValue;

    public double GetX(double time)
      => m_initialVelocity.GeneralUnitValue * System.Math.Cos(m_initialAngle.GeneralUnitValue) * time;
    public double GetY(double time)
      => m_initialVelocity.GeneralUnitValue * System.Math.Sin(m_initialAngle.GeneralUnitValue) * time - m_gravitationalAcceleration.GeneralUnitValue * time * time / 2;
    public double GetVelocityX(double time)
      => m_initialVelocity.GeneralUnitValue * System.Math.Cos(m_initialAngle.GeneralUnitValue);
    public double GetVelocityY(double time)
      => m_initialVelocity.GeneralUnitValue * System.Math.Sin(m_initialAngle.GeneralUnitValue) - m_gravitationalAcceleration.GeneralUnitValue * time;
    public double GetVelocity(double time)
      => m_initialVelocity.GeneralUnitValue * m_initialVelocity.GeneralUnitValue - 2 * m_gravitationalAcceleration.GeneralUnitValue * time * m_initialVelocity.GeneralUnitValue * System.Math.Sin(m_initialAngle.GeneralUnitValue) + System.Math.Pow(m_gravitationalAcceleration.GeneralUnitValue, 2) * time * time;

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
      => System.HashCode.Combine(m_gravitationalAcceleration.GeneralUnitValue, m_initialAngle.GeneralUnitValue, m_initialVelocity.GeneralUnitValue);
    public override string ToString()
      => $"{GetType().Name} {{ MaxHeight = {MaxHeight:N1} m, MaxRange = {MaxRange:N1} m, MaxTime = {MaxTime:N1} s }}";
    #endregion Object overrides
  }
}