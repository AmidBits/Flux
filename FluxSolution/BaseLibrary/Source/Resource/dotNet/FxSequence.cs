namespace Flux.Resources.DotNet
{
  /// <summary>.NET time zones.</summary>
  public sealed class FxSequence
    : ITabularDataAcquirable
  {
    private readonly System.Collections.IEnumerable m_sequence;
    public FxSequence(System.Collections.IEnumerable sequence)
      => m_sequence = sequence;

    private object GetFirstItemOnly() => m_sequence.GetEnumerator() is var e && e.MoveNext() ? e.Current : throw new System.NotSupportedException();

    public string[] FieldNames => GetFirstItemOnly().GetPropertyInfos().Select(pi => pi.Name).ToArray();

    public System.Type[] FieldTypes => GetFirstItemOnly().GetPropertyInfos().Select(pi => pi.PropertyType).ToArray();

    public System.Collections.Generic.IEnumerable<object[]> GetFieldValues()
    {
      foreach (var item in m_sequence)
        yield return item.GetPropertyInfos().Select(pi => pi.GetValue(item)!).ToArray();
    }
  }
}
