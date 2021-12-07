namespace Flux.Mechanics
{
  public struct UphillTrajectory2D
    : System.IEquatable<UphillTrajectory2D>, ITrajectory2D
  {
    private Quantity.Acceleration m_gravitationalAcceleration;
    private Quantity.Angle m_initialAngle;
    private Quantity.Speed m_initialVelocity;
    private Quantity.Length m_verticalDifference;

    public UphillTrajectory2D(Quantity.Length verticalDifference, Quantity.Angle initialAngle, Quantity.Speed initialVelocity, Quantity.Acceleration gravitationalAcceleration)
    {
      m_verticalDifference = verticalDifference;
      m_initialAngle = initialAngle;
      m_initialVelocity = initialVelocity;
      m_gravitationalAcceleration = gravitationalAcceleration;
    }
    public UphillTrajectory2D(Quantity.Length verticalDifference, Quantity.Angle initialAngle, Quantity.Speed initialVelocity)
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
      => System.Math.Pow(m_initialVelocity.DefaultUnitValue, 2) * System.Math.Pow(System.Math.Sin(m_initialAngle.DefaultUnitValue), 2) / (2 * m_gravitationalAcceleration.DefaultUnitValue);
    public double MaxRange
      => m_initialVelocity.DefaultUnitValue * System.Math.Cos(m_initialAngle.DefaultUnitValue) * MaxTime;
    public double MaxTime
      => m_initialVelocity.DefaultUnitValue * System.Math.Sin(m_initialAngle.DefaultUnitValue) / m_gravitationalAcceleration.DefaultUnitValue + System.Math.Sqrt(2 * (MaxHeight - m_verticalDifference.DefaultUnitValue) / m_gravitationalAcceleration.DefaultUnitValue);

    public double GetVelocity(double time)
      => m_initialVelocity.DefaultUnitValue * m_initialVelocity.DefaultUnitValue - 2 * m_gravitationalAcceleration.DefaultUnitValue * time * m_initialVelocity.DefaultUnitValue * System.Math.Sin(m_initialAngle.DefaultUnitValue) + System.Math.Pow(m_gravitationalAcceleration.DefaultUnitValue, 2) * time * time;
    public double GetVelocityX(double time)
      => m_initialVelocity.DefaultUnitValue * System.Math.Cos(m_initialAngle.DefaultUnitValue);
    public double GetVelocityY(double time)
      => m_initialVelocity.DefaultUnitValue * System.Math.Sin(m_initialAngle.DefaultUnitValue) - m_gravitationalAcceleration.DefaultUnitValue * time;
    public double GetX(double time)
      => m_initialVelocity.DefaultUnitValue * System.Math.Cos(m_initialAngle.DefaultUnitValue) * time;
    public double GetY(double time)
      => m_initialVelocity.DefaultUnitValue * System.Math.Sin(m_initialAngle.DefaultUnitValue) * time - m_gravitationalAcceleration.DefaultUnitValue * time * time / 2;

    #region Overloaded operators
    public static bool operator ==(UphillTrajectory2D h1, UphillTrajectory2D h2)
      => h1.Equals(h2);
    public static bool operator !=(UphillTrajectory2D h1, UphillTrajectory2D h2)
      => !h1.Equals(h2);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals(UphillTrajectory2D other)
      => m_gravitationalAcceleration == other.m_gravitationalAcceleration && m_initialAngle == other.m_initialAngle && m_initialVelocity == other.m_initialVelocity && m_verticalDifference == other.m_verticalDifference;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is UphillTrajectory2D o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_gravitationalAcceleration.DefaultUnitValue, m_initialAngle.DefaultUnitValue, m_initialVelocity.DefaultUnitValue, m_verticalDifference.DefaultUnitValue);
    public override string ToString()
      => $"{GetType().Name} {{ MaxHeight = {MaxHeight:N1} m, MaxRange = {MaxRange:N1} m, MaxTime = {MaxTime:N1} s }}";
    #endregion Object overrides
  }
}
