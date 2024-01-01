namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Determines whether the <paramref name="source"/> type is a reference type.</summary>
    public static bool IsReferenceType(this System.Type source)
      => source.GetDefaultValue() is null && !IsSystemNullable(source);
  }
}
