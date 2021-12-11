namespace Flux.Mechanics
{
  public struct DownhillTrajectory2D
    : System.IEquatable<DownhillTrajectory2D>, ITrajectory2D
  {
    private Quantity.Acceleration m_gravitationalAcceleration;
    private Quantity.Angle m_initialAngle;
    private Quantity.Speed m_initialVelocity;
    private Quantity.Length m_verticalDifference;

    public DownhillTrajectory2D(Quantity.Length verticalDifference, Quantity.Angle initialAngle, Quantity.Speed initialVelocity, Quantity.Acceleration gravitationalAcceleration)
    {
      m_verticalDifference = verticalDifference;
      m_initialAngle = initialAngle;
      m_initialVelocity = initialVelocity;
      m_gravitationalAcceleration = gravitationalAcceleration;
    }
    public DownhillTrajectory2D(Quantity.Length verticalDifference, Quantity.Angle initialAngle, Quantity.Speed initialVelocity)
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
      => m_verticalDifference.StandardUnitValue + System.Math.Pow(m_initialVelocity.StandardUnitValue, 2) * System.Math.Pow(System.Math.Sin(m_initialAngle.StandardUnitValue), 2) / (2 * m_gravitationalAcceleration.StandardUnitValue);
    public double MaxRange
      => m_initialVelocity.StandardUnitValue * System.Math.Cos(m_initialAngle.StandardUnitValue) * MaxTime;
    public double MaxTime
      => m_initialVelocity.StandardUnitValue * System.Math.Sin(m_initialAngle.StandardUnitValue) / m_gravitationalAcceleration.StandardUnitValue + System.Math.Sqrt(2 * MaxHeight / m_gravitationalAcceleration.StandardUnitValue);

    public double GetVelocity(double time)
      => m_initialVelocity.StandardUnitValue * m_initialVelocity.StandardUnitValue - 2 * m_gravitationalAcceleration.StandardUnitValue * time * m_initialVelocity.StandardUnitValue * System.Math.Sin(m_initialAngle.StandardUnitValue) + System.Math.Pow(m_gravitationalAcceleration.StandardUnitValue, 2) * time * time;
    public double GetVelocityX(double time)
      => m_initialVelocity.StandardUnitValue * System.Math.Cos(m_initialAngle.StandardUnitValue);
    public double GetVelocityY(double time)
      => m_initialVelocity.StandardUnitValue * System.Math.Sin(m_initialAngle.StandardUnitValue) - m_gravitationalAcceleration.StandardUnitValue * time;
    public double GetX(double time)
      => m_initialVelocity.StandardUnitValue * System.Math.Cos(m_initialAngle.StandardUnitValue) * time;
    public double GetY(double time)
      => m_initialVelocity.StandardUnitValue * System.Math.Sin(m_initialAngle.StandardUnitValue) * time - m_gravitationalAcceleration.StandardUnitValue * time * time / 2;

    #region Overloaded operators
    public static bool operator ==(DownhillTrajectory2D h1, DownhillTrajectory2D h2)
      => h1.Equals(h2);
    public static bool operator !=(DownhillTrajectory2D h1, DownhillTrajectory2D h2)
      => !h1.Equals(h2);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals(DownhillTrajectory2D other)
      => m_gravitationalAcceleration == other.m_gravitationalAcceleration && m_initialAngle == other.m_initialAngle && m_initialVelocity == other.m_initialVelocity && m_verticalDifference == other.m_verticalDifference;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is DownhillTrajectory2D o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_gravitationalAcceleration.StandardUnitValue, m_initialAngle.StandardUnitValue, m_initialVelocity.StandardUnitValue, m_verticalDifference.StandardUnitValue);
    public override string ToString()
      => $"{GetType().Name} {{ MaxHeight = {MaxHeight:N1} m, MaxRange = {MaxRange:N1} m, MaxTime = {MaxTime:N1} s }}";
    #endregion Object overrides
  }
}
