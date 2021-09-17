namespace Flux.Model.Gauntlet
{
  public enum HeroType
  {
    Elf,
    Valkyrie,
    Warrior,
    Wizard
  }

  /// <summary>This is a hero that a player controls.</summary>
  public class Hero
    : GauntletObject
  {
    public HeroType Type { get; set; }

    public Hero(HeroType type)
      : base(type switch
      {
        HeroType.Elf => @"Questor",
        HeroType.Valkyrie => @"Thyra",
        HeroType.Warrior => @"Thor",
        HeroType.Wizard => @"Merlin",
        _ => throw new System.ArgumentOutOfRangeException(nameof(type))
      })
    {
      Type = type;
    }
  }
}
