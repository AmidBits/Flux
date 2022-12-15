namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Creates a new sequence with interfaces that the <paramref name="source"/> type implements, excluding <paramref name="source"/> itself.</summary>
    public static System.Collections.Generic.IEnumerable<System.Type> GetImplements(this System.Type source)
    {
      var (_, sourceType) = source.ToTypeDefinition();

      return source.GetInterfaces().ToTypeDefinitions().Where(target => target.alternate != sourceType).Select(target => target.original);
    }
  }
}
