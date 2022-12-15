namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Returns a generic type "expanded" into generic type definitions.</summary>
    public static (System.Type original, System.Type alternate) ToTypeDefinition(this System.Type source)
      => (source, source.IsGenericType ? source.GetGenericTypeDefinition() : source);

    /// <summary>Creates a new sequence with any generic types "expanded" into generic type definitions.</summary>
    public static System.Collections.Generic.IEnumerable<(System.Type original, System.Type alternate)> ToTypeDefinitions(this System.Collections.Generic.IEnumerable<System.Type> source)
      => source.Select(t => t.ToTypeDefinition());
  }
}
