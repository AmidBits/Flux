using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Flux;

namespace Convert
{
  [TestClass]
  public class Convert
  {
    readonly char c = 'A';

    readonly System.DateTime target = new System.DateTime(2017, 5, 30).AddMonths(6);

    [TestMethod]
    public void Em_Convert_ChangeType()
    {
      Assert.AreEqual(65, Flux.Convert.ChangeType<byte>(c, null));
      Assert.AreEqual(65D, Flux.Convert.ChangeType(c, null, new System.Type[] { typeof(int), typeof(double) }));
    }

    [TestMethod]
    public void Em_Convert_TypeConverter()
    {
      Assert.AreEqual("5/30/1967", Flux.Convert.TypeConverter<string>(System.DateTime.Parse("05/30/1967"), null));
    }
  }
}
