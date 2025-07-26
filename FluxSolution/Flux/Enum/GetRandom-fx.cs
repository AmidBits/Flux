namespace Flux
{
  public static partial class Enums
  {
    public static T GetRandom<T>(System.Random? rng = null)
      where T : struct, Enum
    {
      rng ??= System.Enum.Shared;
        
      T[]? v = System.Enum.GetValues<T>();
        
      return (T)v.GetValue(rng.Next(v.Length));
    }    
  }
}
