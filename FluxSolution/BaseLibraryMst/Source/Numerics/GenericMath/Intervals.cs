#if NET7_0_OR_GREATER
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenericMath
{
    [TestClass]
    public class Intervals
    {
        //[TestMethod]
        //public void Distance()
        //{
        //	Assert.AreEqual(5.0, (5.0).Distance(10));
        //}

        [TestMethod]
        public void Fold()
        {
            Assert.AreEqual(3.5, (6.5).Fold(0, 5));
        }

        [TestMethod]
        public void Rescale()
        {
            Assert.AreEqual(25, (7.5).Rescale(0, 5, 10, 20));
        }

        [TestMethod]
        public void Wrap()
        {
            Assert.AreEqual(2.5, (7.5).Wrap(0, 5));
        }
    }
}
#endif
