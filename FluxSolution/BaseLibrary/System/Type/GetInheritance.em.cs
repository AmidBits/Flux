namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Creates a new sequence with the inheritance "chain" of the <paramref name="source"/> type, excluding the <paramref name="source"/> itself.</summary>
    public static System.Collections.Generic.List<System.Type> GetInheritance(this System.Type source)
    {
      var list = new System.Collections.Generic.List<System.Type>();

      for (var type = source; type != null; type = type.BaseType)
        if (type != source)
          list.Add(type);

      return list;
    }
  }
}
