namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Creates a new sequence with the inheritance "chain" of base types of the <paramref name="source"/> type, excluding the <paramref name="source"/> itself. This does not include interfaces.</summary>
    public static System.Collections.Generic.List<System.Type> GetInheritedBaseTypes(this System.Type source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var list = new System.Collections.Generic.List<System.Type>();

      for (var baseType = source.BaseType; baseType != null; baseType = baseType.BaseType)
        list.Add(baseType);

      //for (var type = source; type != null; type = type.BaseType)
      //  if (type != source)
      //    list.Add(type);

      return list;
    }
  }
}
