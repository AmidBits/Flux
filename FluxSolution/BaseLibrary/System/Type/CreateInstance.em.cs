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
  }
}
