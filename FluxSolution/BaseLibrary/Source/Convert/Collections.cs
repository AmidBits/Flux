using System.Linq;

namespace Flux
{
  /// <summary></summary>
  public static partial class Xtensions
  {
    public static System.Collections.Generic.IEnumerable<object[]> ToTypedObjects(this System.Collections.Generic.IEnumerable<string[]> source, System.Func<string, int, object> typeConverter)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (typeConverter is null) throw new System.ArgumentNullException(nameof(typeConverter));

      var lineCount = 0;

      foreach (var sl in source)
      {
        lineCount++;

        var ol = new object[sl.Length];
        for (var index = 0; index < sl.Length; index++)
          ol[index] = typeConverter(sl[index], index);
        yield return ol;
      }
    }
    public static System.Collections.Generic.IEnumerable<object[]> ToTypedObjects(this System.Collections.Generic.IEnumerable<string[]> source, System.Collections.Generic.IList<System.Type> types)
    {
      if (types is null) throw new System.ArgumentNullException(nameof(types));
      if (types.Count == 0) throw new System.ArgumentOutOfRangeException(nameof(types));

      return ToTypedObjects(source, (string value, int index) =>
      {
        if (!(types is null) && index >= 0 && index < types.Count && types[index] != typeof(string)) // Are we converting?
          return string.IsNullOrEmpty(value) ? Reflection.Helper.GetDefaultValue(types[index]) ?? System.DBNull.Value : Convert.ChangeType(value, null, types[index]); // Either convert the value to the specified field type, or if the value is empty (or externally null) then return the default for the specified type.

        return value; // Either we have no field type, or the field type is a string, so we simply return the value, as is.
      });
    }
  }
}
