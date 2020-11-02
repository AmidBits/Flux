using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Flux;

namespace Types
{
  [TestClass]
  public class String
  {
    readonly string s1 = "something <oh well> else";
    readonly string s2 = "something <oh <well> else";

    [TestMethod]
    public void IsBalanced()
    {
      Assert.AreEqual(true, s1.IsBalanced('<', '>'));
      Assert.AreEqual(false, s2.IsBalanced('<', '>'));
    }

    [TestMethod]
    public void CountEqualAtStart()
    {
      Assert.AreEqual(14, s1.CountEqualAtStart(s2));
    }

    [TestMethod]
    public void EqualsAnyAt()
    {
      Assert.AreEqual(true, s1.EqualsAnyAt(4, "ph", "thi", "s"));
    }

    [TestMethod]
    public void LeftMost()
    {
      Assert.AreEqual(s1, s1.LeftMost(100));
      Assert.AreEqual(s1.Substring(0, 4), s1.LeftMost(4));
    }

    [TestMethod]
    public void RightMost()
    {
      Assert.AreEqual(s1, s1.RightMost(100));
      Assert.AreEqual(s1.Substring(s1.Length - 4), s1.RightMost(4));
    }
  }
}
