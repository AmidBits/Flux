using Microsoft.VisualStudio.TestTools.UnitTesting;

using Flux;

namespace GenericMath
{
	[TestClass]
	public class Interpolations
	{
		[TestMethod]
		public void InterpolateCosine()
		{
			Assert.AreEqual(5.732233047033631, (5.0).InterpolateCosine(10, 0.25));
		}

    [TestMethod]
    public void InterpolateLinear()
    {
      Assert.AreEqual(6.25, (5.0).InterpolateLinear(10, 0.25));
    }
  }
}
