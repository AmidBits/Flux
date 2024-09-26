namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Returns a new <see cref="System.ReadOnlySpan{T}"/> with all consecutive elements satisfying the <paramref name="predicate"/> removed at the start.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static System.Text.StringBuilder TrimStart(this System.Text.StringBuilder source, System.Func<char, bool> predicate)
    {
      System.ArgumentNullException.ThrowIfNull(source);
      System.ArgumentNullException.ThrowIfNull(predicate);

      var index = 0;
      while (index < source.Length && predicate(source[index]))
        index++;
      return source.Remove(0, index); // Remove from the start to (index).
    }
  }
}
