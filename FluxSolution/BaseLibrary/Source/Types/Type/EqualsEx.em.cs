namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Determines whether the source equals the target, taking into account generics.</summary>
    public static bool EqualsEx(this System.Type source, System.Type target)
      => source is null || target is null
      ? source is null && target is null
      : (source.IsGenericType ? source.GetGenericTypeDefinition() : source).Equals(target.IsGenericType ? target.GetGenericTypeDefinition() : target);

     }
}
