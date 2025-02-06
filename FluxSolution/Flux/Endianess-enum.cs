namespace Flux
{
  /// <summary>
  /// <para>In computing, endianness is the order in which bytes within a word (e.g. 32-bits) of digital data are transmitted over a data communication medium or addressed (by rising addresses) in computer memory, counting only byte significance compared to earliness.</para>
  /// <para>Endianness is primarily expressed as big-endian (BE) or little-endian (LE), where BE stores the big end first, and LE stores the little end first. "First" refers to the lowest byte of storage. (Endianess is all about bytes.)</para>
  /// <para>Computers store information in various-sized groups of binary bits. Each group is assigned a number, called its address, that the computer uses to access that data. On most modern computers, the smallest data group with an address is eight bits long and is called a byte. Larger groups comprise two or more bytes, for example, a 32-bit word contains four bytes. There are two possible ways a computer could number the individual bytes in a larger group, starting at either end.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Endianness"/></para>
  /// <code>Consider a memory pointer to a 4-byte address (using offsets) [+3,+2,+1,+0] and a 32-bit hexadecimal number 0x0A0B0C0D.</code>
  /// <code>BE stores it as [0D,0C,0B,0A].</code>
  /// <code>LE stores it as [0A,0B,0C,0D].</code>
  /// </summary>
  public enum Endianess
  {
    /// <summary>
    /// <para>Stores decreasing numeric significance in increasing memory addresses (or increasing time).</para>
    /// <code>Considering a memory pointer to a 4-byte address (using offsets) [+3,+2,+1,+0], and a 32-bit hexadecimal number 0x0A0B0C0D, BE stores it as [0D,0C,0B,0A].</code>
    /// <para>Big-endianness is the dominant ordering in networking protocols, such as in the Internet protocol suite, where it is referred to as network order, transmitting the most significant byte first.</para>
    /// <para>E.g. Motorola.</para>
    /// </summary>
    BigEndian,
    /// <summary>
    /// <para>Stores increasing numeric significance in increasing memory addresses (or increasing time).</para>
    /// <code>Considering a memory pointer to a 4-byte address (using offsets) [+3,+2,+1,+0], and a 32-bit hexadecimal number 0x0A0B0C0D, LE stores it as [0A,0B,0C,0D].</code>
    /// <para>Little-endianness is the dominant ordering for processor architectures (x86, most ARM implementations, base RISC-V implementations) and their associated memory.</para>
    /// <para>E.g. Intel.</para>
    /// </summary>
    LittleEndian,
    /// <summary>
    /// <para>Network-order is the same as big-endian order.</para>
    /// </summary>
    NetworkOrder = BigEndian,
  }
}
