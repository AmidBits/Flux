//using System.Linq;

//namespace Flux.IO.Compression.Lzw
//{
//  [System.CLSCompliant(false)]
//  public abstract class BitStream
//    : Disposable
//  {
//    protected const uint EndOfBitStream = 256;

//    protected System.IO.Stream m_stream = default!;

//    protected uint m_pendingBuffer = 0;
//    protected uint m_pendingBits = 0;

//    protected uint m_codeSize;
//    protected uint m_currentCode;
//    protected uint m_nextBump;
//    protected uint m_maxCode;

//    public abstract bool ReadSymbol(out uint symbol);
//    public abstract void WriteSymbol(uint symbol);

//    protected override void DisposeManaged() => System.Diagnostics.Debug.WriteLine(EndOfBitStream);
//  }

//  [System.CLSCompliant(false)]
//  public class BitStreamDebugOnly : BitStream
//  {
//    public BitStreamDebugOnly()
//    {
//    }

//    public override bool ReadSymbol(out uint symbol)
//    {
//      if (m_stream.ReadByte() is var read && read == -1)
//      {
//        symbol = 0;
//        return false;
//      }

//      symbol = (uint)read;
//      return true;
//    }
//    public override void WriteSymbol(uint symbol)
//    {
//      m_stream.WriteByte((byte)(symbol >> 8));
//      m_stream.WriteByte((byte)symbol);

//      System.Diagnostics.Debug.WriteLine(symbol);
//    }
//  }

//  [System.CLSCompliant(false)]
//  public class BitStreamFixedBitLength : BitStream
//  {
//    public BitStreamFixedBitLength(uint maxCode)
//    {
//      m_codeSize = 1;
//      m_maxCode = maxCode;

//      while ((maxCode >>= 1) > 0)
//        m_codeSize++;
//    }
//    public override bool ReadSymbol(out uint symbol)
//    {
//      while (m_pendingBits < m_codeSize)
//      {
//        if (m_stream.ReadByte() is var read && read == -1)
//        {
//          symbol = 0;
//          return false;
//        }

//        m_pendingBuffer |= (uint)(read & 0xFF) << (int)m_pendingBits;
//        m_pendingBits += 8;
//      }

//      symbol = m_pendingBuffer & ~(~0U << (int)m_codeSize);

//      m_pendingBuffer >>= (int)m_codeSize;
//      m_pendingBits -= m_codeSize;

//      return symbol != EndOfBitStream;
//    }
//    public override void WriteSymbol(uint symbol)
//    {
//      m_pendingBuffer |= symbol << (int)m_pendingBits;
//      m_pendingBits += m_codeSize;

//      Flush(8);
//    }
//    private void Flush(uint value)
//    {
//      while (m_pendingBits >= value)
//      {
//        m_stream.WriteByte((byte)(m_pendingBuffer & 0xff));
//        m_pendingBuffer >>= 8;
//        m_pendingBits -= 8;
//      }
//    }
//  }

//  [System.CLSCompliant(false)]
//  public class BitStreamVariableBitLength : BitStream
//  {
//    public BitStreamVariableBitLength(uint codeSize, uint currentCode, uint nextBump, uint maxCode)
//    {
//      m_codeSize = codeSize;
//      m_currentCode = currentCode;
//      m_nextBump = nextBump;
//      m_maxCode = maxCode;
//    }

//    public override bool ReadSymbol(out uint symbol)
//    {
//      while (m_pendingBits < m_codeSize)
//      {
//        if (m_stream.ReadByte() is var read && read == -1)
//        {
//          symbol = 0;
//          return false;
//        }

//        m_pendingBuffer |= (uint)(read & 0xFF) << (int)m_pendingBits;
//        m_pendingBits += 8;
//      }

//      symbol = m_pendingBuffer & ~(~0U << (int)m_codeSize);

//      m_pendingBuffer >>= (int)m_codeSize;
//      m_pendingBits -= m_codeSize;

//      if (m_currentCode < m_maxCode)
//      {
//        m_currentCode++;

//        if (m_currentCode == m_nextBump)
//        {
//          m_nextBump <<= 1;
//          m_codeSize++;
//        }
//      }

//      return symbol != EndOfBitStream;
//    }
//    public override void WriteSymbol(uint symbol)
//    {
//      m_pendingBuffer |= symbol << (int)m_pendingBits;
//      m_pendingBits += m_codeSize;

//      Flush(8);

//      if (m_currentCode < m_maxCode)
//      {
//        m_currentCode++;
//        if (m_currentCode == m_nextBump)
//        {
//          m_nextBump <<= 1;
//          m_codeSize++;
//        }
//      }
//    }
//    private void Flush(uint value)
//    {
//      while (m_pendingBits >= value)
//      {
//        m_stream.WriteByte((byte)(m_pendingBuffer & 0xff));
//        m_pendingBuffer >>= 8;
//        m_pendingBits -= 8;
//      }
//    }
//  }

//  [System.CLSCompliant(false)]
//  public class LzwCompress
//  {
//    /// <summary>
//    /// 
//    /// </summary>
//    /// <see href="https://marknelson.us/posts/1989/10/01/lzw-data-compression.html"/>
//    /// <see href="https://marknelson.us/posts/2011/11/08/lzw-revisited.html"/>
//    public void Compress(System.IO.Stream reader, System.IO.Stream writer, int max_code = 32767)
//    {
//      var codes = new System.Collections.Generic.Dictionary<string, uint>();

//      for (uint u = 0; u < 256; u++)
//        codes.Add(((char)u).ToString(), u);

//      const uint eof_code = 256;
//      uint next_code = 257;

//      var current_string = new System.Text.StringBuilder();

//      while (reader.ReadByte() is var b && b > -1)
//      {
//        current_string.Append((char)b);

//        if (!codes.ContainsKey(current_string.ToString()))
//        {
//          if (next_code <= max_code)
//            codes.Add(current_string.ToString(), next_code++);

//          WriteSymbol(codes[current_string.ToString(0, current_string.Length - 1)]);

//          current_string = current_string.Remove(0, current_string.Length - 1);
//        }
//      }

//      if (current_string.Length > 0)
//        WriteSymbol(codes[current_string.ToString()]);

//      WriteSymbol(eof_code);
//    }

//    protected uint m_pendingBuffer = 0;
//    protected int m_pendingBits = 0;

//    private uint m_codeSize = 9;
//    private uint m_currentCode = 256;
//    private uint m_nextBump = 512;
//    private uint m_maxCode = 32767;

//    private void WriteSymbol(uint symbol)
//    {
//      System.Diagnostics.Debug.WriteLine($"Symbol: {symbol} ({Flux.Convert.ToRadixString(symbol, 2).PadLeft((int)m_codeSize, '0')}, bits: {m_codeSize}, current: {m_currentCode}, next: {m_nextBump})");

//      m_pendingBuffer |= symbol << (int)m_pendingBits;
//      m_pendingBits += (int)m_codeSize;

//      Flush(8);

//      if (m_currentCode < m_maxCode)
//      {
//        m_currentCode++;
//        if (m_currentCode == m_nextBump)
//        {
//          m_nextBump <<= 1;
//          m_codeSize++;
//        }
//      }
//    }
//    private void Flush(uint value)
//    {
//      while (m_pendingBits >= value)
//      {
//        System.Diagnostics.Debug.WriteLine($"Bits: {Flux.Convert.ToRadixString((byte)(m_pendingBuffer & 0xff), 2).PadLeft(8, '0')}");
//        m_pendingBuffer >>= 8;
//        m_pendingBits -= 8;
//      }
//    }
//  }
//  //public class Lzwv
//  //{
//  //  private const int MAX_BITS = 14; //maimxum bits allowed to read
//  //  private const int HASHING_SHIFT = MAX_BITS - 8; //hash bit to use with the hasing algorithm to find correct index
//  //  private const int MAX_VALUE = (1 << MAX_BITS) - 1; //max value allowed based on max bits
//  //  private const int MAX_CODE = MAX_VALUE - 1; //max code possible

//  //  private const int TABLE_SIZE12 = 5021;
//  //  private const int TABLE_SIZE13 = 9029;
//  //  private const int TABLE_SIZE14 = 18041;

//  //  private const int TABLE_SIZE = TABLE_SIZE14; //must be bigger than the maximum allowed by maxbits and prime

//  //  private int[] _iaCodeTable = new int[TABLE_SIZE]; //code table
//  //  private int[] _iaPrefixTable = new int[TABLE_SIZE]; //prefix table
//  //  private int[] _iaCharTable = new int[TABLE_SIZE]; //character table

//  //  private ulong _iBitBuffer; //bit buffer to temporarily store bytes read from the files

//  //  private int _iBitCounter; //counter for knowing how many bits are in the bit buffer

//  //  private void Initialize() //used to blank  out bit buffer incase this class is called to comprss and decompress from the same instance
//  //  {
//  //    _iBitBuffer = 0;
//  //    _iBitCounter = 0;
//  //  }

//  //  public bool Compress(System.IO.Stream reader, System.IO.Stream writer)
//  //  {
//  //    try
//  //    {
//  //      Initialize();

//  //      int iNextCode = 256;

//  //      int iChar = 0, iString = 0, iIndex = 0;

//  //      for (int i = 0; i < TABLE_SIZE; i++) //blank out table
//  //        _iaCodeTable[i] = -1;

//  //      iString = reader.ReadByte(); //get first code, will be 0-255 ascii char

//  //      while ((iChar = reader.ReadByte()) != -1) //read until we reach end of file
//  //      {
//  //        iIndex = FindMatch(iString, iChar); //get correct index for prefix+char

//  //        if (_iaCodeTable[iIndex] != -1) //set string if we have something at that index
//  //          iString = _iaCodeTable[iIndex];
//  //        else //insert new entry
//  //        {
//  //          if (iNextCode <= MAX_CODE) //otherwise we insert into the tables
//  //          {
//  //            _iaCodeTable[iIndex] = iNextCode++; //insert and increment next code to use
//  //            _iaPrefixTable[iIndex] = iString;
//  //            _iaCharTable[iIndex] = (byte)iChar;
//  //          }

//  //          WriteCode(writer, iString); //output the data in the string

//  //          iString = iChar;
//  //        }
//  //      }

//  //      WriteCode(writer, iString); //output last code
//  //      WriteCode(writer, MAX_VALUE); //output end of buffer
//  //      WriteCode(writer, 0); //flush
//  //    }
//  //    catch (System.Exception ex)
//  //    {
//  //      System.Diagnostics.Debug.WriteLine(ex.StackTrace);
//  //      return false;
//  //    }

//  //    return true;
//  //  }

//  //  //hasing function, tries to find index of prefix+char, if not found returns -1 to signify space available

//  //  private int FindMatch(int pPrefix, int pChar)
//  //  {
//  //    var index = (pChar << HASHING_SHIFT) ^ pPrefix;

//  //    var offset = (index == 0) ? 1 : TABLE_SIZE - index;

//  //    while (true)
//  //    {
//  //      if (_iaCodeTable[index] == -1)
//  //        return index;

//  //      if (_iaPrefixTable[index] == pPrefix && _iaCharTable[index] == pChar)
//  //        return index;

//  //      index -= offset;

//  //      if (index < 0)
//  //        index += TABLE_SIZE;
//  //    }
//  //  }

//  //  private void WriteCode(System.IO.Stream pWriter, int pCode)
//  //  {
//  //    if (pCode <= 255) System.Diagnostics.Debug.WriteLine($"WCc: {(char)pCode}"); else System.Diagnostics.Debug.WriteLine($"WCi: {pCode}");

//  //    _iBitBuffer |= (ulong)pCode << (32 - MAX_BITS - _iBitCounter); //make space and insert new code in buffer
//  //    _iBitCounter += MAX_BITS; //increment bit counter

//  //    while (_iBitCounter >= 8) //write all the bytes we can
//  //    {
//  //      pWriter.WriteByte((byte)((_iBitBuffer >> 24) & 255)); //write byte from bit buffer

//  //      _iBitBuffer <<= 8; //remove written byte from buffer
//  //      _iBitCounter -= 8; //decrement counter
//  //    }
//  //  }

//  //  public bool Decompress(System.IO.Stream reader, System.IO.Stream writer)
//  //  {
//  //    try
//  //    {
//  //      Initialize();

//  //      int iNextCode = 256;
//  //      int iNewCode, iOldCode;
//  //      byte bChar;
//  //      int iCurrentCode, iCounter;
//  //      byte[] baDecodeStack = new byte[TABLE_SIZE];

//  //      iOldCode = ReadCode(reader);

//  //      bChar = (byte)iOldCode;

//  //      writer.WriteByte((byte)iOldCode); //write first byte since it is plain ascii

//  //      iNewCode = ReadCode(reader);

//  //      while (iNewCode != MAX_VALUE) //read file all file
//  //      {
//  //        if (iNewCode >= iNextCode)
//  //        { //fix for prefix+chr+prefix+char+prefx special case
//  //          baDecodeStack[0] = bChar;

//  //          iCounter = 1;

//  //          iCurrentCode = iOldCode;
//  //        }
//  //        else
//  //        {
//  //          iCounter = 0;

//  //          iCurrentCode = iNewCode;
//  //        }

//  //        while (iCurrentCode > 255) //decode string by cycling back through the prefixes
//  //        {
//  //          baDecodeStack[iCounter] = (byte)_iaCharTable[iCurrentCode];

//  //          ++iCounter;

//  //          if (iCounter >= MAX_CODE)
//  //            throw new System.Exception("oh crap");

//  //          iCurrentCode = _iaPrefixTable[iCurrentCode];
//  //        }

//  //        baDecodeStack[iCounter] = (byte)iCurrentCode;

//  //        bChar = baDecodeStack[iCounter]; //set last char used

//  //        while (iCounter >= 0) //write out decodestack
//  //        {
//  //          writer.WriteByte(baDecodeStack[iCounter]);

//  //          --iCounter;
//  //        }

//  //        if (iNextCode <= MAX_CODE) //insert into tables
//  //        {
//  //          _iaPrefixTable[iNextCode] = iOldCode;

//  //          _iaCharTable[iNextCode] = bChar;

//  //          ++iNextCode;
//  //        }

//  //        iOldCode = iNewCode;
//  //        iNewCode = ReadCode(reader);
//  //      }
//  //    }
//  //    catch (System.Exception ex)
//  //    {
//  //      System.Diagnostics.Debug.WriteLine(ex.StackTrace);
//  //      return false;
//  //    }

//  //    return true;
//  //  }

//  //  private int ReadCode(System.IO.Stream pReader)
//  //  {
//  //    while (_iBitCounter <= 24) //fill up buffer
//  //    {
//  //      _iBitBuffer |= (ulong)pReader.ReadByte() << (24 - _iBitCounter); //insert byte into buffer
//  //      _iBitCounter += 8; //increment counter
//  //    }

//  //    var iReturnVal = (uint)_iBitBuffer >> (32 - MAX_BITS); //get last byte from buffer so we can return it

//  //    _iBitBuffer <<= MAX_BITS; //remove it from buffer
//  //    _iBitCounter -= MAX_BITS; //decrement bit counter

//  //    int temp = (int)iReturnVal;

//  //    if (iReturnVal <= 255) System.Diagnostics.Debug.WriteLine($"RCc: {(char)iReturnVal}"); else System.Diagnostics.Debug.WriteLine($"RCi: {iReturnVal}");

//  //    return temp;
//  //  }
//  //}
//}