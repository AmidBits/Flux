namespace Flux.AmbOperator
{
  public sealed class Choices<T>(params T[] values)
    : IChoices, IValue<T>
  {
    public T Value => values[Index];

    #region Implemented interfaces

    // IChoices
    public int Index { get; set; }

    public int Length => values.Length;

    #endregion Implemented interfaces

    public override string ToString() => Value?.ToString() ?? base.ToString() ?? string.Empty;
  }

  internal class ChoiceArray<T>(params T[] values)
    : IChoices, IValue<T>
  {
    #region IChoices<>

    public int Index { get; set; }

    public int Length => values.Length;

    #endregion

    #region IValue<>

    public T Value => values[Index];

    #endregion

    public override string ToString() => Value?.ToString() ?? "[null]";
  }
}
