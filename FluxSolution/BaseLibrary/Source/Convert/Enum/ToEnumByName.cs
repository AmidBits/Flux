namespace Flux
{
  public static partial class Convert
  {
    /// <summary>Attempts to convert the source name string to an enum type (T).</summary>
    public static T ToEnumByName<T>(this string source) where T : struct => (typeof(T).IsEnum && System.Enum.TryParse(source, out T result)) ? result : throw new System.Exception($"Could not convert \"{source}\" to enum {typeof(T).Name}.");
  }
}
