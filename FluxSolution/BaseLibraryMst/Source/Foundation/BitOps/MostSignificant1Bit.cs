using System;
using System.Linq;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Foundation.BitOps
{
  [TestClass]
  public class MostSignificant1Bit
  {
    readonly System.Numerics.BigInteger[] pbi = System.Linq.Enumerable.Range(0, 255).Select(i => (System.Numerics.BigInteger)i).ToArray();
    readonly int[] pi = System.Linq.Enumerable.Range(0, 255).Select(i => (int)i).ToArray();
    readonly long[] pl = System.Linq.Enumerable.Range(0, 255).Select(i => (long)i).ToArray();
    readonly uint[] pui = System.Linq.Enumerable.Range(0, 255).Select(i => (uint)i).ToArray();
    readonly ulong[] pul = System.Linq.Enumerable.Range(0, 255).Select(i => (ulong)i).ToArray();

    [TestMethod]
    public void MostSignificant1Bit_BigInteger()
    {
      Assert.AreEqual(pbi[16], Flux.BitOps.MostSignificant1Bit(pbi[18]));
    }
    [TestMethod]
    public void MostSignificant1Bit_BigInteger_Speed()
    {
      var value = System.Numerics.BigInteger.Parse("670530");
      var expected = 524288.ToBigInteger();
      Flux.Services.Performance.Measure(() => Flux.BitOps.MostSignificant1Bit(value), 1000000).Assert(expected, 1);
    }
    [TestMethod]
    public void MostSignificant1Bit_Int32()
    {
      Assert.AreEqual(pi[16], Flux.BitOps.MostSignificant1Bit(pi[18]));
    }
    [TestMethod]
    public void MostSignificant1Bit_Int32_Speed()
    {
      var value = 670530;
      var expected = 524288;
      Flux.Services.Performance.Measure(() => Flux.BitOps.MostSignificant1Bit(value), 1000000).Assert(expected, 0.3);
    }
    [TestMethod]
    public void MostSignificant1Bit_Int64()
    {
      Assert.AreEqual(pl[16], Flux.BitOps.MostSignificant1Bit(pl[18]));
    }
    [TestMethod]
    public void MostSignificant1Bit_Int64_Speed()
    {
      var value = 670530L;
      var expected = 524288L;
      Flux.Services.Performance.Measure(() => Flux.BitOps.MostSignificant1Bit(value), 1000000).Assert(expected, 0.3);
    }
    [TestMethod]
    public void MostSignificant1Bit_UInt32()
    {
      Assert.AreEqual(pui[16], Flux.BitOps.MostSignificant1Bit(pui[18]));
    }
    [TestMethod]
    public void MostSignificant1Bit_UInt32_Speed()
    {
      var value = 670530U;
      var expected = 524288U;
      Flux.Services.Performance.Measure(() => Flux.BitOps.MostSignificant1Bit(value), 1000000).Assert(expected, 0.3);
    }
    [TestMethod]
    public void MostSignificant1Bit_UInt64()
    {
      Assert.AreEqual(pul[16], Flux.BitOps.MostSignificant1Bit(pul[18]));
    }
    [TestMethod]
    public void MostSignificant1Bit_UInt64_Speed()
    {
      var value = 670530UL;
      var expected = 524288UL;
      Flux.Services.Performance.Measure(() => Flux.BitOps.MostSignificant1Bit(value), 1000000).Assert(expected, 0.3);
    }
  }
}
