namespace Flux.Model.Gaming.Gauntlet
{
  /// <summary>This is a hive where creatures spawn from.</summary>
  public class CreatureHive
    : GauntletObject
  {
    public int HiveSize { get; set; }

    public System.TimeSpan SpawnInterval { get; set; }
    public float SpawnTriggerRadius { get; set; }

    public System.Collections.Generic.List<Creature> CreaturePopulation { get; private set; }
    public Creature.CreatureType CreatureType { get; set; }

    public CreatureHive(Creature.CreatureType type, int size)
      : base(type.ToString())
    {
      HiveSize = size;

      SpawnInterval = new System.TimeSpan(0, 0, size * 3);
      SpawnTriggerRadius = size * 2;

      CreaturePopulation = new System.Collections.Generic.List<Creature>(size);
      CreatureType = type;
    }
  }
}
