namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Keep the specified character from the left, in the StringBuilder (discard on right).</summary>
    public static System.Text.StringBuilder KeepLeft(this System.Text.StringBuilder source, int count)
      => (source ?? throw new System.ArgumentNullException(nameof(source))).Remove(count, source.Length - count);

    /// <summary>Keep the specified character on the right, in the StringBuilder (discard on left).</summary>
    public static System.Text.StringBuilder KeepRight(this System.Text.StringBuilder source, int count)
      => (source ?? throw new System.ArgumentNullException(nameof(source))).Remove(0, source.Length - count);
  }
}
