namespace Flux.Model.Gaming.Gauntlet
{
  /// <summary>This is a hive where creatures spawn from.</summary>
  public class CreatureHive
    : GauntletObject
  {
    public int HiveSize { get; set; }
    public CreatureType HiveType { get; set; }

    public System.TimeSpan SpawnInterval { get; set; }
    public System.Collections.Generic.List<Creature> SpawnPopulation { get; private set; }
    public float SpawnRadius { get; set; }

    public CreatureHive(CreatureType type, int size)
      : base(type.ToString())
    {
      HiveSize = size;
      HiveType = type;

      SpawnInterval = new System.TimeSpan(0, 0, size * 3);
      SpawnPopulation = new System.Collections.Generic.List<Creature>(size);
      SpawnRadius = size * 2;
    }
  }
}
