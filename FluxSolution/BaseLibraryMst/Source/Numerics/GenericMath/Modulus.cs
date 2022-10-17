#if NET7_0_OR_GREATER
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Flux;
using System;

namespace GenericMath
{
  [TestClass]
  public class Modulus
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
    public void TruncMod()
    {
      var truncatedQuotient = (9.0).TruncMod(6, out var remainder);

      Assert.AreEqual(1, truncatedQuotient);
      Assert.AreEqual(3, remainder);
    }
  }
}
#endif
