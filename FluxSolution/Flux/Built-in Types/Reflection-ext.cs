namespace Flux
{
  public static class ReflectionExtensions
  {
    /// <summary>Get the current method name without using reflection.</summary>
    /// <remarks>Using reflection System.Reflection.MethodInfo.GetCurrentMethod() also works.</remarks>
    public static string GetMethodName([System.Runtime.CompilerServices.CallerMemberName] string caller = null!)
      => caller;

    extension(System.Reflection.FieldInfo)
    {
      #region GetFieldInfos

      /// <summary>Enumerates all FieldInfo matching the specified binding flags. If the source is a System.Type, the fields are enumerated from the type, otherwise the instance is used.</summary>
      /// <param name="source">An instance object or a System.Type to enumerate fields on.</param>
      /// <param name="bindingFlags"></param>
      public static System.Reflection.FieldInfo[] GetFieldInfos<T>(T source, System.Reflection.BindingFlags bindingFlags = System.Reflection.BindingFlags.FlattenHierarchy | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
        => source is null
        ? throw new System.ArgumentNullException(nameof(source))
        : source is System.Type type
        ? type.GetFields(bindingFlags | System.Reflection.BindingFlags.Static)
        : source.GetType().GetFields(bindingFlags | System.Reflection.BindingFlags.Instance);

      #endregion
    }

    extension(System.Reflection.PropertyInfo)
    {
      #region GetPropertyInfos

      /// <summary>Enumerates all PropertyInfo matching the specified binding flags. If the source is a System.Type, the properties are enumerated from the type, otherwise the instance is used.</summary>
      /// <param name="source">An instance object or a System.Type to enumerate properties on.</param>
      /// <param name="bindingFlags"></param>
      public static System.Reflection.PropertyInfo[] GetPropertyInfos<T>(T source, System.Reflection.BindingFlags bindingFlags = System.Reflection.BindingFlags.FlattenHierarchy | System.Reflection.BindingFlags.Public)
        => source is null
        ? throw new System.ArgumentNullException(nameof(source))
        : source is System.Type type
        ? type.GetProperties(bindingFlags | System.Reflection.BindingFlags.Static) // Static property containers.
        : source.GetType().GetProperties(bindingFlags | System.Reflection.BindingFlags.Instance); // Instance property containers.

      #endregion
    }
  }
}
