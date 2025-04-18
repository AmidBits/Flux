﻿using System;
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
      var expected = @"robert hugo";
      var actual = @"Robert Hugo".AsSpan().AsSpan().ToLower();
      Assert.AreEqual(expected, actual.ToString());
    }

    [TestMethod]
    public void ToUpperCase()
    {
      var expected = @"ROBERT HUGO";
      var actual = @"Robert Hugo".AsSpan().AsSpan().ToUpper();
      Assert.AreEqual(expected, actual.ToString());
    }
  }
}
