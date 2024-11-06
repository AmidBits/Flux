namespace Flux.Mechanics
{
  /// <summary>
  /// <para>A dropped trajectory model.</para>
  /// <para>A projectile dropped from a moving system.</para>
  /// </summary>
  public readonly record struct DroppedTrajectory2D
    : ITrajectory2D
  {
    private readonly double m_droppedHeight;
    private readonly double m_gravitationalAcceleration;
    private readonly double m_initialAngle;
    private readonly double m_initialVelocity;

    public DroppedTrajectory2D(double droppedHeight, double initialAngle, double initialVelocity, double gravitationalAcceleration)
    {
      m_droppedHeight = droppedHeight;
      m_initialAngle = initialAngle;
      m_initialVelocity = initialVelocity;
      m_gravitationalAcceleration = gravitationalAcceleration;
    }
    public DroppedTrajectory2D(double droppedHeight, double initialAngle, double initialVelocity) : this(droppedHeight, initialAngle, initialVelocity, Quantities.Acceleration.StandardGravity) { }

    /// <summary>
    /// <para>The height when dropped.</para>
    /// </summary>
    public double DroppedHeight => m_droppedHeight;
    public double InitialAngle => m_initialAngle;
    public double InitialVelocity => m_initialVelocity;
    public double GravitationalAcceleration => m_gravitationalAcceleration;

    public double MaxHeight => m_droppedHeight;
    public double MaxRange => m_initialVelocity * MaxTime;
    public double MaxTime => double.Sqrt(2 * MaxHeight / m_gravitationalAcceleration);

    public double GetHeight(double time) => m_gravitationalAcceleration * double.Pow(time, 2) / 2;
    public double GetVelocity(double time) => double.Sqrt(double.Pow(m_initialVelocity, 2) + double.Pow(m_gravitationalAcceleration, 2) * time * time);
    public double GetVelocityX(double time) => m_initialVelocity * time;
    public double GetVelocityY(double time) => -m_gravitationalAcceleration * time;
    public double GetX(double time) => m_initialVelocity * time;
    public double GetY(double time) => GetHeight(time) - m_gravitationalAcceleration * time * time / 2;
  }
}
