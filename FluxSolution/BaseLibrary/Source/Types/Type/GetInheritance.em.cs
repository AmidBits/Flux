namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Creates a new sequence with the inheritance "chain" of the <paramref name="source"/> type, excluding the <paramref name="source"/> itself.</summary>
    public static System.Collections.Generic.IEnumerable<System.Type> GetInheritance(this System.Type source)
    {
      for (var type = source; type != null; type = type.BaseType)
        if (type != source)
          yield return type;
    }
  }
}
