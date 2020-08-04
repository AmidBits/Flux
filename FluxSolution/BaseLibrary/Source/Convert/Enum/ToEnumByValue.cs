namespace Flux
{
  public static partial class Convert
  {
    /// <summary>Attempts to convert the source value type to an enum type (T).</summary>
    public static T ToEnumByValue<T>(this System.ValueType source) where T : System.Enum => (typeof(T).IsEnum && System.Enum.IsDefined(typeof(T), source)) ? (T)System.Enum.ToObject(typeof(T), source) : throw new System.Exception($"Could not convert ({source}) to enum {typeof(T).Name}.");
  }
}
