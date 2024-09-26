namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Returns a new <see cref="System.ReadOnlySpan{T}"/> with all consecutive elements satisfying the <paramref name="predicate"/> removed, at the end.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static System.Text.StringBuilder TrimEnd(this System.Text.StringBuilder source, System.Func<char, bool> predicate)
    {
      System.ArgumentNullException.ThrowIfNull(source);
      System.ArgumentNullException.ThrowIfNull(predicate);

      var index = source.Length - 1;
      while (index >= 0 && predicate(source[index]))
        index--;
      return source.Remove(index + 1); // Remove from (index + 1) to the end.
    }
  }
}
