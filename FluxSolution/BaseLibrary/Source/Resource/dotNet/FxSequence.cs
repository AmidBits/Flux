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
      private readonly System.Collections.IEnumerable m_enumerable;

      public FxSequence(System.Collections.IEnumerable enumerable) => m_enumerable = enumerable;

      private System.Collections.Generic.IEnumerable<object[]> GetData(System.Collections.IEnumerable enumerable)
        => enumerable.Cast<object>().Select(o => o.GetPropertyInfos().Select(pi => pi.GetValue(o)!).ToArray());

      #region Implemented interfaces

      public string[] FieldNames => GetData(m_enumerable).First().GetPropertyInfos().Select(pi => pi.Name).ToArray();
      public System.Type[] FieldTypes => GetData(m_enumerable).First().GetPropertyInfos().Select(pi => pi.PropertyType).ToArray();

      public System.Collections.Generic.IEnumerable<object[]> GetFieldValues() => GetData(m_enumerable);

      #endregion // Implemented interfaces
    }
  }
}