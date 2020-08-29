using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Flux;

namespace IFormatProvider
{
  //[TestClass]
  //public class DEA
  //{
  //  string _dea1 = @"FN5623740-001AB";

  //  [TestMethod]
  //  public void Formatter()
  //  {
  //    Assert.AreEqual(string.Format(new Flux.IFormatProvider.DeaFormatter(), @"{0:DEA}", _dea1), _dea1);
  //  }

  //  [TestMethod]
  //  public void TryParse()
  //  {
  //    Flux.IFormatProvider.DeaFormatter.TryParse(_dea1, out var dea1);

  //    Assert.AreEqual(_dea1, dea1);
  //  }
  //}

  [TestClass]
  public class DMS
  {
    string _dms1 = @"40 11 15";
    double _dms1tp = 40.1875;


    [TestMethod]
    public void Formatter()
    {
      Assert.AreEqual(string.Format(new Flux.IFormatProvider.DmsFormatter(), @"{0:DMS}", _dms1), _dms1);
    }

    [TestMethod]
    public void TryParse()
    {
      Flux.IFormatProvider.DmsFormatter.TryParse(_dms1, out var dms1tp);

      Assert.AreEqual(_dms1tp, dms1tp);
    }
  }

  //[TestClass]
  //public class NANP
  //{
  //  string _nanp7 = @"219-6204";
  //  string _nanp10 = @"520-219-6204";
  //  string _nanp11 = @"1-520-219-6204";


  //  [TestMethod]
  //  public void Formatter()
  //  {
  //    Assert.AreEqual(string.Format(new Flux.IFormatProvider.NanpFormatter(), @"{0:NANP}", _nanp7), _nanp7);

  //    Assert.AreEqual(string.Format(new Flux.IFormatProvider.NanpFormatter(), @"{0:NANP}", _nanp10), _nanp10);

  //    Assert.AreEqual(string.Format(new Flux.IFormatProvider.NanpFormatter(), @"{0:NANP}", _nanp11), _nanp11);
  //  }

  //  [TestMethod]
  //  public void TryParse()
  //  {
  //    Flux.IFormatProvider.NanpFormatter.TryParse(_nanp7, out var nanp7);

  //    Assert.AreEqual(_nanp7, nanp7);

  //    Flux.IFormatProvider.NanpFormatter.TryParse(_nanp10, out var nanp10);

  //    Assert.AreEqual(_nanp10, nanp10);

  //    Flux.IFormatProvider.NanpFormatter.TryParse(_nanp11, out var nanp11);

  //    Assert.AreEqual(_nanp11, nanp11);
  //  }
  //}

  [TestClass]
  public class RADIX
  {
    System.Numerics.BigInteger _radix10 = 32;
    string _radix16 = @"20";


    [TestMethod]
    public void Formatter()
    {
      Assert.AreEqual(_radix16, string.Format(new Flux.IFormatProvider.RadixFormatter(), "{0:RADIX16}", _radix10));
    }

    [TestMethod]
    public void TryParse()
    {
      //Flux.IFormatProvider.RadixFormatter.TryParse(_radix16, Flux.IFormatProvider.RadixFormatter.DefaultNumerals.Take(16).ToArray(), out var radix10);

      //Assert.AreEqual(_radix10, radix10);
    }

    [TestMethod]
    public void TryParseFrom()
    {
      //Flux.IFormatProvider.RadixFormatter.TryConvertFrom(_radix16, Flux.IFormatProvider.RadixFormatter.DefaultNumerals.Take(16).ToArray(), out var radix10);

      //Assert.AreEqual(_radix10, radix10);
    }

    [TestMethod]
    public void TryParseTo()
    {
      //Flux.IFormatProvider.RadixFormatter.TryConvertTo(_radix10, Flux.IFormatProvider.RadixFormatter.DefaultNumerals.Take(16).ToArray(), out var radix16);

      //Assert.AreEqual(radix16, _radix16);
    }
  }

  //[TestClass]
  //public class SSN
  //{
  //  string _ssn1 = @"101-13-1911";


  //  [TestMethod]
  //  public void Formatter()
  //  {
  //    Assert.AreEqual(string.Format(new Flux.IFormatProvider.SsnFormatter(), @"{0:SSN}", _ssn1), _ssn1);
  //  }

  //  [TestMethod]
  //  public void TryParse()
  //  {
  //    Flux.IFormatProvider.SsnFormatter.TryParse(_ssn1, out var ssn1);

  //    Assert.AreEqual(_ssn1, ssn1);
  //  }
  //}
}
