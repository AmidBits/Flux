namespace Flux.Mechanics
{
  public interface ITrajectory2D
  {
    /// <summary>Gravitational acceleration.</summary>
    Acceleration GravitationalAcceleration { get; set; }
    /// <summary>Initial angle.</summary>
    Angle InitialAngle { get; set; }
    /// <summary>Initial velocity.</summary>
    Speed InitialVelocity { get; set; }

    /// <summary>Yields the greatest parabolic height an object reaches within the trajectory</summary>
    double MaxHeight { get; }
    /// <summary>Yields the greatest distance the object travels along the x-axis.</summary>
    double MaxRange { get; }
    /// <summary>Yields the greatest time the object travels before impact.</summary>
    double MaxTime { get; }

    double GetVelocity(double time);
    double GetVelocityX(double time);
    double GetVelocityY(double time);
    double GetX(double time);
    double GetY(double time);
  }
}
