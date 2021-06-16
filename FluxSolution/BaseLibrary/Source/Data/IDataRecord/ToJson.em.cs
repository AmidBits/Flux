using System.Linq;

namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Results in a string formatted as a JSON array from the column values in the current row.</summary>
    public static string GetJsonArray(this System.Data.IDataRecord source, System.Func<object, string> valueSelector)
      => string.Join(@",", GetValues(source).Select(valueSelector)).Wrap('[', ']');

    /// <summary>Results in a string formatted as a JSON object from the column values in the current row.</summary>
    public static string GetJsonObject(this System.Data.IDataRecord source, System.Func<string, string> nameSelector, System.Func<object, string> valueSelector)
      => string.Join(@",", GetNames(source).Zip(GetValues(source), (n, v) => nameSelector(n) + ':' + valueSelector(v))).Wrap('{', '}');
  }
}
