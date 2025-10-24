using System;
using System.Linq;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SystemFx
{
  [TestClass]
  public class Span
  {

    [TestMethod]
    public void ToLowerCase()
    {
      var expected = @"robert hugo".AsSpan().AsSpan().ToString();
      var actual = @"Robert Hugo".AsSpan().AsSpan().ToLower().ToString();
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ToUpperCase()
    {
      var expected = @"ROBERT HUGO";
      var actual = @"Robert Hugo".AsSpan().AsSpan().ToUpper().ToString();
      Assert.AreEqual(expected, actual);
    }
  }
}
