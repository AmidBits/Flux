#if NET7_0_OR_GREATER
using System;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenericMath
{
  [TestClass]
  public class Modular
  {
    [TestMethod]
    public void DivMod()
    {
      var quotient = (9.0).DivMod(6, out var remainder);

      Assert.AreEqual(1.5, quotient);
      Assert.AreEqual(3, remainder);
    }

    [TestMethod]
    public void DivModTrunc()
    {
      var quotient = (9.0).DivModTrunc(6, out var remainder, out var truncatedQuotient);

      Assert.AreEqual(1.5, quotient);
      Assert.AreEqual(3, remainder);
      Assert.AreEqual(1, truncatedQuotient);
    }

    [TestMethod]
    public void Mod()
    {
      var remainder = (9.ToBigInteger()).Mod(6);

      Assert.AreEqual(3, remainder);
    }

    [TestMethod]
    public void ModInv()
    {
      var mi4and7 = Flux.Maths.ModInv(4, 7); // mi = 2, i.e. "2 is the modular multiplicative inverse of 4 (and vice versa), mod 7".;
      Assert.AreEqual(2, mi4and7);

      var mi8and11 = Flux.Maths.ModInv(8, 11); // mi = 7, i.e. "7 is the modular inverse of 8, mod 11".
      Assert.AreEqual(7, mi8and11);
    }

    [TestMethod]
    public void TruncMod()
    {
      var truncatedQuotient = (9.0).TruncMod(6, out var remainder);

      Assert.AreEqual(1, truncatedQuotient);
      Assert.AreEqual(3, remainder);
    }
  }
}
#endif
