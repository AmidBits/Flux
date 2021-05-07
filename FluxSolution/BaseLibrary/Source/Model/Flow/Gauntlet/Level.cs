namespace Flux.Model.Gaming.Gauntlet
{
  /// <summary>This is the hive where monsters spawn from.</summary>
  public class Level : GauntletObject
  {
    public System.Collections.Generic.List<Hero> Heros { get; private set; }
    public System.Collections.Generic.List<CreatureHive> Hives { get; private set; }
    public System.Collections.Generic.List<Portal> Portals { get; private set; }

    public Level()
      : base(nameof(Level))
    {
      Heros = new System.Collections.Generic.List<Hero>();
      Hives = new System.Collections.Generic.List<CreatureHive>();
      Portals = new System.Collections.Generic.List<Portal>();
    }

    public override void Update(float deltaTime)
    {
      foreach (var hero in Heros)
      {
        if (!hero.UpdateDisabled)
        {
          hero.Update(deltaTime);
        }
      }

      foreach (var hive in Hives)
      {
        if (!hive.UpdateDisabled)
        {
          hive.Update(deltaTime);
        }
      }

      foreach (var portal in Portals)
      {
        if (!portal.UpdateDisabled)
        {
          portal.Update(deltaTime);
        }
      }
    }
  }
}
