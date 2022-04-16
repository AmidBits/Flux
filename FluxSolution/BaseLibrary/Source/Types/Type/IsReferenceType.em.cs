namespace Flux
{
  public static partial class TypeEm
  {
    /// <summary>Determines whether the <paramref name="source"/> type is a reference type.</summary>
    public static bool IsReferenceType(this System.Type source)
      => GetDefaultValue(source) is null && !IsSystemNullable(source);
  }
}
