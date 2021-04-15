using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp
{
  /// <summary>
  /// Interaction logic for Drawing.xaml
  /// </summary>
  public partial class Drawing : Window
  {
    public Drawing()
    {
      InitializeComponent();

      var visualHost = new MyVisualHost();

      MyCanvas.Children.Add(visualHost);
    }

    //MyVisualHost visualHost;

    //private void WindowLoaded(object sender, EventArgs e)
    //{
    //  visualHost = new MyVisualHost();

    //  MyCanvas.Children.Add(visualHost);
    //}
  }
}
