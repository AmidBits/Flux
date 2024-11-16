//namespace Flux.Mechanics
//{
//  /// <summary>
//  /// <para>A flat trajectory model.</para>
//  /// </summary>
//  public readonly record struct FlatTrajectory2D
//    : ITrajectory2D
//  {

//    private readonly double m_gravitationalAcceleration;
//    private readonly double m_initialAngle;
//    private readonly double m_initialVelocity;

//    public FlatTrajectory2D(double initialAngle, double initialVelocity, double gravitationalAcceleration)
//    {
//      m_initialAngle = initialAngle;
//      m_initialVelocity = initialVelocity;
//      m_gravitationalAcceleration = gravitationalAcceleration;
//    }
//    public FlatTrajectory2D(double initialAngle, double initialVelocity) : this(initialAngle, initialVelocity, Quantities.Acceleration.StandardGravity) { }

//    public double InitialAngle => m_initialAngle;
//    public double InitialVelocity => m_initialVelocity;
//    public double GravitationalAcceleration => m_gravitationalAcceleration;

//    public double MaxHeight => double.Pow(m_initialVelocity, 2) * double.Pow(double.Sin(m_initialAngle), 2) / (2 * m_gravitationalAcceleration);
//    public double MaxRange => m_initialVelocity * MaxTime * double.Cos(m_initialAngle);
//    public double MaxTime => 2 * m_initialVelocity * double.Sin(m_initialAngle) / m_gravitationalAcceleration;

//    public double GetVelocity(double time) => m_initialVelocity * m_initialVelocity - 2 * m_gravitationalAcceleration * time * m_initialVelocity * double.Sin(m_initialAngle) + double.Pow(m_gravitationalAcceleration, 2) * time * time;
//    public double GetVelocityX(double time) => m_initialVelocity * double.Cos(m_initialAngle) * time;
//    public double GetVelocityY(double time) => m_initialVelocity * double.Sin(m_initialAngle) - m_gravitationalAcceleration * time;
//    public double GetX(double time) => m_initialVelocity * double.Cos(m_initialAngle) * time;
//    public double GetY(double time) => m_initialVelocity * double.Sin(m_initialAngle) * time - m_gravitationalAcceleration * time * time / 2;
//  }
//}
