namespace Flux.Mechanics
{
  public interface ITrajectory2D
  {
    /// <summary>Gravitational acceleration.</summary>
    Units.Acceleration GravitationalAcceleration { get; init; }
    /// <summary>Initial angle.</summary>
    Units.Angle InitialAngle { get; init; }
    /// <summary>Initial velocity.</summary>
    Units.LinearVelocity InitialVelocity { get; init; }

    /// <summary>Yields the greatest parabolic height an object reaches within the trajectory</summary>
    Units.Length MaxHeight { get; }
    /// <summary>Yields the greatest distance the object travels along the x-axis.</summary>
    Units.Length MaxRange { get; }
    /// <summary>Yields the greatest time the object travels before impact.</summary>
    Units.Time MaxTime { get; }

    Units.LinearVelocity GetVelocity(Units.Time time);
    Units.LinearVelocity GetVelocityX(Units.Time time);
    Units.LinearVelocity GetVelocityY(Units.Time time);
    Units.Length GetX(Units.Time time);
    Units.Length GetY(Units.Time time);
  }
}
