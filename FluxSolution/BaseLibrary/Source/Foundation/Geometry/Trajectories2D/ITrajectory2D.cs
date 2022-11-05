namespace Flux.Mechanics
{
  public interface ITrajectory2D
  {
    /// <summary>Gravitational acceleration.</summary>
    Acceleration GravitationalAcceleration { get; init; }
    /// <summary>Initial angle.</summary>
    Angle InitialAngle { get; init; }
    /// <summary>Initial velocity.</summary>
    LinearVelocity InitialVelocity { get; init; }

    /// <summary>Yields the greatest parabolic height an object reaches within the trajectory</summary>
    Length MaxHeight { get; }
    /// <summary>Yields the greatest distance the object travels along the x-axis.</summary>
    Length MaxRange { get; }
    /// <summary>Yields the greatest time the object travels before impact.</summary>
    Time MaxTime { get; }

    LinearVelocity GetVelocity(Time time);
    LinearVelocity GetVelocityX(Time time);
    LinearVelocity GetVelocityY(Time time);
    Length GetX(Time time);
    Length GetY(Time time);
  }
}
