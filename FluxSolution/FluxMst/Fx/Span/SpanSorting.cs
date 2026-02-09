//using System.Linq;
//using Flux;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace SystemFx
//{
//  [TestClass]
//  public class SpanSorting
//  {
//    private readonly string m_text = "AzByCxDwEvFuGtHsIrJqKpLoMn";

//    //Flux.StringComparerX m_comparisonOrdinal = Flux.StringComparerX.Ordinal;
//    //Flux.StringComparerX m_comparisonOrdinalIgnoreCase = Flux.StringComparerX.OrdinalIgnoreCase;

//    //Flux.StringComparerX m_comparableIgnoreNonSpace = new Flux.StringComparerX(System.Globalization.CultureInfo.CurrentCulture, System.Globalization.CompareOptions.IgnoreNonSpace);

//    //Flux.StringComparerX m_comparerDoNotIgnoreCase = Flux.StringComparerX.CurrentCulture;

//    [TestMethod]
//    public void BingoSort()
//    {
//      var original = m_text.ToCharArray();
//      var sortable = new System.Span<char>(original.ToArray());
//      sortable.BingoSort();
//      var actual = sortable.ToArray();
//      CollectionAssert.AreNotEqual(original, actual);
//    }

//    [TestMethod]
//    public void BubbleSort()
//    {
//      var original = m_text.ToCharArray();
//      var sortable = new System.Span<char>(original.ToArray());
//      sortable.BubbleSort();
//      var actual = sortable.ToArray();
//      CollectionAssert.AreNotEqual(original, actual);
//    }

//    [TestMethod]
//    public void CombSort()
//    {
//      var original = m_text.ToCharArray();
//      var sortable = new System.Span<char>(original.ToArray());
//      sortable.CombSort();
//      var actual = sortable.ToArray();
//      CollectionAssert.AreNotEqual(original, actual);
//    }

//    [TestMethod]
//    public void HeapSortBasicDown()
//    {
//      var original = m_text.ToCharArray();
//      var sortable = new System.Span<char>(original.ToArray());
//      sortable.HeapSort(HeapSortType.BasicDown);
//      var actual = sortable.ToArray();
//      CollectionAssert.AreNotEqual(original, actual);
//    }

//    [TestMethod]
//    public void HeapSortFloydDown()
//    {
//      var original = m_text.ToCharArray();
//      var sortable = new System.Span<char>(original.ToArray());
//      sortable.HeapSort(HeapSortType.FloydDown);
//      var actual = sortable.ToArray();
//      CollectionAssert.AreNotEqual(original, actual);
//    }

//    [TestMethod]
//    public void InsertionSort()
//    {
//      var original = m_text.ToCharArray();
//      var sortable = new System.Span<char>(original.ToArray());
//      sortable.InsertionSort();
//      var actual = sortable.ToArray();
//      CollectionAssert.AreNotEqual(original, actual);
//    }

//    [TestMethod]
//    public void MergeSort()
//    {
//      var original = m_text.ToCharArray();
//      var sortable = new System.Span<char>(original.ToArray());
//      sortable.MergeSort();
//      var actual = sortable.ToArray();
//      CollectionAssert.AreNotEqual(original, actual);
//    }

//    [TestMethod]
//    public void QuickSort()
//    {
//      var original = m_text.ToCharArray();
//      var sortable = new System.Span<char>(original.ToArray());
//      sortable.QuickSort();
//      var actual = sortable.ToArray();
//      CollectionAssert.AreNotEqual(original, actual);
//    }

//    [TestMethod]
//    public void SelectionSort()
//    {
//      var original = m_text.ToCharArray();
//      var sortable = new System.Span<char>(original.ToArray());
//      sortable.SelectionSort();
//      var actual = sortable.ToArray();
//      CollectionAssert.AreNotEqual(original, actual);
//    }

//    [TestMethod]
//    public void ShellSort()
//    {
//      var original = m_text.ToCharArray();
//      var sortable = new System.Span<char>(original.ToArray());
//      sortable.ShellSort();
//      var actual = sortable.ToArray();
//      CollectionAssert.AreNotEqual(original, actual);
//    }
//  }
//}
