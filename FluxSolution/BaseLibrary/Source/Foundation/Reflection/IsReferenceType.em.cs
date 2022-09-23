namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Determines whether the <paramref name="source"/> type is a reference type.</summary>
    public static bool IsReferenceType(this System.Type source)
      => GetDefaultValue(source) is null && !IsSystemNullable(source);
  }
}
