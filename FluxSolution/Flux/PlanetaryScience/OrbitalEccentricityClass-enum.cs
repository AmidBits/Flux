namespace Flux.PlanetaryScience
{
  /// <summary>
  /// <para><see href="https://en.wikipedia.org/wiki/Orbital_eccentricity"/></para>
  /// </summary>
  public enum KeplerianOrbit
  {
    /// <summary>
    /// <para>e = 0</para>
    /// </summary>
    CircularOrbit,
    /// <summary>
    /// <para>0 &lt; e &lt; 1</para>
    /// </summary>
    EllipticOrbit,
    /// <summary>
    /// <para>e = 1</para>
    /// </summary>
    ParabolicTrajectory,
    /// <summary>
    /// <para>e > 1</para>
    /// </summary>
    HyperbolicTrajectory
  }
}
