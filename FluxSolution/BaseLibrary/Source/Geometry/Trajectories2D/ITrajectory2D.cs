namespace Flux.Mechanics
{
  public interface ITrajectory2D
  {
    /// <summary>Gravitational acceleration.</summary>
    Quantities.Acceleration GravitationalAcceleration { get; init; }
    /// <summary>Initial angle.</summary>
    Quantities.Angle InitialAngle { get; init; }
    /// <summary>Initial velocity.</summary>
    Quantities.LinearVelocity InitialVelocity { get; init; }

    /// <summary>Yields the greatest parabolic height an object reaches within the trajectory</summary>
    Quantities.Length MaxHeight { get; }
    /// <summary>Yields the greatest distance the object travels along the x-axis.</summary>
    Quantities.Length MaxRange { get; }
    /// <summary>Yields the greatest time the object travels before impact.</summary>
    Quantities.Time MaxTime { get; }

    Quantities.LinearVelocity GetVelocity(Quantities.Time time);
    Quantities.LinearVelocity GetVelocityX(Quantities.Time time);
    Quantities.LinearVelocity GetVelocityY(Quantities.Time time);
    Quantities.Length GetX(Quantities.Time time);
    Quantities.Length GetY(Quantities.Time time);
  }
}
