namespace Flux
{
  public static partial class SystemStringEm
  {
    /// <summary>Replicates the string a specified number of times.</summary>
    public static System.Text.StringBuilder Replicate(this string source, int count)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      else if (source.Length == 0) throw new System.ArgumentOutOfRangeException(nameof(source));

      var sb = new System.Text.StringBuilder(source);
      while (count-- > 0)
        sb.Append(source);
      return sb;
    }
  }
}
