using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Flux;

namespace Formatting
{
  [TestClass]
  public class Dms
  {
    string _dms1 = @"40 11 15";
    double _dms1tp = 40.1875;

    [TestMethod]
    public void Formatter()
    {
      Assert.AreEqual(string.Format(new Flux.Formatting.DmsFormatter(), @"{0:DMS}", _dms1), _dms1);
    }

    [TestMethod]
    public void TryParse()
    {
      Flux.Formatting.DmsFormatter.TryParse(_dms1, out var dms1tp);

      Assert.AreEqual(_dms1tp, dms1tp);
    }
  }

  [TestClass]
  public class Radix
  {
    System.Numerics.BigInteger _radix10 = 32;
    string _radix16 = @"20";

    [TestMethod]
    public void Formatter()
    {
      Assert.AreEqual(_radix16, string.Format(new Flux.Formatting.RadixFormatter(), "{0:RADIX16}", _radix10));
    }
  }
}
