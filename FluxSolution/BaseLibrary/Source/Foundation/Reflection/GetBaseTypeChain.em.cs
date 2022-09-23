namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Creates a new sequence with the inheritance type chain of the <paramref name="source"/>.</summary>
    public static System.Collections.Generic.IEnumerable<System.Type> GetBaseTypeChain(this System.Type source)
    {
      for (var type = source; type != null; type = type.BaseType)
        yield return type;
    }
  }
}
