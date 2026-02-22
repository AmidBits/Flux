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

      var ucn = new int[k];
      var ucn2 = new int[k];

      for (var i = Flux.BinaryInteger.CountCombinationsWithoutRepetition(n, k) - 1; i >= 0; i--)
      {
        Flux.Combinations.CombinatorialNumberSystem.UnrankCombinationWithoutRepetition(i, n, k, ucn);
        var rcn = Flux.Combinations.CombinatorialNumberSystem.RankCombinationWithoutRepetition(ucn, n, k);
        Flux.Combinations.CombinatorialNumberSystem.UnrankCombinationWithoutRepetition(rcn, n, k, ucn2);
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

      var ucr = new int[k];
      var ucr2 = new int[k];

      for (var i = Flux.BinaryInteger.CountCombinationsWithRepetition(n, k) - 1; i >= 0; i--)
      {
        Flux.Combinations.CombinatorialNumberSystem.UnrankCombinationWithRepetition(i, n, k, ucr);
        var rcr = Flux.Combinations.CombinatorialNumberSystem.RankCombinationWithRepetition(ucr, n, k);
        Flux.Combinations.CombinatorialNumberSystem.UnrankCombinationWithRepetition(rcr, n, k, ucr2);
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

      var upn = new int[k];
      var upn2 = new int[k];

      for (var i = Flux.BinaryInteger.CountPermutationsWithoutRepetition(n, k) - 1; i >= 0; i--)
      {
        Flux.Permutations.Factoradic.UnrankPermutationWithoutRepetition(i, n, k, upn);
        var rpn = Flux.Permutations.Factoradic.RankPermutationWithoutRepetition(upn, n, k);
        Flux.Permutations.Factoradic.UnrankPermutationWithoutRepetition(rpn, n, k, upn2);
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

      var upr = new int[k];
      var upr2 = new int[k];

      for (var i = Flux.BinaryInteger.CountPermutationsWithRepetition(n, k) - 1; i >= 0; i--)
      {
        Flux.Permutations.Factoradic.UnrankPermutationWithRepetition(i, n, k, upr);
        var rpr = Flux.Permutations.Factoradic.RankPermutationWithRepetition(upr, n, k);
        Flux.Permutations.Factoradic.UnrankPermutationWithRepetition(rpr, n, k, upr2);
        var rpr2 = Flux.Permutations.Factoradic.RankPermutationWithRepetition(upr2, n, k);

        CollectionAssert.AreEqual(upr, upr2);
        Assert.AreEqual(rpr, rpr2);
      }
    }
  }
}
