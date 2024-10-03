using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SystemFx
{
  [TestClass]
  public class BitConversion
  {
    [TestMethod]
    public void BigEndianWriteBytesBoolean()
    {
      byte[] expected = new byte[] { 1 };
      byte[] actual = new byte[expected.Length];
      true.WriteBytes(actual, 0, Endianess.BigEndian);
      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void BigEndianWriteBytesChar()
    {
      byte[] expected = new byte[] { 0, 1 };
      byte[] actual = new byte[expected.Length];
      ((char)1).WriteBytes(actual, 0, Flux.Endianess.BigEndian);
      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void BigEndianWriteBytesDecimal()
    {
      byte[] expected = new byte[] { 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
      byte[] actual = new byte[expected.Length];
      System.Decimal.One.WriteBytes(actual, 0, Flux.Endianess.BigEndian);
      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void BigEndianWriteBytesDouble()
    {
      byte[] expected = new byte[] { 63, 240, 0, 0, 0, 0, 0, 0 };
      byte[] actual = new byte[expected.Length];
      1D.WriteBytes(actual, 0, Endianess.BigEndian);
      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void BigEndianWriteBytesFloat()
    {
      byte[] expected = new byte[] { 63, 128, 0, 0 };
      byte[] actual = new byte[expected.Length];
      1F.WriteBytes(actual, 0, Endianess.BigEndian);
      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void BigEndianWriteBytesInt16()
    {
      byte[] expected = new byte[] { 0, 1 };
      byte[] actual = new byte[expected.Length];
      ((short)1).WriteBytes(actual, 0, Endianess.BigEndian);
      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void BigEndianWriteBytesInt32()
    {
      byte[] expected = new byte[] { 0, 0, 0, 1 };
      byte[] actual = new byte[expected.Length];
      1.WriteBytes(actual, 0, Endianess.BigEndian);
      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void BigEndianWriteBytesInt64()
    {
      byte[] expected = new byte[] { 0, 0, 0, 0, 0, 0, 0, 1 };
      byte[] actual = new byte[expected.Length];
      1L.WriteBytes(actual, 0, Endianess.BigEndian);
      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void BigEndianWriteBytesUInt16()
    {
      byte[] expected = new byte[] { 0, 1 };
      byte[] actual = new byte[expected.Length];
      ((ushort)1).WriteBytes(actual, 0, Endianess.BigEndian);
      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void BigEndianWriteBytesUInt32()
    {
      byte[] expected = new byte[] { 0, 0, 0, 1 };
      byte[] actual = new byte[expected.Length];
      1U.WriteBytes(actual, 0, Endianess.BigEndian);
      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void BigEndianWriteBytesUInt64()
    {
      byte[] expected = new byte[] { 0, 0, 0, 0, 0, 0, 0, 1 };
      byte[] actual = new byte[expected.Length];
      1UL.WriteBytes(actual, 0, Endianess.BigEndian);
      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LittleEndianWriteBytesBoolean()
    {
      byte[] expected = new byte[] { 1 };
      byte[] actual = new byte[expected.Length];
      true.WriteBytes(actual, 0, Endianess.LittleEndian);
      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LittleEndianWriteBytesChar()
    {
      byte[] expected = new byte[] { 1, 0 };
      byte[] actual = new byte[expected.Length];
      ((char)1).WriteBytes(actual, 0, Endianess.LittleEndian);
      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LittleEndianWriteBytesDecimal()
    {
      byte[] expected = new byte[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
      byte[] actual = new byte[expected.Length];
      1M.WriteBytes(actual, 0, Endianess.LittleEndian);
      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LittleEndianWriteBytesDouble()
    {
      byte[] expected = new byte[] { 0, 0, 0, 0, 0, 0, 240, 63 };
      byte[] actual = new byte[expected.Length];
      1D.WriteBytes(actual, 0, Endianess.LittleEndian);
      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LittleEndianWriteBytesFloat()
    {
      byte[] expected = new byte[] { 0, 0, 128, 63 };
      byte[] actual = new byte[expected.Length];
      1F.WriteBytes(actual, 0, Endianess.LittleEndian);
      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LittleEndianWriteBytesInt16()
    {
      byte[] expected = new byte[] { 1, 0 };
      byte[] actual = new byte[expected.Length];
      ((short)1).WriteBytes(actual, 0, Endianess.LittleEndian);
      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LittleEndianWriteBytesInt32()
    {
      byte[] expected = new byte[] { 1, 0, 0, 0 };
      byte[] actual = new byte[expected.Length];
      1.WriteBytes(actual, 0, Endianess.LittleEndian);
      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LittleEndianWriteBytesInt64()
    {
      byte[] expected = new byte[] { 1, 0, 0, 0, 0, 0, 0, 0 };
      byte[] actual = new byte[expected.Length];
      1L.WriteBytes(actual, 0, Endianess.LittleEndian);
      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LittleEndianWriteBytesUInt16()
    {
      byte[] expected = new byte[] { 1, 0 };
      byte[] actual = new byte[expected.Length];
      ((ushort)1).WriteBytes(actual, 0, Endianess.LittleEndian);
      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LittleEndianWriteBytesUInt32()
    {
      byte[] expected = new byte[] { 1, 0, 0, 0 };
      byte[] actual = new byte[expected.Length];
      1U.WriteBytes(actual, 0, Endianess.LittleEndian);
      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LittleEndianWriteBytesUInt64()
    {
      byte[] expected = new byte[] { 1, 0, 0, 0, 0, 0, 0, 0 };
      byte[] actual = new byte[expected.Length];
      1UL.WriteBytes(actual, 0, Endianess.LittleEndian);
      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void BigEndianReadBoolean()
    {
      bool expected = true;
      bool actual = new byte[] { 1 }.ReadBoolean(0, Endianess.BigEndian);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void BigEndianReadChar()
    {
      var expected = (char)1;
      var actual = new byte[] { 0, 1 }.ReadChar(0, Endianess.BigEndian);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void BigEndianReadDecimal()
    {
      var expected = (decimal)1;
      var actual = new byte[] { 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }.ReadDecimal(0, Endianess.BigEndian);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void BigEndianReadDouble()
    {
      var expected = (double)1;
      var actual = new byte[] { 63, 240, 0, 0, 0, 0, 0, 0 }.ReadDouble(0, Endianess.BigEndian);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void BigEndianReadFloat()
    {
      var expected = (float)1;
      var actual = new byte[] { 63, 128, 0, 0 }.ReadSingle(0, Endianess.BigEndian);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void BigEndianReadInt16()
    {
      var expected = (short)1;
      var actual = new byte[] { 0, 1 }.ReadInt16(0, Endianess.BigEndian);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void BigEndianReadInt32()
    {
      var expected = (int)1;
      var actual = new byte[] { 0, 0, 0, 1 }.ReadInt32(0, Endianess.BigEndian);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void BigEndianReadInt64()
    {
      var expected = (long)1;
      var actual = new byte[] { 0, 0, 0, 0, 0, 0, 0, 1 }.ReadInt64(0, Endianess.BigEndian);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void BigEndianReadUInt16()
    {
      var expected = (ushort)1;
      var actual = new byte[] { 0, 1 }.ReadUInt16(0, Endianess.BigEndian);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void BigEndianReadUInt32()
    {
      var expected = (uint)1;
      var actual = new byte[] { 0, 0, 0, 1 }.ReadUInt32(0, Endianess.BigEndian);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void BigEndianReadUInt64()
    {
      var expected = (ulong)1;
      var actual = new byte[] { 0, 0, 0, 0, 0, 0, 0, 1 }.ReadUInt64(0, Endianess.BigEndian);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LittleEndianReadBoolean()
    {
      bool expected = true;
      bool actual = new byte[] { 1 }.ReadBoolean(0, Endianess.LittleEndian);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LittleEndianReadChar()
    {
      var expected = (char)1;
      var actual = new byte[] { 1, 0 }.ReadChar(0, Endianess.LittleEndian);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LittleEndianReadDecimal()
    {
      var expected = (decimal)1;
      var actual = new byte[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }.ReadDecimal(0, Endianess.LittleEndian);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LittleEndianReadDouble()
    {
      var expected = (double)1;
      var actual = new byte[] { 0, 0, 0, 0, 0, 0, 240, 63 }.ReadDouble(0, Endianess.LittleEndian);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LittleEndianReadFloat()
    {
      var expected = (float)1;
      var actual = new byte[] { 0, 0, 128, 63 }.ReadSingle(0, Endianess.LittleEndian);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LittleEndianReadInt16()
    {
      var expected = (short)1;
      var actual = new byte[] { 1, 0 }.ReadInt16(0, Endianess.LittleEndian);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LittleEndianReadInt32()
    {
      var expected = (int)1;
      var actual = new byte[] { 1, 0, 0, 0 }.ReadInt32(0, Endianess.LittleEndian);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LittleEndianReadInt64()
    {
      var expected = (long)1;
      var actual = new byte[] { 1, 0, 0, 0, 0, 0, 0, 0 }.ReadInt64(0, Endianess.LittleEndian);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LittleEndianReadUInt16()
    {
      var expected = (ushort)1;
      var actual = new byte[] { 1, 0 }.ReadUInt16(0, Endianess.LittleEndian);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LittleEndianReadUInt32()
    {
      var expected = (uint)1;
      var actual = new byte[] { 1, 0, 0, 0 }.ReadUInt16(0, Endianess.LittleEndian);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LittleEndianReadUInt64()
    {
      var expected = (ulong)1;
      var actual = new byte[] { 1, 0, 0, 0, 0, 0, 0, 0 }.ReadUInt16(0, Endianess.LittleEndian);
      Assert.AreEqual(expected, actual);
    }
  }
}
