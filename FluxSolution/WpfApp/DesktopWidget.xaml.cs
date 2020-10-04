using System.ComponentModel;
using System.Linq;

namespace WpfApp
{
  /// <summary>Interaction logic for DesktopWidget.</summary>
  public partial class DesktopWidget : System.Windows.Window
  {
    System.Windows.Media.Brush m_foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.AntiqueWhite);

    System.Windows.Media.FontFamily m_fontFamily = new System.Windows.Media.FontFamily(@"Segoe UI");
    double m_fontSize = 15;

    int m_windowOffsetLeft = 410;
    int m_windowOffsetTop = 10;

    int m_updateIntervalInSeconds = 59;

    string m_itdNocPhoneNumber = @"72-48471";
    System.Uri m_itdServiceDeskUri = new System.Uri(@"http://support.pima.gov/");

    public DesktopWidget()
    {
      InitializeComponent();

      Setup();
    }

    protected override void OnClosing(CancelEventArgs e)
    {
      Cleanup();

      base.OnClosing(e);
    }

    void Setup()
    {
      SetupHelperWindow();

      Left = int.MinValue;
      Top = int.MinValue;

      Update();

      Top = int.MinValue; // Ensure it "opens" outside of the screen area.

      SetupTimer();

      SendWindowToBack(window);
    }

    void Cleanup()
    {
      CleanupTimer();

      CleanupHelperWindow();
    }

    #region Timer functionality
    System.Windows.Threading.DispatcherTimer m_timer = new System.Windows.Threading.DispatcherTimer();

    void SetupTimer()
    {
      var now = System.DateTime.Now;

      m_timer.Interval = System.TimeSpan.FromSeconds(3);
      m_timer.Tick += timer_Tick;
      m_timer.Start();
    }

    void CleanupTimer()
    {
      m_timer.Stop();
      m_timer.Tick -= timer_Tick;
    }

    void timer_Tick(object sender, System.EventArgs e)
    {
      Update();

      m_timer.Interval = System.TimeSpan.FromSeconds(m_updateIntervalInSeconds);
    }
    #endregion Timer functionality

    System.Windows.Window m_helperWindow;

    /// <summary>Hides the window from appearing in the Alt+Tab or Windows+Tab.</summary>
    public void SetupHelperWindow()
    {
      // Create helper window, outside of visible part of screen and small enough to avoid its appearance at the beginning.

      var m_helperWindow = new System.Windows.Window
      {
        Top = int.MinValue,
        Left = int.MinValue,
        Width = 1,
        Height = 1,
        WindowStyle = System.Windows.WindowStyle.ToolWindow, // ToolWindow avoids the icon in Alt+Tab.
        ShowInTaskbar = false
      };

      m_helperWindow.Show(); // Show window before it can own our main window.
      window.Owner = m_helperWindow; // This will remove the icon for main window.
      m_helperWindow.Hide(); // Hide helper window.
    }

    void CleanupHelperWindow()
    {
      if (!(m_helperWindow is null))
      {
        m_helperWindow.Show(); // Show window before it can disown our main window.
        window.Owner = null;

        m_helperWindow.Close();
        m_helperWindow = null;
      }
    }

    #region SetWindowPos, SendWindowToBack
    /// <see cref="https://stackoverflow.com/questions/1181336/how-to-send-a-wpf-window-to-the-back"/>
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    static extern bool SetWindowPos(System.IntPtr hWnd, System.IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    static readonly System.IntPtr HWND_BOTTOM = new System.IntPtr(1);

    const System.UInt32 SWP_NOSIZE = 0x0001;
    const System.UInt32 SWP_NOMOVE = 0x0002;
    const System.UInt32 SWP_NOACTIVATE = 0x0010;

    /// <summary>Send the window to the back of all other windows.</summary>
    static void SendWindowToBack(System.Windows.Window window)
      => SetWindowPos(new System.Windows.Interop.WindowInteropHelper(window).Handle, HWND_BOTTOM, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE | SWP_NOACTIVATE);
    #endregion SetWindowPos, SendWindowToBack

    void Update()
    {
      UpdateGrid(GenerateValues());

      UpdateWindow();
    }

    System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, object>> GenerateValues()
    {
      //TryGetRegistryKey(windowsCurrentVersionRegistryPath, @"ProductName", out var productName);
      //TryGetRegistryKey(windowsCurrentVersionRegistryPath, @"ReleaseId", out var releaseId);

      //TryGetRegistryKey(windowsCurrentVersionRegistryPath, @"CurrentVersion", out var currentVersion);
      //TryGetRegistryKey(windowsCurrentVersionRegistryPath, @"CurrentBuild", out var currentBuild);
      //TryGetRegistryKey(windowsCurrentVersionRegistryPath, @"UBR", out var uBR);

      //var windowsEdition = $"{productName}";
      //var windowsVersion = $"{releaseId}";
      //var windowsBuild = $"{currentBuild}.{uBR}";

      var computerDomainName = System.AppDomain.CurrentDomain.FriendlyName;
      var computerName = System.Environment.MachineName;

      var userDomainName = $"{System.Environment.UserDomainName}.{System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName}";
      var userName = System.Environment.UserName;

      var x = System.Runtime.InteropServices.RuntimeInformation.OSDescription;
      var properties = new System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, object>>();

      //properties.Add(new KeyValuePair<string, object>(@"Computer Domain Name", Locale.ComputerDomainName));
      properties.Add(new System.Collections.Generic.KeyValuePair<string, object>(@"Computer Name", Flux.Locale.MachineName));

      //properties.Add(new KeyValuePair<string, object>(@"User Domain Name", Locale.UserDomainName));
      properties.Add(new System.Collections.Generic.KeyValuePair<string, object>(@"User Name", Flux.Locale.UserName));

      properties.Add(new System.Collections.Generic.KeyValuePair<string, object>(@"Operating System", Flux.Locale.OperatingSystemName));

      var nicsUpWithGateways = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces().Where(nic => nic.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up && nic.GetIPProperties().GatewayAddresses.Any()).ToArray();

      //var ipAddress = string.Join(@"\r\n", nicsUpWithGateways.SelectMany(nic => nic.GetIPProperties().UnicastAddresses.Where(ip => ip.Address.AddressFamily == AddressFamily.InterNetwork).Select(ip => $"{ip.Address} on {string.Join("-", nic.GetPhysicalAddress().GetAddressBytes().Select(b => b.ToString("X2")))}")));
      var ipAddress = string.Join(@"\r\n", nicsUpWithGateways.SelectMany(nic => nic.GetIPProperties().UnicastAddresses.Where(ip => ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).Select(ip => $"{ip.Address}")));

      properties.Add(new System.Collections.Generic.KeyValuePair<string, object>(@"IP Address", string.IsNullOrWhiteSpace(ipAddress) ? "No IP address found." : ipAddress));

      var networkStatistics = nicsUpWithGateways.Aggregate(System.Tuple.Create(0L, 0L, 0L), (t, nic) => nic.GetIPStatistics() is var ips && nic.Speed > 0 && ips.BytesReceived > 0 && ips.BytesSent > 0 ? System.Tuple.Create(t.Item1 + nic.Speed, t.Item2 + ips.BytesReceived, t.Item3 + ips.BytesSent) : t);

      properties.Add(new System.Collections.Generic.KeyValuePair<string, object>($"Traffic ({((double)networkStatistics.Item1 / 8 / 1024 / 1024):N1} MB/s)", $"{((double)networkStatistics.Item2 / 1024 / 1024):N1} MB received / {((double)networkStatistics.Item3 / 1024 / 1024):N1} MB sent"));

      var now = System.DateTime.Now;

      properties.Add(new System.Collections.Generic.KeyValuePair<string, object>("Date & Time", $"{now.ToLongDateString()} {now.ToShortTimeString()}"));

      properties.Add(new System.Collections.Generic.KeyValuePair<string, object>(@"ITD NOC", $"{m_itdNocPhoneNumber}"));
      properties.Add(new System.Collections.Generic.KeyValuePair<string, object>(@"Service Desk URL", $"{m_itdServiceDeskUri}"));

      //properties.Add(new KeyValuePair<string, object>(nameof(Flux.Reflection.Helper.GetApplicationDomainName), Flux.Reflection.Helper.GetApplicationDomainName()));
      //properties.Add(new KeyValuePair<string, object>(nameof(Flux.Reflection.Helper.GetComputerDomainName), Flux.Reflection.Helper.GetComputerDomainName()));
      //properties.Add(new KeyValuePair<string, object>(nameof(Flux.Reflection.Helper.GetComputerHostName), Flux.Reflection.Helper.GetComputerHostName()));
      //properties.Add(new KeyValuePair<string, object>(nameof(Flux.Reflection.Helper.GetComputerPrimaryDnsName), Flux.Reflection.Helper.GetComputerPrimaryDnsName()));
      //properties.Add(new KeyValuePair<string, object>(nameof(Flux.Reflection.Helper.GetFrameworkDescription), Flux.Reflection.Helper.GetFrameworkDescription()));
      //properties.Add(new KeyValuePair<string, object>(nameof(Flux.Reflection.Helper.GetMachineName), Flux.Reflection.Helper.GetMachineName()));
      //properties.Add(new KeyValuePair<string, object>(nameof(Flux.Reflection.Helper.GetOperatingSystemDescription), Flux.Reflection.Helper.GetOperatingSystemDescription()));
      //properties.Add(new KeyValuePair<string, object>(nameof(Flux.Reflection.Helper.GetUserDomainName), Flux.Reflection.Helper.GetUserDomainName()));
      //properties.Add(new KeyValuePair<string, object>(nameof(Flux.Reflection.Helper.GetUserName), Flux.Reflection.Helper.GetUserName()));

      return properties;
    }

    void UpdateGrid(System.Collections.Generic.IList<System.Collections.Generic.KeyValuePair<string, object>> properties)
    {
      grid.Children.Clear();

      grid.ColumnDefinitions.Clear();

      grid.ColumnDefinitions.Add(new System.Windows.Controls.ColumnDefinition() { Width = new System.Windows.GridLength(0, System.Windows.GridUnitType.Auto) }); // Key
      grid.ColumnDefinitions.Add(new System.Windows.Controls.ColumnDefinition() { Width = new System.Windows.GridLength(0, System.Windows.GridUnitType.Auto) }); // Value

      grid.RowDefinitions.Clear();

      grid.RowDefinitions.Add(new System.Windows.Controls.RowDefinition() { Height = new System.Windows.GridLength(0, System.Windows.GridUnitType.Auto) });

      var labelHeader = new System.Windows.Controls.Label
      {
        BorderBrush = window.Foreground,
        BorderThickness = new System.Windows.Thickness(0, 0, 0, 1),
        Content = @"Pima County",
        Foreground = m_foreground,
        FontFamily = m_fontFamily,
        FontSize = m_fontSize,
        FontStyle = System.Windows.FontStyles.Italic,
        Margin = new System.Windows.Thickness(2, 1, 2, 1),
        Padding = new System.Windows.Thickness(0)
      };
      grid.Children.Add(labelHeader);
      System.Windows.Controls.Grid.SetRow(labelHeader, 0);
      System.Windows.Controls.Grid.SetColumn(labelHeader, 0);
      System.Windows.Controls.Grid.SetColumnSpan(labelHeader, 2);

      for (var rowIndex = 1; rowIndex <= properties.Count; rowIndex++)
      {
        grid.RowDefinitions.Add(new System.Windows.Controls.RowDefinition() { Height = new System.Windows.GridLength(0, System.Windows.GridUnitType.Auto) });

        var kvp = properties[rowIndex - 1];

        var labelKey = new System.Windows.Controls.Label
        {
          Content = kvp.Key,
          Foreground = m_foreground,
          FontFamily = m_fontFamily,
          FontSize = m_fontSize,
          Margin = new System.Windows.Thickness(2, 1, 12, 1),
          Padding = new System.Windows.Thickness(0)
        };
        grid.Children.Add(labelKey);
        System.Windows.Controls.Grid.SetRow(labelKey, rowIndex);
        System.Windows.Controls.Grid.SetColumn(labelKey, 0);

        var labelValue = new System.Windows.Controls.Label
        {
          Content = kvp.Value,
          Foreground = m_foreground,
          FontFamily = m_fontFamily,
          FontSize = m_fontSize,
          Margin = new System.Windows.Thickness(2, 1, 2, 1),
          Padding = new System.Windows.Thickness(0)
        };
        grid.Children.Add(labelValue);
        System.Windows.Controls.Grid.SetRow(labelValue, rowIndex);
        System.Windows.Controls.Grid.SetColumn(labelValue, 1);
      }
    }

    void UpdateWindow()
    {
      grid.Measure(new System.Windows.Size(double.PositiveInfinity, double.PositiveInfinity));

      //window.Background = System.Windows.Media.Brushes.Black;
      //window.Opacity = .75;

      var borderThickness = 0;

      window.BorderThickness = new System.Windows.Thickness(borderThickness);
      window.BorderBrush = System.Windows.Media.Brushes.White;

      window.Foreground = m_foreground;
      window.FontFamily = m_fontFamily;
      window.FontSize = m_fontSize;

      window.Height = grid.DesiredSize.Height + borderThickness * 2;
      window.Left = (System.Windows.SystemParameters.WorkArea.Right - window.Width) - m_windowOffsetLeft;
      window.Top = m_windowOffsetTop;
      window.Width = grid.DesiredSize.Width + borderThickness * 2 + 3;
    }
  }
}
