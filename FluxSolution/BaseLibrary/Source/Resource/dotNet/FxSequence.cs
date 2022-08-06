namespace Flux.Resources.DotNet
{
  /// <summary>.NET time zones.</summary>
  public sealed class FxSequence
    : ITabularDataAcquirable
  {
    private System.Collections.IEnumerable m_sequence;
    public FxSequence(System.Collections.IEnumerable sequence)
      => m_sequence = sequence;

    public string[] FieldNames
      => m_sequence.GetType().GenericTypeArguments[0].GetProperties().Select(pi => pi.Name).ToArray();
    public System.Type[] FieldTypes
      => m_sequence.GetType().GenericTypeArguments[0].GetProperties().Select(pi => pi.PropertyType).ToArray();

    public System.Collections.Generic.IEnumerable<object[]> GetFieldValues()
    {
      foreach (var item in m_sequence)
        yield return Flux.Reflection.GetPropertyInfos(item).Select(pi => pi.GetValue(item)!).ToArray();
    }
  }
}
