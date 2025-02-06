//using System.Linq;
//using Flux;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace Text
//{
//  [TestClass]
//  public partial class HtmlEntityNumber
//  {
//    [TestMethod]
//    public void ReplaceCsEscapeSequence()
//    {
//      var html = "\\x60: The non-breaking hyphen (\\u8209) is used to define a hyphen character (-) that does not break (\\u060) into a new line.";

//      var expected = "0060: The non-breaking hyphen (2011) is used to define a hyphen character (-) that does not break (003C) into a new line.";
//      var actual = html.ReplaceCsEscapeSequence((s, r) => r.Value.ToString("X4"));

//      Assert.AreEqual(expected, actual);
//    }
//  }
//}
