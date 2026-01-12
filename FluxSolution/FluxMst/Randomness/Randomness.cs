using System.Diagnostics;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluxMst.Randomness
{
  [TestClass]
  public class StaticCrypto
  {
    [TestMethod]
    public void BigInteger()
    {
      for (var i = 1; i < short.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.SscRng.Shared.NextBigInteger(i);

        Assert.IsTrue(rv >= 0 && rv < (System.Numerics.BigInteger.One << i));
      }

      for (var i = 1; i < short.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.SscRng.Shared.NextNumber<System.Numerics.BigInteger>(i);

        Assert.IsTrue(rv >= 0 && rv < i);
      }

      for (var i = short.MinValue; i < 0; i++)
      {
        var rv = Flux.RandomNumberGenerators.SscRng.Shared.NextNumber<System.Numerics.BigInteger>(i, 0);

        Assert.IsTrue(rv >= i && rv < 0);
      }
    }

    [TestMethod]
    public void DateTime()
    {
      for (var i = 0; i < short.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.SscRng.Shared.NextDateTime();

        Assert.IsTrue(rv >= System.DateTime.MinValue && rv < System.DateTime.MaxValue);
      }

      for (var i = 0; i < short.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.SscRng.Shared.NextDateTime(System.DateTime.MinValue, System.DateTime.MaxValue);

        Assert.IsTrue(rv >= System.DateTime.MinValue && rv < System.DateTime.MaxValue);
      }
    }

    [TestMethod]
    public void Double()
    {
      for (var i = 0; i < short.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.SscRng.Shared.NextDouble();

        Assert.IsTrue(rv >= 0 && rv < 1.0);
      }

      for (var i = 1; i < short.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.SscRng.Shared.NextDouble(i);

        Assert.IsTrue(rv >= 0 && rv < i);
      }

      for (var i = short.MinValue; i < 0; i++)
      {
        var rv = Flux.RandomNumberGenerators.SscRng.Shared.NextDouble(i, 0);

        Assert.IsTrue(rv >= i && rv < 0);
      }
    }

    //[TestMethod]
    //public void Int32()
    //{
    //	for (var i = 2; i < short.MaxValue; i++)
    //	{
    //		var rv = Flux.Random.NumberGenerator.Crypto.NextInt32(i);

    //		Assert.IsTrue(rv >= 0 && rv < int.MaxValue);
    //	}

    //	for (var i = 2; i < short.MaxValue; i++)
    //	{
    //		var rv = Flux.Random.NumberGenerator.Crypto.NextInt32(i);

    //		Assert.IsTrue(rv >= 0 && rv < i);
    //	}

    //	for (var i = short.MinValue; i <= -2; i++)
    //	{
    //		var rv = Flux.Random.NumberGenerator.Crypto.NextInt32(i, 0);

    //		Assert.IsTrue(rv >= i && rv < 0);
    //	}
    //}

    [TestMethod]
    public void Int64()
    {
      for (var i = 2; i < short.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.SscRng.Shared.NextInt64(i);

        Assert.IsTrue(rv >= 0L && rv < long.MaxValue);
      }

      for (var i = 2; i < short.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.SscRng.Shared.NextInt64(i);

        Assert.IsTrue(rv >= 0L && rv < (long)i);
      }

      for (var i = short.MinValue; i < -2; i++)
      {
        var rv = Flux.RandomNumberGenerators.SscRng.Shared.NextInt64(i, 0);

        Assert.IsTrue(rv >= (long)i && rv < 0L);
      }
    }

    //[TestMethod]
    //public void TimeSpan()
    //{
    //  for (var i = 1; i < short.MaxValue; i++)
    //  {
    //    var rv = Flux.Random.NumberGenerator.Crypto.NextTimeSpan(System.TimeSpan.MaxValue);

    //    Assert.IsTrue(rv >= System.TimeSpan.MinValue && rv < System.TimeSpan.MaxValue);
    //  }

    //  for (var i = 1; i < short.MaxValue; i++)
    //  {
    //    var rv = Flux.Random.NumberGenerator.Crypto.NextTimeSpan(System.TimeSpan.MaxValue);

    //    Assert.IsTrue(rv >= System.TimeSpan.MinValue && rv < System.TimeSpan.MaxValue);
    //  }

    //  for (var i = short.MinValue; i < 0; i++)
    //  {
    //    var rv = Flux.Random.NumberGenerator.Crypto.NextTimeSpan(System.TimeSpan.MinValue, System.TimeSpan.MaxValue);

    //    Assert.IsTrue(rv >= System.TimeSpan.MinValue && rv < System.TimeSpan.MaxValue);
    //  }
    //}
  }

  [TestClass]
  public class SscRng
  {
    [TestMethod]
    public void Next()
    {
      var cr = new Flux.RandomNumberGenerators.SscRng();

      for (var i = 0; i < short.MaxValue; i++)
      {
        var rv = cr.Next();
        Assert.IsTrue(rv >= 0 && rv < int.MaxValue);
        rv = cr.Next(i);
        Assert.IsTrue(rv >= 0 && rv < int.MaxValue);
      }

      for (var i = 1; i < short.MaxValue; i++)
      {
        var rv = cr.Next(i);

        Assert.IsTrue(rv >= 0 && rv < i);
      }

      for (var i = short.MinValue; i < 0; i++)
      {
        var rv = cr.Next(i, 0);

        Assert.IsTrue(rv >= i && rv < 0);
      }
    }

    [TestMethod]
    public void NextDouble()
    {
      var cr = new Flux.RandomNumberGenerators.SscRng();

      for (var i = 0; i < short.MaxValue; i++)
      {
        var rv = cr.NextDouble();

        Assert.IsTrue(rv >= 0 && rv < 1.0);
      }

      for (var i = 1; i < short.MaxValue; i++)
      {
        var rv = cr.NextDouble(i);

        Assert.IsTrue(rv >= 0 && rv < i);
      }

      for (var i = short.MinValue; i < 0; i++)
      {
        var rv = cr.NextDouble(i, 0);

        Assert.IsTrue(rv >= i && rv < 0);
      }
    }

    [TestMethod]
    public void NextBigInteger()
    {
      var cr = new Flux.RandomNumberGenerators.SscRng();

      for (var i = 1; i < short.MaxValue; i++)
      {
        var rv = cr.NextBigInteger(i);
        Assert.IsTrue(rv >= 0 && rv < (System.Numerics.BigInteger.One << i));
      }

      for (var i = 1; i < short.MaxValue; i++)
      {
        var rv = cr.NextNumber<System.Numerics.BigInteger>(i);
        Assert.IsTrue(rv >= 0 && rv < i);
      }

      for (var i = short.MinValue; i < 0; i++)
      {
        var rv = cr.NextNumber<System.Numerics.BigInteger>(i, 0);
        Assert.IsTrue(rv >= i && rv < 0);
      }
    }
  }

  [TestClass]
  public class IsaacRandom
  {
    [TestMethod]
    public void Next()
    {
      var cr = new Flux.RandomNumberGenerators.Isaac32();

      for (var i = 0; i < short.MaxValue; i++)
      {
        var rv = cr.Next();

        Assert.IsTrue(rv >= 0 && rv < int.MaxValue);
      }

      for (var i = 1; i < short.MaxValue; i++)
      {
        var rv = cr.Next(i);

        Assert.IsTrue(rv >= 0 && rv < i);
      }

      for (var i = short.MinValue; i < 0; i++)
      {
        var rv = cr.Next(i, 0);

        Assert.IsTrue(rv >= i && rv < 0);
      }
    }

    [TestMethod]
    public void NextDouble()
    {
      var cr = new Flux.RandomNumberGenerators.Isaac32();

      for (var i = 0; i < short.MaxValue; i++)
      {
        var rv = cr.NextDouble();

        Assert.IsTrue(rv >= 0 && rv < 1.0);
      }

      for (var i = 1; i < short.MaxValue; i++)
      {
        var rv = cr.NextDouble(i);

        Assert.IsTrue(rv >= 0 && rv < i);
      }

      for (var i = short.MinValue; i < 0; i++)
      {
        var rv = cr.NextDouble(i, 0);

        Assert.IsTrue(rv >= i && rv < 0);
      }
    }

    [TestMethod]
    public void NextBigInteger()
    {
      var cr = new Flux.RandomNumberGenerators.Isaac32();

      for (var i = 1; i < short.MaxValue; i++)
      {
        var rv = cr.NextBigInteger(i);
        Assert.IsTrue(rv >= 0 && rv < (System.Numerics.BigInteger.One << i));
      }

      for (var i = 1; i < short.MaxValue; i++)
      {
        var rv = cr.NextNumber<System.Numerics.BigInteger>(i);

        Assert.IsTrue(rv >= 0 && rv < i);
      }

      for (var i = sbyte.MinValue; i < 0; i++)
      {
        var rv = cr.NextNumber<System.Numerics.BigInteger>(i, 0);

        Assert.IsTrue(rv >= i && rv < 0);
      }
    }
  }

  [TestClass]
  public class SimpleRng
  {
    [TestMethod]
    public void Next_BigInteger_0()
    {
      for (var i = 1; i < sbyte.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.SimpleRng.Shared.NextBigInteger(i);
        Assert.IsTrue(rv >= 0 && rv < (System.Numerics.BigInteger.One << i));
      }
    }

    [TestMethod]
    public void Next_BigInteger_1()
    {
      for (var i = 1; i < sbyte.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.SimpleRng.Shared.NextNumber<System.Numerics.BigInteger>(i);
        Assert.IsTrue(rv >= 0 && rv < i);
      }
    }

    [TestMethod]
    public void Next_BigInteger_2()
    {
      for (var i = sbyte.MinValue; i < 0; i++)
      {
        var rv = Flux.RandomNumberGenerators.SimpleRng.Shared.NextNumber<System.Numerics.BigInteger>(i, 0);
        Assert.IsTrue(rv >= i && rv < 0);
      }
    }

    [TestMethod]
    public void Next_Double_0()
    {
      var cr = new Flux.RandomNumberGenerators.SimpleRng();

      for (var i = 0; i < short.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.SimpleRng.Shared.NextDouble();
        Assert.IsTrue(rv >= 0 && rv < 1.0);
      }
    }

    [TestMethod]
    public void Next_Double_1()
    {
      for (var i = 1; i < short.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.SimpleRng.Shared.NextDouble(i);
        Assert.IsTrue(rv >= 0 && rv < i);
      }
    }

    [TestMethod]
    public void Next_Double_2()
    {
      for (var i = short.MinValue; i < 0; i++)
      {
        var rv = Flux.RandomNumberGenerators.SimpleRng.Shared.NextDouble(i, 0);
        Assert.IsTrue(rv >= i && rv < 0);
      }
    }

    [TestMethod]
    public void Next_Int32_0()
    {
      for (var i = 1; i < short.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.SimpleRng.Shared.Next();
        Assert.IsTrue(rv >= 0 && rv < int.MaxValue);
      }
    }

    [TestMethod]
    public void Next_Int32_1()
    {
      for (var i = 1; i < short.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.SimpleRng.Shared.Next(i);
        Assert.IsTrue(rv >= 0 && rv < i);
      }
    }

    [TestMethod]
    public void Next_Int32_2()
    {
      for (var i = short.MinValue; i < 0; i++)
      {
        var rv = Flux.RandomNumberGenerators.SimpleRng.Shared.Next(i, 0);
        Assert.IsTrue(rv >= i && rv < 0);
      }
    }

    [TestMethod]
    public void Next_Int64_0()
    {
      for (var i = 0; i < short.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.SimpleRng.Shared.NextInt64();
        Assert.IsTrue(rv >= 0 && rv < long.MaxValue);
      }
    }

    [TestMethod]
    public void Next_Int64_1()
    {
      for (var i = 1; i < short.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.SimpleRng.Shared.NextInt64(i);
        Assert.IsTrue(rv >= 0 && rv < i);
      }
    }

    [TestMethod]
    public void Next_Int64_2()
    {
      for (var i = short.MinValue; i < 0; i++)
      {
        var rv = Flux.RandomNumberGenerators.SimpleRng.Shared.NextInt64(i, 0);
        Assert.IsTrue(rv >= i && rv < 0);
      }
    }
  }

  [TestClass]
  public class SplitMix64
  {
    [TestMethod]
    public void Next_BigInteger_0()
    {
      for (var i = 1; i < sbyte.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.SplitMix64.Shared.NextBigInteger(i);
        Assert.IsTrue(rv >= 0 && rv < (System.Numerics.BigInteger.One << i));
      }
    }

    [TestMethod]
    public void Next_BigInteger_1()
    {
      for (var i = 1; i < sbyte.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.SplitMix64.Shared.NextNumber<System.Numerics.BigInteger>(i);
        Assert.IsTrue(rv >= 0 && rv < i);
      }
    }

    [TestMethod]
    public void Next_BigInteger_2()
    {
      for (var i = sbyte.MinValue; i < 0; i++)
      {
        var rv = Flux.RandomNumberGenerators.SplitMix64.Shared.NextNumber<System.Numerics.BigInteger>(i, 0);
        Assert.IsTrue(rv >= i && rv < 0);
      }
    }

    [TestMethod]
    public void Next_Double_0()
    {
      for (var i = 0; i < short.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.SplitMix64.Shared.NextDouble();
        Assert.IsTrue(rv >= 0 && rv < 1.0);
      }
    }

    [TestMethod]
    public void Next_Double_1()
    {
      for (var i = 1; i < short.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.SplitMix64.Shared.NextDouble(i);
        Assert.IsTrue(rv >= 0 && rv < i);
      }
    }

    [TestMethod]
    public void Next_Double_2()
    {
      for (var i = short.MinValue; i < 0; i++)
      {
        var rv = Flux.RandomNumberGenerators.SplitMix64.Shared.NextDouble(i, 0);
        Assert.IsTrue(rv >= i && rv < 0);
      }
    }

    [TestMethod]
    public void Next_Int32_0()
    {
      for (var i = 1; i < short.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.SplitMix64.Shared.Next();
        Assert.IsTrue(rv >= 0 && rv < int.MaxValue);
      }
    }

    [TestMethod]
    public void Next_Int32_1()
    {
      for (var i = 1; i < short.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.SplitMix64.Shared.Next(i);
        Assert.IsTrue(rv >= 0 && rv < i);
      }
    }

    [TestMethod]
    public void Next_Int32_2()
    {
      for (var i = short.MinValue; i < 0; i++)
      {
        var rv = Flux.RandomNumberGenerators.SplitMix64.Shared.Next(i, 0);
        Assert.IsTrue(rv >= i && rv < 0);
      }
    }

    [TestMethod]
    public void Next_Int64_0()
    {
      for (var i = 0; i < short.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.SplitMix64.Shared.NextInt64();
        Assert.IsTrue(rv >= 0 && rv < long.MaxValue);
      }
    }

    [TestMethod]
    public void Next_Int64_1()
    {
      for (var i = 1; i < short.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.SplitMix64.Shared.NextInt64(i);
        Assert.IsTrue(rv >= 0 && rv < i);
      }
    }

    [TestMethod]
    public void Next_Int64_2()
    {
      for (var i = short.MinValue; i < 0; i++)
      {
        var rv = Flux.RandomNumberGenerators.SplitMix64.Shared.NextInt64(i, 0);
        Assert.IsTrue(rv >= i && rv < 0);
      }
    }
  }

  [TestClass]
  public class Xoshiro128P
  {
    [TestMethod]
    public void Next_BigInteger_0()
    {
      for (var i = 1; i < sbyte.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro128P.Shared.NextBigInteger(i);
        Assert.IsTrue(rv >= 0 && rv < (System.Numerics.BigInteger.One << i));
      }
    }

    [TestMethod]
    public void Next_BigInteger_1()
    {
      for (var i = 1; i < sbyte.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro128P.Shared.NextNumber<System.Numerics.BigInteger>(i);
        Assert.IsTrue(rv >= 0 && rv < i);
      }
    }

    [TestMethod]
    public void Next_BigInteger_2()
    {
      for (var i = sbyte.MinValue; i < 0; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro128P.Shared.NextNumber<System.Numerics.BigInteger>(i, 0);
        Assert.IsTrue(rv >= i && rv < 0);
      }
    }

    [TestMethod]
    public void Next_Double_0()
    {
      var cr = new Flux.RandomNumberGenerators.Xoshiro128P();

      for (var i = 0; i < short.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro128P.Shared.NextDouble();
        Assert.IsTrue(rv >= 0 && rv < 1.0);
      }
    }

    [TestMethod]
    public void Next_Double_1()
    {
      for (var i = 1; i < short.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro128P.Shared.NextDouble(i);
        Assert.IsTrue(rv >= 0 && rv < i);
      }
    }

    [TestMethod]
    public void Next_Double_2()
    {
      for (var i = short.MinValue; i < 0; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro128P.Shared.NextDouble(i, 0);
        Assert.IsTrue(rv >= i && rv < 0);
      }
    }

    [TestMethod]
    public void Next_Int32_0()
    {
      for (var i = 1; i < short.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro128P.Shared.Next();
        Assert.IsTrue(rv >= 0 && rv < int.MaxValue);
      }
    }

    [TestMethod]
    public void Next_Int32_1()
    {
      for (var i = 1; i < short.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro128P.Shared.Next(i);
        Assert.IsTrue(rv >= 0 && rv < i);
      }
    }

    [TestMethod]
    public void Next_Int32_2()
    {
      for (var i = short.MinValue; i < 0; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro128P.Shared.Next(i, 0);
        Assert.IsTrue(rv >= i && rv < 0);
      }
    }

    [TestMethod]
    public void Next_Int64_0()
    {
      for (var i = 0; i < short.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro128P.Shared.NextInt64();
        Assert.IsTrue(rv >= 0 && rv < long.MaxValue);
      }
    }

    [TestMethod]
    public void Next_Int64_1()
    {
      for (var i = 1; i < short.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro128P.Shared.NextInt64(i);
        Assert.IsTrue(rv >= 0 && rv < i);
      }
    }

    [TestMethod]
    public void Next_Int64_2()
    {
      for (var i = short.MinValue; i < 0; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro128P.Shared.NextInt64(i, 0);
        Assert.IsTrue(rv >= i && rv < 0);
      }
    }
  }

  [TestClass]
  public class Xoshiro128SS
  {
    [TestMethod]
    public void Next_BigInteger_0()
    {
      for (var i = 1; i < sbyte.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro128SS.Shared.NextBigInteger(i);
        Assert.IsTrue(rv >= 0 && rv < (System.Numerics.BigInteger.One << i));
      }
    }

    [TestMethod]
    public void Next_BigInteger_1()
    {
      for (var i = 1; i < sbyte.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro128SS.Shared.NextNumber<System.Numerics.BigInteger>(i);
        Assert.IsTrue(rv >= 0 && rv < i);
      }
    }

    [TestMethod]
    public void Next_BigInteger_2()
    {
      for (var i = sbyte.MinValue; i < 0; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro128SS.Shared.NextNumber<System.Numerics.BigInteger>(i, 0);
        Assert.IsTrue(rv >= i && rv < 0);
      }
    }

    [TestMethod]
    public void Next_Double_0()
    {
      var cr = new Flux.RandomNumberGenerators.Xoshiro128SS();

      for (var i = 0; i < short.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro128SS.Shared.NextDouble();
        Assert.IsTrue(rv >= 0 && rv < 1.0);
      }
    }

    [TestMethod]
    public void Next_Double_1()
    {
      for (var i = 1; i < short.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro128SS.Shared.NextDouble(i);
        Assert.IsTrue(rv >= 0 && rv < i);
      }
    }

    [TestMethod]
    public void Next_Double_2()
    {
      for (var i = short.MinValue; i < 0; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro128SS.Shared.NextDouble(i, 0);
        Assert.IsTrue(rv >= i && rv < 0);
      }
    }

    [TestMethod]
    public void Next_Int32_0()
    {
      for (var i = 1; i < short.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro128SS.Shared.Next();
        Assert.IsTrue(rv >= 0 && rv < int.MaxValue);
      }
    }

    [TestMethod]
    public void Next_Int32_1()
    {
      for (var i = 1; i < short.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro128SS.Shared.Next(i);
        Assert.IsTrue(rv >= 0 && rv < i);
      }
    }

    [TestMethod]
    public void Next_Int32_2()
    {
      for (var i = short.MinValue; i < 0; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro128SS.Shared.Next(i, 0);
        Assert.IsTrue(rv >= i && rv < 0);
      }
    }

    [TestMethod]
    public void Next_Int64_0()
    {
      for (var i = 0; i < short.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro128SS.Shared.NextInt64();
        Assert.IsTrue(rv >= 0 && rv < long.MaxValue);
      }
    }

    [TestMethod]
    public void Next_Int64_1()
    {
      for (var i = 1; i < short.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro128SS.Shared.NextInt64(i);
        Assert.IsTrue(rv >= 0 && rv < i);
      }
    }

    [TestMethod]
    public void Next_Int64_2()
    {
      for (var i = short.MinValue; i < 0; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro128SS.Shared.NextInt64(i, 0);
        Assert.IsTrue(rv >= i && rv < 0);
      }
    }
  }

  [TestClass]
  public class Xoshiro256P
  {
    [TestMethod]
    public void Next_BigInteger_0()
    {
      for (var i = 1; i < sbyte.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro256P.Shared.NextBigInteger(i);
        Assert.IsTrue(rv >= 0 && rv < (System.Numerics.BigInteger.One << i));
      }
    }

    [TestMethod]
    public void Next_BigInteger_1()
    {
      for (var i = 1; i < sbyte.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro256P.Shared.NextNumber<System.Numerics.BigInteger>(i);
        Assert.IsTrue(rv >= 0 && rv < i);
      }
    }

    [TestMethod]
    public void Next_BigInteger_2()
    {
      for (var i = sbyte.MinValue; i < 0; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro256P.Shared.NextNumber<System.Numerics.BigInteger>(i, 0);
        Assert.IsTrue(rv >= i && rv < 0);
      }
    }

    [TestMethod]
    public void Next_Double_0()
    {
      for (var i = 0; i < short.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro256P.Shared.NextDouble();
        Assert.IsTrue(rv >= 0 && rv < 1.0);
      }
    }

    [TestMethod]
    public void Next_Double_1()
    {
      for (var i = 1; i < short.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro256P.Shared.NextDouble(i);
        Assert.IsTrue(rv >= 0 && rv < i);
      }
    }

    [TestMethod]
    public void Next_Double_2()
    {
      for (var i = short.MinValue; i < 0; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro256P.Shared.NextDouble(i, 0);
        Assert.IsTrue(rv >= i && rv < 0);
      }
    }

    [TestMethod]
    public void Next_Int32_0()
    {
      for (var i = 1; i < short.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro256P.Shared.Next();
        Assert.IsTrue(rv >= 0 && rv < int.MaxValue);
      }
    }

    [TestMethod]
    public void Next_Int32_1()
    {
      for (var i = 1; i < short.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro256P.Shared.Next(i);
        Assert.IsTrue(rv >= 0 && rv < i);
      }
    }

    [TestMethod]
    public void Next_Int32_2()
    {
      for (var i = short.MinValue; i < 0; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro256P.Shared.Next(i, 0);
        Assert.IsTrue(rv >= i && rv < 0);
      }
    }

    [TestMethod]
    public void Next_Int64_0()
    {
      for (var i = 0; i < short.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro256P.Shared.NextInt64();
        Assert.IsTrue(rv >= 0 && rv < long.MaxValue);
      }
    }

    [TestMethod]
    public void Next_Int64_1()
    {
      for (var i = 1; i < short.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro256P.Shared.NextInt64(i);
        Assert.IsTrue(rv >= 0 && rv < i);
      }
    }

    [TestMethod]
    public void Next_Int64_2()
    {
      for (var i = short.MinValue; i < 0; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro256P.Shared.NextInt64(i, 0);
        Assert.IsTrue(rv >= i && rv < 0);
      }
    }
  }

  [TestClass]
  public class Xoshiro256SS
  {
    [TestMethod]
    public void Next_BigInteger_0()
    {
      for (var i = 1; i < sbyte.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro256SS.Shared.NextBigInteger(i);
        Assert.IsTrue(rv >= 0 && rv < (System.Numerics.BigInteger.One << i));
      }
    }

    [TestMethod]
    public void Next_BigInteger_1()
    {
      for (var i = 1; i < sbyte.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro256SS.Shared.NextNumber<System.Numerics.BigInteger>(i);
        Assert.IsTrue(rv >= 0 && rv < i);
      }
    }

    [TestMethod]
    public void Next_BigInteger_2()
    {
      for (var i = sbyte.MinValue; i < 0; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro256SS.Shared.NextNumber<System.Numerics.BigInteger>(i, 0);
        Assert.IsTrue(rv >= i && rv < 0);
      }
    }

    [TestMethod]
    public void Next_Double_0()
    {
      for (var i = 0; i < short.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro256SS.Shared.NextDouble();
        Assert.IsTrue(rv >= 0 && rv < 1.0);
      }
    }

    [TestMethod]
    public void Next_Double_1()
    {
      for (var i = 1; i < short.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro256SS.Shared.NextDouble(i);
        Assert.IsTrue(rv >= 0 && rv < i);
      }
    }

    [TestMethod]
    public void Next_Double_2()
    {
      for (var i = short.MinValue; i < 0; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro256SS.Shared.NextDouble(i, 0);
        Assert.IsTrue(rv >= i && rv < 0);
      }
    }

    [TestMethod]
    public void Next_Int32_0()
    {
      for (var i = 1; i < short.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro256SS.Shared.Next();
        Assert.IsTrue(rv >= 0 && rv < int.MaxValue);
      }
    }

    [TestMethod]
    public void Next_Int32_1()
    {
      for (var i = 1; i < short.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro256SS.Shared.Next(i);
        Assert.IsTrue(rv >= 0 && rv < i);
      }
    }

    [TestMethod]
    public void Next_Int32_2()
    {
      for (var i = short.MinValue; i < 0; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro256SS.Shared.Next(i, 0);
        Assert.IsTrue(rv >= i && rv < 0);
      }
    }

    [TestMethod]
    public void Next_Int64_0()
    {
      for (var i = 0; i < short.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro256SS.Shared.NextInt64();
        Assert.IsTrue(rv >= 0 && rv < long.MaxValue);
      }
    }

    [TestMethod]
    public void Next_Int64_1()
    {
      for (var i = 1; i < short.MaxValue; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro256SS.Shared.NextInt64(i);
        Assert.IsTrue(rv >= 0 && rv < i);
      }
    }

    [TestMethod]
    public void Next_Int64_2()
    {
      for (var i = short.MinValue; i < 0; i++)
      {
        var rv = Flux.RandomNumberGenerators.Xoshiro256SS.Shared.NextInt64(i, 0);
        Assert.IsTrue(rv >= i && rv < 0);
      }
    }
  }
}
