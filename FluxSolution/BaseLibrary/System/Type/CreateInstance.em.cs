using System.Reflection;
namespace Flux
{
  public static partial class Fx
  {
    public static object? CreateInstance(this System.Type source, params object[] args)
      => source.IsGenericType ? throw new System.TypeInitializationException(@"A generic type.", null) : System.Activator.CreateInstance(source, args);
    public static object? CreateInstance(this System.Type source)
      => source.IsGenericType ? throw new System.TypeInitializationException(@"A generic type.", null) : System.Activator.CreateInstance(source);
    public static object? CreateGenericInstance(this System.Type source, params System.Type[] genericTypeArguments)
      => source.IsGenericType ? System.Activator.CreateInstance(source.MakeGenericType(genericTypeArguments)) : throw new System.TypeInitializationException(@"Not a generic type.", null);

    public static object? CreateDefault(this System.Type type) => type.IsValueType ? System.Activator.CreateInstance(type) : null;

    private static readonly System.Collections.Generic.Dictionary<System.Type, System.Reflection.MethodInfo> m_getDefault = new();
    public static object? GetDefault(this System.Type type)
    {
      if (!m_getDefault.TryGetValue(type, out var methodInfo))
        methodInfo = m_getDefault[type] = m_getDefaultGeneric.MakeGenericMethod(type);

      return methodInfo.Invoke(null, null);
    }

    private static readonly System.Reflection.MethodInfo m_getDefaultGeneric = typeof(Fx).GetType().GetMethod(nameof(GetDefaultGeneric))!;
    public static T? GetDefaultGeneric<T>() => default;
  }
}
