namespace Flux.Model.Gaming.Gauntlet
{
  /// <summary>This is a hive where monsters spawn from.</summary>
  public class Hive : GauntletObject
  {
    public int MaxSize { get; set; }

    public System.Collections.Generic.List<Creature> Population { get; private set; }

    public System.TimeSpan SpawnInterval { get; set; }
    public float SpawnTriggerRadius { get; set; }

    public int Type { get; set; }

    public Hive()
      : base(nameof(Hive))
    {
      MaxSize = 25;

      Population = new System.Collections.Generic.List<Creature>();

      SpawnInterval = new System.TimeSpan(0, 0, 5);
      SpawnTriggerRadius = 10;

      Type = 1000;
    }
  }
}
