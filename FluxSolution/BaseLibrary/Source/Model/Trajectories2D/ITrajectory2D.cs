namespace Flux.Model.Trajectories
{
  public interface ITrajectory2D
  {
    /// <summary>Gravitational acceleration.</summary>
    public Quantity.Acceleration GravitationalAcceleration { get; set; }
    /// <summary>Initial angle.</summary>
    public Quantity.Angle InitialAngle { get; set; }
    /// <summary>Initial velocity.</summary>
    public Quantity.Speed InitialVelocity { get; set; }

    /// <summary>Yields the greatest parabolic height an object reaches within the trajectory</summary>
    public double MaxHeight { get; }
    /// <summary>Yields the greatest distance the object travels along the x-axis.</summary>
    public double MaxRange { get; }
    /// <summary>Yields the greatest time the object travels before impact.</summary>
    public double MaxTime { get; }

    public double GetX(double time);
    public double GetY(double time);
    public double GetVelocityX(double time);
    public double GetVelocityY(double time);
    public double GetVelocity(double time);
  }
}
