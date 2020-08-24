namespace WpfApp
{
  /// <summary>Interaction logic for MainWindow.</summary>
  public partial class MainWindow : System.Windows.Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    #region DesktopWidget
    DesktopWidget m_desktopWidget = null;

    private void toggleButtonDesktopWidget_Checked(object sender, System.Windows.RoutedEventArgs e)
    {
      if (m_desktopWidget is null)
      {
        m_desktopWidget = new DesktopWidget();
        m_desktopWidget.Show();
      }
      else
        throw new System.ArgumentNullException(nameof(m_desktopWidget));
    }

    private void toggleButtonDesktopWidget_Unchecked(object sender, System.Windows.RoutedEventArgs e)
    {
      if (m_desktopWidget is null)
        throw new System.ArgumentNullException(nameof(m_desktopWidget));
      else
      {
        m_desktopWidget.Close();
        m_desktopWidget = null;
      }
    }
    #endregion DesktopWidget
  }
}
