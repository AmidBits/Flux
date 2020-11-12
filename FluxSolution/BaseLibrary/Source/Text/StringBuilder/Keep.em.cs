namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Keep the specified character from the left, in the StringBuilder (discard on right).</summary>
    public static System.Text.StringBuilder KeepLeft(this System.Text.StringBuilder source, int count)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (count < 0 || count > source.Length) throw new System.ArgumentOutOfRangeException(nameof(count));

      return source.Remove(count, source.Length - count);
    }

    /// <summary>Keep the specified character on the right, in the StringBuilder (discard on left).</summary>
    public static System.Text.StringBuilder KeepRight(this System.Text.StringBuilder source, int count)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (count < 0 || count > source.Length) throw new System.ArgumentOutOfRangeException(nameof(count));

      return source.Remove(0, source.Length - count);
    }

    /// <summary>Keep the specified character in the middle, within the StringBuilder (discard on left and right).</summary>
    public static System.Text.StringBuilder KeepMiddle(this System.Text.StringBuilder source, int startindex, int count)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (startindex < 0 || startindex > source.Length - 1) throw new System.ArgumentOutOfRangeException(nameof(startindex));
      if (count < 0 || startindex + count > source.Length) throw new System.ArgumentOutOfRangeException(nameof(count));

      return source.Remove(0, startindex).Remove(count, source.Length - count);
    }
  }
}
