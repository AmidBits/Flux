namespace Flux.Resources.DotNet
{
  /// <summary>.NET time zones.</summary>
  public sealed class FxSequence
      : ITabularDataAcquirable
  {
    private readonly System.Collections.IEnumerable m_enumerable;

    public FxSequence(System.Collections.IEnumerable enumerable) => m_enumerable = enumerable;

    public static System.Collections.Generic.IEnumerable<object[]> GetData(System.Collections.IEnumerable enumerable)
      => enumerable.Cast<object>().Select(o => Reflection.GetPropertyInfos(o).Select(pi => pi.GetValue(o)!).ToArray());

    #region Implemented interfaces

    public string[] FieldNames => Reflection.GetPropertyInfos(GetData(m_enumerable).First()).Select(pi => pi.Name).ToArray();
    public System.Type[] FieldTypes => Reflection.GetPropertyInfos(GetData(m_enumerable).First()).Select(pi => pi.PropertyType).ToArray();

    public System.Collections.Generic.IEnumerable<object[]> GetFieldValues() => GetData(m_enumerable);

    #endregion // Implemented interfaces
  }
}
