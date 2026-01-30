namespace Flux
{
  public static partial class SByteExtensions
  {
    extension(System.Guid)
    {
      public static System.Guid FromInt128(System.Int128 int128)
      {
        var bytes = new byte[16];

        System.Buffers.Binary.BinaryPrimitives.WriteInt128LittleEndian(bytes, int128);

        return new System.Guid(bytes);
      }

      public static System.Int128 ToInt128(System.Guid guid)
      {
        var bytes = guid.ToByteArray();

        return System.Buffers.Binary.BinaryPrimitives.ReadInt128LittleEndian(bytes);
      }
    }
  }
}
