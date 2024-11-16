//namespace Flux.Mechanics
//{
//  /// <summary>
//  /// <para>An uphill trajectory model.</para>
//  /// </summary>
//  public readonly record struct UphillTrajectory2D
//    : ITrajectory2D
//  {
//    private readonly double m_gravitationalAcceleration;
//    private readonly double m_initialAngle;
//    private readonly double m_initialVelocity;
//    private readonly double m_verticalDifference;

//    public UphillTrajectory2D(double verticalDifference, double initialAngle, double initialVelocity, double gravitationalAcceleration)
//    {
//      m_verticalDifference = verticalDifference;
//      m_initialAngle = initialAngle;
//      m_initialVelocity = initialVelocity;
//      m_gravitationalAcceleration = gravitationalAcceleration;
//    }
//    public UphillTrajectory2D(double verticalDifference, double initialAngle, double initialVelocity) : this(verticalDifference, initialAngle, initialVelocity, Quantities.Acceleration.StandardGravity) { }

//    /// <summary>
//    /// <para>The difference of vertical level in meters (M).</para>
//    /// </summary>
//    public double VerticalDifference => m_verticalDifference;
//    public double InitialAngle => m_initialAngle;
//    public double InitialVelocity => m_initialVelocity;
//    public double GravitationalAcceleration => m_gravitationalAcceleration;

//    public double MaxHeight => double.Pow(m_initialVelocity, 2) * double.Pow(double.Sin(m_initialAngle), 2) / (2 * m_gravitationalAcceleration);
//    public double MaxRange => m_initialVelocity * double.Cos(m_initialAngle) * MaxTime;
//    public double MaxTime => m_initialVelocity * double.Sin(m_initialAngle) / m_gravitationalAcceleration + double.Sqrt(2 * (MaxHeight - m_verticalDifference) / m_gravitationalAcceleration);

//    public double GetVelocity(double time) => m_initialVelocity * m_initialVelocity - 2 * m_gravitationalAcceleration * time * m_initialVelocity * double.Sin(m_initialAngle) + double.Pow(m_gravitationalAcceleration, 2) * time * time;
//    public double GetVelocityX(double time) => m_initialVelocity * double.Cos(m_initialAngle) * time;
//    public double GetVelocityY(double time) => m_initialVelocity * double.Sin(m_initialAngle) - m_gravitationalAcceleration * time;
//    public double GetX(double time) => m_initialVelocity * double.Cos(m_initialAngle) * time;
//    public double GetY(double time) => m_initialVelocity * double.Sin(m_initialAngle) * time - m_gravitationalAcceleration * time * time / 2;
//  }
//}
