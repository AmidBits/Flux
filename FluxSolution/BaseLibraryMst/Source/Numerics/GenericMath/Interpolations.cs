﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

using Flux;

namespace GenericMath
{
  [TestClass]
  public class Interpolations
  {
    [TestMethod]
    public void InterpolateCosine()
    {
      var ic = new InterpolationCosine<double, double>();

      Assert.AreEqual(5.732233047033631, ic.Interpolate(5.0, 10, 0.25));
    }

    [TestMethod]
    public void InterpolateLinear()
    {
      var il = new InterpolationLinear<double, double>();

      Assert.AreEqual(6.25, il.Interpolate(5.0, 10, 0.25));
    }
  }
}