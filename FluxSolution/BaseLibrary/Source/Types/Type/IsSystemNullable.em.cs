namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Determines whether the type is System.Nullable<T>.</summary>
    /// <remark>Should be able to alternatively use: (System.Nullable.GetUnderlyingType(typeof(T)) != null)</remark>
    public static bool IsSystemNullable(this System.Type source)
      => source is null
      ? throw new System.ArgumentNullException(nameof(source))
      : source.IsGenericType && source.GetGenericTypeDefinition() == typeof(System.Nullable<>);
  }
}
