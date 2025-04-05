namespace Flux
{
  public static partial class FxType
  {
    /// <summary>Determines whether the type is System.Nullable<T>.</summary>
    /// <remark>Should be able to alternatively use: (System.Nullable.GetUnderlyingType(typeof(T)) != null)</remark>
    public static bool IsSystemNullable(this System.Type source)
      => source is not null && source.IsGenericType && source.GetGenericTypeDefinition() == typeof(System.Nullable<>);
  }
}
