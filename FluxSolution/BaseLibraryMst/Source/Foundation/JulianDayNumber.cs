//using System.Linq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//using Flux;

//namespace Foundation
//{
//  [TestClass]
//  public class JulianDayNumber
//  {
//    [TestMethod]
//    public void TimeIntervalBetweenTwo()
//    {
//      var jd1 = new Flux.JulianDayNumber(1910, 4, 20);
//      var jd2 = new Flux.JulianDayNumber(1986, 2, 9);

//      var diff12 = jd2.Value - jd1.Value;

//      Assert.AreEqual(27689, diff12);
//    }

//    [TestMethod]
//    public void ExactlyTenThousandDaysAfter()
//    {
//      var jd1 = new Flux.JulianDayNumber(1991,7,11);
//      var jd2 = jd1.AddDays(10000);

//      var diff12 = jd2.Value - jd1.Value;

//      Assert.AreEqual(10000, diff12);
//    }
//  }
//}
