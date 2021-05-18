namespace WpfApp
{
  /// <summary>Interaction logic for MainWindow.</summary>
  public partial class MainWindow : System.Windows.Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    void ToggleWindow(ref System.Windows.Window window, System.Type windowType)
    {
      if (window is null)
      {
        window = (System.Windows.Window)System.Activator.CreateInstance(windowType);
        window.Show();
      }
      else
      {
        window.Close();
        window = null;
      }
    }

    #region DesktopWidget
    System.Windows.Window m_windowDesktopWidget = null;
    private void toggleButtonDesktopWidget_Checked(object sender, System.Windows.RoutedEventArgs e)
      => ToggleWindow(ref m_windowDesktopWidget, typeof(DesktopWidget));
    private void toggleButtonDesktopWidget_Unchecked(object sender, System.Windows.RoutedEventArgs e)
      => ToggleWindow(ref m_windowDesktopWidget, typeof(DesktopWidget));

    #endregion DesktopWidget

    #region Window Mazes
    System.Windows.Window m_windowMazes = null;
    private void toggleButtonMazes_Checked(object sender, System.Windows.RoutedEventArgs e)
      => ToggleWindow(ref m_windowMazes, typeof(Mazes));
    private void toggleButtonMazes_Unchecked(object sender, System.Windows.RoutedEventArgs e)
     => ToggleWindow(ref m_windowMazes, typeof(Mazes));
    #endregion Window Mazes
  }
}
