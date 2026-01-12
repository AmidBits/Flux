//namespace Flux.Compressor
//{
//  public class Lzwv
//    : ICompressor
//  {
//    public const int MaxBits = 16;
//    public const int MinBits = 9;

//    public static ICompressor Default { get; } = new Lzwv();

//    public void Compress(System.IO.Stream input, System.IO.Stream output) => PackBits(Encode(input), output);

//    public void Decompress(System.IO.Stream input, System.IO.Stream output) => Decode(UnpackBits(input), output);

//    public static void Decode(System.Collections.Generic.IEnumerable<int> codes, System.IO.Stream output)
//    {
//      var dictionary = new Dictionary<int, string>();
//      for (int i = 0; i < 256; i++)
//        dictionary[i] = ((char)i).ToString();

//      using var e = codes.GetEnumerator();

//      if (e.MoveNext())
//      {
//        int dictSize = 256;
//        string w = dictionary[e.Current];

//        output.Write(System.Text.Encoding.UTF8.GetBytes(w));

//        while (e.MoveNext() && e.Current is var k)
//        {
//          if (!dictionary.TryGetValue(k, out var entry))
//            entry = (k == dictSize) ? w + w[0] : throw new System.IO.InvalidDataException("Invalid LZW code encountered.");

//          output.Write(System.Text.Encoding.UTF8.GetBytes(entry));

//          dictionary[dictSize++] = w + entry[0];

//          w = entry;
//        }
//      }

//      output.Flush();
//    }

//    public static System.Collections.Generic.IEnumerable<int> Encode(System.IO.Stream input)
//    {
//      var dictionary = new Dictionary<string, int>();
//      for (var i = 0; i < 256; i++)
//        dictionary.Add(((char)i).ToString(), i);

//      int dictSize = 256;
//      string w = string.Empty;

//      while (input.ReadByte() is var r && r != -1 && (char)r is var c)
//      {
//        string wc = w + c;

//        if (dictionary.ContainsKey(wc))
//          w = wc;
//        else
//        {
//          yield return dictionary[w];

//          dictionary[wc] = dictSize++;

//          w = c.ToString();
//        }
//      }

//      if (!string.IsNullOrEmpty(w))
//        yield return dictionary[w];
//    }

//    public static void PackBits(System.Collections.Generic.IEnumerable<int> codes, System.IO.Stream output)
//    {
//      var bitLength = MinBits; // Start with 9 bits
//      var nextLimit = 1 << bitLength;

//      var bitBuffer = 0;
//      var bitCount = 0;

//      foreach (var code in codes)
//      {
//        bitBuffer = (bitBuffer << bitLength) | code;
//        bitCount += bitLength;

//        while (bitCount >= 8)
//        {
//          bitCount -= 8;

//          output.WriteByte((byte)((bitBuffer >> bitCount) & 0xFF));
//        }

//        if (code >= nextLimit && bitLength < MaxBits)
//        {
//          bitLength++;
//          nextLimit = 1 << bitLength;
//        }
//      }

//      if (bitCount > 0)
//        output.WriteByte((byte)((bitBuffer << (8 - bitCount)) & 0xFF));

//      output.Flush();
//    }

//    public static System.Collections.Generic.IEnumerable<int> UnpackBits(System.IO.Stream input)
//    {
//      var bitLength = MinBits;
//      var nextLimit = 1 << bitLength;

//      var bitBuffer = 0;
//      var bitCount = 0;

//      while (input.ReadByte() is var r && r != -1 && (byte)r is var b)
//      {
//        bitBuffer = (bitBuffer << 8) | b;
//        bitCount += 8;

//        while (bitCount >= bitLength)
//        {
//          bitCount -= bitLength;

//          var code = (bitBuffer >> bitCount) & ((1 << bitLength) - 1);

//          yield return code;

//          if (code >= nextLimit && bitLength < MaxBits)
//          {
//            bitLength++;
//            nextLimit = 1 << bitLength;
//          }
//        }
//      }

//      //if (bitCount > 0)
//      //  yield return (bitBuffer >> bitCount) & ((1 << bitLength) - 1);
//    }
//  }
//}
