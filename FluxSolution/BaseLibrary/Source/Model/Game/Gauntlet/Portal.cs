namespace Flux.Model.Gaming.Gauntlet
{
  /// <summary>This is the portal that players can send their adventurers through.</summary>
  public class Portal : GauntletObject
  {
    public bool IsOpen { get; set; }

    public Portal(string name)
      : base(name)
    {
    }
  }
}
