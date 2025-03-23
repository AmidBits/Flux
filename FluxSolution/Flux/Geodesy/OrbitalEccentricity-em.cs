namespace Flux
{
  public static partial class Em
  {
    public static Geodesy.OrbitalEccentricityClass GetOrbitalEccentricityClass(this Geodesy.OrbitalEccentricity source)
      => source.Value switch
      {
        0 => Geodesy.OrbitalEccentricityClass.CircularOrbit,
        > 0 and < 1 => Geodesy.OrbitalEccentricityClass.EllipticOrbit,
        1 => Geodesy.OrbitalEccentricityClass.ParabolicTrajectory,
        > 1 => Geodesy.OrbitalEccentricityClass.HyperbolicTrajectory,
        _ => throw new System.ArgumentOutOfRangeException(nameof(source)),
      };
  }
}
