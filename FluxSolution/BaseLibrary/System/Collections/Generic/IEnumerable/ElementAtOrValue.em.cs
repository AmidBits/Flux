namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns the element positioned at <paramref name="index"/>, or <paramref name="value"/> if not found (with index = -1).</summary>
    /// <exception cref="System.ArgumentOutOfRangeException"/>
    public static (T item, int index) ElementAtOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, int index, T value)
    {
      if (index < 0) throw new System.ArgumentOutOfRangeException(nameof(index));

      var internalIndex = 0;

      foreach (var item in source)
      {
        if (internalIndex == index)
          return (item, index);

        internalIndex++;
      }

      return (value, -1);
    }
  }
}
