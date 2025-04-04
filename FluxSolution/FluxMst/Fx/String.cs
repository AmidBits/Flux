﻿using System;
using System.Linq;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SystemFx
{
  [TestClass]
  public class String
  {
    readonly string s1 = "something <oh well> else";
    readonly string s2 = "something <oh <well> else";

    [TestMethod]
    public void IsBalanced()
    {
      Assert.AreEqual(true, s1.AsSpan().IsBalanced('<', '>'));
      Assert.AreEqual(false, s2.AsSpan().IsBalanced('<', '>'));
    }

    [TestMethod]
    public void CountEqualAtStart()
    {
      Assert.AreEqual(14, s1.AsSpan().CommonPrefixLength(s2));
    }

    //[TestMethod]
    //public void EqualsAnyAt()
    //{
    //  Assert.AreEqual(true, s1.AsSpan().EqualsAnyAt(4, "ph", "thi", "s"));
    //}

    [TestMethod]
    public void LeftMost()
    {
      Assert.AreEqual(s1, s1.AsSpan().LeftMost(100).ToString());
      Assert.AreEqual(s1.Substring(0, 4), s1.AsSpan().LeftMost(4).ToString());
    }

    [TestMethod]
    public void RightMost()
    {
      Assert.AreEqual(s1, s1.AsSpan().RightMost(100).ToString());
      Assert.AreEqual(s1.Substring(s1.Length - 4), s1.AsSpan().RightMost(4).ToString());
    }
  }
}
