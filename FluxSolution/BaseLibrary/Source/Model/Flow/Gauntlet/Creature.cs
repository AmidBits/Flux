namespace Flux.Model.Gaming.Gauntlet
{
  public enum CreatureType
  {
    Scorpion,
    Troll
  }

  public class Creature
    : GauntletObject
  {
    public CreatureType Type { get; set; }

    public Creature(CreatureType type)
      : base(type.ToString())
    {
      Type = type;
    }
  }
}
