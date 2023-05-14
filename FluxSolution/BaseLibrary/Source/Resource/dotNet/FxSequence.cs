namespace Flux
{
  public static partial class ResourcesExtensionMethods
  {
    public static System.Data.DataTable ToDataTable(this System.Collections.IEnumerable source, System.Func<string> tableNameSelector)
      => new Flux.Resources.DotNet.FxSequence(source).AcquireDataTable(tableNameSelector());
  }

  namespace Resources.DotNet
  {
    /// <summary>.NET time zones.</summary>
    public sealed class FxSequence
    : ITabularDataAcquirable
    {
      private readonly System.Collections.IEnumerable m_collection;

      public FxSequence(System.Collections.IEnumerable collection) => m_collection = collection;

      private System.Collections.Generic.IEnumerable<object> GetCollection()
      {
        foreach (var item in m_collection)
          yield return item;
      }

      public string[] FieldNames => GetCollection().First().GetPropertyInfos().Select(pi => pi.Name).ToArray();

      public System.Type[] FieldTypes => GetCollection().First().GetPropertyInfos().Select(pi => pi.PropertyType).ToArray();

      public System.Collections.Generic.IEnumerable<object[]> GetFieldValues() => GetCollection().Select(e => e.GetPropertyInfos().Select(pi => pi.GetValue(e)!).ToArray());
    }
  }
}