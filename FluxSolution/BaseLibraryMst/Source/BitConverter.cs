using System.Linq;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Foundation
{
    [TestClass]
    public class BitConverter
    {
        [TestMethod]
        public void GetBytesBoolean()
        {
            byte[] expected = new byte[] { 1 };
            byte[] actual = Flux.BitConverter.GetBytes(true);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BigEndianGetBytesChar()
        {
            byte[] expected = new byte[] { 0, 1 };
            byte[] actual = Flux.BitConverter.BigEndian.GetBytes((char)1);
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void BigEndianGetBytesDecimal()
        {
            byte[] expected = new byte[] { 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            byte[] actual = Flux.BitConverter.BigEndian.GetBytes((decimal)1);
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void BigEndianGetBytesDouble()
        {
            byte[] expected = new byte[] { 63, 240, 0, 0, 0, 0, 0, 0 };
            byte[] actual = Flux.BitConverter.BigEndian.GetBytes((double)1);
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void BigEndianGetBytesFloat()
        {
            byte[] expected = new byte[] { 63, 128, 0, 0 };
            byte[] actual = Flux.BitConverter.BigEndian.GetBytes((float)1);
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void BigEndianGetBytesInt16()
        {
            byte[] expected = new byte[] { 0, 1 };
            byte[] actual = Flux.BitConverter.BigEndian.GetBytes((short)1);
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void BigEndianGetBytesInt32()
        {
            byte[] expected = new byte[] { 0, 0, 0, 1 };
            byte[] actual = Flux.BitConverter.BigEndian.GetBytes((int)1);
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void BigEndianGetBytesInt64()
        {
            byte[] expected = new byte[] { 0, 0, 0, 0, 0, 0, 0, 1 };
            byte[] actual = Flux.BitConverter.BigEndian.GetBytes((long)1);
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void BigEndianGetBytesUInt16()
        {
            byte[] expected = new byte[] { 0, 1 };
            byte[] actual = Flux.BitConverter.BigEndian.GetBytes((ushort)1);
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void BigEndianGetBytesUInt32()
        {
            byte[] expected = new byte[] { 0, 0, 0, 1 };
            byte[] actual = Flux.BitConverter.BigEndian.GetBytes((uint)1);
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void BigEndianGetBytesUInt64()
        {
            byte[] expected = new byte[] { 0, 0, 0, 0, 0, 0, 0, 1 };
            byte[] actual = Flux.BitConverter.BigEndian.GetBytes((ulong)1);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void LittleEndianGetBytesChar()
        {
            byte[] expected = new byte[] { 1, 0 };
            byte[] actual = Flux.BitConverter.LittleEndian.GetBytes((char)1);
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LittleEndianGetBytesDecimal()
        {
            byte[] expected = new byte[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            byte[] actual = Flux.BitConverter.LittleEndian.GetBytes((decimal)1);
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LittleEndianGetBytesDouble()
        {
            byte[] expected = new byte[] { 0, 0, 0, 0, 0, 0, 240, 63 };
            byte[] actual = Flux.BitConverter.LittleEndian.GetBytes((double)1);
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LittleEndianGetBytesFloat()
        {
            byte[] expected = new byte[] { 0, 0, 128, 63 };
            byte[] actual = Flux.BitConverter.LittleEndian.GetBytes((float)1);
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LittleEndianGetBytesInt16()
        {
            byte[] expected = new byte[] { 1, 0 };
            byte[] actual = Flux.BitConverter.LittleEndian.GetBytes((short)1);
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LittleEndianGetBytesInt32()
        {
            byte[] expected = new byte[] { 1, 0, 0, 0 };
            byte[] actual = Flux.BitConverter.LittleEndian.GetBytes((int)1);
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LittleEndianGetBytesInt64()
        {
            byte[] expected = new byte[] { 1, 0, 0, 0, 0, 0, 0, 0 };
            byte[] actual = Flux.BitConverter.LittleEndian.GetBytes((long)1);
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LittleEndianGetBytesUInt16()
        {
            byte[] expected = new byte[] { 1, 0 };
            byte[] actual = Flux.BitConverter.LittleEndian.GetBytes((ushort)1);
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LittleEndianGetBytesUInt32()
        {
            byte[] expected = new byte[] { 1, 0, 0, 0 };
            byte[] actual = Flux.BitConverter.LittleEndian.GetBytes((uint)1);
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LittleEndianGetBytesUInt64()
        {
            byte[] expected = new byte[] { 1, 0, 0, 0, 0, 0, 0, 0 };
            byte[] actual = Flux.BitConverter.LittleEndian.GetBytes((ulong)1);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BigEndianToChar()
        {
            var expected = (char)1;
            var actual = Flux.BitConverter.BigEndian.ToChar(new byte[] { 0, 1 }, 0);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void BigEndianToDecimal()
        {
            var expected = (decimal)1;
            var actual = Flux.BitConverter.BigEndian.ToDecimal(new byte[] { 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 0);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void BigEndianToDouble()
        {
            var expected = (double)1;
            var actual = Flux.BitConverter.BigEndian.ToDouble(new byte[] { 63, 240, 0, 0, 0, 0, 0, 0 }, 0);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void BigEndianToFloat()
        {
            var expected = (float)1;
            var actual = Flux.BitConverter.BigEndian.ToSingle(new byte[] { 63, 128, 0, 0 }, 0);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void BigEndianToInt16()
        {
            var expected = (short)1;
            var actual = Flux.BitConverter.BigEndian.ToInt16(new byte[] { 0, 1 }, 0);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void BigEndianToInt32()
        {
            var expected = (int)1;
            var actual = Flux.BitConverter.BigEndian.ToInt32(new byte[] { 0, 0, 0, 1 }, 0);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void BigEndianToInt64()
        {
            var expected = (long)1;
            var actual = Flux.BitConverter.BigEndian.ToInt64(new byte[] { 0, 0, 0, 0, 0, 0, 0, 1 }, 0);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void BigEndianToUInt16()
        {
            var expected = (ushort)1;
            var actual = Flux.BitConverter.BigEndian.ToUInt16(new byte[] { 0, 1 }, 0);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void BigEndianToUInt32()
        {
            var expected = (uint)1;
            var actual = Flux.BitConverter.BigEndian.ToUInt32(new byte[] { 0, 0, 0, 1 }, 0);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void BigEndianToUInt64()
        {
            var expected = (ulong)1;
            var actual = Flux.BitConverter.BigEndian.ToUInt64(new byte[] { 0, 0, 0, 0, 0, 0, 0, 1 }, 0);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void LittleEndianToChar()
        {
            var expected = (char)1;
            var actual = Flux.BitConverter.LittleEndian.ToChar(new byte[] { 1, 0 }, 0);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LittleEndianToDecimal()
        {
            var expected = (decimal)1;
            var actual = Flux.BitConverter.LittleEndian.ToDecimal(new byte[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 0);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LittleEndianToDouble()
        {
            var expected = (double)1;
            var actual = Flux.BitConverter.LittleEndian.ToDouble(new byte[] { 0, 0, 0, 0, 0, 0, 240, 63 }, 0);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LittleEndianToFloat()
        {
            var expected = (float)1;
            var actual = Flux.BitConverter.LittleEndian.ToSingle(new byte[] { 0, 0, 128, 63 }, 0);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LittleEndianToInt16()
        {
            var expected = (short)1;
            var actual = Flux.BitConverter.LittleEndian.ToInt16(new byte[] { 1, 0 }, 0);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LittleEndianToInt32()
        {
            var expected = (int)1;
            var actual = Flux.BitConverter.LittleEndian.ToInt32(new byte[] { 1, 0, 0, 0 }, 0);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LittleEndianToInt64()
        {
            var expected = (long)1;
            var actual = Flux.BitConverter.LittleEndian.ToInt64(new byte[] { 1, 0, 0, 0, 0, 0, 0, 0 }, 0);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LittleEndianToUInt16()
        {
            var expected = (ushort)1;
            var actual = Flux.BitConverter.LittleEndian.ToUInt16(new byte[] { 1, 0 }, 0);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LittleEndianToUInt32()
        {
            var expected = (uint)1;
            var actual = Flux.BitConverter.LittleEndian.ToUInt32(new byte[] { 1, 0, 0, 0 }, 0);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LittleEndianToUInt64()
        {
            var expected = (ulong)1;
            var actual = Flux.BitConverter.LittleEndian.ToUInt64(new byte[] { 1, 0, 0, 0, 0, 0, 0, 0 }, 0);
            Assert.AreEqual(expected, actual);
        }
    }
}
