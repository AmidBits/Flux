namespace Flux
{
  public static partial class FxReflection
  {
    /// <summary>
    /// <para>Determines whether <paramref name="source"/> is assignable to the <paramref name="genericType"/>.</para>
    /// <see href="https://stackoverflow.com/a/1075059/3178666"/>
    /// </summary>
    // <example>var isUnsignedNumber = typeof(int).IsAssignableToGenericType(typeof(System.Numerics.IUnsignedNumber<>));</example>
    public static bool IsAssignableToGenericType(this System.Type source, System.Type genericType)
      => source.GetInterfaces().Prepend(source).Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == genericType) || (source.BaseType?.IsAssignableToGenericType(genericType) ?? false);
  }
}
