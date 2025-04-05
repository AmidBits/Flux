namespace Flux
{
  public static partial class Em
  {
    public static PlanetaryScience.KeplerianOrbit GetOrbitalEccentricityClass(this PlanetaryScience.OrbitalEccentricity source)
      => source.Value switch
      {
        0 => PlanetaryScience.KeplerianOrbit.CircularOrbit,
        > 0 and < 1 => PlanetaryScience.KeplerianOrbit.EllipticOrbit,
        1 => PlanetaryScience.KeplerianOrbit.ParabolicTrajectory,
        > 1 => PlanetaryScience.KeplerianOrbit.HyperbolicTrajectory,
        _ => throw new System.ArgumentOutOfRangeException(nameof(source)),
      };
  }
}
