namespace Flux
{
  public static partial class DateTimeOffsetExtensions
  {
    extension(System.DateTimeOffset)
    {
      public static System.DateTimeOffset FromInt128(System.Int128 int128)
      {
        return new System.DateTimeOffset(unchecked((long)int128), new System.TimeSpan(unchecked((long)(int128 >>> 64))));
      }

      public static System.Int128 ToInt128(System.DateTimeOffset dateTimeOffset)
      {
        return unchecked(((System.Int128)dateTimeOffset.Offset.Ticks << 64) | (System.Int128)dateTimeOffset.Ticks);
      }
    }
  }
}
