namespace Flux.AmbOps
{
  public sealed class Choices<T>
     : IChoices, IValue<T>
  {
    private readonly T[] Values;

    public Choices(params T[] values) => Values = values;

    public T Value => Values[Index];

    #region Implemented interfaces

    // IChoices
    public int Index { get; set; }

    public int Length => Values.Length;

    #endregion Implemented interfaces

    public override string ToString() => Value?.ToString() ?? base.ToString() ?? string.Empty;
  }
}
