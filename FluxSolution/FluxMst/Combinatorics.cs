namespace Combinatorics
{
  [TestClass]
  public class Combinations
  {
    [TestMethod]
    public void CombinationsWithoutRepetition()
    {
      var n = 5;
      var k = 3;

      for (var i = Flux.IBinaryInteger.CountCombinationsWithoutRepetition(n, k) - 1; i >= 0; i--)
      {
        var ucn = Flux.Combinations.CombinatorialNumberSystem.UnrankCombinationWithoutRepetition(i, n, k);
        var rcn = Flux.Combinations.CombinatorialNumberSystem.RankCombinationWithoutRepetition(ucn, n, k);
        var ucn2 = Flux.Combinations.CombinatorialNumberSystem.UnrankCombinationWithoutRepetition(rcn, n, k);
        var rcn2 = Flux.Combinations.CombinatorialNumberSystem.RankCombinationWithoutRepetition(ucn2, n, k);

        CollectionAssert.AreEqual(ucn, ucn2);
        Assert.AreEqual(rcn, rcn2);
      }
    }

    [TestMethod]
    public void CombinationsWithRepetition()
    {
      var n = 5;
      var k = 3;

      for (var i = Flux.IBinaryInteger.CountCombinationsWithRepetition(n, k) - 1; i >= 0; i--)
      {
        var ucr = Flux.Combinations.CombinatorialNumberSystem.UnrankCombinationWithRepetition(i, n, k);
        var rcr = Flux.Combinations.CombinatorialNumberSystem.RankCombinationWithRepetition(ucr, n, k);
        var ucr2 = Flux.Combinations.CombinatorialNumberSystem.UnrankCombinationWithRepetition(rcr, n, k);
        var rcr2 = Flux.Combinations.CombinatorialNumberSystem.RankCombinationWithRepetition(ucr2, n, k);

        CollectionAssert.AreEqual(ucr, ucr2);
        Assert.AreEqual(rcr, rcr2);
      }
    }

    [TestMethod]
    public void PermutationWithoutRepetition()
    {
      var n = 5;
      var k = 3;

      for (var i = Flux.IBinaryInteger.CountPermutationsWithoutRepetition(n, k) - 1; i >= 0; i--)
      {
        var upn = Flux.Permutations.Factoradic.UnrankPermutationWithoutRepetition(i, n, k);
        var rpn = Flux.Permutations.Factoradic.RankPermutationWithoutRepetition(upn, n, k);
        var upn2 = Flux.Permutations.Factoradic.UnrankPermutationWithoutRepetition(rpn, n, k);
        var rpn2 = Flux.Permutations.Factoradic.RankPermutationWithoutRepetition(upn2, n, k);

        CollectionAssert.AreEqual(upn, upn2);
        Assert.AreEqual(rpn, rpn2);
      }
    }

    [TestMethod]
    public void PermutationWithRepetition()
    {
      var n = 5;
      var k = 3;

      for (var i = Flux.IBinaryInteger.CountPermutationsWithRepetition(n, k) - 1; i >= 0; i--)
      {
        var upr = Flux.Permutations.Factoradic.UnrankPermutationWithRepetition(i, n, k);
        var rpr = Flux.Permutations.Factoradic.RankPermutationWithRepetition(upr, n, k);
        var upr2 = Flux.Permutations.Factoradic.UnrankPermutationWithRepetition(rpr, n, k);
        var rpr2 = Flux.Permutations.Factoradic.RankPermutationWithRepetition(upr2, n, k);

        CollectionAssert.AreEqual(upr, upr2);
        Assert.AreEqual(rpr, rpr2);
      }
    }
  }
}
