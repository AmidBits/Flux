using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Flux;

namespace Collections.Generic
{
  [TestClass]
  public class SpanSorting
  {
    private readonly string m_text = "AzByCxDwEvFuGtHsIrJqKpLoMn";

    //Flux.StringComparerX m_comparisonOrdinal = Flux.StringComparerX.Ordinal;
    //Flux.StringComparerX m_comparisonOrdinalIgnoreCase = Flux.StringComparerX.OrdinalIgnoreCase;

    //Flux.StringComparerX m_comparableIgnoreNonSpace = new Flux.StringComparerX(System.Globalization.CultureInfo.CurrentCulture, System.Globalization.CompareOptions.IgnoreNonSpace);

    //Flux.StringComparerX m_comparerDoNotIgnoreCase = Flux.StringComparerX.CurrentCulture;

    [TestMethod]
    public void BingoSort()
    {
      var original = m_text.ToCharArray();
      var sortable = new System.Span<char>(original.ToArray());
      new Flux.Sequence.Sort.BingoSort<char>().SortInPlace(sortable);
      var actual = sortable.ToArray();
      CollectionAssert.AreNotEqual(original, actual);
    }

    [TestMethod]
    public void BubbleSort()
    {
      var original = m_text.ToCharArray();
      var sortable = new System.Span<char>(original.ToArray());
      new Flux.Sequence.Sort.BingoSort<char>().SortInPlace(sortable);
      var actual = sortable.ToArray();
      CollectionAssert.AreNotEqual(original, actual);
    }

    [TestMethod]
    public void CombSort()
    {
      var original = m_text.ToCharArray();
      var sortable = new System.Span<char>(original.ToArray());
      new Flux.Sequence.Sort.CombSort<char>().SortInPlace(sortable);
      var actual = sortable.ToArray();
      CollectionAssert.AreNotEqual(original, actual);
    }

    [TestMethod]
    public void HeapSort()
    {
      var original = m_text.ToCharArray();
      var sortable = new System.Span<char>(original.ToArray());
      new Flux.Sequence.Sort.HeapSort<char>().SortInPlace(sortable);
      var actual = sortable.ToArray();
      CollectionAssert.AreNotEqual(original, actual);
    }

    [TestMethod]
    public void InsertionSort()
    {
      var original = m_text.ToCharArray();
      var sortable = new System.Span<char>(original.ToArray());
      new Flux.Sequence.Sort.InsertionSort<char>().SortInPlace(sortable);
      var actual = sortable.ToArray();
      CollectionAssert.AreNotEqual(original, actual);
    }

    [TestMethod]
    public void MergeSort()
    {
      var original = m_text.ToCharArray();
      var sortable = new System.Span<char>(original.ToArray());
      new Flux.Sequence.Sort.MergeSort<char>().SortInPlace(sortable);
      var actual = sortable.ToArray();
      CollectionAssert.AreNotEqual(original, actual);
    }

    [TestMethod]
    public void QuickSort()
    {
      var original = m_text.ToCharArray();
      var sortable = new System.Span<char>(original.ToArray());
      new Flux.Sequence.Sort.QuickSort<char>().SortInPlace(sortable);
      var actual = sortable.ToArray();
      CollectionAssert.AreNotEqual(original, actual);
    }

    [TestMethod]
    public void SelectionSort()
    {
      var original = m_text.ToCharArray();
      var sortable = new System.Span<char>(original.ToArray());
      new Flux.Sequence.Sort.SelectionSort<char>().SortInPlace(sortable);
      var actual = sortable.ToArray();
      CollectionAssert.AreNotEqual(original, actual);
    }

    [TestMethod]
    public void ShellSort()
    {
      var original = m_text.ToCharArray();
      var sortable = new System.Span<char>(original.ToArray());
      new Flux.Sequence.Sort.ShellSort<char>().SortInPlace(sortable);
      var actual = sortable.ToArray();
      CollectionAssert.AreNotEqual(original, actual);
    }
  }
}
