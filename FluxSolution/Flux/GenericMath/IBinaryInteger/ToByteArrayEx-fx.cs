namespace Flux
{
  public static partial class GenericMath
  {
    // PLEASE NOTE THAT THE TWO METHODS BELOW AIM TO REPLICATE THE BIGINTEGER EXTENSIONS ABOVE FOR ALL INTEGER TYPES.

    /*
      for (var i = -10; i < 10; i++)
      {
        var n = i.ToBigInteger();

        var bae = n.ToByteArrayEx(out var msbIndex, out var msbValue);
        var ba = n.ToByteArrayEx(out var length);

        var bae2 = i.ToByteArrayEx(out var msbIndex2, out var msbValue2);
        var ba2 = i.ToByteArrayEx(out var length2);

        System.Console.WriteLine($"{n} = {string.Join(Static.CommaSpace, bae)} ({msbIndex}, {msbValue}) | {string.Join(Static.CommaSpace, ba)} ({length}) : {string.Join(Static.CommaSpace, bae2)} ({msbIndex2}, {msbValue2}) | {string.Join(Static.CommaSpace, ba2)} ({length2})");
      }
     */

    ///// <summary>Returns either the built-in byte array, or if a zero byte padding is present, a byte array excluding the zero byte, for the <see cref="System.Numerics.BigInteger"/>.</summary>
    //public static byte[] ToByteArrayEx<TValue>(this TValue source, out int length)
    //  where TValue : System.Numerics.IBinaryInteger<TValue>
    //{
    //  var byteArray = new byte[source.GetByteCount()];

    //  source.WriteLittleEndian(byteArray);

    //  length = byteArray.Length - 1;

    //  while (length > 1 && byteArray[length] is var b && (b == byte.MaxValue || b == byte.MinValue))
    //    length--;

    //  Array.Resize(ref byteArray, length);

    //  return byteArray;
    //}

    ///// <summary>This is essentially the same as the native <see cref="System.Numerics.BigInteger.ToByteArray()"/> with the addition of the most significant byte index and its value as out parameters.</summary>
    //public static byte[] ToByteArrayEx<TValue>(this TValue source, out int msbIndex, out byte msbValue)
    //  where TValue : System.Numerics.IBinaryInteger<TValue>
    //{
    //  var byteArray = source.ToByteArrayEx(out var length);

    //  msbIndex = length - 1;
    //  msbValue = byteArray[msbIndex];

    //  return byteArray;
    //}
  }
}
