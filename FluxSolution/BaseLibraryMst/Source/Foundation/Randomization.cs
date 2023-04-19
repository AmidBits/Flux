using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Foundation.Randomization
{
  [TestClass]
  public class StaticCrypto
  {
    [TestMethod]
    public void BigInteger()
    {
      for (var i = 0; i < short.MaxValue; i++)
      {
        var rv = Flux.Random.NumberGenerators.Crypto.NextBigInteger(i);

        Assert.IsTrue(rv >= 0 && rv < (System.Numerics.BigInteger.One << i));
      }

      for (var i = 1; i < short.MaxValue; i++)
      {
        var rv = Flux.Random.NumberGenerators.Crypto.NextBigInteger(i.ToBigInteger());

        Assert.IsTrue(rv >= 0 && rv < i);
      }

      for (var i = short.MinValue; i < 0; i++)
      {
        var rv = Flux.Random.NumberGenerators.Crypto.NextBigInteger(i, 0);

        Assert.IsTrue(rv >= i && rv < 0);
      }
    }

    [TestMethod]
    public void DateTime()
    {
      for (var i = 0; i < short.MaxValue; i++)
      {
        var rv = Flux.Random.NumberGenerators.Crypto.NextDateTime();

        Assert.IsTrue(rv >= System.DateTime.MinValue && rv < System.DateTime.MaxValue);
      }

      for (var i = 0; i < short.MaxValue; i++)
      {
        var rv = Flux.Random.NumberGenerators.Crypto.NextDateTime(System.DateTime.MinValue, System.DateTime.MaxValue);

        Assert.IsTrue(rv >= System.DateTime.MinValue && rv < System.DateTime.MaxValue);
      }
    }

    [TestMethod]
    public void Double()
    {
      for (var i = 0; i < short.MaxValue; i++)
      {
        var rv = Flux.Random.NumberGenerators.Crypto.NextDouble();

        Assert.IsTrue(rv >= 0 && rv < 1.0);
      }

      for (var i = 1; i < short.MaxValue; i++)
      {
        var rv = Flux.Random.NumberGenerators.Crypto.NextDouble(i);

        Assert.IsTrue(rv >= 0 && rv < i);
      }

      for (var i = short.MinValue; i < 0; i++)
      {
        var rv = Flux.Random.NumberGenerators.Crypto.NextDouble(i, 0);

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
        var rv = Flux.Random.NumberGenerators.Crypto.NextInt64(i);

        Assert.IsTrue(rv >= 0L && rv < long.MaxValue);
      }

      for (var i = 2; i < short.MaxValue; i++)
      {
        var rv = Flux.Random.NumberGenerators.Crypto.NextInt64(i);

        Assert.IsTrue(rv >= 0L && rv < (long)i);
      }

      for (var i = short.MinValue; i < -2; i++)
      {
        var rv = Flux.Random.NumberGenerators.Crypto.NextInt64(i, 0);

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
  public class CryptoRandom
  {
    [TestMethod]
    public void Next()
    {
      var cr = new Flux.Random.Cryptographic();

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
      var cr = new Flux.Random.Cryptographic();

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
      var cr = new Flux.Random.Cryptographic();

      for (var i = 0; i < short.MaxValue; i++)
      {
        var rv = cr.NextBigInteger(i);

        Assert.IsTrue(rv >= 0 && rv < (System.Numerics.BigInteger.One << i));
      }

      for (var i = 1; i < short.MaxValue; i++)
      {
        var rv = cr.NextBigInteger(i.ToBigInteger());

        Assert.IsTrue(rv >= 0 && rv < i);
      }

      for (var i = short.MinValue; i < 0; i++)
      {
        var rv = cr.NextBigInteger(i, 0);

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
      var cr = new Flux.Random.IsaacRandom();

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
      var cr = new Flux.Random.IsaacRandom();

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
      var cr = new Flux.Random.IsaacRandom();

      for (var i = 0; i < short.MaxValue; i++)
      {
        var rv = cr.NextBigInteger(i);

        Assert.IsTrue(rv >= 0 && rv < (System.Numerics.BigInteger.One << i));
      }

      for (var i = 1; i < short.MaxValue; i++)
      {
        var rv = cr.NextBigInteger(i.ToBigInteger());

        Assert.IsTrue(rv >= 0 && rv < i);
      }

      for (var i = short.MinValue; i < 0; i++)
      {
        var rv = cr.NextBigInteger(i, 0);

        Assert.IsTrue(rv >= i && rv < 0);
      }
    }
  }

  [TestClass]
  public class SplitMix64
  {
    [TestMethod]
    public void Next()
    {
      var cr = new Flux.Random.SplitMix64();

      for (var i = 0; i < short.MaxValue; i++)
      {
        var rv = cr.Next();

        Assert.IsTrue(rv >= 0 && rv < int.MaxValue);
      }

      for (var i = 2; i < short.MaxValue; i++)
      {
        var rv = cr.Next(i);

        Assert.IsTrue(rv >= 0 && rv < i);
      }

      for (var i = short.MinValue; i <= -2; i++)
      {
        var rv = cr.Next(i, 0);

        Assert.IsTrue(rv >= i && rv < 0);
      }
    }

    [TestMethod]
    public void NextDouble()
    {
      var cr = new Flux.Random.SplitMix64();

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
      var cr = new Flux.Random.SplitMix64();

      for (var i = 0; i < short.MaxValue; i++)
      {
        var rv = cr.NextBigInteger(i);

        Assert.IsTrue(rv >= 0 && rv < (System.Numerics.BigInteger.One << i));
      }

      for (var i = 1; i < short.MaxValue; i++)
      {
        var rv = cr.NextBigInteger(i.ToBigInteger());

        //var rv = cr.NextBigInteger(i.ToBigInteger());
        Assert.IsTrue(rv >= 0 && rv < i);
      }

      for (var i = short.MinValue; i < 0; i++)
      {
        var rv = cr.NextBigInteger(i, 0);

        Assert.IsTrue(rv >= i && rv < 0);
      }
    }
  }

  [TestClass]
  public class Xoshiro256P
  {
    [TestMethod]
    public void Next()
    {
      var cr = new Flux.Random.Xoshiro256P();

      for (var i = 0; i < short.MaxValue; i++)
      {
        var rv = cr.Next();

        Assert.IsTrue(rv >= 0 && rv < int.MaxValue);
      }

      for (var i = 2; i < short.MaxValue; i++)
      {
        var rv = cr.Next(i);

        Assert.IsTrue(rv >= 0 && rv < i);
      }

      for (var i = short.MinValue; i <= -2; i++)
      {
        var rv = cr.Next(i, 0);

        Assert.IsTrue(rv >= i && rv < 0);
      }
    }

    [TestMethod]
    public void NextDouble()
    {
      var cr = new Flux.Random.Xoshiro256P();

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
      var cr = new Flux.Random.Xoshiro256P();

      for (var i = 0; i < short.MaxValue; i++)
      {
        var rv = cr.NextBigInteger(i);

        Assert.IsTrue(rv >= 0 && rv < (System.Numerics.BigInteger.One << i));
      }

      for (var i = 1; i < short.MaxValue; i++)
      {
        var rv = cr.NextBigInteger(i.ToBigInteger());

        Assert.IsTrue(rv >= 0 && rv < i);
      }

      for (var i = short.MinValue; i < 0; i++)
      {
        var rv = cr.NextBigInteger(i, 0);

        Assert.IsTrue(rv >= i && rv < 0);
      }
    }
  }

  [TestClass]
  public class Xoshiro256SS
  {
    [TestMethod]
    public void Next()
    {
      var cr = new Flux.Random.Xoshiro256SS();

      for (var i = 0; i < short.MaxValue; i++)
      {
        var rv = cr.Next();

        Assert.IsTrue(rv >= 0 && rv < int.MaxValue);
      }

      for (var i = 2; i < short.MaxValue; i++)
      {
        var rv = cr.Next(i);

        Assert.IsTrue(rv >= 0 && rv < i);
      }

      for (var i = short.MinValue; i <= -2; i++)
      {
        var rv = cr.Next(i, 0);

        Assert.IsTrue(rv >= i && rv < 0);
      }
    }

    [TestMethod]
    public void NextDouble()
    {
      var cr = new Flux.Random.Xoshiro256SS();

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
      var cr = new Flux.Random.Xoshiro256SS();

      for (var i = 0; i < short.MaxValue; i++)
      {
        var rv = cr.NextBigInteger(i);

        Assert.IsTrue(rv >= 0 && rv < (System.Numerics.BigInteger.One << i));
      }

      for (var i = 1; i < short.MaxValue; i++)
      {
        var rv = cr.NextBigInteger(i.ToBigInteger());

        Assert.IsTrue(rv >= 0 && rv < i);
      }

      for (var i = short.MinValue; i < 0; i++)
      {
        var rv = cr.NextBigInteger(i, 0);

        Assert.IsTrue(rv >= i && rv < 0);
      }
    }
  }
}
