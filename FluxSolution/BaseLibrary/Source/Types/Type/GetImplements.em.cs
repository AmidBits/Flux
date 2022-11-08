namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Creates a new sequence with interfaces that the <paramref name="source"/> type implements, excluding <paramref name="source"/> itself.</summary>
    public static System.Collections.Generic.IEnumerable<System.Type> GetImplements(this System.Type source)
      => source.GetInterfaces().Where(type => type != source);
  }
}
