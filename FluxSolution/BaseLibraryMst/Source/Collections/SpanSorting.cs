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
      new Flux.IndexedSorting.BingoSort<char>().SortInline(sortable);
      var actual = sortable.ToArray();
      CollectionAssert.AreNotEqual(original, actual);
    }

    [TestMethod]
    public void BubbleSort()
    {
      var original = m_text.ToCharArray();
      var sortable = new System.Span<char>(original.ToArray());
      new Flux.IndexedSorting.BingoSort<char>().SortInline(sortable);
      var actual = sortable.ToArray();
      CollectionAssert.AreNotEqual(original, actual);
    }

    [TestMethod]
    public void CombSort()
    {
      var original = m_text.ToCharArray();
      var sortable = new System.Span<char>(original.ToArray());
      new Flux.IndexedSorting.CombSort<char>().SortInline(sortable);
      var actual = sortable.ToArray();
      CollectionAssert.AreNotEqual(original, actual);
    }

    [TestMethod]
    public void HeapSort()
    {
      var original = m_text.ToCharArray();
      var sortable = new System.Span<char>(original.ToArray());
      new Flux.IndexedSorting.HeapSort<char>().SortInline(sortable);
      var actual = sortable.ToArray();
      CollectionAssert.AreNotEqual(original, actual);
    }

    [TestMethod]
    public void InsertionSort()
    {
      var original = m_text.ToCharArray();
      var sortable = new System.Span<char>(original.ToArray());
      new Flux.IndexedSorting.InsertionSort<char>().SortInline(sortable);
      var actual = sortable.ToArray();
      CollectionAssert.AreNotEqual(original, actual);
    }

    [TestMethod]
    public void MergeSort()
    {
      var original = m_text.ToCharArray();
      var sortable = new System.Span<char>(original.ToArray());
      new Flux.IndexedSorting.MergeSort<char>().SortInline(sortable);
      var actual = sortable.ToArray();
      CollectionAssert.AreNotEqual(original, actual);
    }

    [TestMethod]
    public void QuickSort()
    {
      var original = m_text.ToCharArray();
      var sortable = new System.Span<char>(original.ToArray());
      new Flux.IndexedSorting.QuickSort<char>().SortInline(sortable);
      var actual = sortable.ToArray();
      CollectionAssert.AreNotEqual(original, actual);
    }

    [TestMethod]
    public void SelectionSort()
    {
      var original = m_text.ToCharArray();
      var sortable = new System.Span<char>(original.ToArray());
      new Flux.IndexedSorting.SelectionSort<char>().SortInline(sortable);
      var actual = sortable.ToArray();
      CollectionAssert.AreNotEqual(original, actual);
    }

    [TestMethod]
    public void ShellSort()
    {
      var original = m_text.ToCharArray();
      var sortable = new System.Span<char>(original.ToArray());
      new Flux.IndexedSorting.ShellSort<char>().SortInline(sortable);
      var actual = sortable.ToArray();
      CollectionAssert.AreNotEqual(original, actual);
    }
  }
}
