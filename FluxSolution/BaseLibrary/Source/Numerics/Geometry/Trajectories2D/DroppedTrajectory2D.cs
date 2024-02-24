namespace Flux.Mechanics
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct DroppedTrajectory2D // Projectile dropped from a moving system.
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
    public DroppedTrajectory2D(double droppedHeight, double initialAngle, double initialVelocity) : this(droppedHeight, initialAngle, initialVelocity, 9.80665) { }

    // The height when dropped.
    public double DroppedHeight { get => m_droppedHeight; init => m_droppedHeight = value; }
    public double GravitationalAcceleration { get => m_gravitationalAcceleration; init => m_gravitationalAcceleration = value; }
    public double InitialAngle { get => m_initialAngle; init => m_initialAngle = value; }
    public double InitialVelocity { get => m_initialVelocity; init => m_initialVelocity = value; }

    public double MaxHeight => m_droppedHeight;
    public double MaxRange => m_initialVelocity * MaxTime;
    public double MaxTime => System.Math.Sqrt(2 * MaxHeight / m_gravitationalAcceleration);

    public double GetHeight(double time) => m_gravitationalAcceleration * System.Math.Pow(time, 2) / 2;
    public double GetVelocity(double time) => System.Math.Sqrt(System.Math.Pow(m_initialVelocity, 2) + System.Math.Pow(m_gravitationalAcceleration, 2) * time * time);
    public double GetVelocityX(double time) => m_initialVelocity * time;
    public double GetVelocityY(double time) => -m_gravitationalAcceleration * time;
    public double GetX(double time) => m_initialVelocity * time;
    public double GetY(double time) => GetHeight(time) - m_gravitationalAcceleration * time * time / 2;
  }
}
