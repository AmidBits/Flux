namespace Flux
{
  public static partial class Fx
  {
    public static System.Collections.Generic.IDictionary<System.Reflection.MemberInfo, object?> GetPropertyDictionary<T>(this T source, System.Reflection.BindingFlags bindingFlags = System.Reflection.BindingFlags.FlattenHierarchy | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
    {
      var properties = new System.Collections.Generic.Dictionary<System.Reflection.MemberInfo, object?>();

      var type = source is null ? typeof(T) : source is System.Type st ? st : source.GetType();

      var pis = type.GetProperties(bindingFlags);

      foreach (var pi in pis)
      {
        var value = pi.GetValue(pi.GetMethod?.IsStatic ?? false ? null : source);

        properties.Add(pi, value);
      }

      return properties;
    }
  }
}
