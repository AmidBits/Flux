namespace Flux.Mechanics
{
  public struct DownhillTrajectory2D
    : System.IEquatable<DownhillTrajectory2D>, ITrajectory2D
  {
    private Acceleration m_gravitationalAcceleration;
    private Angle m_initialAngle;
    private Speed m_initialVelocity;
    private Length m_verticalDifference;

    public DownhillTrajectory2D(Length verticalDifference, Angle initialAngle, Speed initialVelocity, Acceleration gravitationalAcceleration)
    {
      m_verticalDifference = verticalDifference;
      m_initialAngle = initialAngle;
      m_initialVelocity = initialVelocity;
      m_gravitationalAcceleration = gravitationalAcceleration;
    }
    public DownhillTrajectory2D(Length verticalDifference, Angle initialAngle, Speed initialVelocity)
      : this(verticalDifference, initialAngle, initialVelocity, Acceleration.StandardAccelerationOfGravity)
    { }

    /// <summary>Gravitational acceleration in meters per second square (M/S²).</summary>
    public Acceleration GravitationalAcceleration { get => m_gravitationalAcceleration; set => m_gravitationalAcceleration = value; }
    /// <summary>Initial angle in radians (RAD).</summary>
    public Angle InitialAngle { get => m_initialAngle; set => m_initialAngle = value; }
    /// <summary>Initial velocity in meters per second (M/S).</summary>
    public Speed InitialVelocity { get => m_initialVelocity; set => m_initialVelocity = value; }
    /// <summary>The difference of vertical level in meters (M).</summary>
    public Length VerticalDifference { get => m_verticalDifference; set => m_verticalDifference = value; }

    public double MaxHeight
      => m_verticalDifference.Value + System.Math.Pow(m_initialVelocity.Value, 2) * System.Math.Pow(System.Math.Sin(m_initialAngle.Value), 2) / (2 * m_gravitationalAcceleration.Value);
    public double MaxRange
      => m_initialVelocity.Value * System.Math.Cos(m_initialAngle.Value) * MaxTime;
    public double MaxTime
      => m_initialVelocity.Value * System.Math.Sin(m_initialAngle.Value) / m_gravitationalAcceleration.Value + System.Math.Sqrt(2 * MaxHeight / m_gravitationalAcceleration.Value);

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
      => System.HashCode.Combine(m_gravitationalAcceleration.Value, m_initialAngle.Value, m_initialVelocity.Value, m_verticalDifference.Value);
    public override string ToString()
      => $"{GetType().Name} {{ MaxHeight = {MaxHeight:N1} m, MaxRange = {MaxRange:N1} m, MaxTime = {MaxTime:N1} s }}";
    #endregion Object overrides
  }
}
